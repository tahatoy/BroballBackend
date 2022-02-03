﻿using Business.Abstract;
using Business.Adapters.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IMailService _mailService;
        IUserDal _userDal;
        ICityDal _cityDal;
        ILeagueDal _leagueDal;


        public EfUserDal EfUserDal { get; }

        public UserManager(IUserDal userDal, IMailService mailService)
        {
            _userDal = userDal;
            _mailService = mailService;
        }

        public UserManager(EfUserDal efUserDal)
        {
            EfUserDal = efUserDal;

        }
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new Result(true,"Kullanıcı başarıyla eklendi");
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new Result(true,"Kullanıcı başarıyla silindi");

        }

        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }


        public IResult GetUserById(int userId)
        {
            var result = _userDal.Get(user => userId == user.UserId);
            return new DataResult<User>(result, true, "data geldi");
        }

        public IDataResult<List<User>> GetUsersByCityId(int id)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(p => p.CitiesId == id));
        }





        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new Result(true,"Kullanıcı başarıyla güncellendi");
        }

        public IResult Login(string email, string password)
        {
            var result = _userDal.Get(user => user.Email == email);

            if (result == null)
                return new Result(false, "giriş başarısız");
            if (result.Password.Equals(password)) return new Result(true, "giriş başarılı");
            {
                return new Result(false, "giriş başarısız");
            }
        }
        public IResult GetUserByEmail(string email)

        {
            var result = _userDal.Get(user => email == user.Email);
            if (result == null)
            {
                return new ErrorResult("data yok");
            }

            return new SuccessDataResult<string>(result.UserId.ToString(), "data getirildi");

        }

        public IResult SendMail(string email)
        {
            var result = _userDal.Get(user => email == user.Email);
            if (!isMailExist(result.Email))
            {
                _mailService.SendMailForPassword(result);
                return new SuccessResult("Mail Gönderildi");
            }

            return new ErrorResult("Mail Gönderilemedi");
        }



        private bool isMailExist(string mail)
        {
            var result = _userDal.Get(user => mail == user.Email);
            if (result == null)
                return true;
            return false;
        }

        public IDataResult<List<User>> GetUsersByUserId(int id)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(p => p.UserId == id));
        }



        public IDataResult<List<User>> GetUsersByLeagueId(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<UserDetailDto>> GetUserDetailsByCityId(int id)
        {
            throw new NotImplementedException();
        }

      










        //public List<User> GetAllByUserId(int userId)
        //{
        //    return _userDal.GetAll(u=>u.UserId==userId);
        //}

        //public List<User> GetByStarPoint(int min, int max)
        //{
        //    return _userDal.GetAll(u=>u.StarPoint>=min && u.StarPoint<=max);
        //}
    }
}

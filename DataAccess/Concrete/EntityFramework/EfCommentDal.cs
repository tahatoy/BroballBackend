using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCommentDal : EfEntityRepositoryBase<Comment, BroballContext>, ICommentDal
    {
        //public List<Comments> GetCommentDetails()
        //{
        //    using (var context = new BroballContext())
        //    {
        //        var result = from u in context.Users

        //                     select new Comments
        //                     {
        //                         UserId = u.UserId,
        //                         StarPoint = u.StarPoint,


        //                     };
        //        return result.ToList();

        //    }
        //}
    }
}

using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<busProvider, int, bool> getBusProvider() { return new busProviderRepo(); }
        public static IRepo<discountCupon, int, bool> getDiscountCupon() { return new discountCuponRepo(); }
        public static IRepo<employee, int, bool> getEmployee() { return new employeeRepo(); }
        public static IRepo<notice, int, bool> getNotice() { return new noticeRepo(); }
    }
}

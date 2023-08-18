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
        public static IRepo<bus, int, bool> getBus() { return new busRepo(); }
        public static IRepo<busProvider, int, bool> getBusProvider() { return new busProviderRepo(); }
        public static IRepo<discountCupon, int, bool> getDiscountCupon() { return new discountCuponRepo(); }
        public static IRepo<employee, int, bool> getEmployee() { return new employeeRepo(); }
        public static IRepo<notice, int, bool> getNotice() { return new noticeRepo(); }
        public static IRepo<place, int, bool> getPlace() { return new placeRepo(); }
        public static IRepo<trip, int, bool> getTrip() { return new tripRepo(); }
        public static IRepo<token, int, token> getToken() { return new tokenRepo(); }
        public static IRepo<user, int, user> getUser() { return new userRepo(); }
        public static IAuth GetAuth() { return new userRepo(); }
    }
}

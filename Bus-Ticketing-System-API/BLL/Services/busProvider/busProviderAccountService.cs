using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class busProviderAccountService
    {
        public static bool rechargeAccount(int userID, int ammount)
        {
            var obj = new transaction()
            {
                details = "Recharging",
                amount = ammount,
                userID = userID
            };
            return DataAccessFactory.getTransaction().create(obj);
        }
    }
}

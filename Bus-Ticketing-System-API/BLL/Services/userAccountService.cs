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
    public class userAccountService
    {
        public static List<transactionDTO> GetTransactions(int userid)
        {
            var data = DataAccessFactory.getUser().get(userid).transactions;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<transaction, transactionDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<transactionDTO>>(data);
        }
        public static bool deposit(int userID, int ammount)
        {
            var obj = new transaction()
            {
                details = "deposit",
                amount = ammount,
                userID = userID,
                time = DateTime.Now,
            };
            return DataAccessFactory.getTransaction().create(obj);
        }
        public static bool withdraw(int userID, int ammount)
        {
            var obj = new transaction()
            {
                details = "withdraw",
                amount = (-ammount),
                userID = userID,
                time = DateTime.Now,
            };
            return DataAccessFactory.getTransaction().create(obj);
        }
        public static bool hasMoney(int userID, int ammount)
        {
            int credit = DataAccessFactory.getUser().get(userID).transactions.Select(t=>t.amount).Sum();
            return credit >= ammount;
        }
    }
}

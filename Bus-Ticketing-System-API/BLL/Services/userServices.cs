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
    public class userServices
    {
        public static List<transactionDTO> GetTransactions(int userid)
        {
            var data = DataAccessFactory.getUser().get(userid).transactions;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<transaction, transactionDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<transactionDTO>>(data);
        }
    }
}

using AutoMapper;
using BLL.DTOs;
using DAL.EF.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class customerUserService
    {
        public static bool resgirstration(customerDTO custObj)
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<customerDTO, user>();
                    cfg.CreateMap<customerDTO, customer>();
                });
            var mapper = config.CreateMapper();
            var userData = mapper.Map<user>(custObj);
            userData.userRole = "customer";
            userData = DataAccessFactory.getUser().create(userData);
            var empData = mapper.Map<customer>(custObj);
            empData.id = userData.id;
            return DataAccessFactory.getCustomer().create(empData);
        }
    }
}

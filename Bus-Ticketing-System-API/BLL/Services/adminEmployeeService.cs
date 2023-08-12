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
    public class adminEmployeeService
    {
        public static List<employeeDTO> allEmployee()
        {
            var data = DataAccessFactory.getEmployee().All();
            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<employee, employeeDTO>()
                .ForMember( dest => dest.username, opt => opt.MapFrom(src => src.user.username))
                );
            var mapper = config.CreateMapper();
            return mapper.Map<List<employeeDTO>>(data);
        }
        public static bool addEmpoloyee(employeeDTO obj)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<employeeDTO, user>(); cfg.CreateMap<employeeDTO, employee>(); });
            var mapper = config.CreateMapper();
            var userData = mapper.Map<user>(obj);
            userData.userRole = "employee";
            userData = DataAccessFactory.getUser().create(userData);
            var empData = mapper.Map<employee>(obj);
            empData.id = userData.id;
            return DataAccessFactory.getEmployee().create(empData);
        }
    }
}

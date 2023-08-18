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
    public class busProviderBusService
    {
        public static List<busDTO> allBus()
        {
            var data = DataAccessFactory.getBus().All();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<bus, busDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<busDTO>>(data);
        }
        public static busDTO GetBus(int id)
        {
            var data = DataAccessFactory.getBus().get(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<bus, busDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<busDTO>(data);
        }
        public static bool deleteBus(int id)
        {
            return DataAccessFactory.getBus().delete(id);
        }
        public static bool addBus(busDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<busDTO, bus>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<bus>(obj);
            return DataAccessFactory.getBus().create(newObj);
        }
        public static bool updateBus(busDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<busDTO, bus>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<bus>(obj);
            return DataAccessFactory.getBus().update(newObj);
        }
    }
}

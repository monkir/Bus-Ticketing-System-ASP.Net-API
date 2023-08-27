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
        public static List<busDTO> allBus(int bp_id)
        {
            var pb_data = DataAccessFactory.getBusProvider().get(bp_id);
            var data = pb_data.buses.ToList();
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
        public static List<tripDTO> tripList_of_bus(int busID)
        {
            var data = DataAccessFactory.getBus().get(busID).trips.ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<trip, tripDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<tripDTO>>(data);
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
        public static bool isOwner(int busID, int busProviderID)
        {
            var obj = DataAccessFactory.getBus().get(busID);
            //return obj != null ? obj.bp_id.Equals(busProviderID) : false;
            if (obj == null)
            {
                return false;
            }
            return obj.bp_id.Equals(busProviderID);
        }
    }
}

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
    public class employeeTripService
    {
        public static List<tripDTO> allTrip()
        {
            var data = DataAccessFactory.getTrip().All();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<trip, tripDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<tripDTO>>(data);
        }
        public static tripDTO GetTrip(int id)
        {
            var data = DataAccessFactory.getTrip().get(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<trip, tripDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<tripDTO>(data);
        }
        public static bool acceptCancelTrip(int tripID)
        {
            var tripData = DataAccessFactory.getTrip().get(tripID);
            tripData.status = "cancelled";
            return DataAccessFactory.getTrip().update(tripData);
        }
        public static bool acceptAddTrip(int tripID)
        {
            var tripData = DataAccessFactory.getTrip().get(tripID);
            tripData.status = "added";
            return DataAccessFactory.getTrip().update(tripData);
        }
        /*public static bool updateTrip(tripDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<tripDTO, trip>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<trip>(obj);
            return DataAccessFactory.getTrip().update(newObj);
        }*/
    }
}

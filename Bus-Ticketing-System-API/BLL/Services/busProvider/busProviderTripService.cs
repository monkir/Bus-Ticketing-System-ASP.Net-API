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
    public class busProviderTripService
    {
        public static bool isOwner(int tripID, int bpID)
        {
            var obj = DataAccessFactory.getTrip().get(tripID);
            return obj.bus.bp_id.Equals(bpID);
        }
        public static List<tripDTO> allTrip(int bpID)
        {
            //var data = DataAccessFactory.getTrip().All();
            var data = (from b in DataAccessFactory.getBusProvider().get(bpID).buses select b.trips).ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<trip, tripDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<tripDTO>>(data);
        }
        public static tripDTO GetTrip(int tripID)
        {
            var data = DataAccessFactory.getTrip().get(tripID);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<trip, tripDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<tripDTO>(data);
        }
        public static bool addTrip(tripDTO obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<tripDTO, trip>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<trip>(obj);
            obj.status = "adding/pending";
            return DataAccessFactory.getTrip().create(newObj);
        }
        public static bool deleteTrip(int tripID)
        {
            return DataAccessFactory.getTrip().delete(tripID);
        }
        public static bool cancelTrip(int tripID)
        {
            var exTrip = DataAccessFactory.getTrip().get(tripID);
            exTrip.status = "cancelling/pending";
            return DataAccessFactory.getTrip().update(exTrip);
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

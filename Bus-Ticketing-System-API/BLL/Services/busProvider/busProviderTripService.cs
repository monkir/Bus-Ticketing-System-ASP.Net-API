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
        public static bool isOwnerOfTrip(int tripID, int bpID)
        {
            var obj = DataAccessFactory.getTrip().get(tripID);
            //return obj != null  obj.bus_id == bpID : false;
            if(obj == null)
                return false;
            return obj.bus.bp_id.Equals(bpID);
        }
        private static List<int> convertSeat(string seats)
        {
            return seats.Split(',').Select(s => Convert.ToInt32(s)).ToList();
        }
        private static string convertSeat(List<int> seats)
        {
            return string.Join(",", seats.Select(s => s.ToString()));
        }
        public static bool isOwnerOfBus(int busID, int bpID)
        {
            var obj = DataAccessFactory.getBus().get(busID);
            //return obj != null  obj.bp_id == bpID : false;
            if (obj == null)
                return false;
            return obj.bp_id.Equals(bpID);
        }
        public static List<tripDTO> allTrip(int bpID)
        {
            
            var buses = DataAccessFactory.getBusProvider().get(bpID).buses;
            //var data = buses.SelectMany(b => b.trips).ToList();
            var data = (from b in buses
                    from t in b.trips
                    select t).ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<trip, tripDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<tripDTO>>(data);
        }
        public static List<tripInDetailsDTO> allTripDetails(int bpID)
        {
            
            var buses = DataAccessFactory.getBusProvider().get(bpID).buses;
            //var data = buses.SelectMany(b => b.trips).ToList();
            var tripData = (from b in buses
                    from t in b.trips
                    select t).ToList();
            //var tripData = DataAccessFactory.getTrip().get(tripID);
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<trip, tripInDetailsDTO>()
                        .ForMember
                        (
                            dst => dst.bookedSeat,
                            opt => opt.MapFrom
                            (
                                src => src.tickets.Where(t => t.status.Equals("booked")).SelectMany(t => convertSeat(t.seat_no)).ToList()
                            )
                        )
                        ;
                        cfg.CreateMap<place, placeDTO>();
                    }
                );
            var mapper = config.CreateMapper();
            return mapper.Map<List<tripInDetailsDTO>>(tripData);
        }
        public static List<tripInDetailsDTO> searchTripDetails(int bpID, string search)
        {
            
            var buses = DataAccessFactory.getBusProvider().get(bpID).buses;
            //var data = buses.SelectMany(b => b.trips).ToList();
            var tripData = (from b in buses
                    from t in b.trips
                    select t).ToList();
            //var tripData = DataAccessFactory.getTrip().get(tripID);
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<trip, tripInDetailsDTO>()
                        .ForMember
                        (
                            dst => dst.bookedSeat,
                            opt => opt.MapFrom
                            (
                                src => src.tickets.Where(t => t.status.Equals("booked")).SelectMany(t => convertSeat(t.seat_no)).ToList()
                            )
                        )
                        ;
                        cfg.CreateMap<place, placeDTO>();
                    }
                );
            var mapper = config.CreateMapper();
            var convertedData =  mapper.Map<List<tripInDetailsDTO>>(tripData);
            search = search.ToLower();
            var searchedData = convertedData.Where(
                t =>
                t.id.ToString().ToLower().Contains(search)
                || t.id.ToString().ToLower().Contains(search)
                || t.ticketPrice.ToString().ToLower().Contains(search)
                || t.status.ToString().ToLower().Contains(search)
                || t.startTime.ToString().ToLower().Contains(search)
                || t.endTime.ToString().ToLower().Contains(search)
                || t.depot.name.ToString().ToLower().Contains(search)
                || t.destination.name.ToString().ToLower().Contains(search)
                );
            return searchedData.ToList();
        }
        public static tripDTO GetTrip(int tripID)
        {
            var data = DataAccessFactory.getTrip().get(tripID);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<trip, tripDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<tripDTO>(data);
        }
        // add amount to account
        private static bool addAccount(int? bp_id, int ammount, string details)
        {
            var obj = new transaction()
            {
                details = "Added: " + details,
                amount = ammount,
                userID = bp_id
            };
            return DataAccessFactory.getTransaction().create(obj);
        }
        // cut amount from account
        private static bool cutAccount(int? bp_id, int ammount, string details)
        {
            var obj = new transaction()
            {
                details = "Cut: " + details,
                amount = (-ammount),
                time = DateTime.Now,
                userID = bp_id
            };
            return DataAccessFactory.getTransaction().create(obj);
        }
        public static bool addTrip(tripDTO obj)
        {
            int? bp_id = DataAccessFactory.getBus().get(obj.bus_id).bp_id;
            if (cutAccount(bp_id, 2000, "for adding trip") == false)
            {
                return false;
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<tripDTO, trip>());
            var mapper = config.CreateMapper();
            var newObj = mapper.Map<trip>(obj);
            newObj.status = "adding-pending";
            return DataAccessFactory.getTrip().create(newObj);
        }
        public static bool undoAddTrip(int tripID)
        {
            int? bp_id = DataAccessFactory.getTrip().get(tripID).bus.bp_id;
            if (addAccount(bp_id, 2000, "for undoing add trip") == false)
            {
                return false;
            }
            return DataAccessFactory.getTrip().delete(tripID);
            /*var exTrip = DataAccessFactory.getTrip().get(tripID);
            exTrip.status = "cancelling-pending";
            return DataAccessFactory.getTrip().update(exTrip);*/
        }
        public static bool cancelTrip(int tripID)
        {
            var exTrip = DataAccessFactory.getTrip().get(tripID);
            exTrip.status = "cancelling-pending";
            return DataAccessFactory.getTrip().update(exTrip);
        }
        public static bool undoCancelTrip(int tripID)
        {
            var exTrip = DataAccessFactory.getTrip().get(tripID);
            exTrip.status = "added";
            return DataAccessFactory.getTrip().update(exTrip);
        }
        public static tripInDetailsDTO getTripDetails(int tripID)
        {
            var tripData = DataAccessFactory.getTrip().get(tripID);
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<trip, tripInDetailsDTO>()
                        .ForMember
                        (
                            dst => dst.bookedSeat,
                            opt => opt.MapFrom
                            (
                                src => src.tickets.Where(t => t.status.Equals("booked")).SelectMany(t => convertSeat(t.seat_no)).ToList()
                            )
                        )
                        ;
                        cfg.CreateMap<place, placeDTO>();
                    }
                );
            var mapper = config.CreateMapper();
            return mapper.Map<tripInDetailsDTO>(tripData);
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

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
    public class customerTicketService
    {
        public static bool isSeatAvailable(int trip_id, List<int> reqSeat)
        {
            var tripObj = DataAccessFactory.getTrip().get(trip_id);
            foreach(int s in reqSeat)
            {
                if(s > tripObj.bus.totalSeat)
                    return false;
            }
            var purchasedTicket = (from t in tripObj.tickets
                                   from s in t.seat_no.Split(',')
                                   select int.Parse(s)).ToList();
            return purchasedTicket.Intersect(reqSeat).Count() == 0;
        }
        public static int getCuponIDbyString(string cuponString)
        {
            return (from dc in DataAccessFactory.getDiscountCupon().All()
                           where dc.cupon.Equals(cuponString)
                           select dc.id).SingleOrDefault();
        }
        public static bool purchaseTicket(ticketDTO obj)
        {
            if (isSeatAvailable(obj.trip_id, obj.seat_no))
            {
                obj.status = "booked";
                var config = new MapperConfiguration(
                    cfg => cfg.CreateMap<ticketDTO, ticket>()
                    .ForMember(dst => dst.seat_no, opt => opt.MapFrom(src => string.Join(",", src.seat_no.Select(s => s.ToString()).ToArray())))
                    );
                var mapper = config.CreateMapper();
                var convertedObj = mapper.Map<ticket>(obj);
                return DataAccessFactory.getTicket().create(convertedObj);
            }
            return false;
        }
        public static List<tripInDetailsDTO> getTripInDetails()
        {
            var tripData = DataAccessFactory.getTrip().All().ToList();
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<trip, tripInDetailsDTO>();
                        cfg.CreateMap<place, placeDTO>();
                    }
                );
            var mapper = config.CreateMapper();
            return mapper.Map<List<tripInDetailsDTO>>(tripData);
        }
        public static tripInDetailsDTO getTripInDetails(int tripid)
        {
            var tripData = DataAccessFactory.getTrip().get(tripid);
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<trip, tripInDetailsDTO>();
                        cfg.CreateMap<place, placeDTO>();
                    }
                );
            var mapper = config.CreateMapper();
            return mapper.Map<tripInDetailsDTO>(tripData);
        }
        public static List<discountCuponDTO> getCuponDetails()
        {
            var tripData = DataAccessFactory.getDiscountCupon().All().ToList();
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<discountCupon, discountCuponDTO>();
                    }
                );
            var mapper = config.CreateMapper();
            return mapper.Map<List<discountCuponDTO>>(tripData);
        }
        public static discountCuponDTO getCuponDetails(int cuponID)
        {
            var tripData = DataAccessFactory.getDiscountCupon().get(cuponID);
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<discountCupon, discountCuponDTO>();
                    }
                );
            var mapper = config.CreateMapper();
            return mapper.Map<discountCuponDTO>(tripData);
        }
    }
}

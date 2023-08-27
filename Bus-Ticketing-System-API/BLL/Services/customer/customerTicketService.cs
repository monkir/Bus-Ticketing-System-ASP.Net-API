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
        public static bool isOwnerOfTicket(int cust_id, int tikcetID)
        {
           var ticketObj = DataAccessFactory.getTicket().get(tikcetID);
            return ticketObj == null ? false : ticketObj.cust_id == cust_id;
        }
        private static List<int> convertSeat(string seats)
        {
            return seats.Split(',').Select(s => Convert.ToInt32(s)).ToList();
        }
        private static string convertSeat(List<int> seats)
        {
            return string.Join(",", seats.Select(s => s.ToString()));
        }
        public static ticketDTO GetTicket(int tikcetID)
        {
            var data = DataAccessFactory.getTicket().get(tikcetID);
            var config = new MapperConfiguration
                (
                    cfg => cfg.CreateMap<ticket, ticketDTO>()
                    .ForMember
                    (
                        dst => dst.seat_no, opt => opt.MapFrom
                        (
                            //src => src.seat_no.Split(',').Select(s => Convert.ToInt32(s)).ToList()
                            src => convertSeat(src.seat_no)
                        )
                    )
                );
            var mapper = config.CreateMapper();
            return mapper.Map<ticketDTO>(data);
        }
        public static List<ticketDTO> GetTicketList(int cust_ID)
        {
            var data = DataAccessFactory.getCustomer().get(cust_ID).tickets;
            var config = new MapperConfiguration
                (
                    cfg => cfg.CreateMap<ticket, ticketDTO>()
                    .ForMember
                    (
                        dst => dst.seat_no, opt => opt.MapFrom
                        (
                            //src => src.seat_no.Split(',').Select(s => Convert.ToInt32(s)).ToList()
                            src => convertSeat(src.seat_no)
                        )
                    )
                );
            var mapper = config.CreateMapper();
            return mapper.Map<List<ticketDTO>>(data);
        }

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
                                   where t.status.Equals("booked")
                                   select int.Parse(s)).ToList();
            return purchasedTicket.Intersect(reqSeat).Count() == 0;
        }
        public static int getCuponIDbyString(string cuponString)
        {
            return (from dc in DataAccessFactory.getDiscountCupon().All()
                           where dc.cupon.Equals(cuponString)
                           select dc.id).SingleOrDefault();
        }
        private static int calculateDiscount(int ammount, int dc_id)
        {
            var obj = DataAccessFactory.getDiscountCupon().get(dc_id);
            var dc = new List<int>() { obj.maxDiscount, Convert.ToInt32(ammount* obj.percentage / 100) };
            return dc.Min();
        }
        private static int calculateRefund(int ammount, DateTime time)
        {
            if (DateTime.Now.AddDays(3).CompareTo(time) < 0)
                return ammount * 90 / 100;
            if (DateTime.Now.AddDays(2).CompareTo(time) < 0)
                return ammount * 70 / 100;
            if (DateTime.Now.AddHours(1).CompareTo(time) < 0)
                return ammount * 50 / 100;
            if (DateTime.Now.AddHours(12).CompareTo(time) < 0)
                return ammount * 25 / 100;
            if (DateTime.Now.AddHours(6).CompareTo(time) < 0)
                return ammount * 10 / 100;
            return 0;
        }
        public static bool purchaseTicket(ticketDTO obj)
        {
            if (isSeatAvailable(obj.trip_id, obj.seat_no))
            {
                obj.status = "booked";
                int ticketPrice = DataAccessFactory.getTrip().get(obj.trip_id).ticketPrice;
                int seatCount = obj.seat_no.Distinct().Count();
                obj.ammount = ticketPrice * seatCount;
                if(obj.dc_id != null)
                {
                    obj.ammount -= calculateDiscount(obj.ammount, (int)obj.dc_id);
                }
                var config = new MapperConfiguration
                    (
                        cfg => cfg.CreateMap<ticketDTO, ticket>()
                        .ForMember
                        (
                            dst => dst.seat_no, opt => opt.MapFrom
                            (
                                //src => string.Join(",", src.seat_no.Select(s => s.ToString()).ToArray()))
                                src => convertSeat(src.seat_no)
                            )
                        )
                    );
                var mapper = config.CreateMapper();
                var convertedObj = mapper.Map<ticket>(obj);
                return DataAccessFactory.getTicket().create(convertedObj);
            }
            return false;
        }

        // add amount to account
        private static bool addAccount(int id, int ammount, string details)
        {
            var obj = new transaction()
            {
                details = "Added: " + details,
                amount = ammount,
                time = DateTime.Now,
                userID = id
            };
            return DataAccessFactory.getTransaction().create(obj);
        }
        public static bool cancelTicket(int ticketId)
        {
            var ticketObj = DataAccessFactory.getTicket().get(ticketId);
            int refund= calculateRefund(ticketObj.ammount, ticketObj.trip.startTime);
            if(refund > 0)
            {
                if(addAccount(ticketObj.cust_id, refund, "Refund") == false)
                {
                    return false;
                }
            }
            ticketObj.status = "cancalled";
            return DataAccessFactory.getTicket().update(ticketObj);
        }
        public static List<tripInDetailsDTO> getTripInDetails()
        {
            var tripData = DataAccessFactory.getTrip().All().
                Where(
                        t => t.status.Equals("added") 
                        && DateTime.Now.AddHours(+1).CompareTo(t.startTime) < 0
                    ).ToList();
            var seat = (from t in tripData
                        from tk in t.tickets
                        from s in convertSeat(tk.seat_no)
                        select s).ToList();
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
        public static tripInDetailsDTO getTripInDetails(int tripid)
        {
            var tripData = DataAccessFactory.getTrip().get(tripid);
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
        public static List<tripInDetailsDTO> getMyTripInDetails(int cust_id)
        {
            var tripData = DataAccessFactory.getCustomer().get(cust_id).tickets.Select(t => t.trip);
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

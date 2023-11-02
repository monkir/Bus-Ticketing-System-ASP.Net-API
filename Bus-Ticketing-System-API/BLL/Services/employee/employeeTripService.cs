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

        // add amount to account
        private static bool addAccount(int? bp_id, int ammount, string details)
        {
            var obj = new transaction()
            {
                details = "Added: " + details,
                amount = ammount,
                time = DateTime.Now,
                userID = bp_id
            };
            return DataAccessFactory.getTransaction().create(obj);
        }
        public static bool acceptCancelTrip(int tripID)
        {
            var tripData = DataAccessFactory.getTrip().get(tripID);
            foreach(var tk in tripData.tickets)
            {
                tk.status = "cancelled";
                DataAccessFactory.getTicket().update(tk);
                addAccount(tk.cust_id, tk.ammount, "Refunded");
            }
            tripData.status = "cancelled";
            return DataAccessFactory.getTrip().update(tripData);
        }
        public static bool acceptAddTrip(int tripID)
        {
            var tripData = DataAccessFactory.getTrip().get(tripID);
            tripData.status = "added";
            return DataAccessFactory.getTrip().update(tripData);
        }
        public static bool doneTrip(int tripID)
        {
            var tripData = DataAccessFactory.getTrip().get(tripID);
            tripData.status = "done";
            int ammount = tripData.tickets.Select(t => t.ammount).Sum();
            addAccount(tripData.bus.bp_id, ammount, "Done trip");
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

using DAL.EF.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class tripRepo:repo, IRepo<trip, int, bool>
    {
        public List<trip> All()
        {
            return db.trip.ToList();
        }

        public bool create(trip obj)
        {
            db.trip.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            db.trip.Remove(get(key));
            return db.SaveChanges() > 0;
        }

        public trip get(int key)
        {
            return db.trip.Find(key);
        }

        public bool update(trip obj)
        {
            db.trip.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

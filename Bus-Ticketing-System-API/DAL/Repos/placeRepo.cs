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
    public class placeRepo: repo, IRepo<place, int, bool>
    {
        public List<place> All()
        {
            return db.place.ToList();
        }

        public bool create(place obj)
        {
            db.place.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            var exObj = db.place.Find(key);
            db.place.Remove(exObj);
            return db.SaveChanges() > 0;
        }

        public place get(int key)
        {
            return db.place.Find(key);
        }

        public bool update(place obj)
        {
            db.place.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

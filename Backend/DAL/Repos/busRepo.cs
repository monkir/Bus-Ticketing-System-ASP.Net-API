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
    internal class busRepo : repo, IRepo<bus, int, bool>
    {
        public List<bus> All()
        {
            return db.bus.ToList();
        }

        public bool create(bus obj)
        {
            db.bus.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            db.bus.Remove(get(key));
            return db.SaveChanges() > 0;
        }

        public bus get(int key)
        {
            return db.bus.Find(key);
        }

        public bool update(bus obj)
        {
            db.bus.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

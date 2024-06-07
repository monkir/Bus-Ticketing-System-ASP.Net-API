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
    public class customerRepo : repo, IRepo<customer, int, bool>
    {
        public List<customer> All()
        {
            return db.customer.ToList();
        }

        public bool create(customer obj)
        {
            db.customer.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            var exObj = db.customer.Find(key);
            db.customer.Remove(exObj);
            return db.SaveChanges() > 0;
        }

        public customer get(int key)
        {
            return db.customer.Find(key);
        }

        public bool update(customer obj)
        {
            db.customer.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

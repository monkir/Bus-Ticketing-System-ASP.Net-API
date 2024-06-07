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
    public class discountCuponRepo: repo, IRepo<discountCupon, int, bool>
    {
        public List<discountCupon> All()
        {
            return db.discountCupon.ToList();
        }

        public bool create(discountCupon obj)
        {
            db.discountCupon.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            var exObj = db.discountCupon.Find(key);
            db.discountCupon.Remove(exObj);
            return db.SaveChanges() > 0;
        }

        public discountCupon get(int key)
        {
            return db.discountCupon.Find(key);
        }

        public bool update(discountCupon obj)
        {
            db.discountCupon.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

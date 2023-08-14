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
    public class userRepo : repo, IRepo<user, int, user>
    {
        public List<user> All()
        {
            return db.user.ToList();
        }

        public user create(user obj)
        {
            db.user.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool delete(int key)
        {
            var exObj = db.user.Find(key);
            db.user.Remove(exObj); 
            return db.SaveChanges() > 0;
        }

        public user get(int key)
        {
            return db.user.Find(key);
        }

        public bool update(user obj)
        {
            db.user.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

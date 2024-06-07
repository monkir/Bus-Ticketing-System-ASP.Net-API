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
    internal class tokenRepo : repo, IRepo<token, int, token>
    {
        public List<token> All()
        {
            return db.token.ToList();
        }

        public token create(token obj)
        {
            db.token.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool delete(int key)
        {
            db.token.Remove(get(key));
            return db.SaveChanges() > 0;
        }

        public token get(int key)
        {
            return db.token.Find(key);
        }

        public bool update(token obj)
        {
            db.token.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

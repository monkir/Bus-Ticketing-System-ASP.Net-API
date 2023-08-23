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
    internal class ticketRepo : repo, IRepo<ticket, int, bool>
    {
        public List<ticket> All()
        {
            return db.ticket.ToList();
        }

        public bool create(ticket obj)
        {
            db.ticket.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            db.ticket.Remove(get(key));
            return db.SaveChanges() > 0;
        }

        public ticket get(int key)
        {
            return db.ticket.Find(key);
        }

        public bool update(ticket obj)
        {
            db.ticket.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

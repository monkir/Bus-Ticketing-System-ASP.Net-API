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
    public class transactionRepo : repo, IRepo<transaction, int, bool>
    {
        public List<transaction> All()
        {
            return db.transaction.ToList();
        }

        public bool create(transaction obj)
        {
            db.transaction.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            db.transaction.Remove(get(key));
            return db.SaveChanges() > 0;
        }

        public transaction get(int key)
        {
            return db.transaction.Find(key);
        }

        public bool update(transaction obj)
        {
            db.transaction.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

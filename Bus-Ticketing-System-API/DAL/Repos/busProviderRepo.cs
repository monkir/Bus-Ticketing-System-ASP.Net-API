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
    public class busProviderRepo : repo, IRepo<busProvider, int, bool>
    {
        public List<busProvider> All()
        {
            return db.busProvider.ToList();
        }

        public bool create(busProvider obj)
        {
            db.busProvider.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            var exObj = db.busProvider.Find(key);
            db.busProvider.Remove(exObj);
            return db.SaveChanges() > 0;
        }

        public busProvider get(int key)
        {
            return db.busProvider.Find(key);
        }

        public bool update(busProvider obj)
        {
            db.busProvider.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

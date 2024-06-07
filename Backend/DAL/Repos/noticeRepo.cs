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
    public class noticeRepo : repo, IRepo<notice, int, bool>
    {
        public List<notice> All()
        {
            return db.notice.ToList();
        }

        public bool create(notice obj)
        {
            db.notice.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            var exObj = db.notice.Find(key);
            db.notice.Remove(exObj);
            return db.SaveChanges() > 0;
        }

        public notice get(int key)
        {
            return db.notice.Find(key);
        }

        public bool update(notice obj)
        {
            db.notice.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

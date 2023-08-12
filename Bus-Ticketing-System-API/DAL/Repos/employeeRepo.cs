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
    internal class employeeRepo : repo, IRepo<employee, int, bool>
    {
        public List<employee> All()
        {
            return db.employee.ToList();
        }

        public bool create(employee obj)
        {
            db.employee.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool delete(int key)
        {
            var exEmp = db.employee.Find(key);
            db.employee.Remove(exEmp);
            return db.SaveChanges() > 0;
        }

        public employee get(int key)
        {
            return db.employee.Find(key);
        }

        public bool update(employee obj)
        {
            db.employee.AddOrUpdate(obj);
            return db.SaveChanges() > 0;
        }
    }
}

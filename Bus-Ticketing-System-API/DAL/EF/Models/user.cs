using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class user
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    //user role maybe admin/employee/bus-providers/customer
        public string userRole { get; set; }

    // If user role is admin
        public virtual admin admin { get; set; }

    // If user role is employee
        public virtual employee employee { get; set; }

    // If user role is customer
        public virtual customer customer { get; set; }

    // If user role is bus-provider
        public virtual busProvider busProvider { get; set; }
    }
}

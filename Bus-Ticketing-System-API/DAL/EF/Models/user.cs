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
        public int userID { get; set; }
        public int userRoleID { get; set;}

    // If user role is admin
        /*[ForeignKey("admin")]
        public int? admin_id { get; set; }*/
        public virtual admin admin { get; set; }

    // If user role is employee
        /*[ForeignKey("employee")]
        public int? emp_id { get; set; }*/
        public virtual employee employee { get; set; }

    // If user role is customer
        /*[ForeignKey("customer")]
        public int? cust_id { get; set; }*/
        public virtual customer customer { get; set; }

    // If user role is bus-provider
        /*[ForeignKey("busProvider")]
        public int? bp_id { get; set; }*/
        public virtual busProvider busProvider { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class userDTO
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        //user role maybe admin/employee/bus-providers/customer
        public string userRole { get; set; }
    }
}

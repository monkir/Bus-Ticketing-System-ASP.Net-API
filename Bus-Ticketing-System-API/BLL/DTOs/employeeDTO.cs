﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class employeeDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        // salary
        public int salary { get; set; }

        // dob
        public DateTime dob { get; set; }

        // Admin who added this employee
        public int admin_id { get; set; }
    // From user model
        public string username { get; set; }
        public string password { get; set; }
    }
}

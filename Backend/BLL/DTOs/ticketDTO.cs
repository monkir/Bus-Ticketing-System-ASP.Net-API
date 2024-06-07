using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ticketDTO
    {
        public int id { get; set; }
        public List<int> seat_no { get; set; }
        public int ammount { get; set; }
        public string status { get; set; }

        // Discount-cupon used in this ticket
        public int? dc_id { get; set; }
        public string cupon { get; set; }

        // The trip of ticket is for
        public int trip_id { get; set; }

        // THe customer who bought this ticket
        public int cust_id { get; set; }
    }
}

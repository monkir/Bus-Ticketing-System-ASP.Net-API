using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class tripDTO
    {
        public int id { get; set; }
        public int ticketPrice { get; set; }
        public string status { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        // The employee who will approve or decline of adding or cancelling trip 
        public int? emp_id { get; set; }
    // The place from where the bus will take off
        public int depot_id { get; set; }

    // The place to where this bus will go
        public int dest_id { get; set; }

    // The bus by which this trip is done
        public int bus_id { get; set; }
    }
}

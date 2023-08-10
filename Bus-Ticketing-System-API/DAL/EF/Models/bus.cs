using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class bus
    {
        public int id { get; set; }
        public string brand { get; set; } //Hunday
        public string model { get; set; } //asdf
        public string serialNo { get; set; } //Dhaka - 3251651
        public string category { get; set; }//AC or NON-AC
        public int totalSeat { get; set; }

    // The bus-provider who provided this bus
        [ForeignKey("busProvider")]
        public int bp_id { get; set; }
        public virtual busProvider busProvider { get; set; }

    // List of trips done with this bus
        public virtual List<trip> trips { get; set; }
        public bus()
        {
            trips = new List<trip>();
        }
    }
}

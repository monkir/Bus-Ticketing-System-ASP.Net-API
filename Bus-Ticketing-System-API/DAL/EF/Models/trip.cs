using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class trip
    {
        public int id { get; set; }
        public int ticketPrice { get; set; }
        [DefaultValue("adding/pending")]
        public string status { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

    // The employee who will approve or decline of adding or cancelling trip 
        [ForeignKey("employee")]
        public int? emp_id { get; set; }
        public virtual employee employee { get; set; }
    // The place from where the bus will take off
        [ForeignKey("depot")]
        public int depot_id { get; set; }
        public virtual place depot { get; set; }

    // The place to where this bus will go
        [ForeignKey("destination")]
        public int dest_id { get; set; }
        public virtual place destination { get; set; }

    // The bus by which this trip is done
        [ForeignKey("bus")]
        public int bus_id { get; set; }
        public virtual bus bus { get; set; }

    // The list of tickets of this bus
        public virtual List<ticket> tickets { get; set; }
        public trip() 
        { 
            tickets = new List<ticket>();
        }
    }
}

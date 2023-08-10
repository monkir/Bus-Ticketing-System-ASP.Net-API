using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class place
    {
        public int id { get; set; }
        public string name { get; set; }
    // List of depots
        [InverseProperty(nameof(trip.depot))]
        public virtual List<trip> depots { get; set; }
    // List of destinations
        [InverseProperty(nameof(trip.destination))]
        public virtual List<trip> destinations { get; set; }
    }
}

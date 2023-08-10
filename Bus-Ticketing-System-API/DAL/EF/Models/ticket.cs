using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class ticket
    {
        public int id { get; set; }
        public List<int> seat_no { get; set; }
        public string status { get; set; }

    // Discount-cupon used in this ticket
        [ForeignKey("discountCupon")]
        public int? dc_id { get; set; }
        public virtual discountCupon discountCupon { get; set;}

    // The trip of ticket is for
        [ForeignKey("trip")]
        public int trip_id { get; set; }
        public virtual trip trip { get; set; }

    // THe customer who bought this ticket
        [ForeignKey("customer")]
        public int cust_id { get; set; }
        public virtual customer customer { get; set; }
    }
}

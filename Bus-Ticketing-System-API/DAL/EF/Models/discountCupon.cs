using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class discountCupon
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cupon { get; set; }
        public float percentage { get; set; }
        public int maxDiscount { get; set; }

    // The admin id who created this token
        [ForeignKey("admin")]
        public int? admin_id { get; set; }
        public virtual admin admin { get; set; }

    // List of tickets where this token is used
        public virtual List<ticket> tickets { get; set; }
        public discountCupon()
        {
            tickets = new List<ticket>();
        }
    }
}

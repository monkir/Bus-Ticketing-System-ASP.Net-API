using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class customer
    {
        [Key, ForeignKey("user")]
        public int id { get; set; }
        public string name { get; set; }

        // user
        /*[ForeignKey("user")]
        public int? user_id { get; set; }*/
        public virtual user user { get; set; }

        // List of ticket bought by this customer
        public virtual List<ticket> tickets { get; set; }
        public customer()
        {
            tickets = new List<ticket>();
        }
    }
}
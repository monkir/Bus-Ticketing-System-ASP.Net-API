using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class busProvider
    {
        [Key, ForeignKey("user")]
        public int id { get; set; }
        public string company { get; set; }

    // user
        /*[ForeignKey("user")]
        public int? user_id { get; set; }*/
        public virtual user user { get; set; }


    // The employee who add this bus-provider
        [ForeignKey("employee")]
        public int emp_id { get; set; }
        public virtual employee employee { get; set; }

    // The list of buses provided by this bus-provider
        public virtual List<bus> buses { get; set; }
        public busProvider()
        {
            buses = new List<bus>();
        }
    }
}

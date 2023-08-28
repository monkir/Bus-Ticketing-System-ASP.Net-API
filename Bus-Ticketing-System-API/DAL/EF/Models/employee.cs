using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class employee
    {
        [Key, ForeignKey("user")]
        public int id { get; set; }
        public string name { get; set; }

    // user
        /*[ForeignKey("user")]
        public int user_id { get; set; }*/
        public virtual user user { get; set; }

    // salary
        public int salary { get; set; }

    // dob
        public DateTime dob { get; set; }

    // Admin who added this employee
        [ForeignKey("admin")]
        public int admin_id { get; set; }
        public virtual admin admin { get; set; }

    // list of bus providers created by employee
        public virtual List<busProvider> busProviders {get; set;}

    // list of notices created by employee
        public virtual List<notice> notices { get; set;}
    // list of places added by employee
        public virtual List<place> places { get; set;}
        public employee()
        {
            busProviders = new List<busProvider>();
            notices = new List<notice>();
            places = new List<place>();
        }
    }
}

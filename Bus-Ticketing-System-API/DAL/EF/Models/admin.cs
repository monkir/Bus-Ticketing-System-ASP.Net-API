using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class admin
    {
        [Key, ForeignKey("user")]
        public int id { get; set; }

        //user
        /*[ForeignKey("user")]
        public int? user_id { get; set; }*/
        public virtual user user { get; set; }

        // List of employee added by this admin
        public virtual List<employee> employees { get; set; }

        // List of notices created by this admin
        public virtual List<discountCupon> discountCupons { get; set; }
        public admin()
        {
            employees = new List<employee>();
            discountCupons = new List<discountCupon>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class notice
    {
        public int id {  get; set; }
        public string title { get; set; }
        public string description { get; set; }

    // The employee who post this notice
        [ForeignKey("employee")]
        public int emp_id { get; set; }
        public virtual employee employee { get; set; }
    }
}

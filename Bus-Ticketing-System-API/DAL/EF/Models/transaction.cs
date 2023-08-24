using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class transaction
    {
        public int id { get; set; }
        public string details { get; set; }
        public int amount { get; set; }
        public DateTime time { get; set; }
        [ForeignKey("user")]
        public int userID { get; set; }
        public virtual user user { get; set; }
    }
}

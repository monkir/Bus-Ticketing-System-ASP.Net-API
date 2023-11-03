using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class token
    {
        public int id { get; set; }
        public string token_string { get; set; }
        public DateTime expireTime { get; set; }

        // User
        [ForeignKey("user")]
        public int userid { get; set; }
        public virtual user user { get; set; }
    }
}
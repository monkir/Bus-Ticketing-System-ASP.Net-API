using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class tokenDTO
    {
        public int id { get; set; }
        public string token_string { get; set; }
        public DateTime expireTime { get; set; }

        // User
        public int userid { get; set; }
    }
}

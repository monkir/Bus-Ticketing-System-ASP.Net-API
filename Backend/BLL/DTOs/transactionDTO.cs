using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class transactionDTO
    {
        public int id { get; set; }
        public string details { get; set; }
        public int amount { get; set; }
        public DateTime time { get; set; }
        public int userID { get; set; }
    }
}

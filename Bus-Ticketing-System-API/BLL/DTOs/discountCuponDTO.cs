using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class discountCuponDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string cupon { get; set; }
        public float percentage { get; set; }
        public int maxDiscount { get; set; }

        // The admin id who created this token
        public int admin_id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class placeDTO
    {
        public int id { get; set; }
        public string name { get; set; }
    // The employee who post this notice
        public int emp_id { get; set; }
    }
}

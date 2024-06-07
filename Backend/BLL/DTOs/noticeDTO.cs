using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class noticeDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        // The employee who post this notice
        public int emp_id { get; set; }
    }
}

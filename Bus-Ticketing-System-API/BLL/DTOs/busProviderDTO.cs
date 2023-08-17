using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class busProviderDTO
    {
        public int id { get; set; }
        public string company { get; set; }

        // The employee who add this bus-provider
        public int emp_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}

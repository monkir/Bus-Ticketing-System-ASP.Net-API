using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class busProviderDTO:userDTO
    {
        public string company { get; set; }

        // The employee who add this bus-provider
        public int emp_id { get; set; }
    }
}

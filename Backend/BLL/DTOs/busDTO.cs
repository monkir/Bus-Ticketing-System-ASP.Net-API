using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class busDTO
    {
        public int id { get; set; }
        public string brand { get; set; } //Hunday
        public string model { get; set; } //asdf
        public string serialNo { get; set; } //Dhaka - 3251651
        public string category { get; set; }//AC or NON-AC
        public int totalSeat { get; set; }

        // The bus-provider who provided this bus
        public int bp_id { get; set; }
    }
}

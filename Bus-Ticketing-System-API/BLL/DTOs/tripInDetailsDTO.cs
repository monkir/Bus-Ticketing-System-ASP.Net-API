using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class tripInDetailsDTO:tripDTO
    {
        public placeDTO depot { get; set; }
        public placeDTO destination { get; set; }
        public List<int> bookedSeat { get; set; } = new List<int>();
    }
}

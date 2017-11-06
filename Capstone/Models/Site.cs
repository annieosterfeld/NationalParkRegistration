using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Site
    {
        public int Site_id { get; set; }
        public int Camground_id { get; set; }
        public int Site_number { get; set; }
        public int Max_occupancy { get; set; }
        public bool Accessible { get; set; }
        public int Max_rv_length { get; set; }
        public bool Utilities { get; set; }

        public override string ToString()
        {
            return $"{Site_id.ToString().PadRight(10)} {Site_number.ToString().PadRight(10)} {Max_occupancy.ToString().PadRight(15)} {Accessible.ToString().PadRight(24)} {Max_rv_length.ToString().PadRight(15)} {Utilities.ToString().PadRight(14)}";
        }
    }
}

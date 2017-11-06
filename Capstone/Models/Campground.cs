using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        public int Campground_id { get; set; }
        public int Park_id { get; set; }
        public string Campground_name { get; set; }
        public int Open_from_month { get; set; }
        public int Open_to_month { get; set; }
        public int Daily_fee { get; set; }

        public override string ToString()
        {
            return $"# {Campground_id.ToString().PadRight(5)} {Campground_name.PadRight(40)} {monthNumberToString(Open_from_month).ToString().PadRight(15)} {monthNumberToString(Open_to_month).ToString().PadRight(15)} {Daily_fee.ToString("C2").PadRight(5)}";
        }

        public string monthNumberToString(int num)
        {
            if (num == 1)
                return "January";
            if (num == 2)
                return "February";
            if (num == 3)
                return "March";
            if (num == 4)
                return "April";
            if (num == 5)
                return "May";
            if (num == 6)
                return "June";
            if (num == 7)
                return "July";
            if (num == 8)
                return "August";
            if (num == 9)
                return "September";
            if (num == 10)
                return "October";
            if (num == 11)
                return "November";
            if (num == 12)
                return "December";
            else
                return "Invalid";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        public int Reservation_id { get; set; }
        public int Site_id { get; set; }
        public string Reservation_name { get; set; }
        public DateTime Reservation_from_date { get; set; }
        public DateTime Reservation_to_date { get; set; }
        public DateTime Reservation_create_date { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using Capstone.DAL;
using System.Windows;

namespace Capstone.Models
{
    public class Park
    {
        public int Park_id { get; set; }
        public string Park_name{ get; set; }
        public string Park_location { get; set; }
        public DateTime Established_dateTime { get; set; }
        public int Area { get; set; }
        public int Annual_visit_count { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            //return $"{Park_name.ToString()} National Park\nLocation: {String.Format("{0, -5}",Park_location.ToString())}\nEstablished: {String.Format("{0,-5}",Established_dateTime.ToShortDateString())}\nArea: {String.Format("{0,-5}", String.Format("{0:n0}",Area).ToString() + " sq km")}\nAnnual Visitors: {String.Format("{0,-5}",String.Format("{0:n0}", Annual_visit_count).ToString())}\n";
            return $"{Park_name.ToString()} National Park\n" + 
                "Location:".PadRight(20) + $" {Park_location.ToString()}\n" + 
                "Established:".PadRight(21) + $"{Established_dateTime.ToShortDateString()}\n" + 
                "Area:".PadRight(20) + $" {(String.Format("{0:n0}", Area).ToString() + " sq km")}\n" + 
                "Annual Visitors:".PadRight(20) + $" {String.Format("{0:n0}", Annual_visit_count).ToString()}\n" + 
                "Description: \n".PadRight(20) + $"{Description.ToString()}";
        }
    }
}

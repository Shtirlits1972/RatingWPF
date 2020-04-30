using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingWPF.Models
{
     public class Rating
    {
        public int Npp { get; set; }
        public int rating_scale_point_id {get; set;}
        public DateTime rating_date {get; set;}
        public int agency_id { get; set; }
        public int scale_id { get; set; }
        public string name_rus { get; set; }
        public string agency_name_rus { get; set; }
        public string point_name { get; set; }


        public override string ToString()
        {
            return $" {name_rus} {rating_date.ToString("D")}";
        }

    }
}

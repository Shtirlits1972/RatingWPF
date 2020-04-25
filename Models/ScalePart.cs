using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingWPF.Models
{
      public class ScalePart
    {
        public string rating_scale_point_name { get; set; }
        public string rating_scale_point_description_rus { get; set; }

        public override string ToString()
        {
            return rating_scale_point_name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingWPF.Models
{
     public class ScaleLine
    {
        public string AgencyName { get; set; }
        public string StatusN { get; set; } = "";
        public string StatusF { get; set; } = "";
        public string PointNameN { get; set; }
        public string PointNameF { get; set; }


        public List<ScalePart> National { get; set; }
        public List<ScalePart> Foreign { get; set; }

        public override string ToString()
        {
            return $"{AgencyName} {StatusN} {StatusF}";
        }
    }
}

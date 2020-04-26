using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingWPF.Models
{
      public class ScalePart
    {
        public int Npp { get; set; }
        public int rating_scale_point_id { get; set; }
        public int rating_scale_id { get; set; }
        public string rating_scale_point_name { get; set; }
        public string rating_scale_point_description_rus { get; set; }
        public string rating_scale_point_description_eng { get; set; }
        public string rating_scale_name_rus { get; set; }
        public string rating_scale_name_eng { get; set; }
        public string name_of_emitent_rus { get; set; }
        public string name_of_emitent_eng { get; set; }
        public DateTime dt_act { get; set; }

        public override string ToString()
        {
            return rating_scale_point_name;
        }
    }
}

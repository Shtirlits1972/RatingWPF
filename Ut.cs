using System.Configuration;

namespace RatingWPF
{
     public class Ut
    {
        public static readonly string strConn = ConfigurationManager.AppSettings["SqlConnString"];
    }
}

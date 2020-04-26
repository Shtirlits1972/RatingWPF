using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System;

namespace RatingWPF.Models
{
      public class ScalePartCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<ScalePart> GetAllScalePart(DateTime Dtt, int id_ag, int id_sc)
        {
            List<ScalePart> list = new List<ScalePart>();

            string strCommand = "Exec GetScale @id_ag, @id_sc, @Dtt ";

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<ScalePart>(strCommand, new {  id_ag, id_sc, Dtt }).ToList();
            }
            return list;
        }

        public static string [] GetGraceWorld()
        {
            string[] graceWorld = { "Withdrawn", "NR", "Suspended", "PIF" };
            return graceWorld;
        }
        public static string GetPointName(int PointId)
        {
            string strPointName = string.Empty;

            string strCommand = " select " +
                 "  distinct top 1 " +
                 "  rating_scale_point_name " +
                 "  from Rating_Cbonds_scale " +
                 "  where " +
                 "  rating_scale_point_id = @PointId; ";

            using (IDbConnection db = new SqlConnection(strConn))
            {
                strPointName = db.Query<string>(strCommand, new { PointId }).FirstOrDefault();
            }

            return strPointName;
        }


    }
}

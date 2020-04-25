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

            string strCommand = "SELECT  " +
             " [rating_scale_point_name]  " +
             " ,[rating_scale_point_description_rus]  " +
             " FROM[bko_web].[dbo].[Rating_Cbonds_scale]  " +
             " where[dt_act] = (Select max([dt_act])   " +
             " FROM[bko_web].[dbo].[Rating_Cbonds_emit] Where dt_act<= @Dtt)   " +
             " and [scale.rating_agency.id_of_emitent] = @id_ag and [scale.rating_scale_id] = @id_sc  " +
             " order by[rating_scale_point_ordnum]";

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<ScalePart>(strCommand, new { Dtt, id_ag, id_sc }).ToList();
            }
            return list;
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

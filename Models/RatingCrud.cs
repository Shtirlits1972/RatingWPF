using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System;

namespace RatingWPF.Models
{
     public class RatingCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Rating> GetAll(string INN, DateTime Dtt)
        {
            List<Rating> list = new List<Rating>();

            string strCommand = "SELECT TOP (100) [rating_scale_point_id]  " +
                              "  , [rating_date]   " +
                              "  ,[emitent.id_of_emitent] as id_of_emitent " +
                              "  ,[emitent.emitent_inn] as emitent_inn " +
                              "  ,[emitent.name_of_emitent_rus] as name_of_emitent_rus  " +
                              "  ,[rating_forecast.name_rus] as name_rus  " +
                              "  ,[services_scale_points.agency_id] as agency_id  " +
                              "  ,[services_scale_points.scale_id] as scale_id  " +
                              "  FROM [Rating_Cbonds_emit] left join [Rating_Cbonds_scale]     " +
                              "  on [Rating_Cbonds_scale].ID_scale= [Rating_Cbonds_emit].ID_SCALE    " +
                              "  where [emitent.emitent_inn] = @INN    " +
                              "  and [Rating_Cbonds_emit].[dt_act]=(Select max([dt_act]) FROM [Rating_Cbonds_emit] Where dt_act<=@Dtt)";

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Rating>(strCommand, new { INN, Dtt }).ToList();
            }
            return list;
        }
    }
}

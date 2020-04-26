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
        public static List<Rating> GetAll(string INN, DateTime Dtt, int typeNum=1)
        {
            //  Exec Ratings_Info 1, '7702070139',   '2020-04-21';
            List<Rating> list = new List<Rating>();

            string strCommand = "Exec Ratings_Info @typeNum, @INN, @Dtt;";

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Rating>(strCommand, new { typeNum, INN, Dtt }).ToList();
            }
            return list;
        }
    }
}

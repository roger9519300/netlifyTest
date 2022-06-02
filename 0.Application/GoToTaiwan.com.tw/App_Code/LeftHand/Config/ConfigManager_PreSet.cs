using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

namespace LeftHand.Config
{

    public static partial class ConfigManager
    {
        public static void PreSet()
        {
            CreateTable();
        }

        //建立資料庫結構
        private static void CreateTable()
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                string TableName = "LeftHand_Config";

                SqlCommand.CommandText += "IF EXISTS"
                                       + "("
                                       + "SELECT "
                                       + " TABLE_NAME "
                                       + "FROM "
                                       + " INFORMATION_SCHEMA.TABLES "
                                       + "WHERE "
                                       + " TABLE_NAME = '" + TableName + "'"
                                       + ")"
                                       + "DROP TABLE " + TableName + "; "

                                       + "CREATE TABLE " + TableName
                                       + "("
                                       + " [ConfigKey] [varchar](100) NOT NULL, "
                                       + " [Value] [nvarchar](4000) NOT NULL, "

                                       + " Primary Key ([ConfigKey]) "
                                       + "); ";

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
            }
        }
    }
}
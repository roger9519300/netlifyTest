using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using LeftHand.Config;

namespace LeftHand.MemberShip2
{
    public static partial class UserManager
    {
        public static void PreSet()
        {
            CreateTable();
            CreateDefaultValue();
        }

        //建立資料庫結構
        private static void CreateTable()
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                string TableName = "LeftHand_MemberShip2_User";

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
                                       + " [ParentId] [varchar](50) NOT NULL, "
                                       + " [Id] [varchar](50) NOT NULL, "
                                       + " [Account] [varchar](50) NOT NULL, "
                                       + " [Password] [varchar](50) NOT NULL, "
                                       + " [IsFreeze] [bit] NOT NULL, "
                                       + " [Type] [varchar](50) NOT NULL, "
                                       + " [Profiles] [nvarchar](4000) NOT NULL, "
                                       + " [UpdateTime] [datetime] NOT NULL, "
                                       + " [CreateTime] [datetime] NOT NULL, "

                                       + " Primary Key ([Id]) "
                                       + "); ";

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
            }
        }

        //插入預設帳號資料
        private static void CreateDefaultValue()
        {
            List<User> Users = new List<User>();
            Users.Add(new Visitor("", "visitor", "visitor", "PASSWORD!@#$%^&*", false, "訪客", DateTime.Now, DateTime.Now));
            Users.Add(new Admin("", "admin", "admin", "123!@#", false, "系統管理者■■", DateTime.Now, DateTime.Now));

            UserAccessor.UpdateInsert(Users);
        }
    }
}
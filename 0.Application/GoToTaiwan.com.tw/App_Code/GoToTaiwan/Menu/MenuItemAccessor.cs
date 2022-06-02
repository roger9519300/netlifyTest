using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using LeftHand.Config;

namespace GoToTaiwan.Menu
{
    public class MenuItemAccessor
    {
        internal static List<MenuItem> SelectByDataReader(SqlCommand SqlCommand)
        {
            List<MenuItem> MenuItems = new List<MenuItem>();

            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    using (SqlDataReader SqlDataReader = SqlCommand.ExecuteReader())
                    {
                        while (SqlDataReader.Read())
                        {
                            string Id = (string)SqlDataReader["Id"];
                            string Title = (string)SqlDataReader["Title"];
                            string Localization = (string)SqlDataReader["Localization"];
                            int Sort = (int)SqlDataReader["Sort"];
                            bool Enable = (bool)SqlDataReader["Enable"];
                            DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                            DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                            MenuItem MenuItem = new MenuItem(Id, Title, Localization, Sort, Enable, UpdateTime, CreateTime);
                            MenuItems.Add(MenuItem);
                        }
                    }
                }
            }
            return MenuItems;
        }

        internal static List<MenuItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Menu_MenuItem ";

            return SelectByDataReader(SqlCommand);
        }

        internal static void UpdateInsert(List<MenuItem> MenuItems)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlConnection.Open();

                for (int i = 0; i < (MenuItems.Count / 100) + (MenuItems.Count % 100 == 0 ? 0 : 1); i++)
                {
                    List<MenuItem> PartMenuItems = MenuItems.Skip(i * 100).Take(100).ToList();

                    //大量資料串接
                    StringBuilder CommandText = new StringBuilder();

                    using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                    {
                        for (int j = 0; j < PartMenuItems.Count; j++)
                        {
                            CommandText.Append("UPDATE "
                                                + " Menu_MenuItem "
                                                + "SET "
                                                + " Title = @Title" + j
                                                + " ,Localization = @Localization" + j
                                                + " ,Sort = @Sort" + j
                                                + " ,Enable = @Enable" + j
                                                + " ,UpdateTime = @UpdateTime" + j
                                                + " WHERE "
                                                + " Id = @Id" + j

                                                + " IF @@ROWCOUNT = 0 "
                                                + "BEGIN "

                                                + "INSERT INTO "
                                                + " Menu_MenuItem "
                                                + "( Id, Title, Localization, Sort, Enable, UpdateTime, CreateTime ) "
                                                + "VALUES "
                                                + "( @Id" + j + ", @Title" + j + ", @Localization" + j + ", @Sort" + j + ", @Enable" + j + ", @UpdateTime" + j + ", @CreateTime" + j + ") "
                                                + "END ");

                            MenuItem MenuItem = PartMenuItems[j];

                            SqlCommand.Parameters.AddWithValue("Id" + j, MenuItem.Id);
                            SqlCommand.Parameters.AddWithValue("Title" + j, MenuItem.Title);
                            SqlCommand.Parameters.AddWithValue("Localization" + j, MenuItem.Localization);
                            SqlCommand.Parameters.AddWithValue("Sort" + j, MenuItem.Sort);
                            SqlCommand.Parameters.AddWithValue("Enable" + j, MenuItem.Enable);
                            SqlCommand.Parameters.AddWithValue("UpdateTime" + j, MenuItem.UpdateTime);
                            SqlCommand.Parameters.AddWithValue("CreateTime" + j, MenuItem.CreateTime);
                        }
                        SqlCommand.CommandText = CommandText.ToString();
                        SqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }
        internal static void Delete(List<MenuItem> MenuItems)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "DELETE "
                                           + " Menu_MenuItem "
                                           + "WHERE "
                                           + " Id  IN ('" + string.Join("','", MenuItems.Select(a => a.Id)) + "') ";

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
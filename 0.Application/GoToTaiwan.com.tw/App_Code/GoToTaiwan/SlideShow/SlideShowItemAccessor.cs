using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using LeftHand.Config;

namespace GoToTaiwan.SlideShow
{
    public class SlideShowItemAccessor
    {
        internal static List<SlideShowItem> SelectByDataReader(SqlCommand SqlCommand)
        {
            List<SlideShowItem> SlideShowItems = new List<SlideShowItem>();

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
                            string Name = (string)SqlDataReader["Name"];
                            string Image = (string)SqlDataReader["Image"];
                            string LinkUrl = (string)SqlDataReader["LinkUrl"];
                            int Sort = (int)SqlDataReader["Sort"];
                            DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                            DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                            SlideShowItem SlideShowItem = new SlideShowItem(Id, Name, Image, LinkUrl, Sort, UpdateTime, CreateTime);
                            SlideShowItems.Add(SlideShowItem);
                        }
                    }
                }
            }
            return SlideShowItems;
        }

        internal static List<SlideShowItem> SelectAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " SlideShow_SlideShowItem ";

            return SelectByDataReader(SqlCommand);
        }

        internal static void UpdateInsert(List<SlideShowItem> SlideShowItems)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlConnection.Open();

                for (int i = 0; i < (SlideShowItems.Count / 100) + (SlideShowItems.Count % 100 == 0 ? 0 : 1); i++)
                {
                    List<SlideShowItem> PartSlideShowItems = SlideShowItems.Skip(i * 100).Take(100).ToList();

                    //大量資料串接
                    StringBuilder CommandText = new StringBuilder();

                    using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                    {
                        for (int j = 0; j < PartSlideShowItems.Count; j++)
                        {
                            CommandText.Append("UPDATE "
                                                + " SlideShow_SlideShowItem "
                                                + "SET "
                                                + " Name = @Name" + j
                                                + " ,Image = @Image" + j
                                                + " ,LinkUrl = @LinkUrl" + j
                                                + " ,Sort = @Sort" + j
                                                + " ,UpdateTime = @UpdateTime" + j
                                                + " WHERE "
                                                + " Id = @Id" + j

                                                + " IF @@ROWCOUNT = 0 "
                                                + "BEGIN "

                                                + "INSERT INTO "
                                                + " SlideShow_SlideShowItem "
                                                + "( Id, Name, Image, LinkUrl, Sort, UpdateTime, CreateTime ) "
                                                + "VALUES "
                                                + "( @Id" + j + ", @Name" + j + ", @Image" + j + ", @LinkUrl" + j + ", @Sort" + j + ", @UpdateTime" + j + ", @CreateTime" + j + ") "
                                                + "END ");

                            SlideShowItem SlideShowItem = PartSlideShowItems[j];

                            SqlCommand.Parameters.AddWithValue("Id" + j, SlideShowItem.Id);
                            SqlCommand.Parameters.AddWithValue("Name" + j, SlideShowItem.Name);
                            SqlCommand.Parameters.AddWithValue("Image" + j, SlideShowItem.Image);
                            SqlCommand.Parameters.AddWithValue("LinkUrl" + j, SlideShowItem.LinkUrl);
                            SqlCommand.Parameters.AddWithValue("Sort" + j, SlideShowItem.Sort);
                            SqlCommand.Parameters.AddWithValue("UpdateTime" + j, SlideShowItem.UpdateTime);
                            SqlCommand.Parameters.AddWithValue("CreateTime" + j, SlideShowItem.CreateTime);
                        }
                        SqlCommand.CommandText = CommandText.ToString();
                        SqlCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        internal static void Delete(List<SlideShowItem> SlideShowItems)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "DELETE "
                                           + " SlideShow_SlideShowItem "
                                           + "WHERE "
                                           + " Id  IN ('" + string.Join("','", SlideShowItems.Select(a => a.Id)) + "') ";

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
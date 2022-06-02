using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using GoToTaiwan.Article;
using LeftHand.Config;
using GoToTaiwan.Menu;

namespace GoToTaiwan.Article
{
    public static partial class ArticleItemManager
    {
        public static string UploadPath { get { return "/_Upload/Article/"; } }
        public static string PhysicalUploadPath { get { return HttpContext.Current.Server.MapPath(UploadPath); } }

        private static List<ArticleItem> GetByDataReader(SqlCommand SqlCommand)
        {
            List<ArticleItem> ArticleItems = new List<ArticleItem>();

            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        string MenuItemId = (string)SqlDataReader["MenuItemId"];
                        string Id = (string)SqlDataReader["Id"];
                        string Title = (string)SqlDataReader["Title"];
                        string Content = (string)SqlDataReader["Content"];
                        bool Enable = (bool)SqlDataReader["Enable"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        ArticleItems.Add(new ArticleItem(MenuItemId, Id, Title, Content, Enable, UpdateTime, CreateTime));
                    }

                }
            }

            return ArticleItems;
        }

        public static int GetAmount(MenuItem MenuItem)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                    + " COUNT(*) "
                                    + "FROM "
                                    + " Article_ArticleItem "
                                    + "WHERE "
                                    + " MenuItemId = @MenuItemId";

            SqlCommand.Parameters.AddWithValue("MenuItemId", MenuItem.Id);

            return (int)GetByScalar(SqlCommand);
        }

        public static ArticleItem GetById(string Id)
        {
            SqlCommand SqlCommand = new SqlCommand();

            SqlCommand.CommandText = "SELECT "
                                       + " * "
                                       + "FROM "
                                       + " Article_ArticleItem "
                                       + "WHERE "
                                       + "Id = @Id";

            SqlCommand.Parameters.AddWithValue("Id", Id);

            return GetByDataReader(SqlCommand).FirstOrDefault();

        }

        public static List<ArticleItem> GetByMenuItemId(string MenuItemId)
        {
            SqlCommand SqlCommand = new SqlCommand();

            SqlCommand.CommandText = "SELECT "
                                       + " * "
                                       + "FROM "
                                       + " Article_ArticleItem "
                                       + "WHERE "
                                       + " MenuItemId = @MenuItemId"
                                       + " ORDER BY UpdateTime "
                                       + " DESC ";

            SqlCommand.Parameters.AddWithValue("MenuItemId", MenuItemId);

            return GetByDataReader(SqlCommand);
        }

        public static List<ArticleItem> GetByIndex(MenuItem MenuItem, int StartIndex, int EndIndex)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "WITH ItemByPagger AS "
                                   + "( "
                                   + "SELECT "
                                   + " ROW_NUMBER() OVER( ORDER BY Id DESC ) AS RowNumber , * "
                                   + "FROM "
                                   + " Article_ArticleItem "
                                   + "WHERE "
                                   + " MenuItemId = @MenuItemId"
                                   + ") "

                                   + "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " ItemByPagger "
                                   + " WHERE "
                                   + " RowNumber BETWEEN " + StartIndex + " AND " + EndIndex;

            SqlCommand.Parameters.AddWithValue("MenuItemId", MenuItem.Id);

            return GetByDataReader(SqlCommand);
        }

        public static List<ArticleItem> GetByTopN(int TopN, bool OnlyEnable)
        {
            SqlCommand SqlCommand = new SqlCommand();

            SqlCommand.CommandText = "SELECT "
                                       + " @TopN * "
                                       + "FROM "
                                       + " Article_ArticleItem "
                                       + "@WHERE "
                                       + "ORDER BY "
                                       + " Id DESC ";

            //TopN
            SqlCommand.CommandText = SqlCommand.CommandText.Replace("@TopN", "TOP " + TopN);

            //OnlyEnable
            if (OnlyEnable == true)
            { SqlCommand.CommandText = SqlCommand.CommandText.Replace("@WHERE", " WHERE Enable = 1 "); }
            else
            { SqlCommand.CommandText = SqlCommand.CommandText.Replace("@WHERE", ""); }

            return GetByDataReader(SqlCommand);

        }

        private static object GetByScalar(SqlCommand SqlCommand)
        {
            object Result = "";

            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                using (SqlCommand)
                {
                    SqlCommand.Connection = SqlConnection;

                    SqlConnection.Open();

                    Result = SqlCommand.ExecuteScalar();
                }
            }

            return Result;
        }

        internal static int GetSort(MenuItem MenuItem)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " ISNULL(MAX(CONVERT(int,Sort)),0)"
                                   + "FROM "
                                   + " Article_ArticleItem "
                                   + "WHERE "
                                   + " Id = @Id ";

            SqlCommand.Parameters.AddWithValue("Id", MenuItem.Id);

            return (int)GetByScalar(SqlCommand) + 5;
        }

            public static List<ArticleItem> GetByDate(DateTime Date)
        {
            SqlCommand SqlCommand = new SqlCommand();

            SqlCommand.CommandText = "SELECT "
                                       + " * "
                                       + "FROM "
                                       + " Article_ArticleItem "
                                       + "WHERE "
                                       + " CreateTime >= @StartDate "
                                       + "AND "
                                       + " CreateTime <= @EndDate "
                                       + "ORDER BY "
                                       + " Id DESC ";

            SqlCommand.Parameters.AddWithValue("StartDate", Date.Date);
            SqlCommand.Parameters.AddWithValue("EndDate", Date.Date.AddDays(1).AddSeconds(-1));

            return GetByDataReader(SqlCommand);
        }

        public static void Save(ArticleItem ArticleItem)
        {
            Save(new List<ArticleItem> { ArticleItem });
        }
        public static void Save(List<ArticleItem> ArticleItemList)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlConnection.Open();

                for (int i = 0; i < (ArticleItemList.Count / 100) + (ArticleItemList.Count % 100 == 0 ? 0 : 1); i++)
                {
                    List<ArticleItem> PartArticleItems = ArticleItemList.Skip(i * 100).Take(100).ToList();
                    StringBuilder CommandText = new StringBuilder();

                    SqlCommand SqlCommand = SqlConnection.CreateCommand();

                    for (int j = 0; j < PartArticleItems.Count; j++)
                    {
                        CommandText.Append("UPDATE "
                                             + " Article_ArticleItem "
                                             + "SET "
                                             + " MenuItemId = @MenuItemId" + j
                                             + " ,Title = @Title" + j
                                             + " ,Content = @Content" + j
                                             + " ,SeoTitle = @SeoTitle" + j
                                             + " ,SeoDescription = @SeoDescription" + j
                                             + " ,Enable = @Enable" + j
                                             + " ,UpdateTime = @UpdateTime" + j
                                             + " ,CreateTime = @CreateTime" + j
                                             + " WHERE "
                                             + " Id = @Id" + j

                                             + " IF @@ROWCOUNT = 0 "
                                             + "BEGIN "
                                             + "INSERT INTO "
                                             + " Article_ArticleItem "
                                             + "( MenuItemId, Id, Title, Content, SeoTitle,  SeoDescription, Enable, UpdateTime, CreateTime ) "
                                             + "VALUES "
                                             + "( @MenuItemId" + j + ", @Id" + j + ", @Title" + j + ", @Content" + j  + ", @SeoTitle" + j + ", @SeoDescription" + j + ",@Enable" + j + ",  @UpdateTime" + j + ", @CreateTime" + j + ") "

                                             + "END ");

                        SqlCommand.Parameters.AddWithValue("MenuItemId" + j, PartArticleItems[j].MenuItemId);
                        SqlCommand.Parameters.AddWithValue("Id" + j, PartArticleItems[j].Id);
                        SqlCommand.Parameters.AddWithValue("Title" + j, PartArticleItems[j].Title);
                        SqlCommand.Parameters.AddWithValue("Content" + j, PartArticleItems[j].Content);
                        SqlCommand.Parameters.AddWithValue("SeoTitle" + j, PartArticleItems[j].SeoTitle);
                        SqlCommand.Parameters.AddWithValue("SeoDescription" + j, PartArticleItems[j].SeoDescription);
                        SqlCommand.Parameters.AddWithValue("Enable" + j, PartArticleItems[j].Enable);
                        SqlCommand.Parameters.AddWithValue("UpdateTime" + j, PartArticleItems[j].UpdateTime);
                        SqlCommand.Parameters.AddWithValue("CreateTime" + j, PartArticleItems[j].CreateTime);
                    }

                    SqlCommand.CommandText = CommandText.ToString();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(ArticleItem ArticleItem)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Article_ArticleItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", ArticleItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }


    }
}

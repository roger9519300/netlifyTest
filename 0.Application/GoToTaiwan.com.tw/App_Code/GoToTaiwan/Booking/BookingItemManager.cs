using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using GoToTaiwan.Booking;
using LeftHand.Config;

namespace GoToTaiwan.Booking
{
    public static partial class BookingItemManager
    {
        private static List<BookingItem> GetByDataReader(SqlCommand SqlCommand)
        {
            List<BookingItem> BookingItems = new List<BookingItem>();

            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                using (SqlCommand.Connection = SqlConnection)
                {
                    SqlConnection.Open();

                    SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                    while (SqlDataReader.Read())
                    {
                        string Id = (string)SqlDataReader["Id"];
                        string Name = (string)SqlDataReader["Name"];
                        DateTime StartTime = (DateTime)SqlDataReader["StartTime"];
                        DateTime EndTime = (DateTime)SqlDataReader["EndTime"];
                        string StartLocation = (string)SqlDataReader["StartLocation"];
                        string EndLocation = (string)SqlDataReader["EndLocation"];
                        int People = (int)SqlDataReader["People"];
                        string Schedule = (string)SqlDataReader["Schedule"];
                        string Email = (string)SqlDataReader["Email"];
                        string Line = (string)SqlDataReader["Line"];
                        string WeChat = (string)SqlDataReader["WeChat"];
                        string WhatsApp = (string)SqlDataReader["WhatsApp"];
                        string Remark = (string)SqlDataReader["Remark"];
                        DateTime UpdateTime = (DateTime)SqlDataReader["UpdateTime"];
                        DateTime CreateTime = (DateTime)SqlDataReader["CreateTime"];

                        BookingItems.Add(new BookingItem(Id, Name, StartTime, EndTime, StartLocation, EndLocation, People, Schedule, Email, Line, WeChat, WhatsApp, Remark, UpdateTime, CreateTime));
                    }

                }
            }

            return BookingItems;
        }

        public static List<BookingItem> GetAll()
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " Booking_BookingItem ";

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


        public static int GetAmount()
        {
            int Amount = 0;

            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();
                SqlCommand.CommandText = "SELECT "
                                        + " COUNT(*) "
                                        + "FROM "
                                        + " Booking_BookingItem ";


                SqlConnection.Open();

                Amount = int.Parse(SqlCommand.ExecuteScalar().ToString());
            }

            return Amount;
        }

        public static BookingItem GetById(string Id)
        {
            SqlCommand SqlCommand = new SqlCommand();

            SqlCommand.CommandText = "SELECT "
                                       + " * "
                                       + "FROM "
                                       + " Booking_BookingItem "
                                       + "WHERE "
                                       + "Id = @Id";

            SqlCommand.Parameters.AddWithValue("Id", Id);

            return GetByDataReader(SqlCommand).FirstOrDefault();

        }

        public static List<BookingItem> GetByIndex(int StartIndex, int EndIndex)
        {
            SqlCommand SqlCommand = new SqlCommand();
            SqlCommand.CommandText = "WITH ItemByPagger AS "
                                   + "( "
                                   + "SELECT "
                                   + " ROW_NUMBER() OVER( ORDER BY Id DESC ) AS RowNumber , * "
                                   + "FROM "
                                   + " Booking_BookingItem "
                                   + ") "

                                   + "SELECT "
                                   + " * "
                                   + "FROM "
                                   + " ItemByPagger "
                                   + " WHERE "
                                   + " RowNumber BETWEEN " + StartIndex + " AND " + EndIndex;


            return GetByDataReader(SqlCommand);
        }

        public static List<BookingItem> GetByTopN(int TopN, bool OnlyEnable)
        {
            SqlCommand SqlCommand = new SqlCommand();

            SqlCommand.CommandText = "SELECT "
                                       + " @TopN * "
                                       + "FROM "
                                       + " Booking_BookingItem "
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

        public static List<BookingItem> GetByDate(DateTime Date)
        {
            SqlCommand SqlCommand = new SqlCommand();

            SqlCommand.CommandText = "SELECT "
                                       + " * "
                                       + "FROM "
                                       + " Booking_BookingItem "
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

        public static void Save(BookingItem BookingItem)
        {
            Save(new List<BookingItem> { BookingItem });
        }
        public static void Save(List<BookingItem> BookingItemList)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlConnection.Open();

                for (int i = 0; i < (BookingItemList.Count / 100) + (BookingItemList.Count % 100 == 0 ? 0 : 1); i++)
                {
                    List<BookingItem> PartBookingItems = BookingItemList.Skip(i * 100).Take(100).ToList();
                    StringBuilder CommandText = new StringBuilder();

                    SqlCommand SqlCommand = SqlConnection.CreateCommand();

                    for (int j = 0; j < PartBookingItems.Count; j++)
                    {
                        CommandText.Append("UPDATE "
                                             + " Booking_BookingItem "
                                             + "SET "
                                             + " Name = @Name" + j
                                             + " ,StartTime = @StartTime" + j
                                             + " ,EndTime = @EndTime" + j
                                             + " ,StartLocation = @StartLocation" + j
                                             + " ,EndLocation = @EndLocation" + j
                                             + " ,People = @People" + j
                                             + " ,Schedule = @Schedule" + j
                                             + " ,Email = @Email" + j
                                             + " ,Line = @Line" + j
                                             + " ,WeChat = @WeChat" + j
                                             + " ,WhatsApp = @WhatsApp" + j
                                             + " ,Remark = @Remark" + j
                                             + " ,UpdateTime = @UpdateTime" + j
                                             + " ,CreateTime = @CreateTime" + j
                                             + " WHERE "
                                             + " Id = @Id" + j

                                             + " IF @@ROWCOUNT = 0 "
                                             + "BEGIN "
                                             + "INSERT INTO "
                                             + " Booking_BookingItem "
                                             + "( Id, Name, StartTime, EndTime, StartLocation, EndLocation, People, Schedule, Email, Line, WeChat, WhatsApp, Remark, UpdateTime, CreateTime ) "
                                             + "VALUES "
                                             + "( @Id" + j + ", @Name" + j + ", @StartTime" + j + ", @EndTime" + j + ", @StartLocation" + j + ", @EndLocation" + j + ", @People" + j + ", @Schedule" + j + ", @Email" + j + ", @Line" + j + ", @WeChat" + j + ", @WhatsApp" + j + ", @Remark" + j + ", @UpdateTime" + j + ", @CreateTime" + j + ") "

                                             + "END ");

                        SqlCommand.Parameters.AddWithValue("Id" + j, PartBookingItems[j].Id);
                        SqlCommand.Parameters.AddWithValue("Name" + j, PartBookingItems[j].Name);
                        SqlCommand.Parameters.AddWithValue("StartTime" + j, PartBookingItems[j].StartTime);
                        SqlCommand.Parameters.AddWithValue("EndTime" + j, PartBookingItems[j].EndTime);
                        SqlCommand.Parameters.AddWithValue("StartLocation" + j, PartBookingItems[j].StartLocation);
                        SqlCommand.Parameters.AddWithValue("EndLocation" + j, PartBookingItems[j].EndLocation);
                        SqlCommand.Parameters.AddWithValue("People" + j, PartBookingItems[j].People);
                        SqlCommand.Parameters.AddWithValue("Schedule" + j, PartBookingItems[j].Schedule);
                        SqlCommand.Parameters.AddWithValue("Email" + j, PartBookingItems[j].Email);
                        SqlCommand.Parameters.AddWithValue("Line" + j, PartBookingItems[j].Line);
                        SqlCommand.Parameters.AddWithValue("WeChat" + j, PartBookingItems[j].WeChat);
                        SqlCommand.Parameters.AddWithValue("WhatsApp" + j, PartBookingItems[j].WhatsApp);
                        SqlCommand.Parameters.AddWithValue("Remark" + j, PartBookingItems[j].Remark);
                        SqlCommand.Parameters.AddWithValue("UpdateTime" + j, PartBookingItems[j].UpdateTime);
                        SqlCommand.Parameters.AddWithValue("CreateTime" + j, PartBookingItems[j].CreateTime);
                    }

                    SqlCommand.CommandText = CommandText.ToString();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }

        public static void Remove(BookingItem BookingItem)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    SqlCommand.CommandText = "Delete "
                                           + " Booking_BookingItem "
                                           + "WHERE "
                                           + " Id = @Id ";

                    SqlCommand.Parameters.AddWithValue("Id", BookingItem.Id);

                    SqlConnection.Open();
                    SqlCommand.ExecuteNonQuery();
                }
            }
        }


    }
}

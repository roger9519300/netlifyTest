using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;

namespace LeftHand.Config
{
    public enum ConfigKey { SeoTitle, AnalyticsCode, 微信QrCode, 預約說明 }

    public static partial class ConfigManager
    {
        private static Dictionary<ConfigKey, string> Configs;

        //上傳路徑
        public static string UploadPath { get { return "/_Upload/Config/"; } }
        public static string PhysicalUploadPath { get { return HttpContext.Current.Server.MapPath(UploadPath); } }

        //初始化
        public static void Initial()
        {
            Configs = ((ConfigKey[])Enum.GetValues(typeof(ConfigKey))).ToDictionary(k => k, v => "");

            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {

                SqlCommand SqlCommand = SqlConnection.CreateCommand();
                SqlCommand.CommandText = "SELECT "
                                      + " * "
                                      + "FROM "
                                      + " LeftHand_Config ";

                SqlConnection.Open();
                SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                while (SqlDataReader.Read())
                {
                    string KeyString = (string)SqlDataReader["ConfigKey"];
                    string ValueString = (string)SqlDataReader["Value"];

                    ConfigKey Key;
                    if (Enum.TryParse(KeyString, out Key) == false) { continue; }

                    Configs[Key] = ValueString;
                }
            }
        }

        //取得所有的Configs
        public static Dictionary<ConfigKey, string> GetAll()
        {
            return Configs;
        }

        public static string GetByConfigKey(ConfigKey ConfigKey)
        {
            return Configs[ConfigKey];
        }

        //儲存Configs
        public static void Save(Dictionary<ConfigKey, string> Configs)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                StringBuilder Commandtext = new StringBuilder();
                Commandtext.Append("DELETE FROM LeftHand_Config WHERE 1 = 1 ;");

                foreach (KeyValuePair<ConfigKey, string> Item in Configs)
                {
                    Commandtext.Append("INSERT INTO "
                                      + " LeftHand_Config "
                                      + "( ConfigKey ,Value ) "
                                      + "VALUES "
                                      + " ( @ConfigKey" + Item.Key + " ,@Value" + Item.Key + " ) ;"
                                      );

                    SqlCommand.Parameters.AddWithValue("ConfigKey" + Item.Key, Item.Key.ToString());
                    SqlCommand.Parameters.AddWithValue("Value" + Item.Key, Item.Value);
                }

                SqlCommand.CommandText = Commandtext.ToString();

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
            }
        }
    }
}
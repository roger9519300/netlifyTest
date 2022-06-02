using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Reflection;
using LeftHand.Config;

namespace LeftHand.MemberShip2
{
    internal static partial class UserAccessor
    {
        internal static List<User> SelectAll()
        {
            List<User> Users = new List<User>();

            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();

                SqlCommand.CommandText = "SELECT "
                                       + " * "
                                       + "FROM "
                                       + " LeftHand_MemberShip2_User ";
                SqlConnection.Open();

                SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();

                while (SqlDataReader.Read())
                {
                    Type Type = System.Type.GetType(string.Format("LeftHand.MemberShip2.{0},App_Code", (string)SqlDataReader["Type"]));

                    object[] Propertys = new object[] {
                        (string)SqlDataReader["ParentId"],
                        (string)SqlDataReader["Id"],
                        (string)SqlDataReader["Account"],
                        LeftHand.Gadget.Encoder.AES_Decryption(SqlDataReader["Password"].ToString()),
                        (bool)SqlDataReader["IsFreeze"],
                        (string)SqlDataReader["Profiles"],
                        (DateTime)SqlDataReader["UpdateTime"],
                        (DateTime)SqlDataReader["CreateTime"]
                    };

                    User User = (User)Activator.CreateInstance(Type, (BindingFlags.NonPublic | BindingFlags.Instance), null, Propertys, null);

                    Users.Add(User);
                };
            }

            return Users;
        }

        internal static void UpdateInsert(List<User> Users)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlConnection.Open();

                using (SqlCommand SqlCommand = SqlConnection.CreateCommand())
                {
                    foreach (List<User> GroupUsers in Users.Select((a, index) => new { i = index % 100, a }).GroupBy(g => g.i).Select(v => v.Select(c => c.a).ToList()))
                    {
                        StringBuilder Commandtext = new StringBuilder();
                        for (int i = 0; i < GroupUsers.Count; i++)
                        {
                            Commandtext.Append("UPDATE "
                                              + " LeftHand_MemberShip2_User "
                                              + "SET "
                                              + " ParentId = @ParentId" + i
                                              + " ,Account = @Account" + i
                                              + " ,Password = @Password" + i
                                              + " ,IsFreeze = @IsFreeze" + i
                                              + " ,Type = @Type" + i
                                              + " ,Profiles = @Profiles" + i
                                              + " ,UpdateTime = @UpdateTime" + i
                                              + " WHERE "
                                              + " Id = @Id" + i

                                              + " IF @@ROWCOUNT = 0 "
                                              + " BEGIN "

                                              + "INSERT INTO "
                                              + " LeftHand_MemberShip2_User ( "
                                              + " ParentId, "
                                              + " Id, "
                                              + " Account, "
                                              + " Password, "
                                              + " IsFreeze, "
                                              + " Type, "
                                              + " Profiles, "
                                              + " UpdateTime, "
                                              + " CreateTime "
                                              + ") VALUES ( "
                                              + " @ParentId" + i
                                              + " ,@Id" + i
                                              + " ,@Account" + i
                                              + " ,@Password" + i
                                              + " ,@IsFreeze" + i
                                              + " ,@Type" + i
                                              + " ,@Profiles" + i
                                              + " ,@UpdateTime" + i
                                              + " ,@CreateTime" + i
                                              + " ) "

                                              + " END "
                                              );

                            User PartUser = GroupUsers[i];
                            SqlCommand.Parameters.AddWithValue("ParentId" + i, PartUser.ParentId);
                            SqlCommand.Parameters.AddWithValue("Id" + i, PartUser.Id);
                            SqlCommand.Parameters.AddWithValue("Account" + i, PartUser.Account);
                            SqlCommand.Parameters.AddWithValue("Password" + i, LeftHand.Gadget.Encoder.AES_Encryption(PartUser.Password));
                            SqlCommand.Parameters.AddWithValue("IsFreeze" + i, PartUser.IsFreeze);
                            SqlCommand.Parameters.AddWithValue("Type" + i, PartUser.GetType().Name);
                            SqlCommand.Parameters.AddWithValue("Profiles" + i, PartUser.ProfileToString());
                            SqlCommand.Parameters.AddWithValue("UpdateTime" + i, PartUser.UpdateTime);
                            SqlCommand.Parameters.AddWithValue("CreateTime" + i, PartUser.CreateTime);
                        }

                        SqlCommand.CommandText = Commandtext.ToString();
                        SqlCommand.ExecuteNonQuery();

                        SqlCommand.Parameters.Clear();
                        Commandtext.Clear();
                    }

                }
            }
        }

        internal static void Delete(List<User> Users)
        {
            using (SqlConnection SqlConnection = ConfigManager.GetConnection())
            {
                SqlCommand SqlCommand = SqlConnection.CreateCommand();
                SqlCommand.CommandText = string.Format("DELETE "
                                       + " LeftHand_MemberShip2_User "
                                       + "WHERE "
                                       + " Account IN  ('{0}') ; ", string.Join("','", Users.Select(u => u.Account))
                                       );

                SqlConnection.Open();
                SqlCommand.ExecuteNonQuery();
            }
        }
    }
}
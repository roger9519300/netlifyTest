using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;

namespace LeftHand.Gadget
{
    public class Encoder
    {
        private const string DefaultHashKey = "GmZOoW002d7OJ2G2";
        private const string DefaultRijndaelIV = "bgfEcfYdWA6NSUr9";

        //AES加密
        public static string AES_Encryption(string PlainCode, string HashKey = DefaultHashKey, string RijndaelIV = DefaultRijndaelIV)
        {
            RijndaelManaged RijndaelObject = new RijndaelManaged();
            RijndaelObject.Key = Encoding.Default.GetBytes(HashKey);
            RijndaelObject.IV = Encoding.Default.GetBytes(RijndaelIV);
            RijndaelObject.Mode = CipherMode.CBC;
            RijndaelObject.Padding = PaddingMode.PKCS7;

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, RijndaelObject.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] DataArray = Encoding.UTF8.GetBytes(PlainCode);
            cs.Write(DataArray, 0, DataArray.Length);
            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        //AES解密
        public static string AES_Decryption(string CipherText, string HashKey = DefaultHashKey, string RijndaelIV = DefaultRijndaelIV)
        {
            RijndaelManaged RijndaelObject = new RijndaelManaged();
            RijndaelObject.Key = Encoding.Default.GetBytes(HashKey);
            RijndaelObject.IV = Encoding.Default.GetBytes(RijndaelIV);
            RijndaelObject.Mode = CipherMode.CBC;
            RijndaelObject.Padding = PaddingMode.PKCS7;

            byte[] DataArray = Convert.FromBase64String(CipherText);
            MemoryStream ms = new MemoryStream(DataArray);
            CryptoStream cs = new CryptoStream(ms, RijndaelObject.CreateDecryptor(), CryptoStreamMode.Read);

            byte[] ReadArray = new byte[DataArray.Length];
            int ReadLenth = cs.Read(ReadArray, 0, ReadArray.Length);

            return Encoding.UTF8.GetString(ReadArray, 0, ReadLenth);
        }

        //MD5加密
        public static string MD5_Encryption(string PlainCode)
        {
            MD5 MD5 = MD5.Create(); //使用MD5
            byte[] Change = MD5.ComputeHash(Encoding.Default.GetBytes(PlainCode));//進行加密
            return BitConverter.ToString(Change).Replace("-", "");
        }

        //SHA1加密
        public static string SHA1_Encryption(string PlainCode)
        {
            SHA1 sha1Hasher = SHA1.Create();
            byte[] data = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(PlainCode));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        //Url加密傳值
        public static string DictionaryEncoder(Dictionary<string, string> PlainData)
        {
            string ReturnString = "";

            StringBuilder StringBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> item in PlainData)
            {
                StringBuilder.Append(item.Key + "，" + item.Value + "│");
            }

            ReturnString = AES_Encryption(StringBuilder.ToString());

            return ReturnString;
        }

        //Url傳值解密
        public static Dictionary<string, string> DictionaryDecoder(string EncoderData)
        {
            Dictionary<string, string> DecodeDateDictionary = new Dictionary<string, string>(); ;

            string PlainCode = AES_Decryption(EncoderData);

            foreach (string item in PlainCode.Split('│'))
            {
                if (string.IsNullOrEmpty(item) == true) { break; }

                int SparatorIndex = item.IndexOf('，');
                string Key = item.Substring(0, SparatorIndex);
                string Value = item.Substring(SparatorIndex + 1);

                DecodeDateDictionary.Add(Key, Value);
            }

            return DecodeDateDictionary;
        }
    }
}
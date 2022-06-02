using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeftHand.Gadget
{
    public static class IdCard
    {
        //地區碼
        static Dictionary<string, string> _AreaCodeList = new Dictionary<string, string>() { 
            { "A", "10" }, { "B", "11" }, { "C", "12" }, { "D", "13" }, { "E", "14" }, { "F", "15" }, { "G", "16" }, { "H", "17" }, { "I", "34" }, { "J", "18" }, { "K", "19" }, { "L", "20" }, { "M", "21" },
            { "N", "22" }, { "O", "35" }, { "P", "23" }, { "Q", "24" }, { "R", "25" }, { "S", "26" }, { "T", "27" }, { "U", "28" }, { "V", "29" }, { "W", "32" }, { "X", "30" }, { "Y", "31" }, { "Z", "33" } 
            };

        public static string GetNew()
        {
            //亂數區碼 Key = 英文 , Value=代表數字
            KeyValuePair<string, string> AreaCode =
                _AreaCodeList
                .OrderBy(c => Guid.NewGuid().ToString())
                .First();

            //性別
            int SexCode = new Random().Next(1, 2);

            //後7碼
            int Last7Code = new Random().Next(1000000, 9999999);

            //撿查碼1
            List<string> Area = AreaCode.Value.ToCharArray().Select(c => c.ToString()).ToList();
            List<string> Last7 = Last7Code.ToString().ToCharArray().Select(c => c.ToString()).ToList();
            int FullCode =
                    Convert.ToInt32(Area[0]) +
                    Convert.ToInt32(Area[1]) * 9 +

                    SexCode * 8 +

                    Convert.ToInt32(Last7[0]) * 7 +
                    Convert.ToInt32(Last7[1]) * 6 +
                    Convert.ToInt32(Last7[2]) * 5 +
                    Convert.ToInt32(Last7[3]) * 4 +
                    Convert.ToInt32(Last7[4]) * 3 +
                    Convert.ToInt32(Last7[5]) * 2 +
                    Convert.ToInt32(Last7[6]) * 1;

            int LastCode = 10 - (FullCode % 10);
            if (LastCode >= 10) { LastCode = 0; }

            return AreaCode.Key + SexCode.ToString() + Last7Code.ToString() + LastCode.ToString();
        }

        public static bool Check(string IdCode)
        {
            List<string> Chars = IdCode
                .Trim()
                .ToUpper()
                .ToCharArray()
                .Select(c => c.ToString())
                .ToList();

            //初步格式驗證
            if (new Regex(@"([A-Z])\d{9}").Match(IdCode).Success == false) { return false; }

            //長度必需為10
            if (Chars.Count != 10) { return false; }

            //地區碼
            List<string> AreaCode;
            if (_AreaCodeList.ContainsKey(Chars[0].ToString()) == false)
            { return false; }
            else
            { AreaCode = _AreaCodeList[Chars[0].ToString()].ToCharArray().Select(c => c.ToString()).ToList(); }

            //第二位必需是1=男或者2=女
            int SexCode = 0;
            if (int.TryParse(Chars[1].ToString(), out SexCode) == false) { return false; }
            if (SexCode < 1 || SexCode > 2) { return false; }

            //驗證
            int CheckCode =
                    Convert.ToInt32(AreaCode[0]) +
                    Convert.ToInt32(AreaCode[1]) * 9 +

                    Convert.ToInt32(Chars[1]) * 8 +

                    Convert.ToInt32(Chars[2]) * 7 +
                    Convert.ToInt32(Chars[3]) * 6 +
                    Convert.ToInt32(Chars[4]) * 5 +
                    Convert.ToInt32(Chars[5]) * 4 +
                    Convert.ToInt32(Chars[6]) * 3 +
                    Convert.ToInt32(Chars[7]) * 2 +
                    Convert.ToInt32(Chars[8]) * 1 +
                    Convert.ToInt32(Chars[9]) * 1;

            if (CheckCode % 10 != 0) { return false; }

            return true;
        }
    }
}

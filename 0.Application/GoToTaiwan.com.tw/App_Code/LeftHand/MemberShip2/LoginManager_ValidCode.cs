using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LeftHand.MemberShip2
{
    public static partial class LoginManager
    {
        //產生新的驗證碼
        public static string GetNewValidCode(bool OnlyNumber = true)
        {
            string ValidCode = "";

            switch (OnlyNumber)
            {
                case true:
                    ValidCode = new Random().Next(1111, 9999).ToString();
                    break;

                case false:
                    ValidCode = Guid.NewGuid().ToString().Substring(0, 4);
                    break;
            }

            ValidCode = ValidCode.ToUpper();

            HttpContext.Current.Session["MemberShip2LoginValidCode"] = ValidCode;

            return ValidCode;
        }

        //取得現在的驗證碼
        public static string GetCurrentValidCode()
        {
            object SessionState = HttpContext.Current.Session["MemberShip2LoginValidCode"];

            if (SessionState == null)
            { return ""; }
            else
            { return SessionState.ToString(); }
        }

        //取得現在的驗證碼Bitmap
        public static Bitmap GetCurrentValidCodeBitmap()
        {
            string ValidCode = GetCurrentValidCode();

            if (string.IsNullOrWhiteSpace(ValidCode) == true) { return null; }

            Bitmap Image = new Bitmap(ValidCode.Length * 17, 30);
            Graphics g = Graphics.FromImage(Image);
            g.Clear(Color.White);

            //增加背景雜線
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                int x1 = random.Next(Image.Width);
                int x2 = random.Next(Image.Width);
                int y1 = random.Next(Image.Height);
                int y2 = random.Next(Image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            Font font = new Font("Arial", 17, (FontStyle.Bold | FontStyle.Italic));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, Image.Width, Image.Height), Color.Red, Color.Blue, 2f, true);
            g.DrawString(ValidCode, font, brush, 3, 3);

            //躁點產生
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(Image.Width);
                int y = random.Next(Image.Height);
                Image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            return Image;
        }

        //取得現在的驗證碼Base64Sring
        public static string GetCurrentValidCodeBase64String()
        {
            Bitmap Bitmap = GetCurrentValidCodeBitmap();

            if (Bitmap == null) { return ""; }

            MemoryStream MemoryStream = new MemoryStream();
            Bitmap.Save(MemoryStream, System.Drawing.Imaging.ImageFormat.Gif);

            byte[] arr = new byte[MemoryStream.Length];
            MemoryStream.Position = 0;
            MemoryStream.Read(arr, 0, (int)MemoryStream.Length);
            MemoryStream.Close();

            return "data:image/gif;base64," + Convert.ToBase64String(arr);
        }

        //檢查驗證碼是否正確
        public static bool CheckValidCode(string Code)
        {
            return Code.ToUpper() == GetCurrentValidCode();
        }

    }

}
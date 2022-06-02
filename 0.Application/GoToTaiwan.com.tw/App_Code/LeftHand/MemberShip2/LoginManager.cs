using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LeftHand.MemberShip2
{
    public static partial class LoginManager
    {
        //取得目前的使用者
        public static User GetCurrentUser()
        {
            User CurrentUser = UserManager.GetUserById(GetPassCard());

            if (CurrentUser == null)
            {
                CurrentUser = UserManager.GetUserById("visitor");

                SetPassCard(CurrentUser);
            }

            return CurrentUser;
        }

        public static User Login(string Account, string Password, string RedirectUrl = "")
        {
            GetNewValidCode();
            return Login(Account, Password, GetCurrentValidCode(), RedirectUrl);
        }

        public static User Login(string Account, string Password, string ValidCode, string RedirectUrl = "")
        {
            List<string> Errors = new List<string>();

            Account = Account.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(Account) == true) { Errors.Add("帳號不可空白"); }

            Password = Password.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(Password) == true) { Errors.Add("密碼不可空白"); }

            ValidCode = ValidCode.Trim();
            if (LoginManager.CheckValidCode(ValidCode) == false) { Errors.Add("驗證碼錯誤"); }

            RedirectUrl = RedirectUrl.Trim();

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }


            User LoginUser = UserManager.GetUserByAccount(Account);
            if (LoginUser == null) { throw new Exception("帳號錯誤"); }
            if (LoginUser.Password != Password) { throw new Exception("密碼錯誤"); }
            if (LoginUser.IsFreeze == true) { throw new Exception("帳號已凍結"); }


            //登入成功---------------------------------------------------------------------------------------------------------

            SetPassCard(LoginUser);

            if (string.IsNullOrWhiteSpace(RedirectUrl) == false) { HttpContext.Current.Response.Redirect(RedirectUrl); }

            return LoginUser;
        }

        public static void LogOut(string RedirectUrl = "")
        {

            //發與驗證票
            User Visitor = UserManager.GetUserById("visitor");

            SetPassCard(Visitor);

            if (string.IsNullOrWhiteSpace(RedirectUrl) == false) { HttpContext.Current.Response.Redirect(RedirectUrl); }
        }


        //發與驗證票
        private static void SetPassCard(User User)
        {
            HttpCookie PassCard = new HttpCookie("MemberShip2_Login_PassCard");
            PassCard.HttpOnly = true;
            PassCard.Value = LeftHand.Gadget.Encoder.AES_Encryption(User.Id);
            //PassCard.Expires = DateTime.Now.AddDays(1); //註解的話關掉視窗就會登出

            HttpContext.Current.Response.Cookies.Add(PassCard);
        }

        //取得驗證票的值
        private static string GetPassCard()
        {
            HttpCookie PassCard = HttpContext.Current.Request.Cookies["MemberShip2_Login_PassCard"];

            if (PassCard == null) { return ""; }

            return LeftHand.Gadget.Encoder.AES_Decryption(PassCard.Value);
        }

    }
}
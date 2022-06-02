using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip2
{
    public static partial class UserManager
    {
        public static string UploadPath = "/Upload/User/";
        public static string PhysicalUploadPath = HttpContext.Current.Server.MapPath(UploadPath);

        private static List<User> _UserCache;

        public static void Initial()
        {
            _UserCache = UserAccessor.SelectAll();
        }

        public static List<User> GetAll()
        {
            return _UserCache.ToList();
        }

        //透過Id取得User
        public static User GetUserById(string Id)
        {
            return _UserCache.FirstOrDefault(u => u.Id == Id);
        }
        public static List<User> GetUserById(List<string> Ids)
        {
            return _UserCache.Where(u => Ids.Contains(u.Id)).ToList();
        }

        //透過Account取得User
        public static User GetUserByAccount(string Account)
        {
            Account = Account.ToLower();

            return _UserCache.FirstOrDefault(u => u.Id == Account);
        }
        public static List<User> GetUserByAccount(List<string> Accounts)
        {
            Accounts.ForEach(a => a.ToLower());

            return _UserCache.Where(u => Accounts.Contains(u.Id)).ToList();
        }

        //透過上線關係取得User
        public static List<User> GetAllParent(User Child)
        {
            List<User> Parents = new List<User>();

            User Parent = Child.Parent;

            if (Parent == null) { return Parents; }

            Parents.Add(Parent);
            Parents.AddRange(GetAllParent(Parent));

            return Parents;
        }
        public static List<User> GetParents(User Child, int Stage)
        {
            List<User> Parents = new List<User>();

            User Parent = Child.Parent;

            if (Stage == 0) { return Parents; }
            if (Parent == null) { return Parents; }

            Parents.Add(Parent);
            Parents.AddRange(GetParents(Parent, Stage -= 1));

            return Parents;
        }

        //透過下線關係取得User
        public static List<User> GetChilds(User Parent)
        {
            return _UserCache.Where(u => u.Parent == Parent).ToList();
        }

        //儲存
        public static void SaveUser(User User)
        {
            SaveUser(new List<User> { User });
        }
        public static void SaveUser(List<User> Users)
        {
            Users.ForEach(u => u.UpdateTime = DateTime.Now);

            UserAccessor.UpdateInsert(Users);

            _UserCache = _UserCache.Union(Users).ToList();
        }

        //刪除
        public static void RemoveUser(User User)
        {
            RemoveUser(new List<User> { User });
        }
        public static void RemoveUser(List<User> Users)
        {
            UserAccessor.Delete(Users);

            _UserCache = _UserCache.Except(Users).ToList();
        }
    }
}
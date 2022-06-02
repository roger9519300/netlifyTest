using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip2
{
    //User
    public abstract class User
    {
        internal string ParentId;
        public User Parent { get { return UserManager.GetUserById(ParentId); } set { ParentId = (value == null) ? "" : value.Id; } }
        public List<User> Childs { get { return UserManager.GetChilds(this); } }

        public string Id { get; private set; }

        string _Account;
        public string Account { get { return _Account; } set { _Account = value.ToLower(); } }
        string _Password;
        public string Password { get { return _Password; } set { _Password = value.ToLower(); } }

        public bool IsFreeze { get; set; }

        public DateTime UpdateTime { get; internal set; }
        public DateTime CreateTime { get; internal set; }

        protected User(string ParentId, string Id, string Account, string Password, bool IsFreeze, DateTime UpdateTime, DateTime CreateTime)
        {
            this.ParentId = ParentId;

            this.Id = Id;

            this.Account = Account;
            this.Password = Password;

            this.IsFreeze = IsFreeze;

            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }

        //Profile
        internal abstract string ProfileToString();
    }

}
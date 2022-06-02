using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip2
{
    public class Visitor : User
    {
        //Profile
        public string Name { get; set; }

        internal Visitor(string ParentId, string Id, string Account, string Password, bool IsFreeze, string ProfileStirng, DateTime UpdateTime, DateTime CreateTime)
            : base(ParentId, Id, Account, Password, false, UpdateTime, CreateTime)
        {
            this.Name = ProfileStirng;
        }

        internal override string ProfileToString()
        {
            return Name;
        }
    }
}
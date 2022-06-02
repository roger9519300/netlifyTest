using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.MemberShip2
{

    public class Member : User
    {
        //Profile
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }

        public Member(User Parent, string Account, string Password, string Name, string Phone, string Address, string Remark)
            : base(Parent.Id, Guid.NewGuid().ToString(), Account, Password, false, DateTime.Now, DateTime.Now)
        {
            this.Name = Name;
            this.Phone = Phone;
            this.Address = Address;
            this.Remark = Remark;
        }

        internal Member(string ParentId, string Id, string Account, string Password, bool IsFreeze, string ProfileStirng, DateTime UpdateTime, DateTime CreateTime)
            : base(ParentId, Id, Account, Password, IsFreeze, UpdateTime, CreateTime)
        {
            string[] Progiles = ProfileStirng.Split('■');
            this.Name = Progiles[0];
            this.Phone = Progiles[1];
            this.Address = Progiles[2];
            this.Remark = Progiles[3];
        }

        internal override string ProfileToString()
        {
            return string.Join("■", new string[] { Name, Phone, Address, Remark });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeftHand.Config;

namespace GoToTaiwan.Booking
{
    public class BookingItem
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }
        public string StartLocation { set; get; }
        public string EndLocation { set; get; }
        public int People { set; get; }
        public string Schedule { set; get; }
        public string Email { set; get; }
        public string Line { set; get; }
        public string WeChat { set; get; }
        public string WhatsApp { set; get; }
        public string Remark { set; get; }
        public DateTime UpdateTime { set; get; }
        public DateTime CreateTime { set; get; }
        
        public BookingItem(string Name, string StartLocation, 
                           string EndLocation, int People, string Schedule, string Email, string Line, 
                           string WeChat, string WhatsApp, string Remark)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = Name;
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.StartLocation = StartLocation;
            this.EndLocation = EndLocation;
            this.People = People;
            this.Schedule = Schedule;
            this.Email = Email;
            this.Line = Line;
            this.WeChat = WeChat;
            this.WhatsApp = WhatsApp;
            this.Remark = Remark;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal BookingItem(string Id, string Name, DateTime StartTime, DateTime EndTime, string StartLocation, 
                             string EndLocation, int People, string Schedule, string Email, string Line, 
                             string WeChat, string WhatsApp, string Remark, DateTime UpdateTime, 
                             DateTime CreateTime)
        {
            this.Id = Id;
            this.Name = Name;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.StartLocation = StartLocation;
            this.EndLocation = EndLocation;
            this.People = People;
            this.Schedule = Schedule;
            this.Email = Email;
            this.Line = Line;
            this.WeChat = WeChat;
            this.WhatsApp = WhatsApp;
            this.Remark = Remark;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }

    }
}
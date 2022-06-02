using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoToTaiwan.Menu
{    
    public class MenuItem
    {
        public string Id { set; get; }
        public string Title { set; get; }
        public string Localization { set; get; }
        public int Sort { set; get; }
        public bool Enable { set; get; }
        public DateTime UpdateTime { set; get; }
        public DateTime CreateTime { set; get; }

        public MenuItem(string Title,string Localization)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Title = Title;
            this.Localization = Localization;
            this.Sort = 0;
            this.Enable = true;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal MenuItem(string Id, string Title, string Localization, int Sort,bool Enable, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.Title = Title;
            this.Localization = Localization;
            this.Sort = Sort;
            this.Enable = Enable;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
        
    }
}
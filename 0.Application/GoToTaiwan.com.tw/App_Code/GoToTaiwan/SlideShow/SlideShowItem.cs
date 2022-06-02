using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoToTaiwan.SlideShow
{
    public class SlideShowItem
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Image { set; get; }
        public string LinkUrl { set; get; }
        public int Sort { set; get; }
        public DateTime UpdateTime { set; get; }
        public DateTime CreateTime { set; get; }

        public SlideShowItem(string Name, string Image, string LinkUrl)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = Name;
            this.Image = Image;
            this.LinkUrl = LinkUrl;
            this.Sort = SlideShowItemManager.GetNewSort();
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal SlideShowItem(string Id, string Name, string Image, string LinkUrl, int Sort, DateTime UpdateTime, DateTime CreateTime)
        {
            this.Id = Id;
            this.Name = Name;
            this.Image = Image;
            this.LinkUrl = LinkUrl;
            this.Sort = Sort;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoToTaiwan.Menu;
using System.Text.RegularExpressions;
using LeftHand.Config;

namespace GoToTaiwan.Article
{
    public class ArticleItem
    {
        public string MenuItemId { set; get; }
        public string Id { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public string SeoTitle { get { return string.Format("{0} - {1}", this.Title, ConfigManager.GetByConfigKey(ConfigKey.SeoTitle)); } }
        public string SeoDescription
        {
            get { return (LeftHand.Gadget.String.RemoveHtmlTag(this.Content)).Substring(0, 100) + " - " + ConfigManager.GetByConfigKey(ConfigKey.SeoTitle); }
        }
        public bool Enable { set; get; }
        public DateTime UpdateTime { set; get; }
        public DateTime CreateTime { set; get; }

        public ArticleItem(string MenuItemId, string Title, string Content)
        {
            this.MenuItemId = MenuItemId;
            this.Id = Guid.NewGuid().ToString();
            this.Title = Title;
            this.Content = Content;
            this.Enable = true;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
        }

        internal ArticleItem(string MenuItemId, string Id, string Title, string Content, bool Enable, DateTime UpdateTime, DateTime CreateTime)
        {
            this.MenuItemId = MenuItemId;
            this.Id = Id;
            this.Title = Title;
            this.Content = Content;
            this.Enable = Enable;
            this.UpdateTime = UpdateTime;
            this.CreateTime = CreateTime;
        }

    }
}
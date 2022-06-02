using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.AdminTab
{
    public class TabItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Selected { get; set; }
        public List<TabItem> Childs { get; set; }

        public TabItem(string Name, string Url)
        {
            this.Name = Name;
            this.Url = Url;
            this.Selected = false;
            this.Childs = new List<TabItem>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeftHand.MemberShip2;

namespace LeftHand.AdminTab
{
    public partial class TabItemManager
    {
        private static Dictionary<Type, List<TabItem>> _TabItemCache;

        public static void Initial()
        {
            CreateAllTabItem();
        }

        private static void CreateAllTabItem()
        {
            _TabItemCache = new Dictionary<Type, List<TabItem>>();

            CreateAdminTabItems(ref _TabItemCache);
            CreateEmployeeTabItems(ref _TabItemCache);
        }

        public static List<TabItem> GetByUser(User User)
        {
            if (_TabItemCache.ContainsKey(User.GetType()) == true)
            {
                return _TabItemCache[User.GetType()];
            }
            else                
            {
                return new List<TabItem>();
            }
        }

        //Admin
        private static void CreateAdminTabItems(ref Dictionary<Type, List<TabItem>> TabItemCatche)
        {
            Type Type = typeof(Admin);
            List<TabItem> TabItems = new List<TabItem>();

            int i = 0;

            //SlideShow
            TabItems.Add(new TabItem("橫幅管理", "/SlideShow/Admin/SlideShow_List.aspx"));
            TabItems[i++].Childs = new List<TabItem>()
                {
                    new TabItem("橫幅列表", "/SlideShow/Admin/SlideShow_List.aspx"),
                };            

            //Article
            TabItems.Add(new TabItem("文章管理", "/Article/Admin/Article_List.aspx"));
            TabItems[i++].Childs = new List<TabItem>()
                {
                    new TabItem("文章列表", "/Article/Admin/Article_List.aspx"),
                };

            //Booking
            TabItems.Add(new TabItem("預約管理", "/Booking/Admin/Booking_List.aspx"));
            TabItems[i++].Childs = new List<TabItem>()
                {
                    new TabItem("預約列表", "/Booking/Admin/Booking_List.aspx"),
                };

            //Menu
            TabItems.Add(new TabItem("選單設定", "/Menu/Admin/Menu_List.aspx"));
            TabItems[i++].Childs = new List<TabItem>()
                {
                    new TabItem("選單列表", "/Menu/Admin/Menu_List.aspx")
                };

            //Config
            TabItems.Add(new TabItem("基本設定", "/Config/Admin/Config_List.aspx"));

            TabItemCatche.Add(Type, TabItems);
        }

        //Employee
        private static void CreateEmployeeTabItems(ref Dictionary<Type, List<TabItem>> TabItemCatche)
        {
            Type Type = typeof(Employee);
            List<TabItem> TabItems = new List<TabItem>();

            int i = 0;

            //Article
            TabItems.Add(new TabItem("人員管理", "/User/Admin/UserList.aspx"));
            TabItems[i++].Childs = new List<TabItem>()
                {
                    new TabItem("人員管理", "/User/Admin/UserList.aspx"),
                    new TabItem("人員管理X", "/User/Admin/UserList_PageA.aspx"),
                    new TabItem("人員管理X", "/User/Admin/UserList_PageB.aspx"),
                    new TabItem("人員管理X", "/User/Admin/UserList_PageC.aspx")
                };

            //Config
            TabItems.Add(new TabItem("系統設定", "/Config/Admin/Config_List.aspx"));
            TabItems[i++].Childs = new List<TabItem>()
                {
                    new TabItem("基本設定", "/Config/Admin/Config_List.aspx"),
                    new TabItem("基本設定A", "/Config/Admin/Config_ListA.aspx"),
                    new TabItem("基本設定B", "/Config/Admin/Config_ListB.aspx")
                };

            TabItemCatche.Add(Type, TabItems);
        }

    }
}
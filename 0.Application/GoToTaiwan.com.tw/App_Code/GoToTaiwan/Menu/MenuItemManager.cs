using LeftHand.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoToTaiwan.Menu
{
    public class MenuItemManager
    {

        private static List<MenuItem> _MenuItemCache;

        public static void Initial()
        {
            _MenuItemCache = MenuItemAccessor.SelectAll()
                .OrderBy(c => c.Sort)
                .ToList();
        }
        public static int GetNewSort()
        {
            return _MenuItemCache.Count > 0 ? _MenuItemCache.Select(c => c.Sort).Max() + 1 : 1;
        }

        public static List<MenuItem> GetAll()
        {
            return _MenuItemCache.ToList();
        }

        public static MenuItem GetById(string Id)
        {
            return _MenuItemCache.Where(c => c.Id == Id).FirstOrDefault();
        }

        public static List<MenuItem> GetByLocalization(Localization Localization)
        {
            return _MenuItemCache
                .Where(c => c.Localization == Localization.ToString())
                .OrderBy(c => c.Sort)
                .ToList();
        }

        public static List<MenuItem> Get(int Startindex, int EndIndex)
        {
            return _MenuItemCache.Skip(Startindex).Take(EndIndex - Startindex).ToList();
        }

        public static void Save(MenuItem MenuItem)
        {
            Save(new List<MenuItem>() { MenuItem });
        }
        public static void Save(List<MenuItem> MenuItems)
        {
            foreach (MenuItem MenuItem in MenuItems)
            {
                MenuItem.UpdateTime = DateTime.Now;
            }

            //更新資料庫
            MenuItemAccessor.UpdateInsert(MenuItems);

            //更新記憶体
            _MenuItemCache = _MenuItemCache
                .Union(MenuItems)
                .OrderByDescending(c => c.Sort)
                .ToList();
        }

        public static void Remove(MenuItem MenuItem)
        {
            Remove(new List<MenuItem>() { MenuItem });
        }
        public static void Remove(List<MenuItem> MenuItems)
        {

            //更新資料庫
            MenuItemAccessor.Delete(MenuItems);

            //更新記憶体
            _MenuItemCache = _MenuItemCache
                .Except(MenuItems)
                .ToList();
        }
    }
}
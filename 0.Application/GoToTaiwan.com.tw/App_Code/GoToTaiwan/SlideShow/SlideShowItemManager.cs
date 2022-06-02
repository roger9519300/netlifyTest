using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoToTaiwan.SlideShow
{
    public class SlideShowItemManager
    {
        //上傳路徑
        public static string UploadPath { get { return "/_Upload/SlideShow/"; } }
        public static string PhysicalUploadPath { get { return HttpContext.Current.Server.MapPath(UploadPath); } }
        
        private static List<SlideShowItem> _SlideShowItemCache;

        public static void Initial()
        {
            _SlideShowItemCache = SlideShowItemAccessor.SelectAll()
                .OrderBy(c => c.Sort)
                .ToList();
        }
        
        public static int GetNewSort()
        {
            return _SlideShowItemCache.Count > 0 ? _SlideShowItemCache.Select(c => c.Sort).Max() + 1 : 1;
        }

        public static List<SlideShowItem> GetAll()
        {
            return _SlideShowItemCache.ToList();
        }

        public static SlideShowItem GetById(string Id)
        {
            return _SlideShowItemCache.Where(c => c.Id == Id).FirstOrDefault();
        }

        public static List<SlideShowItem> Get(int Startindex, int EndIndex)
        {
            return _SlideShowItemCache.Skip(Startindex).Take(EndIndex - Startindex).ToList();
        }

        public static void Save(SlideShowItem SlideShowItem)
        {
            Save(new List<SlideShowItem>() { SlideShowItem });
        }
        public static void Save(List<SlideShowItem> SlideShowItems)
        {
            foreach (SlideShowItem SlideShowItem in SlideShowItems)
            {
                SlideShowItem.UpdateTime = DateTime.Now;
            }

            //更新資料庫
            SlideShowItemAccessor.UpdateInsert(SlideShowItems);

            //更新記憶体
            _SlideShowItemCache = _SlideShowItemCache
                .Union(SlideShowItems)
                .OrderByDescending(c => c.Sort)
                .ToList();
        }
        
        public static void Remove(SlideShowItem SlideShowItem)
        {
            Remove(new List<SlideShowItem>() { SlideShowItem });
        }
        public static void Remove(List<SlideShowItem> SlideShowItems)
        {     
            foreach (SlideShowItem SlideShowItem in SlideShowItems)
            {
                FileManager.DeletePhysicalFile(PhysicalUploadPath, SlideShowItem.Image);
            }
        
                //更新資料庫
                SlideShowItemAccessor.Delete(SlideShowItems);

            //更新記憶体
            _SlideShowItemCache = _SlideShowItemCache
                .Except(SlideShowItems)
                .ToList();
        }
    }
}
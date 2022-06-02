using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace LeftHand.Gadget
{
    public class Debug
    {
        private static string _UploadPath = "/_Log/";
        private static string _UploadPhysicalPath = HttpContext.Current.Server.MapPath(_UploadPath);

        static object LockFlag = new object();
        public static void CreateLog(string Content)
        {
            Content = string.Format("[{0}]\r\n{1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), Content);

            lock (LockFlag)
            {
                string FileName = string.Format("{0}log_{1}.txt", _UploadPhysicalPath, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                using (StreamWriter sw = new StreamWriter(FileName, true))
                {
                    sw.WriteLine(Content);
                    sw.Close();
                }
            }
        }
    }
}
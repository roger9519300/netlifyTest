using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO.Compression;

namespace LeftHand.Gadget
{
    public class File
    {
        //將文字內容轉成檔案後匯出
        public enum TextEncoding { UTF8, Big5 }
        public static void Export(string Content, string FileName, TextEncoding TextEncoding)
        {
            switch (TextEncoding)
            {
                default:
                case TextEncoding.UTF8:
                    HttpContext.Current.Response.Charset = "UTF-8";
                    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.UTF8;
                    HttpContext.Current.Response.BinaryWrite(new byte[] { 0xEF, 0xBB, 0xBF });

                    break;

                case TextEncoding.Big5:
                    HttpContext.Current.Response.Charset = "big5";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding(950);
                    HttpContext.Current.Response.HeaderEncoding = Encoding.GetEncoding(950);
                    break;
            }

            //檔案匯出
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(Content);
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).Replace("+", "%20"));
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        //輸出指定檔案
        public static void Export(string FilePath)
        {
            string FileName = System.IO.Path.GetFileName(FilePath);

            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            HttpContext.Current.Response.TransmitFile(FilePath);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
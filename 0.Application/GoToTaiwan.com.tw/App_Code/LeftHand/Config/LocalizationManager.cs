using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeftHand.Config
{
    public enum Localization { 繁體中文, 简体中文 }
    public class LocalizationManager
    {
        private static Dictionary<Localization, string> Localizations;

        //Localization
        public static Localization GetLocalization()
        {
            HttpCookie LocalizationCookie = new HttpCookie("Localization");
            LocalizationCookie = HttpContext.Current.Request.Cookies.Get("Localization");
            if (LocalizationCookie == null)
            {
                LocalizationCookie = SetLocalization(Localization.繁體中文);
            }

            return (Localization)Enum.Parse(typeof(Localization), HttpUtility.UrlDecode(LocalizationCookie.Value));
        }
        public static HttpCookie SetLocalization(Localization Localization)
        {
            HttpCookie LocalizationCookie = new HttpCookie("Localization");
            LocalizationCookie.Value = HttpUtility.UrlEncode(Localization.ToString());

            HttpContext.Current.Request.Cookies.Add(LocalizationCookie);
            HttpContext.Current.Response.Cookies.Add(LocalizationCookie);

            return LocalizationCookie;
        }
    }
}
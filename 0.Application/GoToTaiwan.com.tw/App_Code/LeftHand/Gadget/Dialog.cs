using System.Collections.Generic;
using System.Web;
using System.Web.UI;


namespace LeftHand.Gadget
{
    public class Dialog
    {
        public static void AlertWithCloseFancybox(string Message)
        {
            Alert(Message);

            Page Page = (Page)HttpContext.Current.Handler;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), System.Guid.NewGuid().ToString(), "window.parent.$.fancybox.close();", true);
        }

        public static void Alert(string Message)
        {
            Message = HttpContext.Current.Server.HtmlEncode(Message).Replace("\r\n", "\\r\\n");

            Page Page = (Page)HttpContext.Current.Handler;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), System.Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
        }

        public static void Alert(List<string> Messages)
        {
            Alert(string.Join("\r\n", Messages));
        }

    }
}
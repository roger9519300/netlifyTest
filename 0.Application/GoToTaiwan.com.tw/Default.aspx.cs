using LeftHand.MemberShip2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using GoToTaiwan.Menu;
using GoToTaiwan.Article;
using LeftHand.Config;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Url.ToString().ToLower().Contains("admin.") == true)
        { Server.Transfer("/DefaultAdmin.aspx"); }
        else
        {
            Localization _Localization = LocalizationManager.GetLocalization();
            MenuItem _Menu = MenuItemManager.GetByLocalization(_Localization).FirstOrDefault();

            if (_Menu == null)
            { Server.Transfer("/DefaultVisitor.aspx"); }
            else
            { Server.Transfer("/Article/Visitor/Article_List.aspx?" + _Menu.Id); }
        }

    }
}
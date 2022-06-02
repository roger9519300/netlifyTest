using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.Config;
using GoToTaiwan.Menu;

public partial class Menu_Admin_Menu_List : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void vMenuItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GoToTaiwan.Menu.MenuItem MenuItem = (GoToTaiwan.Menu.MenuItem)e.Item.DataItem;

            //vTittle
            Literal vTittle = (Literal)e.Item.FindControl("vTittle");
            vTittle.Text = MenuItem.Title.ToString();

            //vLocalization
            Literal vLocalization = (Literal)e.Item.FindControl("vLocalization");
            vLocalization.Text = MenuItem.Localization.ToString();

            //vSort
            Literal vSort = (Literal)e.Item.FindControl("vSort");
            vSort.Text = MenuItem.Sort.ToString();

            //vEnable
            Literal vEnable = (Literal)e.Item.FindControl("vEnable");
            vEnable.Text = (MenuItem.Enable) ? "V" : "X";

            //vUpdateTime
            Literal vUpdateTime = (Literal)e.Item.FindControl("vUpdateTime");
            vUpdateTime.Text = MenuItem.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //vSort
            Literal vCreateTime = (Literal)e.Item.FindControl("vCreateTime");
            vCreateTime.Text = MenuItem.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //vEdit
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", MenuItem.Id);
            vEdit.NavigateUrl = "/Menu/Admin/Menu_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_MenuItemList();
        Render_MenuItemAdd();
    }

    private void Render_MenuItemList()
    {
        this.vMenuItemList.DataSource = MenuItemManager.GetAll();
        this.vMenuItemList.DataBind();
    }

    private void Render_MenuItemAdd()
    {
        Dictionary<string, string> Argument = new Dictionary<string, string>();
        Argument.Add("Mode", "Add");
        Argument.Add("Id", "");
        this.vAdd.NavigateUrl = "/Menu/Admin/Menu_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LeftHand.MemberShip2;
using LeftHand.AdminTab;

public partial class _MasterPage_Base_Admin : System.Web.UI.MasterPage
{
    User _CurrentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentUser = LoginManager.GetCurrentUser();

        if (_CurrentUser.IsFreeze == true) { Response.Redirect("/Default.aspx"); }
    }

    protected void vLogoutButton_Click(object sender, EventArgs e)
    {
        LoginManager.LogOut("/Default.aspx");
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_UserFunction();
        Render_Tabs();
    }

    private void Render_UserFunction()
    {
        if (Page.IsPostBack == true) { return; }

        //名字(密碼修改)
        this.vAccount.Text = _CurrentUser.Account;
    }

    private void Render_Tabs()
    {
        if (Page.IsPostBack == true) { return; }

        List<TabItem> TabItems = TabItemManager.GetByUser(_CurrentUser);
        SelectedCurrentTabs(ref TabItems);
        BindTabs(ref TabItems);
    }

    private void SelectedCurrentTabs(ref List<TabItem> TabItems)
    {
        //將目前對應的Tab項目Selected = true
        string PageUrl = Request.RawUrl;
        foreach (TabItem MainTabItem in TabItems)
        {
            string MainTabUrlPart = MainTabItem.Url.Replace(System.IO.Path.GetFileName(MainTabItem.Url), "");
            MainTabItem.Selected = PageUrl.Contains(MainTabUrlPart);

            foreach (TabItem SubTabItem in MainTabItem.Childs)
            {
                string SubTabUrlPart = SubTabItem.Url.Split('_')[0];
                SubTabItem.Selected = PageUrl.Contains(SubTabUrlPart);
            }

        }

    }
    private void BindTabs(ref List<TabItem> TabItems)
    {
        //MainTabList
        this.vMainTabList.DataSource = TabItems;
        this.vMainTabList.DataBind();
    }
    protected void vMainTabList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TabItem TabItem = (TabItem)e.Item.DataItem;

            //vMainTabItem
            HyperLink vMainTabItem = (HyperLink)e.Item.FindControl("vMainTabItem");
            vMainTabItem.Text = TabItem.Name;
            vMainTabItem.NavigateUrl = TabItem.Url;

            if (TabItem.Selected == false)
            { vMainTabItem.CssClass = "MainTab"; }
            else
            {
                //vMainTabItem
                vMainTabItem.CssClass = "MainTab Selected";

                //SubTabList
                this.vSubTabList.DataSource = TabItem.Childs;
                this.vSubTabList.DataBind();
            }
        }
    }
    protected void vSubTabList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TabItem TabItem = (TabItem)e.Item.DataItem;

            //vSubTabItem
            HyperLink vSubTabItem = (HyperLink)e.Item.FindControl("vSubTabItem");
            vSubTabItem.Text = TabItem.Name;
            vSubTabItem.NavigateUrl = TabItem.Url;
            vSubTabItem.CssClass = (TabItem.Selected) ? "SubTab Selected" : "SubTab";
        }
    }
}
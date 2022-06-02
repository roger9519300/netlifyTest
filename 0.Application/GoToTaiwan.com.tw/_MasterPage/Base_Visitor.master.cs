using LeftHand.MemberShip2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.Config;
using GoToTaiwan.Menu;
using GoToTaiwan.SlideShow;
using GoToTaiwan.Article;

public partial class _MasterPage_Base_Member : System.Web.UI.MasterPage
{
    List<ArticleItem> _ArticleItems;
    Localization _LocalizationSelected { set { ViewState["Localization"] = value.ToString(); } get { return (Localization)Enum.Parse(typeof(Localization), (string)ViewState["Localization"]); } }
    string _SelectedMenuId { set { ViewState["SelectedMenuId"] = value; } get { return (string)ViewState["SelectedMenuId"]; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Page.IsPostBack == false)
        {
            _LocalizationSelected = LocalizationManager.GetLocalization();
            _SelectedMenuId = GetSelectedMenuItemId();
        }
        LocalizationManager.GetLocalization();
        GetSelectedMenuItemId();
    }

    protected void vLocalizationItem_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton vLocalizationItem = (LinkButton)sender;
            Localization Localization = (Localization)Enum.Parse(typeof(Localization), vLocalizationItem.CommandArgument);
            _LocalizationSelected = Localization;

            LocalizationManager.SetLocalization(Localization);

            //切換語系後，內容、MenuItem一併切換至該語系第一順位的MenuItem
            string MenuItemId = MenuItemManager.GetByLocalization(_LocalizationSelected).FirstOrDefault().Id;
            if (MenuItemId != null)
            {
                SetSelectedMenuItemId(MenuItemManager.GetById(MenuItemId));
                switch (ArticleItemManager.GetAmount(MenuItemManager.GetById(MenuItemId)))
                {
                    case 1:
                        Response.Redirect("/Article/Visitor/Article_Description.aspx?" + ArticleItemManager.GetByMenuItemId(MenuItemId).FirstOrDefault().Id);
                        break;
                    default:
                        Response.Redirect("/Article/Visitor/Article_List.aspx?" + MenuItemId);
                        break;
                }
            }
            else
            {
                Response.Redirect("DefaultVisitor.aspx");
            }
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            HyperLink vMenuItem = (HyperLink)sender;
            GoToTaiwan.Menu.MenuItem MenuItem = MenuItemManager.GetById(vMenuItem.ID);

            _SelectedMenuId = (MenuItem == null) ? "" : MenuItem.Id;
            SetSelectedMenuItemId(MenuItem);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vMenuItem_Booking_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton vMenuItem = (LinkButton)sender;
            GoToTaiwan.Menu.MenuItem MenuItem = MenuItemManager.GetById(vMenuItem.CommandArgument);
            if (GetSelectedMenuItemId() != null)
            { SetSelectedMenuItemId(MenuItem); }
            Response.Redirect("/Booking/Visitor/Booking_Board.aspx");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_LocalizationList();
        Render_MenuItemList();
        Render_SlideShowItemList();
        Render_vSEOkeyList();

        Render_vGoogleAnalytics();
    }

    private void Render_LocalizationList()
    {
        this.vLocalizationList.DataSource = Enum.GetNames(typeof(Localization));
        this.vLocalizationList.DataBind();
    }

    protected void vLocalizationList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            String Localization = (String)e.Item.DataItem;

            //vLocalization
            LinkButton vLocalizationItem = (LinkButton)e.Item.FindControl("vLocalizationItem");
            vLocalizationItem.Text = Localization;
            vLocalizationItem.CommandArgument = Localization;
            vLocalizationItem.CssClass = (Localization == _LocalizationSelected.ToString()) ? "LocalizationItem Selected" : "LocalizationItem";
        }
    }

    private void Render_SlideShowItemList()
    {
        this.vSlideShowList.DataSource = SlideShowItemManager.GetAll();
        this.vSlideShowList.DataBind();
    }
    protected void vSlideShowList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            SlideShowItem SlideShowItem = (SlideShowItem)e.Item.DataItem;

            //vSlideShowItem
            HyperLink vSlideShowItem = (HyperLink)e.Item.FindControl("vSlideShowItem");
            vSlideShowItem.NavigateUrl = SlideShowItem.LinkUrl;
            vSlideShowItem.ImageUrl = SlideShowItemManager.UploadPath + SlideShowItem.Image;
        }
    }

    private void Render_MenuItemList()
    {
        this.vMenuItemList.DataSource = MenuItemManager.GetByLocalization(_LocalizationSelected);
        this.vMenuItemList.DataBind();
    }

    protected void vMenuItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GoToTaiwan.Menu.MenuItem MenuItem = (GoToTaiwan.Menu.MenuItem)e.Item.DataItem;

            //vMenuItem
            HyperLink vMenuItem = (HyperLink)e.Item.FindControl("vMenuItem");
            vMenuItem.Text = MenuItem.Title;
            vMenuItem.ID = MenuItem.Id;
            vMenuItem.NavigateUrl = "/Article/Visitor/Article_List.aspx?" + MenuItem.Id;

            vMenuItem.CssClass = "MenuItem";
            if (Request.RawUrl.Contains("Article_List.aspx") == true)
            {
                if (Request.RawUrl.Contains(vMenuItem.NavigateUrl) == true) { vMenuItem.CssClass = "MenuItem Selected"; }
            }
            else if (Request.RawUrl.Contains("Article_Description") == true)
            {
                if ((Request.UrlReferrer != null) && (Request.UrlReferrer.ToString().Contains(vMenuItem.NavigateUrl) == true)) { vMenuItem.CssClass = "MenuItem Selected"; }
            }

        }
    }

    private void Render_vSEOkeyList()
    {
        this.vSEOkeyList.DataSource = MenuItemManager.GetAll();
        this.vSEOkeyList.DataBind();
    }

    protected void vSEOkeyList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GoToTaiwan.Menu.MenuItem MenuItem = (GoToTaiwan.Menu.MenuItem)e.Item.DataItem;

            //vSEOkeyItem
            HyperLink vSEOkeyItem = (HyperLink)e.Item.FindControl("vSEOkeyItem");
            vSEOkeyItem.Text += MenuItemManager.GetById(MenuItem.Id).Title;

            List<ArticleItem> _ArticleItem = ArticleItemManager.GetByMenuItemId(MenuItem.Id);
            foreach (ArticleItem ArticleTitle in _ArticleItem)
            {
                vSEOkeyItem.Text += ArticleTitle.Title;
                vSEOkeyItem.NavigateUrl = "/Article/Visitor/Article_List.aspx?" + MenuItem.Id;
            }
        }
    }

    private void Render_vGoogleAnalytics()
    {
        if (Page.IsPostBack == true) { return; }

        this.vAnalyticsCode.Text = ConfigManager.GetByConfigKey(ConfigKey.AnalyticsCode);
    }

    //Menu
    private string GetSelectedMenuItemId()
    {
        HttpCookie SelectedMenuItemIdCookie = Request.Cookies.Get("SelectedMenuItemId");
        if (SelectedMenuItemIdCookie == null)
        {
            SelectedMenuItemIdCookie = SetSelectedMenuItemId(null);
        }

        return SelectedMenuItemIdCookie.Value;
    }
    private HttpCookie SetSelectedMenuItemId(GoToTaiwan.Menu.MenuItem MenuItem)
    {
        HttpCookie SelectedMenuItemIdCookie = new HttpCookie("SelectedMenuItemId");
        SelectedMenuItemIdCookie.Value = (MenuItem == null) ? "" : MenuItem.Id;

        Request.Cookies.Add(SelectedMenuItemIdCookie);
        Response.Cookies.Add(SelectedMenuItemIdCookie);

        return SelectedMenuItemIdCookie;
    }

}

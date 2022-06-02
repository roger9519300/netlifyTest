using GoToTaiwan.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.Config;
using GoToTaiwan.Menu;

public partial class Article_Admin_Article_List : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }
    ArticleItem _ArticleItem;
    string _LocalizationSelected { get { return ViewState["_LocalizationSelected"].ToString(); } set { ViewState["_LocalizationSelected"] = value; } }
    string _MenuItemSelected { get { return ViewState["_MenuItemSelected"].ToString(); } set { ViewState["_MenuItemSelected"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //第一次進到頁面給初始值
        if (this.IsPostBack == false)
        {
            _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString()));

            _LocalizationSelected = Enum.GetNames(typeof(Localization)).GetValue(0).ToString();
            _MenuItemSelected = "";
        }
        else
        {
            //判斷選項是否存在
            _MenuItemSelected = (MenuItemManager.GetAll().FirstOrDefault() == null) ? "" : MenuItemManager.GetAll().FirstOrDefault().Id;
        }
    }

    protected void vArticle_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ArticleItem ArticleItem = (ArticleItem)e.Item.DataItem;

            //標題
            Literal vTitle = (Literal)e.Item.FindControl("vTitle");
            vTitle.Text = ArticleItem.Title.ToString();

            //內容
            Literal vContent = (Literal)e.Item.FindControl("vContent");
            vContent.Text = (ArticleItem.SeoDescription.Trim().Length > 300) ? ArticleItem.SeoDescription.Trim().Substring(0, 300) + "..." : vContent.Text = ArticleItem.SeoDescription.Trim();

            //顯示
            Literal vEnable = (Literal)e.Item.FindControl("vEnable");
            vEnable.Text = (ArticleItem.Enable) ? "V" : "X";

            //更新時間
            Literal vUpDateTime = (Literal)e.Item.FindControl("vUpDateTime");
            vUpDateTime.Text = ArticleItem.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //建立時間
            Literal vCreateTime = (Literal)e.Item.FindControl("vCreateTime");
            vCreateTime.Text = ArticleItem.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //vEdit
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", ArticleItem.Id);
            vEdit.NavigateUrl = "/Article/Admin/Article_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_LocalizationList();
        Render_MenuItemList();
        Render_Article_List();
        Render_ArticleAdd();
    }

    private void Render_ArticleAdd()
    {
        if (_MenuItemSelected.Equals(""))
        {
            this.vAdd.Visible = false;
        }
        else
        {
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Add");
            Argument.Add("Id", _MenuItemSelected);
            this.vAdd.NavigateUrl = "/Article/Admin/Article_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
            this.vAdd.Visible = true;

        }
    }

    private void Render_Article_List()
    {
        this.vArticle_List.DataSource = ArticleItemManager.GetByMenuItemId(_MenuItemSelected);
        this.vArticle_List.DataBind();
    }

    protected void vMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton vMenuItem = (LinkButton)sender;

            _MenuItemSelected = vMenuItem.CommandArgument;
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vMenuItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            GoToTaiwan.Menu.MenuItem MenuItem = (GoToTaiwan.Menu.MenuItem)e.Item.DataItem;

            //vMenuItem
            LinkButton vMenuItem = (LinkButton)e.Item.FindControl("vMenuItem");
            vMenuItem.Text = MenuItem.Title;
            vMenuItem.CommandArgument = MenuItem.Id;

            if (MenuItem.Id == _MenuItemSelected) { vMenuItem.CssClass += " Selected"; }
        }
    }

    private void Render_MenuItemList()
    {
        this.vMenuItemList.DataSource = MenuItemManager.GetByLocalization((Localization)Enum.Parse(typeof(Localization), _LocalizationSelected));
        this.vMenuItemList.DataBind();
    }

    protected void vLocalizationList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            String LocalizationList = (String)e.Item.DataItem;

            //vMenuItem
            LinkButton vLocalizationItem = (LinkButton)e.Item.FindControl("vLocalizationItem");
            vLocalizationItem.Text = LocalizationList;
            vLocalizationItem.CommandArgument = LocalizationList;

            if (LocalizationList == _LocalizationSelected) { vLocalizationItem.CssClass += " Selected"; }
        }
    }

    private void Render_LocalizationList()
    {
        this.vLocalizationList.DataSource = Enum.GetNames(typeof(Localization));
        this.vLocalizationList.DataBind();
    }

    protected void vLocalizationItem_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton vLocalizationItem = (LinkButton)sender;
            _LocalizationSelected = vLocalizationItem.CommandArgument;
            _MenuItemSelected = "";
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }
}


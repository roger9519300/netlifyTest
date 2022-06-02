using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoToTaiwan.Menu;
using GoToTaiwan.Article;

public partial class Article_Admin_Article_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }
    ArticleItem _ArticleItem;
    List<GoToTaiwan.Menu.MenuItem> _MenuItem;
    
    string _MenuItemSelected { get { return ViewState["_MenuItemSelected"].ToString(); } set { ViewState["_MenuItemSelected"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //第一次進到頁面給初始值
        if (this.IsPostBack == false)
        {
            _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString()));
            _MenuItemSelected = _Argument["Id"];
        }
        else
        {
            //判斷選項是否存在
            _MenuItemSelected = (MenuItemManager.GetAll().FirstOrDefault() == null) ? "" : MenuItemManager.GetAll().FirstOrDefault().Id;
        }

        switch (_Argument["Mode"])
        {
            case "Add":
                _ArticleItem = new ArticleItem(_Argument["Id"], "", "");
                break;
            case "Edit":
                _ArticleItem = ArticleItemManager.GetById(_Argument["Id"]);
                break;
        }

        //MenuItem
        _MenuItem = MenuItemManager.GetAll();
    }



    protected void vMenuItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GoToTaiwan.Menu.MenuItem MenuItem = (GoToTaiwan.Menu.MenuItem)e.Item.DataItem;

            //vSubjectName
            LinkButton vMenuItem = (LinkButton)e.Item.FindControl("vMenuItem");
            vMenuItem.Text = MenuItem.Title;
            vMenuItem.CommandArgument = MenuItem.Id;

            if (MenuItem.Id == _MenuItemSelected) { vMenuItem.CssClass += " Selected"; }
        }
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

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {            
            ArticleItemManager.Delete(_ArticleItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "window.parent.$.fancybox.close();window.parent.__doPostBack('', '');", true);
        }
        catch (Exception Exception)
        {
            LeftHand.Gadget.Dialog.Alert(Exception.Message);
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            //vTitle
            string Title = this.vTitle.Text.Trim();
            if (string.IsNullOrEmpty(Title) == true) { Errors.Add("請輸入標題"); }

            //預約說明
            string Content = this.vContent.Content.Trim();
            if (string.IsNullOrEmpty(Content) == true) { Errors.Add("請輸入內容"); }

            //Enable
            bool Enable = bool.Parse(this.vEnable.SelectedValue);

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _ArticleItem.Title = Title;
            _ArticleItem.Content = Content;
            _ArticleItem.Enable = Enable;
            _ArticleItem.MenuItemId = MenuItemManager.GetById(_ArticleItem.MenuItemId).Id;

            ArticleItemManager.Save(_ArticleItem);

            LeftHand.Gadget.Dialog.AlertWithCloseFancybox("儲存成功");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_Article();        
    }

    private void Render_Article()
    {
        if (this.Page.IsPostBack == true) { return; }

        //標題
        this.vTitle.Text = _ArticleItem.Title;

        //內容
        this.vContent.Width = 600;
        this.vContent.Height = 300;
        this.vContent.Content = _ArticleItem.Content;

        //顯示
        this.vEnable.SelectedValue = _ArticleItem.Enable.ToString();

        try
        {
            //選單        
            this.vMenuItem.Text = MenuItemManager.GetById(_ArticleItem.MenuItemId).Title;
        }
        catch(Exception ex)
        {
            LeftHand.Gadget.Dialog.AlertWithCloseFancybox("請選擇文章對應選單"+ex.Message);
        }


        //刪除
        this.vDelete.OnClientClick = "return confirm('確定刪除此內容');";
        this.vDelete.Visible = (_Argument["Mode"] == "Edit");
    }
}
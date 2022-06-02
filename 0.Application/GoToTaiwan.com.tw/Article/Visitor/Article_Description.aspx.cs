using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoToTaiwan.Menu;
using GoToTaiwan.Article;

public partial class Article_Visitor_Article_Description : System.Web.UI.Page
{
    GoToTaiwan.Menu.MenuItem _MenuItem;
    ArticleItem _ArticleItem;
    protected void Page_Load(object sender, EventArgs e)
    {
        _ArticleItem = ArticleItemManager.GetById(Request.QueryString.ToString());
        _MenuItem = MenuItemManager.GetById(_ArticleItem.MenuItemId);
        this.vMentItemTitle.Text = _MenuItem.Title;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_Article();
        this.Page.Title = _ArticleItem.SeoTitle;
        this.Page.MetaDescription = _ArticleItem.SeoDescription;
    }

    private void Render_Article()
    {
        if (this.Page.IsPostBack == true) { return; }

        this.vTitle.Text = _ArticleItem.Title;
        this.vContent.Text = _ArticleItem.Content;
        this.vBackArticleList.NavigateUrl = "/Article/Visitor/Article_List.aspx?" + _ArticleItem.MenuItemId;
        this.vBackArticleList.Text = "回到 " + _MenuItem.Title + " 文章列表";
        this.vBackArticleList.CssClass = "BackArticleList";
    }
}
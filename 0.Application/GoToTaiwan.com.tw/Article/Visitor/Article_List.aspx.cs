using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoToTaiwan.Article;
using GoToTaiwan.Menu;
using LeftHand.Config;

public partial class Article_Visitor_Article_List : System.Web.UI.Page
{
    GoToTaiwan.Menu.MenuItem _MenuItem;
    List<ArticleItem> _ArticleItems;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //_MenuItem
            _MenuItem = MenuItemManager.GetById(Request.QueryString.ToString());
            if (_MenuItem == null) { Response.Redirect("/"); }

            this.vMentItemTitle.Text = _MenuItem.Title;

            //_ArticleItem
            _ArticleItems = ArticleItemManager.GetByMenuItemId(_MenuItem.Id);
            if (_ArticleItems.Count == 1) { Server.Transfer("/Article/Visitor/Article_Description.aspx?" + _ArticleItems[0].Id); }
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_Article_List();
        Render_Seo();
    }

    private void Render_Seo()
    {
        if (this.Page.IsPostBack == true) { return; }

        string SeoContent = ConfigManager.GetByConfigKey(ConfigKey.SeoTitle);

        this.Page.Title = string.Format("{0} - {1}", _MenuItem.Title, SeoContent);
        this.Page.MetaDescription = string.Format("{0} - {1}", _MenuItem.Title, SeoContent);
    }

    private void Render_Article_List()
    {
        this.Pagger.PageSize = 15;
        this.Pagger.DataAmount = ArticleItemManager.GetByMenuItemId(_MenuItem.Id).Count;

        this.vArticle_List.DataSource = ArticleItemManager.GetByMenuItemId(_MenuItem.Id).Where((a, index) => index >= this.Pagger.DataStartIndex - 1 && index <= this.Pagger.DataEndIndex - 1);
        this.vArticle_List.DataBind();
    }
    protected void vArticle_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ArticleItem ArticleItem = (ArticleItem)e.Item.DataItem;

            if (ArticleItem.Enable)
            {
                //vArticleTitle
                HyperLink vArticleTitle = (HyperLink)e.Item.FindControl("vArticleTitle");
                vArticleTitle.Text = ArticleItem.Title;
                vArticleTitle.NavigateUrl = "/Article/Visitor/Article_Description.aspx?" + ArticleItem.Id;

                //vArticleSeoContent
                Literal vArticleSeoContent = (Literal)e.Item.FindControl("vArticleSeoContent");
                vArticleSeoContent.Text = (ArticleItem.SeoDescription.Trim().Length > 71) ? ArticleItem.SeoDescription.Trim().Substring(0, 70) + "..." : vArticleSeoContent.Text = ArticleItem.SeoDescription.Trim();

                //ReadMore
                HyperLink vReadMore = (HyperLink)e.Item.FindControl("vReadMore");
                vReadMore.NavigateUrl = "/Article/Visitor/Article_Description.aspx?" + ArticleItem.Id;
            }
        }
    }
}
﻿using GoToTaiwan.SlideShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SlideShow_Admin_SlideShow_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void vSlideShowList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            SlideShowItem SlideShowItem = (SlideShowItem)e.Item.DataItem;

            //名稱
            Literal vName = (Literal)e.Item.FindControl("vName");
            vName.Text = SlideShowItem.Name.ToString();

            //圖片
            Image vImage = (Image)e.Item.FindControl("vImage");
            vImage.ImageUrl = SlideShowItemManager.UploadPath + SlideShowItem.Image;

            //排序
            Literal vSort = (Literal)e.Item.FindControl("vSort");
            vSort.Text = SlideShowItem.Sort.ToString();

            //更新時間
            Literal vUpdateTime = (Literal)e.Item.FindControl("vUpdateTime");
            vUpdateTime.Text = SlideShowItem.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //建立時間
            Literal vCreateTime = (Literal)e.Item.FindControl("vCreateTime");
            vCreateTime.Text = SlideShowItem.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //編輯
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", SlideShowItem.Id.ToString());
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            vEdit.NavigateUrl = "/SlideShow/Admin/SlideShow_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_SlideShowList();
        Render_SlideShowAdd();
    }

    private void Render_SlideShowList()
    {
        this.vSlideShowList.DataSource = SlideShowItemManager.GetAll();
        this.vSlideShowList.DataBind();
    }

    private void Render_SlideShowAdd()
    {
        Dictionary<string, string> Argument = new Dictionary<string, string>();
        Argument.Add("Mode", "Add");
        Argument.Add("Id", "");
        this.vAdd.NavigateUrl = "/SlideShow/Admin/SlideShow_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
    }
}
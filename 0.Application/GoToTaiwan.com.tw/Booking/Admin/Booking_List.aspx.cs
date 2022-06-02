using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoToTaiwan.Booking;

public partial class Booking_Admin_Booking_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void vBooking_List_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            BookingItem BookingItem = (BookingItem)e.Item.DataItem;

            //姓名
            Literal vName = (Literal)e.Item.FindControl("vName");
            vName.Text = BookingItem.Name.ToString();

            //開始包車日期
            Literal vStartTime = (Literal)e.Item.FindControl("vStartTime");
            vStartTime.Text = BookingItem.StartTime.ToString("yyyy-MM-dd HH:mm:ss");

            //開始包車地點
            Literal vStartLocation = (Literal)e.Item.FindControl("vStartLocation");
            vStartLocation.Text = BookingItem.StartLocation.ToString();

            //結束包車日期
            Literal vEndTime = (Literal)e.Item.FindControl("vEndTime");
            vEndTime.Text = BookingItem.StartTime.ToString("yyyy-MM-dd HH:mm:ss");

            //結束包車地點
            Literal vEndLocation = (Literal)e.Item.FindControl("vEndLocation");
            vEndLocation.Text = BookingItem.StartLocation.ToString();

            //人數
            Literal vPeople = (Literal)e.Item.FindControl("vPeople");
            vPeople.Text = BookingItem.People.ToString();

            //建立時間
            Literal vCreateTime = (Literal)e.Item.FindControl("vCreateTime");
            vCreateTime.Text = BookingItem.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //編輯
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", BookingItem.Id.ToString());
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            vEdit.NavigateUrl = "/Booking/Admin/Booking_Modify.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_BookingList();
    }

    private void Render_BookingList()
    {
        this.vBooking_List.DataSource = BookingItemManager.GetAll();
        this.vBooking_List.DataBind();
    }

}
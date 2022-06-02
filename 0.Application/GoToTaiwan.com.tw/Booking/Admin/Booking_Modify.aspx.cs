using System;
using GoToTaiwan.Booking;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Booking_Admin_Booking_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    BookingItem _BooingItem;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString()));
        }

        switch (_Argument["Mode"])
        {
            case "Edit":
                _BooingItem = BookingItemManager.GetById(_Argument["Id"]);
                break;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_Booking();
    }

    private void Render_Booking()
    {
        if (this.Page.IsPostBack == true) { return; }

        //姓名
        this.vName.Text = _BooingItem.Name;

        //聯絡方式
        this.vMail.Text = _BooingItem.Email;
        this.vLine.Text = _BooingItem.Line;
        this.vWeChat.Text = _BooingItem.WeChat;
        this.vWhatApp.Text = _BooingItem.WhatsApp;

        //包車開始日期
        this.vStartTime.Text = _BooingItem.StartTime.ToShortDateString();

        //包車開始地點
        this.vStartLocation.Text = _BooingItem.StartLocation;

        //包車結束日期
        this.vEndTime.Text = _BooingItem.EndTime.ToShortDateString();

        //包車結束地點
        this.vEndLocation.Text = _BooingItem.EndLocation;

        //旅客人數
        this.vPeople.Text = _BooingItem.People.ToString();
        
        //行程內容
        this.vSchedule.Text = _BooingItem.Schedule;

        //備註
        this.vRemark.Text = _BooingItem.Remark;


        //刪除
        this.vDelete.OnClientClick = "return confirm('確定刪除此內容');";
        this.vDelete.Visible = (_Argument["Mode"] == "Edit");
    }

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {

            BookingItemManager.Remove(_BooingItem);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "window.parent.$.fancybox.close();window.parent.__doPostBack('', '');", true);
        }
        catch (Exception Exception)
        {
            LeftHand.Gadget.Dialog.Alert(Exception.Message);
        }
    }
}
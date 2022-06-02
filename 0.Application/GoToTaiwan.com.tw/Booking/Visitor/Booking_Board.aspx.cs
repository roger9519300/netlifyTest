using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoToTaiwan.Booking;
using LeftHand.Config;

public partial class Booking_Visitor_Booking_Board : System.Web.UI.Page
{
    BookingItem _BookingItem;
    protected void Page_Load(object sender, EventArgs e)
    {
        _BookingItem = new BookingItem("", "", "", 0, "", "", "", "", "", "");
    }

    protected void vSendButton_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();
            DateTime TryParseStartTime;
            DateTime TryParseEndTime;

            //vName
            string Name = this.vName.Text.Trim();
            if (string.IsNullOrEmpty(Name) == true) { Errors.Add("請輸入姓名"); }

            //vEmail
            string Email = this.vEmail.Text.Trim();
            if (string.IsNullOrEmpty(Email) == true) { Errors.Add("請輸入電子信箱"); }

            //vStartTime
            string StartTime = this.vStartTime.Text.Trim();
            if (string.IsNullOrEmpty(StartTime) == true) { Errors.Add("請輸入開始包車日期"); }
            else
            {
                if (DateTime.TryParse(this.vStartTime.Text.Trim(), out TryParseStartTime) == false)
                { Errors.Add("開始包車日期格式錯誤"); }
            }

            //vEndTime
            string EndTime = this.vEndTime.Text.Trim();
            if (string.IsNullOrEmpty(EndTime) == true) { Errors.Add("請輸入結束包車日期"); }
            else
            {
                if (DateTime.TryParse(this.vEndTime.Text.Trim(), out TryParseEndTime) == false)
                { Errors.Add("結束包車日期格式錯誤"); }
            }

            //vStartLocation
            string StartLocation = this.vStartLocation.Text.Trim();
            if (string.IsNullOrEmpty(StartLocation) == true) { Errors.Add("請輸入包車出發地點"); }

            //vEndLocation
            string EndLocation = this.vEndLocation.Text.Trim();
            if (string.IsNullOrEmpty(EndLocation) == true) { Errors.Add("請輸入包車結束地點"); }

            //vPeople
            int People;
            if (int.TryParse(this.vPeople.Text.Trim(), out People) == false) { Errors.Add("包車旅客人數格式錯誤"); }

            //vSchedule
            string Schedule = this.vSchedule.Text.Trim();
            if (string.IsNullOrEmpty(Schedule) == true) { Errors.Add("請輸入行程內容"); }



            string Line = this.vLine.Text.Trim();
            string WeChat = this.vWeChat.Text.Trim();
            string WhatApp = this.vWhatApp.Text.Trim();
            string Remark = this.vRemark.Text.Trim();

            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _BookingItem.Name = Name;
            _BookingItem.Email = Email;
            _BookingItem.StartTime = DateTime.Parse(StartTime);
            _BookingItem.EndTime = DateTime.Parse(EndTime);
            _BookingItem.StartLocation = StartLocation;
            _BookingItem.EndLocation = EndLocation;
            _BookingItem.People = People;
            _BookingItem.Schedule = Schedule;
            _BookingItem.Line = Line;
            _BookingItem.WeChat = WeChat;
            _BookingItem.Remark = Remark;

            BookingItemManager.Save(_BookingItem);

            LeftHand.Gadget.Dialog.Alert("儲存成功");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_Seo();
    }

    private void Render_Seo()
    {
        if (this.Page.IsPostBack == true) { return; }

        string SeoContent = ConfigManager.GetByConfigKey(ConfigKey.SeoTitle);

        this.Page.Title = "客戶留言 - " + SeoContent;
        this.Page.MetaDescription = SeoContent;
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.MemberShip2;

public partial class User_Admin_UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void vUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            User user = (User)e.Item.DataItem;

            //名稱
            Literal vAccount = (Literal)e.Item.FindControl("vAccount");
            vAccount.Text = user.Account.ToString();


            //更新時間
            Literal vUpdateTime = (Literal)e.Item.FindControl("vUpdateTime");
            vUpdateTime.Text = user.UpdateTime.ToString();

            //建立時間
            Literal vCreateTime = (Literal)e.Item.FindControl("vCreateTime");
            vCreateTime.Text = user.CreateTime.ToString();

            //編輯
            Dictionary<string, string> Argument = new Dictionary<string, string>();
            Argument.Add("Mode", "Edit");
            Argument.Add("Id", user.Id.ToString());
            HyperLink vEdit = (HyperLink)e.Item.FindControl("vEdit");
            vEdit.NavigateUrl = "/User/Admin/ChangePassword.aspx?" + Server.UrlEncode(LeftHand.Gadget.Encoder.DictionaryEncoder(Argument));
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_UserList();
    }

    private void Render_UserList()
    {
        this.vUserList.DataSource = UserManager.GetAll();
        this.vUserList.DataBind();
    }
}
using LeftHand.MemberShip2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _ManagerLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void vLoginButton_Click(object sender, EventArgs e)
    {
        try
        {
            string Account = this.vAccount.Text;
            string Password = this.vPassword.Text;

            LoginManager.Login(Account, Password, "/Config/Admin/Config_List.aspx");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Page.Title = "管理者登入";
    }
}
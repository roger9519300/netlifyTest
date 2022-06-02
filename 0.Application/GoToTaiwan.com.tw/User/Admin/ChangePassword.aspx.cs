using LeftHand.MemberShip2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Admin_ChangePassword : System.Web.UI.Page
{
    User _CurrentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        _CurrentUser = LoginManager.GetCurrentUser();
    }

    protected void vSaveBtn_Click(object sender, EventArgs e)
    {
        try
        {
            string Password = this.vPassword.Text.Trim();
            string PasswordConfirm = this.vPasswordConfirm.Text.Trim();

            if (Password != PasswordConfirm) { throw new Exception("密碼與確認密碼不相同"); }

            if (Password == "") { throw new Exception("密碼不可空白"); }

            _CurrentUser.Password = Password;

            UserManager.SaveUser(_CurrentUser);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "alert('儲存成功');window.parent.$.fancybox.close();", true);
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.MemberShip2;

public partial class _MasterPage_Base_Visitor_Member : System.Web.UI.MasterPage
{
    Member _CurrentMember;

    protected void Page_Load(object sender, EventArgs e)
    {
        User User = LoginManager.GetCurrentUser();
        if (User.IsFreeze == true) { Response.Redirect("/Default.aspx"); }
        if (User is Member == false) { Response.Redirect("/Default.aspx"); }

        _CurrentMember = (Member)User;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

    }
}

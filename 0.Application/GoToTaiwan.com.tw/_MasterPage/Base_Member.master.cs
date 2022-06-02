using LeftHand.MemberShip2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _MasterPage_Base_Member : System.Web.UI.MasterPage
{
    Member _CurrentMember;

    protected void Page_Load(object sender, EventArgs e)
    {
        User User = LoginManager.GetCurrentUser();

        _CurrentMember = (Member)User;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

    }

}

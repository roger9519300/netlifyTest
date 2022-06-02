using LeftHand.Gadget;
using LeftHand.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Transactions;

public partial class WeChat_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.vQrCode.ImageUrl = ConfigManager.UploadPath + ConfigManager.GetByConfigKey(ConfigKey.微信QrCode) + "?v" + DateTime.Now.ToString("yyyyMMddHHmmss");
        this.vQrCode.Visible = (string.IsNullOrWhiteSpace(ConfigManager.GetByConfigKey(ConfigKey.微信QrCode)) == false);
    }
}
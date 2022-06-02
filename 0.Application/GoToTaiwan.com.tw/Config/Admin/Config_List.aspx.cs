using LeftHand.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;


public partial class Config_Manager_Config : System.Web.UI.Page
{
    Dictionary<ConfigKey, string> _Configs;

    List<TextBox> Description1_10 = new List<TextBox>();

    protected void Page_Load(object sender, EventArgs e)
    {
        _Configs = ConfigManager.GetAll();
    }

    protected void vSaveBtn_Click(object sender, EventArgs e)
    {
        try
        {
            //錯誤集合
            List<string> Errors = new List<string>();

            //SeoTitle 
            string SeoTitle = this.vSeoTitle.Text.Trim();

            //流量分析代碼
            string AnalyticsCode = this.vAnalyticsCode.Text.Trim();

            //QrCode
            string QrCodeName = _Configs[ConfigKey.微信QrCode];
            if (this.vQrCodeUpload.HasFile == true)
            {
                QrCodeName = "WeChat_QrCode.jpg";
                Bitmap Bitmap = LeftHand.Gadget.Graphics.ResizeByScope(new Bitmap(this.vQrCodeUpload.PostedFile.InputStream), 220, 220, LeftHand.Gadget.Graphics.ScopeMode.InScope);
                Bitmap.Save(ConfigManager.PhysicalUploadPath + QrCodeName, ImageFormat.Jpeg);
            }

            if (Errors.Count > 0) { throw new Exception(string.Join("\r\n", Errors)); }

            _Configs[ConfigKey.SeoTitle] = SeoTitle;
            _Configs[ConfigKey.AnalyticsCode] = AnalyticsCode;
            _Configs[ConfigKey.微信QrCode] = QrCodeName;

            ConfigManager.Save(_Configs);

            LeftHand.Gadget.Dialog.Alert("儲存成功");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_Configs();
    }

    private void Render_Configs()
    {
        //QrCode
        this.vQrCode.ImageUrl = ConfigManager.UploadPath + _Configs[ConfigKey.微信QrCode] + "?v" + DateTime.Now.ToString("yyyyMMddHHmmss");
        this.vQrCode.Visible = (string.IsNullOrWhiteSpace(_Configs[ConfigKey.微信QrCode]) == false);

        if (this.Page.IsPostBack == true) { return; }

        //vSeoTitle
        this.vSeoTitle.Text = _Configs[ConfigKey.SeoTitle];

        //流量分析代碼
        this.vAnalyticsCode.Text = _Configs[ConfigKey.AnalyticsCode];

    }
}
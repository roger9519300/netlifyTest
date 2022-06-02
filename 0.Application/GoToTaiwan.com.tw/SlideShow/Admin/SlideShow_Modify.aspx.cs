using GoToTaiwan.SlideShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SlideShow_Admin_SlideShow_Modify : System.Web.UI.Page
{
    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    SlideShowItem _SlideShow;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.IsPostBack == false)
        {
            _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString()));
        }

        switch (_Argument["Mode"])
        {
            case "Add":
                _SlideShow = new SlideShowItem("","","");
                break;
            case "Edit":
                _SlideShow = SlideShowItemManager.GetById(_Argument["Id"]);
                break;
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            //vName
            string Name = this.vName.Text.Trim();
            if (string.IsNullOrEmpty(Name) == true) { Errors.Add("請輸入橫幅名稱"); }
            int Width = 1920;
            int Height = 480;

            //vImage
            string ImageName = (string.IsNullOrEmpty(_SlideShow.Image) == true) ? "" : _SlideShow.Image;
            if (string.IsNullOrEmpty(ImageName) == true && this.vFileUpload.HasFile == false) { Errors.Add("請上傳橫幅圖"); }
            if (this.vFileUpload.HasFile == true)
            {
                ImageName = _SlideShow.Id + ".jpg";
                Bitmap UploadBitmap = new System.Drawing.Bitmap(this.vFileUpload.PostedFile.InputStream);
                Bitmap Bitmap = LeftHand.Gadget.Graphics.ResizeByScope(UploadBitmap, Width, Height, LeftHand.Gadget.Graphics.ScopeMode.OutScope);
                LeftHand.Gadget.Graphics.SaveToJpg(Bitmap, SlideShowItemManager.PhysicalUploadPath + ImageName);
            }

            //vLinkUrl
            string LinkUrl = this.vLinkUrl.Text.Trim();
            if (string.IsNullOrEmpty(LinkUrl) == true) { Errors.Add("請輸入連結"); }

            //vSort
            int Sort;
            if (int.TryParse(this.vSort.Text.Trim(), out Sort) == false) { Errors.Add("排序格式錯誤"); }


            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _SlideShow.Name = Name;
            _SlideShow.Image = ImageName;
            _SlideShow.LinkUrl = LinkUrl;
            _SlideShow.Sort = Sort;

            SlideShowItemManager.Save(_SlideShow);

            LeftHand.Gadget.Dialog.AlertWithCloseFancybox("儲存成功");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SlideShowItemManager.Remove(_SlideShow);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "window.parent.$.fancybox.close();window.parent.__doPostBack('', '');", true);
        }
        catch (Exception Exception)
        {
            LeftHand.Gadget.Dialog.Alert(Exception.Message);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_SlideShow();
    }

    private void Render_SlideShow()
    {
        if (this.Page.IsPostBack == true) { return; }

        //名稱
        this.vName.Text = _SlideShow.Name;

        //圖片
        this.vImage.ImageUrl = SlideShowItemManager.UploadPath + _SlideShow.Image;
        this.vImage.Visible = (_Argument["Mode"] == "Edit");

        //連結網址
        this.vLinkUrl.Text = _SlideShow.LinkUrl;

        //排序
        this.vSort.Text = _SlideShow.Sort.ToString();

        //刪除
        this.vDelete.OnClientClick = "return confirm('確定刪除此內容');";
        this.vDelete.Visible = (_Argument["Mode"] == "Edit");
    }
}
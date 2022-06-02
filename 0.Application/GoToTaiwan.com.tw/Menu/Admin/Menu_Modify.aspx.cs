using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LeftHand.Config;
using GoToTaiwan.Menu;

public partial class Menu_Admin_Menu_Modify : System.Web.UI.Page
{

    Dictionary<string, string> _Argument { get { return (Dictionary<string, string>)ViewState["_Argument"]; } set { ViewState["_Argument"] = value; } }

    GoToTaiwan.Menu.MenuItem _MenuItem;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            _Argument = LeftHand.Gadget.Encoder.DictionaryDecoder(Server.UrlDecode(Request.QueryString.ToString()));
        }

        switch (_Argument["Mode"])
        {
            case "Add":
                _MenuItem = new GoToTaiwan.Menu.MenuItem("", "");
                break;
            case "Edit":
                _MenuItem = MenuItemManager.GetById(_Argument["Id"]);
                break;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Render_LocalizationFilter();
        Render_Menu();
    }

    private void Render_Menu()
    {
        if (this.Page.IsPostBack == true) { return; }

        //名稱
        this.vTitle.Text = _MenuItem.Title;

        //語系
        this.vLocalizationFilter.SelectedValue = _MenuItem.Localization;

        //排序
        this.vSort.Text = _MenuItem.Sort.ToString();

        //顯示
        this.vEnable.SelectedValue = _MenuItem.Enable.ToString();

        //刪除
        this.vDelete.OnClientClick = "return confirm('確定刪除此內容');";
        this.vDelete.Visible = (_Argument["Mode"] == "Edit");
    }

    protected void vDelete_Click(object sender, EventArgs e)
    {
        try
        {
            MenuItemManager.Remove(_MenuItem);
            
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Message", "window.parent.$.fancybox.close();window.parent.__doPostBack('', '');", true);
        }
        catch (Exception Exception)
        {
            LeftHand.Gadget.Dialog.Alert(Exception.Message);
        }
    }

    protected void vSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> Errors = new List<string>();

            //vTitle
            string Title = this.vTitle.Text.Trim();
            if (string.IsNullOrEmpty(Title) == true) { Errors.Add("請輸入顯示名稱"); }

            //vLocalization
            string Localization = this.vLocalizationFilter.SelectedValue.Trim();

            //vSort
            int Sort;
            if (int.TryParse(this.vSort.Text.Trim(), out Sort) == false) { Errors.Add("排序格式錯誤"); }

            //Enable
            bool Enable = bool.Parse(this.vEnable.SelectedValue);


            if (Errors.Count > 0) { throw new Exception(string.Join("\\r\\n", Errors)); }

            _MenuItem.Title = Title;
            _MenuItem.Localization = Localization;
            _MenuItem.Sort = Sort;
            _MenuItem.Enable = Enable;

            MenuItemManager.Save(_MenuItem);

            LeftHand.Gadget.Dialog.AlertWithCloseFancybox("儲存成功");
        }
        catch (Exception ex)
        {
            LeftHand.Gadget.Dialog.Alert(ex.Message);
        }
    }

    private void Render_LocalizationFilter()
    {
        if (Page.IsPostBack == false)
        {
            foreach (string LocalizationItem in Enum.GetNames(typeof(Localization)))
            {
                this.vLocalizationFilter.Items.Add(new ListItem(LocalizationItem, LocalizationItem));
            }
            this.vLocalizationFilter.SelectedValue = Localization.繁體中文.ToString();
        }
    }
}
<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        //PreSet(這個順序是有意義的，沒事別亂動)
        //LeftHand.Config.ConfigManager.PreSet();
        //LeftHand.MemberShip2.UserManager.PreSet();
        //LeftHand.AdminTab.TabItemManager.PreSet();

        //Normal        
        LeftHand.Config.ConfigManager.Initial();
        LeftHand.MemberShip2.UserManager.Initial();
        LeftHand.AdminTab.TabItemManager.Initial();

        //GoToTaiwan
        GoToTaiwan.SlideShow.SlideShowItemManager.Initial();
        GoToTaiwan.Menu.MenuItemManager.Initial();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  應用程式關閉時執行的程式碼
    }

    void Application_Error(object sender, EventArgs e)
    {
        // 發生未處理錯誤時執行的程式碼
    }

    void Session_Start(object sender, EventArgs e)
    {
        // 啟動新工作階段時執行的程式碼
    }

    void Session_End(object sender, EventArgs e)
    {
        // 工作階段結束時執行的程式碼。 
        // 注意: 只有在 Web.config 檔將 sessionstate 模式設定為 InProc 時，
        // 才會引發 Session_End 事件。如果將工作階段模式設定為 StateServer 
        // 或 SQLServer，就不會引發這個事件。
    }

</script>

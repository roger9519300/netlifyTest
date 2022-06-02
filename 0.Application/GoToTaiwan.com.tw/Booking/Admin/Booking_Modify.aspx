<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="Booking_Modify.aspx.cs" Inherits="Booking_Admin_Booking_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/Booking/Admin/_Css/Booking_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <div id="Booking_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 140px;" />
                <col style="width: auto;" />
            </colgroup>
            <tr>
                <th>姓名</th>
                <td>
                    <asp:Literal ID="vName" runat="server" Text="Name"></asp:Literal>
                </td>
            </tr>    
            <tr>
                <th>聯絡方式</th>
                <td>
                    E-Mail：<asp:Literal ID="vMail" runat="server" Text="Mail"></asp:Literal><br />
                    LINE：<asp:Literal ID="vLine" runat="server" Text="Line"></asp:Literal><br />
                    WeChat：<asp:Literal ID="vWeChat" runat="server" Text="WeChat"></asp:Literal><br />
                    WhatApp：<asp:Literal ID="vWhatApp" runat="server" Text="WhatApp"></asp:Literal>
                </td>
            </tr> 
            <tr>
                <th>包車開始日期地點</th>
                <td>
                    日期：<asp:Literal ID="vStartTime" runat="server" Text="StartTime"></asp:Literal><br />
                    地點：<asp:Literal ID="vStartLocation" runat="server" Text="StartLocation"></asp:Literal>
                </td>
            </tr> 
            <tr>
                <th>包車結束日期地點</th>
                <td>
                    日期：<asp:Literal ID="vEndTime" runat="server" Text="EndTime"></asp:Literal><br />
                    地點：<asp:Literal ID="vEndLocation" runat="server" Text="EndLocation"></asp:Literal>
                </td>
            </tr> 
            <tr>
                <th>旅客人數</th>
                <td>
                    <asp:Literal ID="vPeople" runat="server" Text="People"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>行程內容</th>
                <td>
                    <asp:Literal ID="vSchedule" runat="server" Text="Schedule" Mode="PassThrough"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>備註</th>
                <td>
                    <asp:Literal ID="vRemark" runat="server" Text="Remark"></asp:Literal>
                </td>
            </tr>
            <tfoot>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="vDelete" CssClass="Button2 Red" runat="server" OnClick="vDelete_Click">刪除</asp:LinkButton>
                        <div style="clear: both; height: 1px;"></div>
                    </td>
                </tr>
            </tfoot>
        </table>
</div>
</asp:Content>


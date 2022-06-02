<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Admin.master" AutoEventWireup="true" CodeFile="Booking_List.aspx.cs" Inherits="Booking_Admin_Booking_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/Booking/Admin/_Css/Booking_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="Booking_List">
        <table class="Table1">
            <colgroup>
                <col style="width: 170px;" />
                <col style="width: 200px;" />
                <col style="width: 200px;" />
                <col style="width: auto;" />
                <col style="width: 170px;" />
                <col style="width: 100px;" />                
            </colgroup>
            <thead>
                <tr>
                    <th>姓名</th>
                    <th>開始包車日期/地點</th>
                    <th>結束包車日期/地點</th>
                    <th>人數</th>
                    <th>建立時間</th>                    
                    <th>訂單內容</th> 
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vBooking_List" runat="server" OnItemDataBound="vBooking_List_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td  style="text-align: center;">
                                <asp:Literal ID="vName" runat="server" Text="Name"></asp:Literal>
                            </td>
                            <td  style="text-align: center;">
                                <asp:Literal ID="vStartTime" runat="server" Text="StartTime"></asp:Literal><br />
                                <asp:Literal ID="vStartLocation" runat="server" Text="StartLocation"></asp:Literal>
                            </td>
                            <td  style="text-align: center;">
                                <asp:Literal ID="vEndTime" runat="server" Text="EndTime"></asp:Literal><br />
                                <asp:Literal ID="vEndLocation" runat="server" Text="EndLocation"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vPeople" runat="server"  Text="People"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vCreateTime" runat="server" Text="CreateTime"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" CssClass="Button1 Edit fancybox" runat="server" NavigateUrl="/Booking/Admin/Booking_Modify.aspx"></asp:HyperLink>
                            </td>                            
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>


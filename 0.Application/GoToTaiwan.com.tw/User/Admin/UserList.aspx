<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Admin.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="User_Admin_UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/User/Admin/_CSS/UserList.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="UserList">
        <table class="Table1">
            <colgroup>
                <col style="width: auto;" />
                <col style="width: 150px;" />
                <col style="width: 150px;" />
                <col style="width: 100px;" />                
            </colgroup>
            <thead>
                <tr>
                    <th>帳號</th>
                    <th>更新時間</th>
                    <th>建立時間</th>                    
                    <th>更改密碼</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vUserList" runat="server" OnItemDataBound="vUserList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td  style="text-align: center;">
                                <asp:Literal ID="vAccount" runat="server" Text="Account"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vUpdateTime" runat="server" Text="UpdateTime"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vCreateTime" runat="server" Text="CreateTime"></asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" CssClass="Button1 Edit fancybox" runat="server" NavigateUrl="/SlideShow/Admin/SlideShow_Modify.aspx"></asp:HyperLink>
                            </td>                            
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>


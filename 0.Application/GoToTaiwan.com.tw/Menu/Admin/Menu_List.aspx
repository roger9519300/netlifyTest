<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Admin.master" AutoEventWireup="true" CodeFile="Menu_List.aspx.cs" Inherits="Menu_Admin_Menu_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Menu/Admin/_Css/Menu_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Menu_List">
        <div>
            <h2>選單列表</h2>
        </div>
        <table class="Table1">
            <colgroup>
                <col style="width: 100px;" />
                <col style="width: auto;" />
                <col style="width: 70px;" />
                <col style="width: 70px;" />
                <col style="width: 120px;" />
                <col style="width: 120px;" />
                <col style="width: 70px;" />
            </colgroup>
            <thead>
                <tr>
                    <th>語系</th>
                    <th>顯示名稱</th>
                    <th>排序</th>
                    <th>顯示</th>
                    <th>更新時間</th>
                    <th>建立時間</th>
                    <th>
                        <asp:HyperLink ID="vAdd" NavigateUrl="/Menu/Admin/Menu_Modify.aspx" CssClass="Button1 Add fancybox" runat="server"></asp:HyperLink>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vMenuItemList" OnItemDataBound="vMenuItemList_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="vLocalization" runat="server">Localization</asp:Literal><br />
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vTittle" runat="server">Tittle</asp:Literal><br />
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vSort" runat="server">Sort</asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vEnable" runat="server">Enable</asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vUpdateTime" runat="server">UpdateTime</asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vCreateTime" runat="server">CreateTime</asp:Literal>
                            </td>
                            <td style="text-align: center;">
                                <asp:HyperLink ID="vEdit" runat="server" NavigateUrl="/User/Manager/Menu_Modify.aspx" CssClass="Button1 Edit fancybox" Text=" "></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>


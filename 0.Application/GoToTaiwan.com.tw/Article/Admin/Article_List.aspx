<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Admin.master" AutoEventWireup="true" CodeFile="Article_List.aspx.cs" Inherits="Article_Admin_Article_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Article/Admin/_Css/Article_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Article_List">
        <div class="Section">
            <div class="TitleBox">
                <h2 class="Title3">語系列表</h2>
            </div>
            <div class="Localization_List  ItemBox_Wrapper">
                <asp:Repeater ID="vLocalizationList" OnItemDataBound="vLocalizationList_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="ItemBox">
                            <asp:LinkButton ID="vLocalizationItem" CssClass="SelectItem" OnClick="vLocalizationItem_Click" runat="server">語系</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div style="clear: both; height: 1px;"></div>
            </div>
            <div class="TitleBox">
                <h2 class="Title3">選單列表</h2>
            </div>
            <div class="MenuItem_List  ItemBox_Wrapper">
                <asp:Repeater ID="vMenuItemList" OnItemDataBound="vMenuItemList_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="ItemBox">
                            <asp:LinkButton ID="vMenuItem" CssClass="SelectItem" runat="server" OnClick="vMenuItem_Click">選單</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="TitleBox">
            <h2 class="Title3">文章內容</h2>
        </div>
        <div id="ArticleItem_List">
            <table class="Table1">
                <colgroup>
                    <col style="width: 200px;" />
                    <col style="width: auto;" />
                    <col style="width: 70px;" />
                    <col style="width: 100px;" />
                    <col style="width: 100px;" />
                    <col style="width: 50px;" />
                </colgroup>
                <thead>
                    <tr>
                        <th>標題</th>
                        <th>內容</th>
                        <th>顯示</th>
                        <th>更新時間</th>
                        <th>建立時間</th>
                        <th>
                            <asp:HyperLink ID="vAdd" NavigateUrl="/Article/Admin/Article_Modify.aspx" CssClass="Button1 Add fancybox" runat="server"></asp:HyperLink>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="vArticle_List" runat="server" OnItemDataBound="vArticle_List_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Literal ID="vTitle" runat="server" Text="Title"></asp:Literal>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Literal ID="vContent" runat="server" Text="Content"></asp:Literal>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Literal ID="vEnable" runat="server" Text="Enable"></asp:Literal>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Literal ID="vUpdateTime" runat="server" Text="UpdateTime"></asp:Literal>
                                </td>
                                <td style="text-align: center;">
                                    <asp:Literal ID="vCreateTime" runat="server" Text="CreateTime"></asp:Literal>
                                </td>
                                <td style="text-align: center;">
                                    <asp:HyperLink ID="vEdit" CssClass="Button1 Edit fancybox" runat="server" NavigateUrl="/Article/Admin/Article_Modify.aspx"></asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
</asp:Content>


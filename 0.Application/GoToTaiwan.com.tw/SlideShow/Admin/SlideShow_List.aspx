<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Admin.master" AutoEventWireup="true" CodeFile="SlideShow_List.aspx.cs" Inherits="SlideShow_Admin_SlideShow_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/SlideShow/Admin/_Css/SlideShow_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="SlideShow_List">
        <table class="Table1">
            <colgroup>
                <col style="width: 100px;" />
                <col style="width: auto;" />
                <col style="width: 50px;" />
                <col style="width: 170px;" />
                <col style="width: 170px;" />
                <col style="width: 40px;" />                
            </colgroup>
            <thead>
                <tr>
                    <th>名稱</th>
                    <th>圖片</th>
                    <th>排序</th>
                    <th>更新時間</th>
                    <th>建立時間</th>                    
                    <th>
                        <asp:HyperLink ID="vAdd" CssClass="Button1 Add fancybox" runat="server"></asp:HyperLink>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="vSlideShowList" runat="server" OnItemDataBound="vSlideShowList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td  style="text-align: center;">
                                <asp:Literal ID="vName" runat="server" Text="Name"></asp:Literal>
                            </td>
                            <td>
                                <div class="Image">
                                    <asp:Image ID="vImage" runat="server" Width="560" ImageUrl="~/_Image/Main/Img3.png" />
                                </div>
                            </td>
                            <td style="text-align: center;">
                                <asp:Literal ID="vSort" runat="server"></asp:Literal>
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


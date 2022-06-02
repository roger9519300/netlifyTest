<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="SlideShow_Modify.aspx.cs" Inherits="SlideShow_Admin_SlideShow_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/SlideShow/Admin/_Css/SlideShow_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <div id="SlideShow_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 80px" />
                <col style="width: auto" />
            </colgroup>
            <tr>
                <th>名稱</th>
                <td>
                    <asp:TextBox ID="vName" MaxLength="100" Style="width: 99%;" runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <th>圖片<br />
                    <asp:Literal ID="vImageSize" runat="server"></asp:Literal>
                </th>
                <td>
                    <asp:Image ID="vImage" runat="server" ImageUrl="~/_MasterPage/_Image/Base_Admin/Img1.jpg" />
                    <br />
                    <br />
                    <asp:FileUpload ID="vFileUpload" runat="server" />
                </td>
            </tr>
            <tr>
                <th>連結網址</th>
                <td>
                    <asp:TextBox ID="vLinkUrl" MaxLength="500" Style="width: 99%;" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>排序</th>
                <td>
                    <asp:TextBox ID="vSort" runat="server" MaxLength="3" Width="30px" Style="text-align: right;"></asp:TextBox></td>
            </tr>
            <tfoot>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="vDelete" CssClass="Button2 Red" runat="server" OnClick="vDelete_Click">刪除</asp:LinkButton>
                        <asp:LinkButton ID="vSave" CssClass="Button2 Blue" runat="server" OnClick="vSave_Click">儲存</asp:LinkButton>
                        <div style="clear: both; height: 1px;"></div>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>


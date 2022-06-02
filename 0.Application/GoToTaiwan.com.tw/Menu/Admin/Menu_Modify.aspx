<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="Menu_Modify.aspx.cs" Inherits="Menu_Admin_Menu_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/Menu/Admin/_Css/Menu_Modify.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
     <div id="Menu_Modify">
        <table class="Table2">
            <colgroup>
                <col style="width: 80px" />
                <col style="width: auto" />
            </colgroup>
            <tr>
                <th>顯示名稱</th>
                <td>
                    <asp:TextBox ID="vTitle" MaxLength="100" Style="width: 99%;" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>語系</th>
                <td>
                    <asp:RadioButtonList ID="vLocalizationFilter" CssClass="LocalizationFilter"  AutoPostBack="true" RepeatLayout="Flow" RepeatDirection="Horizontal"  runat="server"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>排序</th>
                <td>
                    <asp:TextBox ID="vSort" runat="server" MaxLength="3" Width="30px" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>           
            <tr>
                <th>顯示</th>
                <td>
                    <asp:RadioButtonList ID="vEnable" CssClass="SelectBoxList  Radio  js-SelectBoxList" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server">
                        <asp:ListItem Text="開啟" Value="True" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="關閉" Value="False"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>                    
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


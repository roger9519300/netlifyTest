<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="User_Admin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/User/Admin/_Css/ChangePassword.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="ChangePassword">
        <table class="Table2">
            <tbody>
                <tr>
                    <th>密碼
                    </th>
                    <td>
                        <asp:TextBox ID="vPassword" runat="server" TextMode="Password" MaxLength="15" Width="90%" CssClass="TextBox1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>確認密碼
                    </th>
                    <td>
                        <asp:TextBox ID="vPasswordConfirm" runat="server" TextMode="Password" MaxLength="15" Width="90%" CssClass="TextBox1"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:LinkButton ID="vSaveBtn" runat="server" CssClass="Button2 Blue" OnClick="vSaveBtn_Click">儲存</asp:LinkButton>
        <div style="clear: both; height: 1px"></div>
    </div>
</asp:Content>


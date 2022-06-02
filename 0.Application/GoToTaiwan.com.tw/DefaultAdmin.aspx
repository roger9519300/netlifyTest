<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base.master" AutoEventWireup="true" CodeFile="DefaultAdmin.aspx.cs" Inherits="_ManagerLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/_Css/DefaultAdmin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="DefaultAdmin">
        <asp:Panel DefaultButton="vLoginButton" runat="server">
            <table class="LoginTable">
                <tr>
                    <td>
                        <label>Account</label>
                        <asp:TextBox ID="vAccount" MaxLength="20" Width="260" autocomplete="off" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Password</label>
                        <asp:TextBox ID="vPassword" MaxLength="20" TextMode="Password" Width="260" autocomplete="off" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:LinkButton ID="vLoginButton" runat="server" CssClass="LoginButton" OnClick="vLoginButton_Click">LogIn</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>



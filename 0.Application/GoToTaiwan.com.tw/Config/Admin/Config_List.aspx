<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Admin.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="Config_List.aspx.cs" Inherits="Config_Manager_Config" %>

<%@ Register Src="~/_Element/HtmlEditor/HtmlEditor.ascx" TagPrefix="uc1" TagName="HtmlEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/Config/Admin/_Css/Config_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Config_List">
        <table class="Table1">
            <colgroup>
                <col style="width: 120px;" />
                <col style="width: auto;" />
            </colgroup>
            <tbody>
                <tr>
                    <th>SEO設定</th>
                    <td style="line-height: 30px;">
                        <asp:TextBox ID="vSeoTitle" CssClass="form-control" Width="99%" TextMode="MultiLine" Rows="3" placeholder="SeoTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>流量分析代碼</th>
                    <td>
                        <asp:TextBox ID="vAnalyticsCode" TextMode="MultiLine" Rows="15" Width="99%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>微信QrCode</th>
                    <td>
                        <asp:Image ID="vQrCode" Width="100" runat="server" />
                        <br />
                        <asp:FileUpload ID="vQrCodeUpload" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:LinkButton ID="vSaveBtn" CssClass="Button2 Blue" Style="float: right" OnClick="vSaveBtn_Click" runat="server">儲存</asp:LinkButton>
    </div>
</asp:Content>


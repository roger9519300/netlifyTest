<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Popup.master" AutoEventWireup="true" CodeFile="WeChat_Detail.aspx.cs" Inherits="WeChat_Detail" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/_Css/WeChat_Detail.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div id="WeChat_Detail">
        <asp:Image ID="vQrCode" Width="99%" runat="server" />
    </div>
</asp:Content>

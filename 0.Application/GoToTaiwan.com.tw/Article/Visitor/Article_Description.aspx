<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Visitor.master" AutoEventWireup="true" CodeFile="Article_Description.aspx.cs" Inherits="Article_Visitor_Article_Description" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/Article/Visitor/_Css/Article_Description.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="Article_Description">
        <dl>
            <dt>
                <asp:Literal ID="vMentItemTitle" runat="server">MenuItem</asp:Literal>->
                <asp:Literal ID="vTitle" runat="server" Text="Title"></asp:Literal>
            </dt>
            <dd>
                <asp:Literal ID="vContent" runat="server" >Content</asp:Literal>
            </dd>
        </dl>
        <asp:HyperLink ID="vBackArticleList" runat="server">BackArticleList</asp:HyperLink>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Visitor.master" AutoEventWireup="true" CodeFile="Article_List.aspx.cs" Inherits="Article_Visitor_Article_List" %>

<%@ Register Src="~/_Element/Pagger/Pagger.ascx" TagPrefix="uc1" TagName="Pagger" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/Article/Visitor/_Css/Article_List.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="Article_List">
        <div class="MenuItemTitle">
            <asp:Literal ID="vMentItemTitle" runat="server"></asp:Literal>
        </div>
        <div class="ArticleList">
            <dl>
                <asp:Repeater ID="vArticle_List" runat="server" OnItemDataBound="vArticle_List_ItemDataBound">
                    <ItemTemplate>
                            <dt>
                                <asp:HyperLink ID="vArticleTitle" runat="server" Text="Title"></asp:HyperLink>
                            </dt>
                            <dd>
                                <asp:Literal ID="vArticleSeoContent" runat="server" Text="Content"></asp:Literal>
                            </dd>
                            <dd class="ReadMore">
                                <asp:HyperLink ID="vReadMore" runat="server">繼續閱讀</asp:HyperLink>
                            </dd>
                    </ItemTemplate>
                </asp:Repeater>
            </dl>
        </div>
        <div>
            <uc1:Pagger ID="Pagger" runat="server" />
        </div>
    </div>
</asp:Content>


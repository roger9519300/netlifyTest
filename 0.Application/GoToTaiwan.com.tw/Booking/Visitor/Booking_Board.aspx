<%@ Page Title="" Language="C#" MasterPageFile="~/_MasterPage/Base_Visitor.master" AutoEventWireup="true" CodeFile="Booking_Board.aspx.cs" Inherits="Booking_Visitor_Booking_Board" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/Booking/Visitor/_Css/Booking_Board.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 86px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="Booking_Board">
        <div class="Title">
            客戶留言
        </div>
        <table class="Table1">
            <tr>
                <td class="TdTitle"><span class="RedFont">*</span>姓名：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vName" runat="server"></asp:TextBox>
                </td>
                <td class="TdTitle"><span class="RedFont">*</span>開始包車日期：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vStartTime" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_vEndTime\',{d:-1});}',minDate:'%y-%M-{%d+1}'});" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="TdTitle"><span class="RedFont">*</span>電子信箱：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vEmail" runat="server"></asp:TextBox>
                </td>
                <td class="TdTitle"><span class="RedFont">*</span>結束包車日期：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vEndTime" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_vStartTime\',{d:1});}'});" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TdTitle">LINE：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vLine" runat="server"></asp:TextBox>
                </td>
                <td class="TdTitle"><span class="RedFont">*</span>包車出發地點：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vStartLocation" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TdTitle">WeChat：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vWeChat" runat="server"></asp:TextBox>
                </td>
                <td class="TdTitle"><span class="RedFont">*</span>包車結束地點：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vEndLocation" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TdTitle">WhatApp：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vWhatApp" runat="server"></asp:TextBox>
                </td>
                <td class="TdTitle"><span class="RedFont">*</span>包車旅客人數：</td>
                <td class="TdColumn">
                    <asp:TextBox ID="vPeople" Width="30" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TdTitle"><span class="RedFont">*</span>行程內容：</td>
                <td colspan="3" class="TdColumn">                   
                    <asp:TextBox ID="vSchedule" Height="200" Width="500" TextMode="MultiLine"  runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="TdTitle" style="height: 86px">備註：</td>
                <td colspan="3" class="TdColumn">                   
                    <asp:TextBox ID="vRemark" Height="70" Width="500" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">                   
                    <asp:Button ID="vSendButton" CssClass="bt" runat="server" Text="送　出" OnClick="vSendButton_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


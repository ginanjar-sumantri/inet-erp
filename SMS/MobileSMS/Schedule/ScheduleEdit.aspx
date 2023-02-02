<%@ Page Title="" Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true" CodeFile="ScheduleEdit.aspx.cs" Inherits="SMS.SMSWeb.Schedule.ScheduleEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="../calendar/calendar.js"></script>
    <link href="../calendar/calendar.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
    SCHEDULE EDIT
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <div style="padding:5px 5px 5px 5px">
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:150px;display:block;float:left;">Schedule Date </span>: </span> &nbsp; <asp:TextBox runat="server" ID="ScheduleDateTextBox" Width="80px"></asp:TextBox>
            <input type="button" id="btnScheduleDate" value="..." onclick="displayCalendar(ctl00_ContentPlaceHolderContent_ScheduleDateTextBox,'yyyy-mm-dd',this)" />
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:150px;display:block;float:left;">Schedule Time </span>: </span> &nbsp; <asp:DropDownList runat="server" ID="HourDropDownList"></asp:DropDownList><asp:DropDownList runat="server" ID="MinuteDropDownList"></asp:DropDownList>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:150px;display:block;float:left;">Message </span>: </span> &nbsp; <asp:TextBox runat="server" ID="MessageTextBox" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:150px;display:block;float:left;">Destination Phone No. </span>: </span> &nbsp; <asp:TextBox runat="server" ID="DestinationPhoneNoTextBox"></asp:TextBox>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <asp:Label runat="server" id="WarningLabel" style="color:Red;font-weight:bold"></asp:Label>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
        <asp:ImageButton runat="server" ID="SaveButton" ImageUrl="../images/save.jpg" 
            onclick="SaveButton_Click" />
        <asp:ImageButton runat="server" ID="CancelButton" 
            ImageUrl="../images/cancel.jpg" onclick="CancelButton_Click" />
        </div>
    </div>
</asp:Content>


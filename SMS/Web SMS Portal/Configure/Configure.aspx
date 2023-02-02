<%@ Page Title="" Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true" CodeFile="Configure.aspx.cs" Inherits="SMS.SMSWeb.Configure.Configure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">
    .panelConfigure {margin:10px;}
    .panelConfigure div div{padding:3px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
CONFIGURE
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
<div class="panelConfigure">
    <div style="clear:both;display:block;">
        <div style="float:left;width:200px;">Admin's Mobile Phone Number</div>
        <div style="float:left">:</div>
        <div style="float:left"><asp:TextBox runat="server" ID="AdminPhoneNumberTextBox"></asp:TextBox></div>
    </div>
    <div style="clear:both;display:block;">
        <div style="float:left;width:200px;">Admin's E-mail</div>
        <div style="float:left">:</div>
        <div style="float:left"><asp:TextBox runat="server" ID="AdminEmailTextBox"></asp:TextBox></div>
    </div>
    <div style="clear:both;display:block;">
        <div style="float:left;width:200px;">Global Auto Reply Message</div>
        <div style="float:left">:</div>
        <div style="float:left"><asp:TextBox runat="server" ID="GlobalAutoReplyTextBox"></asp:TextBox></div>
    </div>
    <div style="clear:both;display:block;">
        <div style="float:left;width:200px;">Footer Additional Message</div>
        <div style="float:left">:</div>
        <div style="float:left"><asp:TextBox runat="server" ID="FooterAdditionalMessageTextBox"></asp:TextBox></div>
    </div>
    <div style="clear:both;display:block;">
        <asp:ImageButton runat="server" ID="UpdateButton" 
            onclick="UpdateButton_Click" />
    </div>
</div>
</asp:Content>


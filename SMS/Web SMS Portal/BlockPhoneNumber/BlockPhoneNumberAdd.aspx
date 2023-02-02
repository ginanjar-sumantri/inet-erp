<%@ Page Title="" Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true" CodeFile="BlockPhoneNumberAdd.aspx.cs" Inherits="SMS.SMSWeb.BlockPhoneNumber.BlockPhoneNumberAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
    BLOCK PHONE NUMBER
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <div style="padding:5px 5px 5px 5px">
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:120px;display:block;float:left;">Block Phone Number </span>: </span> &nbsp; <asp:TextBox runat="server" ID="BlockPhoneNumberTextBox"></asp:TextBox>
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


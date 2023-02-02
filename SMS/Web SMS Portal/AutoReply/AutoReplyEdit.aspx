<%@ Page Title="" Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true" CodeFile="AutoReplyEdit.aspx.cs" Inherits="SMS.SMSWeb.AutoReply.AutoReplyEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
    AUTO REPLY EDIT
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <div style="padding:5px 5px 5px 5px">
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:120px;display:block;float:left;">Sender Phone </span>: </span> &nbsp; <asp:TextBox runat="server" ID="SenderPhoneTextBox"></asp:TextBox>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:120px;display:block;float:left;">Key Word </span>: </span> &nbsp; <asp:TextBox runat="server" ID="KeyWordTextBox"></asp:TextBox>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:120px;display:block;float:left;">Reply Message </span>: </span> &nbsp; <asp:TextBox runat="server" ID="ReplyMessageTextBox" TextMode="MultiLine"></asp:TextBox>
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


<%@ Page Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
    CHANGE PASSWORD
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <div style="padding : 5px 5px 5px 5px;">
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:120px;display:block;float:left;">Old Password </span>: </span> &nbsp; <asp:TextBox runat="server" ID="oldPasswordTextBox" TextMode="Password"></asp:TextBox>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:120px;display:block;float:left;">New Password </span>: </span> &nbsp; <asp:TextBox runat="server" ID="newPasswordTextBox" TextMode="Password"></asp:TextBox>            
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <span style="float:left"><span style="width:120px;display:block;float:left;">Retype New Password </span>: </span> &nbsp; <asp:TextBox runat="server" ID="retypeNewPasswordTextBox" TextMode="Password"></asp:TextBox>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <asp:Label runat="server" id="WarningLabel" style="color:Red;font-weight:bold"></asp:Label>
        </div>
        <div style="clear:both;padding : 3px 3px 3px 3px;">
            <asp:Button runat="server" ID="ButtonChangePassword" Text="Change Password" 
                onclick="ButtonChangePassword_Click" />
        </div>
    </div>
</asp:Content>
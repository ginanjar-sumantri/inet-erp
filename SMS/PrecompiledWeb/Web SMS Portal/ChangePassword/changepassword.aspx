<%@ page language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.ChangePassword.ChangePassword, App_Web_imr5ol-z" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    CHANGE PASSWORD
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <div style="padding: 5px 5px 5px 5px;">
        <div style="clear: both; padding: 3px 3px 3px 3px;">
            <span style="float: left"><span style="width: 120px; display: block; float: left;">Old
                Password </span>: </span>&nbsp;
            <asp:TextBox runat="server" ID="oldPasswordTextBox" TextMode="Password"></asp:TextBox>
        </div>
        <div style="clear: both; padding: 3px 3px 3px 3px;">
            <span style="float: left"><span style="width: 120px; display: block; float: left;">New
                Password </span>: </span>&nbsp;
            <asp:TextBox runat="server" ID="newPasswordTextBox" TextMode="Password"></asp:TextBox>
        </div>
        <div style="clear: both; padding: 3px 3px 3px 3px;">
            <span style="float: left"><span style="width: 120px; display: block; float: left;">Retype
                New Password </span>: </span>&nbsp;
            <asp:TextBox runat="server" ID="retypeNewPasswordTextBox" TextMode="Password"></asp:TextBox>
        </div>
        <div style="clear: both; padding: 3px 3px 3px 3px;">
            <asp:Label runat="server" ID="WarningLabel" Style="color: Red; font-weight: bold"></asp:Label>
        </div>
        <div style="clear: both; padding: 3px 3px 3px 3px;">
            <asp:Button runat="server" ID="ButtonChangePassword" Text="Change Password" OnClick="ButtonChangePassword_Click" />
        </div>
    </div>
</asp:Content>

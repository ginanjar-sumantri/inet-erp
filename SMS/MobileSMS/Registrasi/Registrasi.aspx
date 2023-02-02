<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registrasi.aspx.cs" Inherits="SMS.SMSWeb.Registrasi.Registrasi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<script src="../jquery-1.2.6.min.js"></script>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        //$(".loginForm").fadeIn("fast");
        //$(".loginForm div div").fadeIn("slow");
    });
</script>

<head>
    <title>Registration SMS Portal</title>
    <style type="text/css">
        BODY{margin: 0px 0px 0px 0px;background: #efefef;font-family: Calibri;font-size: 12px;}        
        div.loginForm{display: block;text-align: left;width: 350px;padding-top: 20px;}
        div.loginForm div div{padding: 3px 3px 3px 3px;display: inline;}
        .uppanelTitle{clear: both;color: #fffc00;background-color : #69F;text-align: left;
            font-weight: bold;font-family: Arial;font-size: 14px;padding:5px;}
        .footerAllRightReserved{position:absolute;bottom:0px;color:#666;padding:3px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal runat="server" ID="browserBouncer"></asp:Literal>
    <div class="uppanelTitle">Web SMS</div>
        <table width="350px">
            <tr>
                <td colspan="3" height="20px"></td>
            </tr>
            <tr>
                <td colspan="3">
                <asp:Label ID="WarningLabel" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="14px"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="150px">Corporate Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="CorporateNameTextBox" Text="PERSONAL" ReadOnly="true" BackColor="#CCCCCC"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>User ID</td>
                <td>:</td>
                <td><asp:TextBox ID="UserIDTextBox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Password</td>
                <td>:</td>
                <td><asp:TextBox ID="PasswordTextBox" runat="server" MaxLength="49" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Confirm Password</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" MaxLength="49" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="PasswordTextBox"
                        ControlToValidate="ConfirmPasswordTextBox" ErrorMessage="Password is not identical"
                        Text="*"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="EmailTextBox" runat="server" MaxLength="49"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                        ErrorMessage="Email is not valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3"><asp:Image ID="imCaptcha" ImageUrl="Captcha.ashx" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:TextBox ID="txtVerify" runat="server"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtVerify"
                        ErrorMessage="You have Entered a Wrong Verification Code!Please Re-enter!!!"
                        OnServerValidate="CAPTCHAValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="SaveImageButton" runat="server" OnClick="SaveImageButton_Click" Text="Register" /></td>
                <td></td>
                <td align="right">
                    <asp:LinkButton ID="RegisterLinkButton" runat="server" PostBackUrl="../Login/Login.aspx"
                        Text="Login" CausesValidation="false"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <div class="footerAllRightReserved">All Right Reserved &copy; 2010</div>
    </form>
</body>
</html>

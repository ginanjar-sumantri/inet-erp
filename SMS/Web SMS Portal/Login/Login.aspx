<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="SMS.SMSWeb.Login.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><script src="../jquery-1.2.6.min.js"></script>
<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        //$(".loginForm").fadeIn("fast");
        //$(".loginForm div div").fadeIn("slow");
    });
</script>
<head>
    <title>Login SMS Portal</title>
    <style type="text/css">
        BODY {margin: 0px 0px 0px 0px; background:#efefef;font-family:Calibri;font-size:12px;}
        .bodyContent {width:796px;}
        .uppanelTop {float:left;background-image:url("../images/layout/uppanelbg.jpg");width:735px;height:60px;}
        .uppanelTitle {clear:both;color:#fffc00;text-align:left;font-weight:bold;padding-top:10px;font-family:Arial;font-size:14px;}
        .uppanelContent {clear:both;border-left:solid 1px #b2b2b2;border-right:solid 1px #b2b2b2;background-color:White;text-align:center;min-height:250px;}
        div.loginForm {display:block;text-align:left;width:350px;padding-top:20px;}
        div.loginForm div div {padding:3px 3px 3px 3px;display:inline;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div class="bodyContent">
        <img style="clear:both" src="../images/layout/header.jpg" />
        <div style="clear:both">
            <img style="float:left" src="../images/layout/uppanelleft.jpg" />
            <div class="uppanelTop">
                <div class="uppanelTitle">Login Form</div>
            </div>
            <img style="float:left" src="../images/layout/uppanelright.jpg" />
        </div>
        <div class="uppanelContent">
        <center>
                <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="14px"></asp:Label>
                <table width="300px">
                    <tr><td colspan="3" height="30px"></td></tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="WarningLabel" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="150px">Corporate Name</td>
                        <td>:</td>
                        <td><asp:TextBox ID="CorporateNameTextBox" runat="server"></asp:TextBox></td>
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
                        <td colspan="3"><asp:Image ID="imCaptcha" ImageUrl="Captcha.ashx" runat="server" /></td>
                    </tr>
                    <tr>
                        <td colspan="3"><asp:TextBox ID="txtVerify" runat="server"></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtVerify"
                            ErrorMessage="You have Entered a Wrong Verification Code!Please Re-enter!!!"
                            OnServerValidate="CAPTCHAValidate"></asp:CustomValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" /></td>
                        <td></td>
                        <td align="right"><asp:LinkButton ID="RegisterLinkButton" runat="server" PostBackUrl="../Registrasi/Registrasi.aspx"
                            Text="Registrasi"></asp:LinkButton></td>
                    </tr>
                </table>
        </center>
        </div>
        <img style="clear:both" src="../images/layout/uppanelbottom.jpg" />        
    </div>
    Best Viewed with <a href="http://www.mozilla.com/" style="text-decoration:none;color:#666">Mozilla Firefox</a>.
    </center>
    </form>
</body>
</html>

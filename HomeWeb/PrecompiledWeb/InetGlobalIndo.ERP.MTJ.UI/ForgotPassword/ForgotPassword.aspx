<%@ page language="C#" autoeventwireup="true" inherits="ForgotPassword, App_Web_wc2wbelq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal ID="AppNameLiteral" runat="server"></asp:Literal></title>
    <link href="~/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" BackColor="#F7F7DE" BorderColor="#CCCC99"
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt">
            <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:PasswordRecovery>
    </div>
    </form>
</body>
</html>

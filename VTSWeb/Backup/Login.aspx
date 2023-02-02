<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <asp:Literal ID="AppNameLiteral" runat="server"></asp:Literal></title>
    <asp:Literal ID="StyleSheetLiteral" runat="server" />

    <script language="javascript" src="jquery-1.4.2.js"></script>

    <script language="javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <img src="images/login/pic.png" />
            <table class="container" cellpadding="0" cellspacing="0" border="0" style="width: 346px;
                height: 288px;">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr class="headerLogin">
                    <td>
                        <div class="logo">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="top">
                        <table class="containLogin" border="0" cellpadding="0" align="left" width="346px"
                            height="208px">
                            <tr>
                                <td align="center" colspan="3" style="color: Red; font-size: 12px; font-weight: bold;">
                                    <asp:Literal ID="CompanyNameLiteral" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="padding-left: 2px" style="padding-right: 8px">
                                    <fieldset>
                                        <legend>USERNAME</legend>
                                        <asp:TextBox ID="UserName" runat="server" Width="300"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        <%--<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User 
                                    Name</asp:Label>--%>
                                    </fieldset>
                                </td>
                                <%--<td align="center">
                                    :
                                </td>--%>
                            </tr>
                            <tr>
                                <%--<td align="left">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                </td>
                                <td align="center">
                                    :
                                </td>--%>
                                <td align="left" colspan="2" style="padding-left: 2px" style="padding-right: 8px">
                                    <fieldset>
                                        <legend>PASSWORD</legend>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="300"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </fieldset>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td align="left">
                                    Instance
                                </td>
                                <td align="center">
                                    :
                                </td>
                                <td align="left" colspan="2">
                                    <fieldset>
                                        <legend>INSTANCE</legend>
                                        <asp:DropDownList ID="ConnModeDropDownList" runat="server" Width="250">
                                        </asp:DropDownList>
                                    </fieldset>
                                </td>
                            </tr>--%>
                            <tr>
                                <td align="left" style="padding-left: 5px">
                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                </td>
                                <td align="right" class="style3" style="padding-right: 5px">
                                    <asp:LinkButton ID="ForgotPasswordLinkButton" runat="server">Forgot Your Password?</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="LoginButton" class="buttonLogin" runat="server" ValidationGroup="Login1"
                                        CommandName="Login" OnClick="LoginButton_Click"></asp:Button>
                                    <%--<asp:ImageButton ID="LoginButton" runat="server" OnClick="LoginButton_Click" ValidationGroup="Login1"
                                        CommandName="Login" ImageUrl="images/login/login.png" />--%>
                                    <%--<asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                                                                ValidationGroup="Login1" onclick="LoginButton_Click" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height: 20px">
                                    <font color="red" style="padding-left: 5px">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        <asp:Literal ID="FailureText2" runat="server" EnableViewState="false"></asp:Literal>
                                    </font>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <%--<table>
            <tr>
                <td align="center" style="color: Red; background-color: White;">
                </td>
            </tr>
        </table>--%>
        </center>
    </div>
    </form>
</body>
</html>

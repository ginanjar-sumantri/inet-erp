<%@ page language="C#" autoeventwireup="true" inherits="Login, App_Web_e1v-hlpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal ID="AppNameLiteral" runat="server"></asp:Literal></title>
    <asp:Literal ID="StyleSheetLiteral" runat="server" />
</head>
<body id="bodyLogin" runat="server">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="500px">
        <tr>
            <td align="center">
                <asp:Login ID="Login1" runat="server" BorderColor="#CCCC99" BorderStyle="Solid"
                    BorderWidth="0px" Font-Names="Verdana" Font-Size="10pt" DestinationPageUrl="~/Default.aspx"
                    OnLoggedIn="Login1_LoggedIn">
                    <LayoutTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table id="panelLogin" runat="server">
                                        <tr>
                                            <td align="right" valign="bottom">
                                                <table border="0" cellpadding="0">
                                                    <tr>
                                                        <td align="left" colspan="3" style="color: Red; font-size: 12px; font-weight: bold;">
                                                            <asp:Literal ID="CompanyNameLiteral" runat="server"></asp:Literal><br />
                                                            <%--<asp:Literal ID="AppNameLiteral" runat="server"></asp:Literal>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            Company
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="CompanyDropDownList" Width="200px" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="CompanyDropDownList_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            Instance
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ConnModeDropDownList" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User 
                                    Name</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            :
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="left">
                                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <asp:LinkButton ID="ForgotPasswordLinkButton" runat="server">Forgot Your Password?</asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="LoginButton" runat="server" OnClick ="LoginButton_Click" ValidationGroup="Login1" CommandName="Login" ImageUrl="images/login/login.png" />
                                                            <%--<asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                                                                ValidationGroup="Login1" onclick="LoginButton_Click" />--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 30px;">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="color: Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    <asp:Literal ID="FailureText2" runat="server" EnableViewState="false"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
                </asp:Login>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

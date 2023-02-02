<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackEndLogin.aspx.cs" Inherits="SMS.BackEndSMSPortal.BackEndLogin.BackEndLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        BODY
        {
            margin: 0px 0px 0px 0px;
            background: #efefef;
            font-family: Calibri;
            font-size: 12px;
        }
        .bodyContent
        {
            width: 796px;
        }
        .uppanelTop
        {
            float: left;
            background-image: url("../images/layout/uppanelbg.jpg");
            width: 735px;
            height: 60px;
        }
        .uppanelTitle
        {
            clear: both;
            color: #fffc00;
            text-align: left;
            font-weight: bold;
            padding-top: 10px;
            font-family: Arial;
            font-size: 14px;
        }
        .uppanelContent
        {
            clear: both;
            border-left: solid 1px #b2b2b2;
            border-right: solid 1px #b2b2b2;
            background-color: White;
            text-align: center;
            min-height: 90px;
        }
        div.loginForm
        {
            display: none;
            text-align: left;
            width: 300px;
            padding-top: 20px;
        }
        div.loginForm div div
        {
            padding: 3px 3px 3px 3px;
            float: left;
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div class="bodyContent">
            <img style="clear: both" src="../images/layout/header.jpg" />
            <div style="clear: both">
                <img style="float: left" src="../images/layout/uppanelleft.jpg" />
                <div class="uppanelTop">
                    <div class="uppanelTitle">
                        Login Admin Form</div>
                </div>
                <img style="float: left" src="../images/layout/uppanelright.jpg" />
            </div>
            <div class="uppanelContent">
                <center>
                    <table>
                        <tr>
                            <td valign="middle">
                                <table>
                                    <tr>
                                        <td align="left" colspan="3">
                                            <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                            <asp:Label ID="WarrningLabel" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="16px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            User Admin
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="UserAdminTextBox" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserAdminValidator" runat="server" Text="*" ErrorMessage="User Admin must be filled"
                                                ControlToValidate="UserAdminTextBox"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            Password Admin
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="PasswordAdminTextBox" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordAdminValidator" runat="server" Text="*" ErrorMessage="Password Admin must be filled"
                                                ControlToValidate="PasswordAdminTextBox"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td colspan="3">
                                            <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
            <img style="clear: both" src="../images/layout/uppanelbottom.jpg" />
        </div>
    </center>
    </form>
</body>
</html>

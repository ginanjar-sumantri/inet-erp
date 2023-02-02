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
        BODY {margin: 0px 0px 0px 0px;background: #efefef;font-family: Calibri;font-size: 12px;}
        .bodyContent{width: 796px;}
        .uppanelTop{float: left;background-image: url("../images/layout/uppanelbg.jpg");width: 735px;height: 60px;}
        .uppanelTitle{clear: both;color: #fffc00;text-align: left;font-weight: bold;padding-top: 10px;font-family: Arial;font-size: 14px;}
        .uppanelContent{clear: both;border-left: solid 1px #b2b2b2;border-right: solid 1px #b2b2b2;min-height: 10px;background-color: White;}
        .lowerpanelContent{clear: both;border-left: solid 1px #7e7e7e;border-right: solid 1px #7e7e7e;min-height: 10px;background-color: #999999;}
        .menupanel{float: left;margin-left: 10px;width: 230px;}
        .menuheader{float: left;width: 201px;height: 25px;background-image: url("../images/layout/menuheaderbg.jpg");
            text-align: left;font-size: 12px;font-weight: bold;font-family: Microsoft Sans Serif;color: #696969;
            padding-top: 7px;padding-left: 15px;}
        .menuitem{clear: both;display: block;width: 228;height: 20px;border: solid 1px #b8b8b8;
            background-image: url("../images/layout/menubg.jpg");text-align: left;font-size: 10px;
            font-weight: bold;font-family: Microsoft Sans Serif;color: #b8b8b8;padding-left: 15px;
            padding-top: 7px;text-decoration: none;}
        .menuitem:hover{color: #696969;cursor: pointer;}
        .contentpanel{float: left;margin-left: 12px;width: 530px;}
        .contentpanelTitle{float: left;width: 220px;height: 23px;background-image: url("../images/layout/contentpanelheaderbg1.jpg");
            text-align: left;font-size: 12px;font-weight: bold;font-family: Microsoft Sans Serif;color: White;
            padding-top: 7px;}
        .contentpanelTitle2{float: left;width: 263px;height: 30px;background-image: url("../images/layout/contentpanelheaderbg2.jpg");}
        .contentpanelContent{clear: both;border-left: solid 1px #b2b2b2;border-right: solid 1px #b2b2b2;
            min-height: 500px;background-color: White;text-align: left;padding: 5px 5px 5px 5px;}
        .bgcolor_gray{background-color: #999;color: White;}
        .TableRowOver{background-color: #EEE;cursor: pointer;}
        .TableRow{background-color: White;}
        div.loginForm{display: block;padding: 30px 50px 30px 50px;text-align: left;width: 50%;}
        div.loginForm div div{padding: 3px 3px 3px 3px;display: inline;}
        h1{color: #352500;font-family: Minion Pro;}
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
                        Registrasi</div>
                </div>
                <img style="float: left" src="../images/layout/uppanelright.jpg" />
            </div>
            <div class="uppanelContent">
                <center>
                    <div class="loginForm">
                        <asp:Label ID="WarningLabel" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="14px"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                        <div style="clear: both">
                            <div style="width: 150px; float: left;">
                                Corporate Name</div>
                            <div style="width: 15px; float: left;">
                                :</div>
                            <div style="width: 200px; float: left;">
                                <asp:TextBox ID="CorporateNameTextBox" Text="PERSONAL" ReadOnly="true" BackColor="#CCCCCC"
                                    runat="server"></asp:TextBox></div>
                        </div>
                        <div style="clear: both">
                            <div style="width: 150px; float: left;">
                                User ID</div>
                            <div style="width: 15px; float: left;">
                                :</div>
                            <div style="width: 200px; float: left;">
                                <asp:TextBox ID="UserIDTextBox" runat="server"></asp:TextBox></div>
                        </div>
                        <div style="clear: both">
                            <div style="width: 150px; float: left;">
                                Password</div>
                            <div style="width: 15px; float: left;">
                                :</div>
                            <div style="width: 200px; float: left;">
                                <asp:TextBox ID="PasswordTextBox" runat="server" MaxLength="49" TextMode="Password"></asp:TextBox></div>
                        </div>
                        <div style="clear: both">
                            <div style="width: 150px; float: left;">
                                Confirm Password</div>
                            <div style="width: 15px; float: left;">
                                :</div>
                            <div style="width: 200px; float: left;">
                                <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" MaxLength="49" TextMode="Password"></asp:TextBox>
                                <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="PasswordTextBox"
                                    ControlToValidate="ConfirmPasswordTextBox" ErrorMessage="Password is not identical"
                                    Text="*"></asp:CompareValidator></div>
                        </div>
                        <div style="clear: both">
                            <div style="width: 150px; float: left;">
                                Email</div>
                            <div style="width: 15px; float: left;">
                                :</div>
                            <div style="width: 200px; float: left;">
                                <asp:TextBox ID="EmailTextBox" runat="server" MaxLength="49"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                    ErrorMessage="Email is not valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div style="clear: both">
                            <div>
                                <asp:Image ID="imCaptcha" ImageUrl="Captcha.ashx" runat="server" />
                            </div>
                            <br />
                            <div>
                                <asp:TextBox ID="txtVerify" runat="server"></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtVerify"
                                    ErrorMessage="You have Entered a Wrong Verification Code!Please Re-enter!!!"
                                    OnServerValidate="CAPTCHAValidate"></asp:CustomValidator>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                        <div>
                            <div style="float: left;">
                                <asp:Button ID="SaveImageButton" runat="server" OnClick="SaveImageButton_Click" Text="Register" />
                            </div>
                            <div style="float: right;">
                                <asp:LinkButton ID="RegisterLinkButton" runat="server" PostBackUrl="../Login/Login.aspx"
                                    Text="Login" CausesValidation="false"></asp:LinkButton>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                    </div>
                </center>
            </div>
            <img style="clear: both" src="../images/layout/uppanelbottom.jpg" />
        </div>
    </center>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>
        <asp:Literal ID="ModuleTitleLiteral" runat="server"></asp:Literal></title>
    <asp:Literal ID="StyleSheetLiteral" runat="server" />
    <style type="text/css">
        .adjustedZIndex
        {
            vertical-align: bottom;
            z-index: 999;
        }
    </style>
</head>
<body runat="server" id="bodyIndex" style="margin: 0px 0px 0px 0px">
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td id="tdImage" width="10px" style="padding-left: 5px;">
                <asp:Image ID="AppImage" runat="server" />
            </td>
            <td width="10px">
            </td>
            <td valign="top">
                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td style="padding-top: 5px; padding-left: 5px;" colspan="3">
                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tahoma_18_black">
                                        ERP MANAGEMENT SYSTEM
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="tahoma_14_black" style="padding-left: 5px;" colspan="3">
                            <asp:Literal ID="CompanyNameLiteral" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom" height="45px">
                            <div>
                                <asp:Menu ID="Menu2" runat="server" DynamicHorizontalOffset="2" Font-Names="Century Gothic"
                                    Orientation="Horizontal" VerticalPadding="0px" CssClass="adjustedZIndex" StaticDisplayLevels="1"
                                    StaticMenuItemStyle-VerticalPadding="0" StaticMenuItemStyle-Font-Name="Century Gothic"
                                    StaticMenuItemStyle-Font-Size="8pt" StaticMenuItemStyle-ForeColor="#000000" DynamicMenuStyle-HorizontalPadding="5"
                                    DynamicMenuStyle-VerticalPadding="2" DynamicMenuStyle-BackColor="#E0E0E0" DynamicMenuStyle-ForeColor="#990000"
                                    DynamicMenuStyle-BorderWidth="1px" DynamicMenuStyle-BorderColor="#C0C0C0" DynamicMenuItemStyle-VerticalPadding="2"
                                    DynamicMenuItemStyle-Font-Name="Century Gothic" DynamicMenuItemStyle-Font-Size="8pt"
                                    DynamicMenuItemStyle-ForeColor="#000000" DynamicHoverStyle-BackColor="#707070"
                                    DynamicHoverStyle-ForeColor="#FFFFFF" BorderWidth="0px" StaticPopOutImageUrl="images/transparent.gif">
                                    <StaticMenuItemStyle HorizontalPadding="2px" />
                                </asp:Menu>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td id="tdImageCompany" width="10px" style="padding-right: 5px;">
                <asp:Image ID="CompanyLogoImage" runat="server" />
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td style="height: 3px">
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" width="100%" runat="server" id="welcometable">
                    <tr>
                        <td width="10px">
                            &nbsp;
                        </td>
                        <td align="right" style="padding-right: 5px;">
                            <b>Welcome</b>,
                            <asp:LoginName ID="LoginName1" runat="server" />
                            <asp:Literal ID="ConnModeLiteral" runat="server"></asp:Literal>
                        </td>
                        <td width="10px" align="right" style="padding-right: 5px">
                            <asp:LoginStatus ID="LoginStatus1" runat="server" LoginText="" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    <iframe id="mainFrame" runat="server" style="border: 0; width: 100%" frameborder="0">
    </iframe>
</body>

<script type="text/javascript" language="javascript">
    function alertSize() {
        var myWidth = 0, myHeight = 0;
        if (typeof (window.innerWidth) == 'number') {
            //Non-IE
            myWidth = window.innerWidth;
            myHeight = window.innerHeight;
        } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
            //IE 6+ in 'standards compliant mode'
            myWidth = document.documentElement.clientWidth;
            myHeight = document.documentElement.clientHeight;
        } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
            //IE 4 compatible
            myWidth = document.body.clientWidth;
            myHeight = document.body.clientHeight;
        }
        //window.alert('Width = ' + myWidth);
        //window.alert('Height = ' + myHeight);
        document.getElementById("mainFrame").style.height = (myHeight - 133) + "px";
    }
    alertSize();
</script>

</html>

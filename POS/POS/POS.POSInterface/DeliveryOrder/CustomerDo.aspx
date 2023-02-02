<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerDo.aspx.cs" Inherits="POS.POSInterface.DeliveryOrder.CustomerDo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Delivery Order</title>
    <asp:Literal ID="javaScriptDeclaration" runat="server"></asp:Literal>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-1.4.3.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery.event.drag-1.5.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/style3.js" type="text/javascript"></script>

    <script src="../CSS/orange/JScript.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/orange/style3.css" media="all" rel="Stylesheet" type="text/css" />
    <asp:Literal ID="JScriptLiteral" runat="server"></asp:Literal>
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" language="javascript">
        var dateObject = new Date();
        var Hari = new Array("SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY");
        var Bulan = new Array("JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER");
        var hour = dateObject.getHours();
        var minute = dateObject.getMinutes();
        var second = dateObject.getSeconds();
        if (hour < 10) {
            hour = "0" + hour;
        }
        if (minute < 10) {
            minute = "0" + minute;
        }
        if (second < 10) {
            second = "0" + second;
        }
        $('.container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('.container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        function updateJam() {
            var dateObject = new Date();
            var Hari = new Array("SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY");
            var Bulan = new Array("JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER");
            var hour = dateObject.getHours();
            var minute = dateObject.getMinutes();
            var second = dateObject.getSeconds();
            if (hour < 10) {
                hour = "0" + hour;
            }
            if (minute < 10) {
                minute = "0" + minute;
            }
            if (second < 10) {
                second = "0" + second;
            }
            $('.container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('.container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);
    </script>

    <style type="text/css">
        .td
        {
            width: 230px;
        }
        .textValid
        {
            color: Red;
            font-size: 17px;
            font-family: caption;
        }
        .Highlight
        {
            background-color: Red;
        }
    </style>
</head>
<body id="bodyDeliveryOrder">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <asp:UpdatePanel ID="contentupdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Literal runat="server" ID="CSSLiteral"></asp:Literal>
            <asp:Literal runat="server" ID="javascriptReceiver"></asp:Literal>
            <div class="container">
                <div class="header">
                    <div class="left">
                        <div class="title">
                            <div class="text">
                                Delivery Order</div>
                            <div class="sep">
                            </div>
                        </div>
                    </div>
                    <div class="right">
                        <div class="date">
                            <div id="tanggal">
                            </div>
                            <div id="jam">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="warning">
                    <%--<asp:ValidationSummary ID="ValidationSummary" runat="server" />
--%>
                    <asp:Label runat="server" ID="WarningLabel"></asp:Label>
                </div>
                <div class="ContentForm">
                    <div class="formRegister1">
                        <table class="table1">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    NAME
                                </td>
                                <td>
                                </td>
                                <td class="textValid">
                                    <asp:TextBox ID="NameTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CustomerDONameRequiredFieldValidator" runat="server"
                                        ErrorMessage="CustomerDO Name Must Be Filled" Text="*" ControlToValidate="NameTextBox"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                                        TargetControlID="CustomerDONameRequiredFieldValidator" HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    TELEPHONE
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="TelephoneTextBox" runat="server" CssClass="widthText" MaxLength="50"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="TelephoneRequiredFieldValidator" runat="server" ErrorMessage="Telephone Must Be Filled"
                                Text="*" ControlToValidate="TelephoneTextBox" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    HANDPHONE
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="HandphoneTextBox" runat="server" CssClass="widthText" MaxLength="13"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    ADDRESS 1
                                </td>
                                <td>
                                </td>
                                <td class="textValid">
                                    <asp:TextBox TextMode="MultiLine" Height="110" CssClass="widthText" ID="Address1TextBox"
                                        runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="Address1RequiredFieldValidator" runat="server" ErrorMessage="Address1 Must Be Filled"
                                        Text="*" ControlToValidate="Address1TextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2"
                                        TargetControlID="Address1RequiredFieldValidator" HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    ADDRESS 2
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" Height="110" CssClass="widthText" ID="Address2TextBox"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    CITY
                                </td>
                                <td>
                                </td>
                                <td class="textValid">
                                    <asp:DropDownList ID="CityDDL" runat="server" Width="255px">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CityDDLCustomValidator" runat="server" ErrorMessage="<b>City Must Be Choose.</b> "
                                        Text="*" ControlToValidate="CityDDL" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                                    <%--<asp:RequiredFieldValidator ID="CityDDLRequiredFieldValidator" runat="server" ErrorMessage="City Must Be Choosed"
                                        Text="*" ControlToValidate="CityDDL" Display="Dynamic"></asp:RequiredFieldValidator>
--%>
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3"
                                        TargetControlID="CityDDLCustomValidator" HighlightCssClass="Highlight">
                                        <Animations>
                                 <OnShow>                                    
                                 <Sequence>   
        <HideAction Visible="true" /> 
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>    
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide></Animations>
                                    </ajaxToolkit:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    POSTAL CODE
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="PostalCodeTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="formRegister2">
                        <table class="table2">
                            <tr style="font-weight: bold; color: #FFFF00;">
                                <td>
                                    DELIVERY TO
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    ADDRESS 1
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" ID="DeliveryAddress1TextBox" runat="server" CssClass="widthText"
                                        Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    ADDRESS 2
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox TextMode="MultiLine" ID="DeliveryAddress2TextBox" runat="server" CssClass="widthText"
                                        Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    CITY
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:DropDownList ID="CityDeliveryDDL" runat="server" Width="255px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    POSTAL CODE
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="PostalCodeDeliveryTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    REFERENCE NO
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="ReferenceNoLabel" runat="server" CssClass="widthText"></asp:Label>
                                    <asp:HiddenField ID="CustDOCodeHiddenField" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div style="margin-top: 10px; padding-left: 0px;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="BackButton" runat="server" Text="search" CausesValidation="false"
                                            OnClick="BackButton_Click" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                    </td>
                                    <td style="padding-left: 6px;">
                                        <asp:ImageButton ID="ResetButton" runat="server" Text="Back" CausesValidation="false"
                                            OnClick="ResetButton_Click" />
                                    </td>
                                    <td style="padding-left: 6px;">
                                        <asp:ImageButton ID="SearchButton" runat="server" Text="Back" CausesValidation="false"
                                            OnClick="SearchButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="divisi">
                    <div class="LabelDivisi">
                        DIVISI
                    </div>
                    <div>
                        <div>
                            <asp:ImageButton ID="Printing" runat="server" OnClick="Printing_Click" CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="Stationary" runat="server" OnClick="Stationary_Click" CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="GraphicDesain" runat="server" OnClick="GraphicDesain_Click"
                                CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="Tiketing" runat="server" OnClick="Tiketing_Click" CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="Internet" runat="server" OnClick="Internet_Click" CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="Cafe" runat="server" OnClick="Cafe_Click" CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="Photocopy" runat="server" OnClick="Photocopy_Click" CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="Shipping" runat="server" OnClick="Shipping_Click" CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="EVoucher" runat="server" OnClick="EVoucher_Click" CausesValidation="false" />
                        </div>
                        <div>
                            <asp:ImageButton ID="VoucherHotel" runat="server" OnClick="VoucherHotel_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POSShipping.aspx.cs" Inherits="POS.POSInterface.Shipping.POSShipping" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Shipping</title>
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script src="../CSS/orange/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery.event.drag-1.5.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../CSS/orange/scrollsync.js"></script>

    <script type="text/javascript" src="../CSS/orange/dragscrollable.js"></script>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

    <script src="../CSS/orange/style3.js" type="text/javascript"></script>

    <script src="../CSS/orange/JScript.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/orange/style4.css" media="all" rel="Stylesheet" type="text/css" />
    <asp:Literal ID="JScriptLiteral" runat="server"></asp:Literal>

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

    <script type="text/javascript" language="javascript">
        function CancelPaid() {
            var _result = false;

            if (confirm("Are you sure want to cancel this order ?") == true) {
                _result = true;
            }
            else {
                _result = false;
            }

            return _result
        }
    </script>

    <asp:Literal runat="server" ID="javascriptReceiver"></asp:Literal>
</head>
<body id="bodyShipping">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:Literal runat="server" ID="CSSLiteral"></asp:Literal>
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        Shipping</div>
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
        <asp:Panel ID="PasswordPanel" runat="server">
            <div>
                <table cellpadding="3" cellspacing="1" width="0" border="0">
                    <tr>
                        <td style="width: 190px; font-size: 16px; text-align: left; color: White;">
                            <b>Password Required : </b>
                        </td>
                        <td>
                            <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="OKButton" runat="server" Text="OK" OnClick="OKButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="FormPanel">
            <div class="warning">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                <asp:Label runat="server" ID="WarningLabel"></asp:Label>
                <asp:HiddenField ID="StatusOpenHiddenField" runat="server" />
            </div>
            <div class="ContentForm">
                <div style="float: left;">
                    <table class="table1">
                        <tr>
                            <td>
                                REFERENCE
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ReferenceNoTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                <asp:HiddenField ID="TransRefHiddenField" runat="server" />
                                <asp:RequiredFieldValidator ID="ReferenceRequiredFieldValidator" runat="server" ControlToValidate="ReferenceNoTextBox"
                                    Text="*" ErrorMessage="Reference Must Be Filled" Font-Size="17px"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MEMBER
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="MemberIDTextBox" runat="server" CssClass="widthText" BackColor="#CCCCCC"
                                    OnTextChanged="MemberIDTextBox_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                NAME
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox CssClass="widthText" ID="CustNameTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CustNameRequiredFieldValidator" runat="server" ControlToValidate="CustNameTextBox"
                                    Text="*" ErrorMessage="Customer Name Invoice Must Be Filled" Font-Size="17px"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PHONE NUMBER
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustPhoneTextBox" CssClass="widthText" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left;">
                    <asp:ImageButton ID="NewMemberButton" CausesValidation="false" runat="server" OnClick="NewMemberButton_Click" />
                </div>
            </div>
            <div class="ContentForm1">
                <table class="table1">
                    <tr>
                        <td>
                            FROM
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 82px;">
                            Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SenderNameTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SenderNameRequiredFieldValidator" runat="server"
                                ControlToValidate="SenderNameTextBox" Text="*" ErrorMessage="Sender Name Must Be Filled"
                                Font-Size="17px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Address
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SenderAddressTextBox" runat="server" CssClass="widthText" BackColor="#CCCCCC"
                                Height="40"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SenderAddressRequiredFieldValidator" runat="server"
                                ControlToValidate="SenderAddressTextBox" Text="*" ErrorMessage="Sender Address Must Be Filled"
                                Font-Size="17px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Phone
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox CssClass="widthText" ID="SenderTelephoneTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SenderTelephoneRequiredFieldValidator" runat="server"
                                ControlToValidate="SenderTelephoneTextBox" Text="*" ErrorMessage="Sender Telephone Must Be Filled"
                                Font-Size="17px"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            City
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList CssClass="widthText" ID="SenderCityDDL" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Postal Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SenderPostalCodeTextBox" CssClass="widthText" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="ContentForm2">
                <div class="formRegister1">
                    <div style="float: left;">
                        <table class="table1">
                            <tr>
                                <td>
                                    DELIVERY TO
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 82px;">
                                    Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="DeliverNameTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="DeliverNameRequiredFieldValidator" runat="server"
                                        ControlToValidate="DeliverNameTextBox" Text="*" ErrorMessage="Deliver Name Must Be Filled"
                                        Font-Size="17px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Address
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="DeliverAddressTextBox" runat="server" CssClass="widthText" BackColor="#CCCCCC"
                                        Height="40"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="DeliverAddressRequiredFieldValidator" runat="server"
                                        ControlToValidate="DeliverAddressTextBox" Text="*" ErrorMessage="Deliver Address Must Be Filled"
                                        Font-Size="17px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Phone
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox CssClass="widthText" ID="DeliverTelephoneTextBox" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="DeliverTelephoneRequiredFieldValidator" runat="server"
                                        ControlToValidate="DeliverTelephoneTextBox" Text="*" ErrorMessage="Deliver Telephone Invoice Must Be Filled"
                                        Font-Size="17px"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Country
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="CountryDDL" runat="server" CssClass="widthText" AutoPostBack="true"
                                        OnSelectedIndexChanged="CountryDDL_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    City
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="DeliverCityDDL" runat="server" CssClass="widthText" AutoPostBack="true"
                                        OnSelectedIndexChanged="DeliverCityDDL_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Postal Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="DeliverPostalCodeTextBox" CssClass="widthText" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: left; margin: 122px 9px 9px;">
                        <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" CausesValidation="true" />
                    </div>
                </div>
            </div>
            <div class="ContentForm3" style="">
                <table class="table1">
                    <tr>
                        <td>
                            AirWay Bill
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="AirwayBillTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Vendor
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="VendorDDL" runat="server" CssClass="widthText" Width="170px"
                                Height="21px" AutoPostBack="true" OnSelectedIndexChanged="VendorDDL_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:HiddenField ID="FgZoneHiddenField" runat="server" />
                        </td>
                        <td>
                            Type
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="ShippingTypeDDL" runat="server" CssClass="widthText" Width="170px"
                                Height="21px" AutoPostBack="true" OnSelectedIndexChanged="ShippingTypeDDL_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Shipping Shape
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="ProductShapeRBL" runat="server" RepeatDirection="Vertical"
                                RepeatColumns="2" CssClass="SizeRB" AutoPostBack="true" OnSelectedIndexChanged="ProductShapeRBL_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Document" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Non Document"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RadioButtonList ID="ProductShapeWithIntPriorityRBL" runat="server" RepeatDirection="Vertical"
                                RepeatColumns="1" CssClass="SizeRB" OnSelectedIndexChanged="ProductShapeWithIntPriorityRBL_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Document" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Non Document"></asp:ListItem>
                                <asp:ListItem Value="2" Text="International Priority"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            Estimation Time
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Literal ID="EstimationTimeLiteral" runat="server"></asp:Literal>
                            <asp:HiddenField ID="UnitCodeHiddenField" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            1st Price
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Literal ID="Price1Literal" runat="server"></asp:Literal>
                            <asp:Literal ID="MaxLiteral" runat="server"></asp:Literal>
                        </td>
                        <td>
                            2nd Price
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Literal ID="Price2Literal" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Weight (KGS)
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="WeightTextBox" runat="server" CssClass="widthText" OnTextChanged="WeightTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="WeightRequiredFieldValidator" runat="server" ControlToValidate="WeightTextBox"
                                Text="*" ErrorMessage="Weight Must Be Filled" Font-Size="17px"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            Notes
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox TextMode="MultiLine" ID="NotesTextBox" Height="25px" runat="server"
                                CssClass="widthText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Discount
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="DiscountTextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Payment Type
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="PaymentTypeRBL" runat="server" RepeatDirection="Vertical"
                                RepeatColumns="2" CssClass="SizeRB">
                                <asp:ListItem Value="0" Text="Cash" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Cash On Delivery"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Transportation Related Surcharge
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ShippingTRSDDL" runat="server" CssClass="widthText" Width="170px"
                                Height="21px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="TRSTextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ShippingTRS2DDL" runat="server" CssClass="widthText" Width="170px"
                                Height="21px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="TRS2TextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ShippingTRS3DDL" runat="server" CssClass="widthText" Width="170px"
                                Height="21px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="TRS3TextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Other Surcharge
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ShippingOSDDL" runat="server" CssClass="widthText" Width="170px"
                                Height="21px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="OSTextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ShippingOS2DDL" runat="server" CssClass="widthText" Width="170px"
                                Height="21px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="OS2TextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ShippingOS3DDL" runat="server" CssClass="widthText" Width="170px"
                                Height="21px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="OS3TextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Insurance Cost
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="InsuranceCostTextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td>
                            Pack.Cost
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="PackagingCostTextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            SubTotal
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Literal ID="SubTotalLiteral" runat="server"></asp:Literal>
                        </td>
                        <td>
                            DFS Value
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Literal ID="DFSValueLiteral" runat="server"></asp:Literal>
                            <asp:HiddenField ID="DFSValueHiddenField" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Other Fee
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="OtherFeeTextBox" runat="server" CssClass="widthText" OnTextChanged="DiscountTextBox_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Total
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Literal ID="TotalLiteral" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td colspan="5px">
                            <div>
                                <asp:ImageButton ID="SaveImageButton" runat="server" OnClick="SaveImageButton_Click" />
                            </div>
                            <div>
                                <asp:ImageButton ID="ResetImageButton" runat="server" OnClick="ResetImageButton_Click" />
                            </div>
                            <div>
                                <asp:ImageButton ID="DeleteImageButton" runat="server" OnClick="DeleteImageButton_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="clear: both;">
                <div class="detail">
                    <div class="ContentView1">
                        <table class="table">
                            <tr>
                                <td>
                                    TRANSNMBR
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TransNoTextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Discount
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="DiscForexTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sub Total
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="AmountBaseTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <%--                            <tr>
                                <td>
                                    PPN
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PPNForexTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    Other Forex
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="OtherForexTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TotalForexTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="detail2">
                    <div class="ContentView2">
                        <div class="productDetailListBox">
                            <asp:HiddenField ID="ItemNoHiddenField" runat="server" />
                            <table cellpadding="3" cellspacing="1" width="0" border="0">
                                <tr style="">
                                    <td class="action" align="center">
                                        <b>Action</b>
                                    </td>
                                    <td class="bookingCode" align="center">
                                        <b>Vendor</b>
                                    </td>
                                    <td class="airLine" align="center">
                                        <b>Type</b>
                                    </td>
                                    <td class="bookingCode" align="center">
                                        <b>Location</b>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="table-container-scroll2">
                            <table class="dragger" cellpadding="3" cellspacing="1" width="0" border="0">
                                <asp:Repeater ID="DetailItemRepeater" runat="server" OnItemCommand="DetailItemRepeater_ItemCommand"
                                    OnItemDataBound="DetailItemRepeater_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="RepeaterTemplate" runat="server">
                                            <td class="action" align="center">
                                                <asp:ImageButton runat="server" ID="ViewButton" class="ViewButton" CausesValidation="false" />
                                            </td>
                                            <td class="bookingCode" align="center">
                                                <asp:Literal runat="server" ID="VendorLiteral"></asp:Literal>
                                            </td>
                                            <td class="airLine" align="center">
                                                <asp:Literal runat="server" ID="TypeLiteral"></asp:Literal>
                                            </td>
                                            <td class="bookingCode" align="center">
                                                <asp:Literal runat="server" ID="LocationLiteral"></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <div class="table-navigation-scroll2">
                            <div class="up">
                            </div>
                            <div class="down">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bottom-center">
                    <div class="Button1">
                        <asp:Button ID="BackButton" runat="server" ToolTip="Back" CausesValidation="false"
                            OnClick="BackButton_Click" />
                        <asp:Button ID="GotoCashierButton" runat="server" ToolTip="Go to Cashier" CausesValidation="false"
                            PostBackUrl="../General/Cashier.aspx" />
                        <asp:ImageButton runat="server" class="button-submit" ID="CashierAbuButton" />
                        <asp:Button ID="CancelAllButton" runat="server" ToolTip="Cancel All" CausesValidation="false"
                            OnClick="CancelAllButton_Click" />
                        <div class="clear">
                        </div>
                        <asp:Button ID="JoinJobOrderButton" runat="server" ToolTip="Join Job Order" CausesValidation="false"
                            OnClick="JoinJobOrderButton_Click" />
                        <asp:Button ID="CheckStatusButton" runat="server" ToolTip="Check Status" CausesValidation="false" />
                        <asp:Button ID="PrintPreviewButton" runat="server" ToolTip="Print Preview" CausesValidation="false"
                            OnClick="PrintPreviewButton_Click" />
                    </div>
                    <div class="clear">
                    </div>
                    <div class="Button2">
                        <asp:Button ID="SendToCashierButton" runat="server" ToolTip="Send To Cashier" OnClick="SendToCashierButton_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="ReasonListPanel">
            <div class="reasonList">
                <table cellpadding="3" cellspacing="1" width="0" border="0">
                    <tr class="bgproductListBox">
                        <td style="width: 40px" class="tahoma_11_white" align="center">
                            <b>Pick</b>
                        </td>
                        <td style="width: 345px" class="tahoma_11_white" align="center">
                            <b>Reason</b>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="table-container-scroll2">
                <table class="dragger2">
                    <asp:Repeater ID="ReasonListRepeater" runat="server" OnItemCommand="ReasonListRepeater_ItemCommand"
                        OnItemDataBound="ReasonListRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr style="width: 40px" class="tahoma_11_white" id="RepeaterTemplate" runat="server">
                                <td align="center" class="pickReasonButton">
                                    <asp:ImageButton ID="PickReasonImageButton" runat="server" />
                                </td>
                                <td style="width: 345px" class="tahoma_11_white">
                                    <asp:Literal runat="server" ID="ReasonLiteral"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="table-navigation-scroll2">
                <div class="up">
                </div>
                <div class="down">
                </div>
            </div>
            <div class="reasonback">
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            <asp:ImageButton runat="server" ID="Back2ImageButton" OnClick="Back2ImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>

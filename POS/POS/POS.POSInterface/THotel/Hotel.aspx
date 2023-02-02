<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hotel.aspx.cs" Inherits="POS.POSInterface.Hotel.Hotel"
    EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hotel</title>
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

    <script language="javascript" type="text/javascript">
        function Calculate3(_prmQty, _prmBasic, _prmTotal, _prmSellPrice, _prmDiscount) {

            var _tempQty = _prmQty.value
            if (isNaN(_tempQty) == true) {
                _tempQty = 0
            }

            var _tempBasic = _prmBasic.value
            if (isNaN(_tempBasic) == true) {
                _tempBasic = 0
            }

            _prmTotal.value = _tempQty * _tempBasic;
            _prmSellPrice.value = _prmTotal.value - _prmDiscount.value;

        }

        function Calculate2(_prmDiscount, _prmTotal, _prmSellPrice) {
            _prmSellPrice.value = _prmTotal.value - _prmDiscount.value;

        }          
    </script>

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

    <script language="javascript" type="text/javascript">
        function Calculate(_prmPPNPercent, _prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmDiscPercentage, _prmDiscForex, _prmPPNForex, _prmOtherForex, _prmTotalForex, _prmDecimalPlace) {
            var _tempPPNPercent = parseFloat(GetCurrency2(_prmPPNPercent.value, _prmDecimalPlace.value));

            if (isNaN(_tempPPNPercent) == true) {
                _tempPPNPercent = 0;
            }

            var _tempDiscPercentage = parseFloat(GetCurrency2(_prmDiscPercentage.value, _prmDecimalPlace.value));

            if (isNaN(_tempDiscPercentage) == true) {
                _tempDiscPercentage = 0;
            }

            var _tempAmountBase = parseFloat(GetCurrency2(_prmAmountBase.value, _prmDecimalPlace.value));

            if (isNaN(_tempAmountBase) == true) {
                _tempAmountBase = 0;
            }

            var _tempDiscForex = parseFloat(GetCurrency2(_prmDiscForex.value, _prmDecimalPlace.value));

            if (isNaN(_tempDiscForex) == true) {
                _tempDiscForex = 0;
            }

            var _tempOtherForex = parseFloat(GetCurrency2(_prmOtherForex.value, _prmDecimalPlace.value));

            if (isNaN(_tempOtherForex) == true) {
                _tempOtherForex = 0;
            }

            if (_tempPPNPercent > 0) {
                _prmPPNPercent.value = FormatCurrency2(_tempPPNPercent, _prmDecimalPlace.value);

                _prmPPNNo.readOnly = false;
                _prmPPNNo.style.background = '#FFFFFF';

                _prmPPNDate.readOnly = true;
                _prmCal.style.visibility = "visible";
            }
            else {
                _prmPPNPercent.value = FormatCurrency2(0, _prmDecimalPlace.value);

                _prmPPNNo.value = "";
                _prmPPNNo.readOnly = true;
                _prmPPNNo.style.background = '#CCCCCC';

                _prmPPNDate.value = "";
                _prmPPNDate.readOnly = true;
                _prmCal.style.visibility = "hidden";
            }

            _prmDiscPercentage = FormatCurrency2(_tempDiscPercentage, _prmDecimalPlace.value);

            _prmDiscForex.value = FormatCurrency2(_tempDiscForex, _prmDecimalPlace.value);

            _prmOtherForex.value = FormatCurrency2(_tempOtherForex, _prmDecimalPlace.value);

            if (_tempAmountBase > 0) {
                var _discForex = (_tempAmountBase * _tempDiscPercentage / 100);
                _prmDiscForex.value = FormatCurrency2(_discForex, _prmDecimalPlace.value);
                var _ppnForex = (_tempAmountBase - _tempDiscForex) * _tempPPNPercent / 100;
                _prmPPNForex.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);

                var _totalForex = _tempAmountBase + _ppnForex - _discForex + _tempOtherForex
                _prmTotalForex.value = FormatCurrency2(_totalForex, _prmDecimalPlace.value);

            }
            else {
                _prmDiscForex.value = FormatCurrency2(0, _prmDecimalPlace.value);

                _prmPPNForex.value = FormatCurrency2(0, _prmDecimalPlace.value);

                _prmTotalForex.value = FormatCurrency2(0, _prmDecimalPlace.value);

            }
        }
    </script>

    <asp:Literal runat="server" ID="javascriptReceiver"></asp:Literal>
    <asp:Literal runat="server" ID="javascriptReceiverDt"></asp:Literal>
    <asp:Literal runat="server" ID="javascriptReceiverJO"></asp:Literal>
    <asp:Literal runat="server" ID="javascriptReceiverCheck"></asp:Literal>
    <style type="text/css">
        #CustNameRequiredFieldValidator
        {
            display: none;
        }
        .Highlight
        {
            background-color: Red;
        }
    </style>
</head>
<body id="bodyTicketing">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
    <%-- <asp:UpdatePanel ID="contentupdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <asp:Literal runat="server" ID="CSSLiteral"></asp:Literal>
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        Hotel</div>
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
                <%--                <asp:ValidationSummary ID="ValidationSummary" runat="server" />--%>
                <asp:Label runat="server" ID="WarningLabel"></asp:Label>
                <asp:HiddenField ID="StatusOpenHiddenField" runat="server" />
            </div>
            <div class="ContentForm">
                <table class="table1">
                    <tr>
                        <td>
                            Reference
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ReferenceTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="CustomerDO Name Must Be Filled"
                            Text="*" ControlToValidate="NameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            <asp:HiddenField ID="ReferenceNoHiddenField" runat="server" />
                            <asp:RequiredFieldValidator ID="ReferenceRequiredFieldValidator" runat="server" ControlToValidate="ReferenceTextBox"
                                Text="*" ErrorMessage="Reference Must Be Filled"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
                                TargetControlID="ReferenceRequiredFieldValidator" HighlightCssClass="Highlight">
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
                            Customer Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="CustCodeTextBox" runat="server" CssClass="widthText" BackColor="#CCCCCC"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSearchCustomer" CssClass="SearchButtonForm" runat="server"
                                CausesValidation="false" OnClick="btnSearchCustomer_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox CssClass="widthText" ID="CustNameTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CustNameRequiredFieldValidator" runat="server" ControlToValidate="CustNameTextBox"
                                Text="*" ErrorMessage="Customer Name Invoice Must Be Filled"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2"
                                TargetControlID="CustNameRequiredFieldValidator" HighlightCssClass="Highlight">
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
                            Member Barcode
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="MemberBarcodeTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Telephone
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox CssClass="widthText" ID="CustPhoneTextBox" runat="server"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                            <asp:HiddenField runat="server" ID="DateTimeHiddenField" />
                        </td>
                    </tr>
                    <%-- <tr>
                    <td width="93px">
                        Currency
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="CurrCodeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CurrCodeDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="CurrCodeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                            ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrCodeDropDownList">
                        </asp:CustomValidator>
                        <asp:TextBox runat="server" ID="CurrRateTextBox" Width="80" MaxLength="23">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                            Text="*" ControlToValidate="CurrRateTextBox" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                      
                    </td>
                </tr>--%>
                    <tr>
                        <td>
                            PPN %
                        </td>
                        <td>
                            :
                        </td>
                        <td style="width: 110px">
                            <asp:TextBox ID="PPNPercentTextBox" MaxLength="23" runat="server" Width="55" CssClass="widthText">
                            </asp:TextBox>
                            No
                            <asp:TextBox ID="PPNNoTextBox" runat="server" Width="50" BackColor="#cccccc" CssClass="widthText">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            PPN Date
                        </td>
                        <td>
                            :
                        </td>
                        <td style="width: 110px">
                            <asp:TextBox ID="PPNDateTextBox" runat="server" Width="99" BackColor="#CCCCCC" CssClass="widthText">
                            </asp:TextBox>
                            <asp:Literal ID="PPNDateLiteral" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Rate
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="PPNRateTextBox" runat="server" MaxLength="23" Width="100" BackColor="#cccccc"
                                CssClass="widthText">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Discount %
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="DiscountPercentageTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Other
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="OtherForexTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<asp:UpdatePanel ID="PaymentUpdatePanel" runat="server">
                    <ContentTemplate>
                        <tr>
                            <td>
                                Payment Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="PaymentTypeDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PaymentTypeDDL_SelectedIndexChanged">
                                    <asp:ListItem Text="Account Receiveable" Value="AR"></asp:ListItem>
                                    <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PaymentTypeDDLCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Payment Type Must Be Filled" Text="*" ControlToValidate="PaymentTypeDDL">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="paymentDropDownList" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                    <%--           <tr>
                    <td>
                        Sales
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="SalesDropDownList" runat="server">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="SalesCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                            ErrorMessage="Sales Must Be Filled" Text="*" ControlToValidate="SalesDropDownList">
                        </asp:CustomValidator>
                    </td>
                </tr>--%>
                    <tr>
                        <td valign="top">
                            Remark
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox TextMode="MultiLine" Height="56" CssClass="widthText" ID="RemarkTextBox"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="clear: both;">
                    <div style="margin-left: 80px; float: left;">
                        <asp:ImageButton ID="SaveHeaderButton" runat="server" OnClick="SaveHeaderButton_Click" />
                    </div>
                    <div style="margin-left: 8px; float: left;">
                        <%--<asp:ImageButton ID="EditHeaderButton" runat="server" Text="Back" OnClick="EditHeaderButton_Click" />--%>
                    </div>
                    <div style="margin-left: 8px; float: left;">
                        <asp:ImageButton ID="ResetHeaderButton" runat="server" CausesValidation="false" OnClick="ResetHeaderButton_Click" />
                    </div>
                </div>
            </div>
            <div class="ContentForm2">
                <div class="formRegister1">
                    <div class="left">
                        <div class="title">
                            <div class="text">
                                Detail</div>
                            <div class="sep">
                            </div>
                        </div>
                    </div>
                    <table class="table1">
                        <tr>
                            <td>
                                Voucher Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="VoucherCodeTextBox" runat="server" MaxLength="20" CssClass="widthText"
                                    OnTextChanged="VoucherCodeTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <%--       <asp:RequiredFieldValidator ID="VoucherCodeRequiredFieldValidator" runat="server"
                                ErrorMessage="Voucher Code Must Be Filled" Text="*" ControlToValidate="VoucherCodeTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                <asp:HiddenField runat="server" ID="TransNoHiddenField" />
                                <asp:HiddenField runat="server" ID="ItemNoHiddenField" />
                            </td>
                        </tr>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Supplier Name
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SupplierNameDLL" CssClass="widthText" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="SupplierNameDDL_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="SuplierCodeHiddenField" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Hotel Code
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="HotelNameDDL" CssClass="widthText" runat="server">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="HotelHiddenField" runat="server" />
                                        <asp:HiddenField runat="server" ID="HiddenField1" />
                                        <br />
                                        <%--<asp:TextBox ID="HotelTextBox" runat="server" MaxLength="20" CssClass="widthText"
                                    BackColor="#CCCCCC"></asp:TextBox>--%>
                                        <%--<asp:RequiredFieldValidator ID="HotelRequiredFieldValidator" runat="server" ErrorMessage="Hotel Must Be Filled"
                                Text="*" ControlToValidate="HotelTextBox" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <%--<td>
                                <asp:ImageButton ID="btnSearchHotel" CssClass="SearchButtonForm" runat="server" CausesValidation="false"
                                    OnClick="btnSearchHotel_Click" />
                            </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox CssClass="widthText" runat="server" ID="HotelNameTextBox" ReadOnly="true"
                                            BackColor="#CCCCCC" Visible="false"></asp:TextBox>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Check In
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CheckInDateTextBox" BackColor="#CCCCCC" CssClass="widthText"></asp:TextBox>
                                <asp:Literal ID="CheckInDateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Check Out
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CheckOutDateTextBox" BackColor="#CCCCCC" CssClass="widthText"></asp:TextBox>
                                <asp:Literal ID="CheckOutDateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Guest
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QtyRoomTextBox" MaxLength="23" CssClass="widthText"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRoomRequiredFieldValidator" runat="server" ErrorMessage="Total Guest Must Be Filled"
                                    Text="*" ControlToValidate="QtyRoomTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Basic Fare
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BasicFareTextBox" MaxLength="23" CssClass="widthText"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="BasicFareRequiredFieldValidator" runat="server" ErrorMessage="Basic Fare Must Be Filled"
                                    Text="*" ControlToValidate="BasicFareTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="TotalTextBox" MaxLength="23" BackColor="#CCCCCC"
                                    CssClass="widthText"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TotalRequiredFieldValidator" runat="server" ErrorMessage="Total Must Be Filled"
                                    Text="*" ControlToValidate="TotalTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="DiscountTextBox" runat="server" MaxLength="23" CssClass="widthText"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DiscountRequiredFieldValidator" runat="server" ErrorMessage="Discount Must Be Filled"
                                    Text="*" ControlToValidate="DiscountTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Selling Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SellingPriceTextBox" runat="server" MaxLength="23" BackColor="#CCCCCC"
                                    CssClass="widthText"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SellingPriceRequiredFieldValidator1" runat="server"
                                    ErrorMessage="Selling Price Must Be Filled" Text="*" ControlToValidate="SellingPriceTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Buying Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BuyingPriceTextBox" MaxLength="23" CssClass="widthText"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="BuyingPriceRequiredFieldValidator" runat="server"
                                    ErrorMessage="Buying Price Must Be Filled" Text="*" ControlToValidate="BuyingPriceTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="formRegister2">
                    <table class="table2">
                        <tr>
                            <td valign="top">
                                Guest Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox TextMode="MultiLine" ID="GuestTextBox" runat="server" CssClass="widthText"
                                    Height="80px" Width="153px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Voucher Information
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox TextMode="MultiLine" ID="VoucherInformationTextBox" runat="server" CssClass="widthText"
                                    Height="80px" Width="153px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox TextMode="MultiLine" ID="RemarkDtTextBox" runat="server" CssClass="widthText"
                                    Height="80px" Width="153px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="margin-top: 70px; padding-left: 0px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                </td>
                                <td style="padding-left: 6px;">
                                    <%--<asp:ImageButton ID="DeleteButton" runat="server" Text="Back" CausesValidation="false" />
--%>
                                </td>
                                <td style="padding-left: 6px;">
                                    <asp:ImageButton ID="ResetButton" runat="server" Text="Back" CausesValidation="false"
                                        OnClick="ResetButton_Click2" />
                                </td>
                                <td style="padding-left: 6px;">
                                    <asp:ImageButton runat="server" ID="DeleteButton2" class="DeleteButton" CausesValidation="false"
                                        OnClick="DeleteButton2_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div style="clear: both;">
                <div class="detail">
                    <div class="ContentView1">
                        <table class="table">
                            <tr>
                                <td>
                                    Trans No
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <%--<asp:Label ID="TransNoLabel" runat="server"></asp:Label>--%>
                                    <asp:TextBox ID="TransNoTextBoxt" runat="server" Width="191px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Base Forex
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="AmountBaseTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Discount Forex
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
                                    PPN Forex
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PPNForexTextBox" runat="server" CssClass="widthText"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total Forex
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
                <%--       <div class="detailTitle">
                <div class="left">
                    <div class="title">
                        <div class="text">
                            Detail</div>
                        <div class="sep">
                        </div>
                    </div>
                </div>
            </div>--%>
                <div class="detail2">
                    <div class="ContentView2">
                        <div class="productDetailListBox">
                            <table cellpadding="3" cellspacing="1" width="0" border="0">
                                <tr>
                                    <td class="action" align="center">
                                        <b>Action</b>
                                    </td>
                                    <td class="bookingCode" align="center">
                                        <b>Voucher Code</b>
                                    </td>
                                    <td class="airLine" align="center">
                                        <b>Hotel</b>
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
                                                <asp:ImageButton runat="server" ID="ViewButton" class="ViewButton" />
                                            </td>
                                            <td class="bookingCode">
                                                <asp:Literal runat="server" ID="VoucherCodeLiteral"></asp:Literal>
                                            </td>
                                            <td class="airLine" align="center">
                                                <asp:Literal runat="server" ID="HotelLiteral"></asp:Literal>
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
                        <%--<asp:Button ID="InvoiceButton" runat="server" ToolTip="Invoice" CausesValidation="false" />--%>
                        <asp:Button ID="JoinJobOrderButton" runat="server" ToolTip="Join Job Order" CausesValidation="false"
                            OnClick="JoinJobOrderButton_Click" />
                        <asp:Button ID="CheckStatusButton" runat="server" ToolTip="CheckStatus" CausesValidation="false"
                            OnClick="CheckStatusButton_Click" />
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
        <div style="background-color: white; background-image: none;">
            <asp:Panel runat="server" ID="PrintPreviewPanel">
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td style="text-align: right;">
                            <input type="button" value="Go Back" onclick="history.back()" style="height: 40px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="ReportViewer1" Width="100%" runat="server" Height="650px">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
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
                    <%--<asp:HiddenField ID="TransNmbrHiddenField" runat="server" />--%>
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
                            <%--<input type="button" value="Go Back" onclick="history.back()" style="height: 40px;">--%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>

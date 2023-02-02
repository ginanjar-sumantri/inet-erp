<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="HotelEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.THotel.HotelEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

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

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
    <asp:Literal ID="javascriptReceiver2" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td width="120px">
                                            Transaction Number
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TransNmbrTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                                Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            File No
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="FileNoTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                                Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Trans Date
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                            <%--<input id="headline_date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                            <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td width="15px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            Payment Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <%--<asp:UpdatePanel ID="PaymentTypeUpdatePanel" runat="server">
                                                <ContentTemplate>--%>
                                            <asp:DropDownList ID="PaymentTypeDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PaymentTypeDDL_SelectedIndexChanged">
                                                <asp:ListItem Text="Account Receiveable" Value="AR"></asp:ListItem>
                                                <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="PaymentTypeDDLCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                                ErrorMessage="Payment Type Must Be Filled" Text="*" ControlToValidate="PaymentTypeDDL">
                                            </asp:CustomValidator>
                                            <%-- </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Branch
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="BranchDropDownList" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Payment
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="PaymentUpdatePanel" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="paymentDropDownList" runat="server">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
                                            <asp:TextBox ID="CustCodeTextBox" runat="server"></asp:TextBox>
                                            <asp:Button ID="btnSearchCustomer" runat="server" Text="..." CausesValidation="False" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
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
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer Name
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CustNameTextBox" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="CustNameRequiredFieldValidator" runat="server" ControlToValidate="CustNameTextBox"
                                                Text="*" ErrorMessage="Customer Name Invoice Must Be Filled"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            Remark
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td rowspan="2">
                                            <asp:TextBox ID="RemarkTextBox" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                                            <asp:HiddenField runat="server" ID="MemberBarcodeHiddenField" OnValueChanged="MemberBarcodeHiddenField_OnValueChanged" />
                                            <asp:TextBox runat="server" ID="MemberBarcodeTextBox"></asp:TextBox>
                                            <asp:Button ID="btnSearchMember" runat="server" CausesValidation="false" Text="..." />
                                            <%--  <asp:RequiredFieldValidator ID="CashierCodeRequiredFieldValidator" runat="server"
                                                ErrorMessage="Cashier Code Must Be Filled" Text="*" ControlToValidate="CashierCodeTextBox"
                                                Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer Phone
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CustPhoneTextBox" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <tr>
                                                <td width="120px">
                                                    Currency / Rate
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
                                                    <asp:TextBox runat="server" ID="CurrRateTextBox" Width="150" MaxLength="23">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="CurrRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                                                        Text="*" ControlToValidate="CurrRateTextBox" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table width="0">
                                                        <tr class="bgcolor_gray" height="20">
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>PPN %</b>
                                                            </td>
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>No.</b>
                                                            </td>
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>Date</b>
                                                            </td>
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>Rate</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    PPN
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="PPNPercentTextBox" MaxLength="23" runat="server" Width="100">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="PPNNoTextBox" runat="server" Width="100" BackColor="#cccccc">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="PPNDateTextBox" runat="server" Width="70" BackColor="#CCCCCC">
                                                                </asp:TextBox>
                                                                <%--<input id="ppn_date_start" runat="server" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_PPNDateTextBox,'yyyy-mm-dd',this)"
                                                        value="..." style="visibility: hidden; width: 20px;" />--%>
                                                                <asp:Literal ID="PPNDateLiteral" runat="server"></asp:Literal>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="PPNRateTextBox" runat="server" MaxLength="23" Width="100" BackColor="#cccccc">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table width="0">
                                                        <tr class="bgcolor_gray" height="20">
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>Discount %</b>
                                                            </td>
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>Discount Forex</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Discount
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="DiscountPercentageTextBox" MaxLength="23" runat="server" Width="100">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="DiscForexTextBox" runat="server" Width="100" BackColor="#CCCCCC">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr class="bgcolor_gray" height="20">
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>Currency</b>
                                                            </td>
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>Base Forex</b>
                                                            </td>
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>PPN Forex</b>
                                                            </td>
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>Other Forex</b>
                                                            </td>
                                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                                <b>Total Forex</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Amount
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="AmountBaseTextBox" MaxLength="23" runat="server" Width="100" BackColor="#CCCCCC">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="PPNForexTextBox" MaxLength="23" runat="server" Width="100" BackColor="#CCCCCC">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="OtherForexTextBox" MaxLength="23" runat="server" Width="100px">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td style="width: 110px" align="center">
                                                                <asp:TextBox ID="TotalForexTextBox" MaxLength="23" runat="server" Width="100px" BackColor="#CCCCCC">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" CausesValidation="False"
                                    OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

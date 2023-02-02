﻿<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerInvoiceEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.CustomerInvoice.CustomerInvoiceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function CalculateDiscountPercent(_prmPPNPercent,_prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmDiscPercent, _prmDiscForex, _prmPPNForex, _prmStampFee, _prmOtherFee, _prmCommissionExpense, _prmTotalForex, _prmDecimalPlace)
        {
            var _tempDiscPercent = parseFloat(GetCurrency2((_prmDiscPercent.value == "") ? "0" : _prmDiscPercent.value), _prmDecimalPlace.value);
            if(isNaN(_tempDiscPercent) == true)
            {
                _tempDiscPercent = 0;
            }
            var _tempBaseForex = parseFloat(GetCurrency2((_prmAmountBase.value == "") ? "0" : _prmAmountBase.value), _prmDecimalPlace.value);
            if(isNaN(_tempBaseForex) == true)
            {
                _tempBaseForex = 0;
            }
            var _tempDiscForex = parseFloat(GetCurrency2((_prmDiscForex.value == "") ? "0" : _prmDiscForex.value), _prmDecimalPlace.value);
            if(isNaN(_tempDiscForex) == true)
            {
                _tempDiscForex = 0;
            }
            
            if (_tempDiscPercent > 0)
            {
                _prmDiscForex.readOnly = true;
                _prmDiscForex.style.background = '#CCCCCC';
            }
            else
            {
                _prmDiscForex.readOnly = false;
                _prmDiscForex.style.background = '#FFFFFF';
            }
            _prmDiscPercent.value = (_tempDiscPercent == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDiscPercent, _prmDecimalPlace.value);
            
            var _discountForex = _tempBaseForex * _prmDiscPercent.value / 100 ;
            _prmDiscForex.value = (_discountForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_discountForex, _prmDecimalPlace.value);

            Calculate(_prmPPNPercent,_prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmDiscPercent,_prmDiscForex, _prmPPNForex, _prmStampFee, _prmOtherFee, _prmCommissionExpense, _prmTotalForex, _prmDecimalPlace);
        }
        

        function CalculateDiscountForex(_prmPPNPercent,_prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmDiscPercent, _prmDiscForex, _prmPPNForex, _prmStampFee, _prmOtherFee, _prmCommissionExpense, _prmTotalForex, _prmDecimalPlace)
        {
            var _tempBaseForex = parseFloat(GetCurrency2((_prmAmountBase.value == "") ? "0" : _prmAmountBase.value), _prmDecimalPlace.value);
            if(isNaN(_tempBaseForex) == true)
            {
                _tempBaseForex = 0;
            }
            var _tempDiscForex = parseFloat(GetCurrency2((_prmDiscForex.value == "") ? "0" : _prmDiscForex.value), _prmDecimalPlace.value);
            if(isNaN(_tempDiscForex) == true)
            {
                _tempDiscForex = 0;
            }

            _prmDiscForex.value = (_tempDiscForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDiscForex, _prmDecimalPlace.value);

            if(_prmDiscPercent.value == "0" || _prmDiscPercent.value == "")
            {
                _prmDiscPercent.value = "0";
            }
           
            Calculate(_prmPPNPercent,_prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmDiscPercent, _prmDiscForex, _prmPPNForex, _prmStampFee, _prmOtherFee, _prmCommissionExpense, _prmTotalForex, _prmDecimalPlace);
        }
        
        function Calculate(_prmPPNPercent,_prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmDiscPercent, _prmDiscForex, _prmPPNForex, _prmStampFee, _prmOtherFee, _prmCommissionExpense, _prmTotalForex, _prmDecimalPlace)
        {
            var _tempPPNPercent = parseFloat(GetCurrency2(_prmPPNPercent.value, _prmDecimalPlace.value));
            if(isNaN(_tempPPNPercent) == true)
            {
                _tempPPNPercent = 0;
            }
            
            var _tempAmountBase = parseFloat(GetCurrency2(_prmAmountBase.value, _prmDecimalPlace.value));
            if(isNaN(_tempAmountBase) == true)
            {
                _tempAmountBase = 0;
            }
            
            var _tempDiscPercent = parseFloat(GetCurrency2(_prmDiscPercent.value, _prmDecimalPlace.value));
            if(isNaN(_tempDiscPercent) == true)
            {
                _tempDiscPercent = 0;
            }
            
            var _tempDiscForex = parseFloat(GetCurrency2(_prmDiscForex.value, _prmDecimalPlace.value));
            if(isNaN(_tempDiscForex) == true)
            {
                _tempDiscForex = 0;
            }
            
            var _tempStampFee = parseFloat(GetCurrency2(_prmStampFee.value, _prmDecimalPlace.value));
            if(isNaN(_tempStampFee) == true)
            {
                _tempStampFee = 0;
            }
            
            var _tempOtherFee = parseFloat(GetCurrency2(_prmOtherFee.value, _prmDecimalPlace.value));
            if(isNaN(_tempOtherFee) == true)
            {
                _tempOtherFee = 0;
            }
            
            var _tempCommissionExpense = parseFloat(GetCurrency2(_prmCommissionExpense.value, _prmDecimalPlace.value));
            if(isNaN(_tempCommissionExpense) == true)
            {
                _tempCommissionExpense = 0;
            }
            
            if(_tempPPNPercent > 0)
            {
                _prmPPNPercent.value = FormatCurrency2(_tempPPNPercent, _prmDecimalPlace.value);
                
                _prmPPNNo.readOnly = false;
                _prmPPNNo.style.background = '#FFFFFF';
                
                _prmPPNDate.readOnly = true;
                _prmCal.style.visibility = "visible";
            }
            else
            {
                _prmPPNPercent.value =  FormatCurrency2(0, _prmDecimalPlace.value);
                
                _prmPPNNo.value = "";            
                _prmPPNNo.readOnly = true;
                _prmPPNNo.style.background = '#CCCCCC';
                
                _prmPPNDate.value = "";
                _prmPPNDate.readOnly = true;
                _prmCal.style.visibility = "hidden";
            }
            
            _prmDiscPercent.value = FormatCurrency2(_tempDiscPercent, _prmDecimalPlace.value);
            _prmDiscForex.value = FormatCurrency2(_tempDiscForex, _prmDecimalPlace.value);
            _prmStampFee.value = FormatCurrency2(_tempStampFee, _prmDecimalPlace.value);
            _prmOtherFee.value = FormatCurrency2(_tempOtherFee, _prmDecimalPlace.value);
            _prmCommissionExpense.value = FormatCurrency2(_tempCommissionExpense, _prmDecimalPlace.value);
                
            if(_tempAmountBase > 0)
            {
                var _ppnForex = (_tempAmountBase - _tempDiscForex)  * _tempPPNPercent / 100;
                _prmPPNForex.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);
                
                var _totalForex = _tempAmountBase + GetCurrency2(_ppnForex, _prmDecimalPlace.value) - _tempDiscForex + _tempStampFee + _tempOtherFee - _tempCommissionExpense;
                _prmTotalForex.value = FormatCurrency2(_totalForex, _prmDecimalPlace.value);
            }
            else
            {
                _prmPPNForex.value = FormatCurrency2(0, _prmDecimalPlace.value);
                
                _prmTotalForex.value = FormatCurrency2(0, _prmDecimalPlace.value);
            }
        }
        
        function ValidatePeriod(_prmPeriod)
        {
            var _tempPeriod = _prmPeriod.value;
            if (parseInt(_tempPeriod) < 1 || parseInt(_tempPeriod) > 12)
            {
                _prmPeriod.value = "";
            }
        }
        
        function ValidateYear(_prmYear)
        {
            var _tempYear = _prmYear.value;
            if (parseInt(_tempYear) < 1 || parseInt(_tempYear) > 9999)
            {
                _prmYear.value = "";
            }
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TransactionNoTextBox" Width="150" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
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
                                <input id="DateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Period
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PeriodTextBox" Width="50" MaxLength="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PeriodRequiredFieldValidator" runat="server" ControlToValidate="PeriodTextBox"
                                    Display="Dynamic" ErrorMessage="Period Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Year
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="YearTextBox" Width="50" MaxLength="4"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="YearRequiredFieldValidator" runat="server" ControlToValidate="YearTextBox"
                                    Display="Dynamic" ErrorMessage="Year Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="CustomerDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomerCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Customer Must Be Filled" Text="*" ControlToValidate="CustomerDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Due Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DueDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <input id="DueDateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DueDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Attn
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AttnTextBox" Width="280" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Term
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="TermDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="TermCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Term Must Be Filled" Text="*" ControlToValidate="TermDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TypeTextbox" Width="280" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency / Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CurrCodeTextbox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <asp:TextBox runat="server" ID="ForexRateTextBox" MaxLength="23" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ForexRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                                    Text="*" ControlToValidate="ForexRateTextBox" Display="Dynamic">
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
                                            </asp:TextBox>&nbsp;
                                            <input id="ppn_date_start" runat="server" type="button" style="visibility: hidden;
                                                width: 20px;" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_PPNDateTextBox,'yyyy-mm-dd',this)"
                                                value="..." />
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="PPNRateTextBox" MaxLength="23" runat="server" Width="100" BackColor="#cccccc">
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
                                            <b>Discount %</b>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>Discount Forex</b>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>PPN Forex</b>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>Other Fee</b>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>Stamp Fee</b>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>Commission Expense</b>
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
                                            <asp:TextBox ID="AmountBaseTextBox" runat="server" Width="100" BackColor="#CCCCCC">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="DiscPercentTextBox" MaxLength="23" runat="server" Width="100">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="DiscForexTextBox" MaxLength="23" runat="server" Width="100">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" BackColor="#CCCCCC">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="OtherFeeTextBox" MaxLength="23" runat="server" Width="100">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="StampFeeTextBox" MaxLength="23" runat="server" Width="100">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="CommissionExpenseTextBox" MaxLength="23" runat="server" Width="100">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100px" BackColor="#CCCCCC">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade.ReceiptTradeCrEdit, App_Web_hbijsilb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script type="text/javascript" language="javascript">
        function CalculateAmountBalance(_prmARToBePaid, _prmPPNToBePaid, _prmAmountToBePaid, _prmAmountForex, _prmAmountBalance, _prmPPN, _prmPPNBalance, _prmTotalAmount, _prmTotalAmountBalance, _prmDecimalPlace, _prmDecimalPlace2) {
            var _tempARToBePaid = parseFloat(GetCurrency2(_prmARToBePaid.value, _prmDecimalPlace.value));
            if (isNaN(_tempARToBePaid) == true) {
                _tempARToBePaid = 0;
            }
            
            var _tempPPNToBePaid = parseFloat(GetCurrency2(_prmPPNToBePaid.value, _prmDecimalPlace.value));
            if (isNaN(_tempPPNToBePaid) == true) {
                _tempPPNToBePaid = 0;
            }
            
            var _tempAmountToBePaid = parseFloat(GetCurrency2(_prmAmountToBePaid.value, _prmDecimalPlace.value));
            if (isNaN(_tempAmountToBePaid) == true) {
                _tempAmountToBePaid = 0;
            }
            
            var _tempAmountForex = parseFloat(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value));
            if (isNaN(_tempAmountForex) == true) {
                _tempAmountForex = 0;
            }
            
            var _tempPPN = parseFloat(GetCurrency2(_prmPPN.value, _prmDecimalPlace.value));
            if (isNaN(_tempPPN) == true) {
                _tempPPN = 0;
            }

            var _tempAmountBalance = _tempARToBePaid - _tempAmountForex;
            _prmAmountBalance.value = FormatCurrency2(_tempAmountBalance, _prmDecimalPlace.value);

            var _tempPPNBalance = _tempPPNToBePaid - _tempPPN;
            _prmPPNBalance.value = FormatCurrency2(_tempPPNBalance, _prmDecimalPlace.value);
            
            var _tempTotalAmount = _tempAmountForex + _tempPPN;
            _prmTotalAmount.value = FormatCurrency2(_tempTotalAmount, _prmDecimalPlace.value);
            
            var _tempTotalAmountBalance = _tempAmountToBePaid - _tempTotalAmount;
            _prmTotalAmountBalance.value = FormatCurrency2(_tempTotalAmountBalance, _prmDecimalPlace.value);
            
            _prmAmountForex.value = FormatCurrency2(_tempAmountForex, _prmDecimalPlace.value);
            _prmPPN.value = FormatCurrency2(_tempPPN, _prmDecimalPlace.value);
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
                                Invoice No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="InvoiceNoTextBox" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="40" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField2" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                AR
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr style="height: 10px">
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AR Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AR Already Paid</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AR to be Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="ARInvoiceTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ARBalanceTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ARToBePaidTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                PPN
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr style="height: 10px">
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Rate</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNRateTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                            </td>
                            <td valign="top">
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Already Paid</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN to be Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNInvoiceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNBalanceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNToBePaidTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Total Amount
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr style="height: 10px">
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Already Paid</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount to be Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="AmountInvoiceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="AmountBalanceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="AmountToBePaidTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Receipt Amount AR
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Balance</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReceiptAmountForexTextBox"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReceiptAmountBalanceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Receipt Amount PPN
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Balance</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReceiptPPNTextBox"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReceiptPPNBalanceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Receipt Amount Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Balance</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="TotalAmountForexTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TotalAmountBalanceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Value
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FgValueTextBox" Width="50" BackColor="#CCCCCC"></asp:TextBox>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

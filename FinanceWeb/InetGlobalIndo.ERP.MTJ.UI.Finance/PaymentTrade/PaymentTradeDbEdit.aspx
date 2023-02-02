<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PaymentTradeDbEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade.PaymentTradeDbEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" language="javascript">
        function CalculateAmountHome(_prmAmountForex, _prmForexRate, _prmPPNPaid, _prmARPaid, _prmPPNRate, _prmAmountHome, _prmDecimalPlace, _prmDecimalPlace2) {
            var _tempAmountForex = parseFloat(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value));
            if (isNaN(_tempAmountForex) == true) {
                _tempAmountForex = 0;
            }

            var _tempForexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPlace.value));
            if (isNaN(_tempForexRate) == true) {
                _tempForexRate = 0;
            }

            var _tempPPNPaid = parseFloat(GetCurrency2(_prmPPNPaid.value, _prmDecimalPlace.value));
            if (isNaN(_tempPPNPaid) == true) {
                _tempPPNPaid = 0;
            }

            var _tempPPNRate = parseFloat(GetCurrency2(_prmPPNRate.value, _prmDecimalPlace.value));
            if (isNaN(_tempPPNRate) == true) {
                _tempPPNRate = 0;
            }

            _prmAmountForex.value = FormatCurrency2(_tempAmountForex, _prmDecimalPlace.value);
            _prmPPNPaid.value = FormatCurrency2(_tempPPNPaid, _prmDecimalPlace.value);

            var _arPaid = _tempAmountForex - _tempPPNPaid;
            _prmARPaid.value = FormatCurrency2(_arPaid, _prmDecimalPlace.value);

            var _amountHome = ((_arPaid * _tempForexRate) + (_tempPPNPaid * _tempPPNRate));
            _prmAmountHome.value = FormatCurrency2(_amountHome, _prmDecimalPlace2.value);
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
                                <asp:TextBox runat="server" ID="InvoiceNoTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="50" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox ID="ForexRateTextBox" runat="server" BackColor="#CCCCCC" Width="100"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Amount
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Forex</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Balance</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Home</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="AmountForexTextBox" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="AmountInvoiceTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="AmountBalanceTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="AmountHomeTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField2" />
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
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Rate</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Balance</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNRateTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNInvoiceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNBalanceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNPaidTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                AP
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr>
                                        <td style="width: 100%;">
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AP Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AP Balance</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AP Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="APInvoiceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="APBalanceTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="APPaidTextBox" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
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

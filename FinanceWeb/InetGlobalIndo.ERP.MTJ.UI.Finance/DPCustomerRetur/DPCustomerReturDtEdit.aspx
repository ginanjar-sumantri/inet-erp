<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DPCustomerReturDtEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerRetur.DPCustomerReturDtEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script type="text/javascript" language="javascript">
        function CalculateTotalForexFromBase(_prmBaseForex, _prmPPN, _prmPPNForex, _prmTotalForex, _prmDecimalPlace) {
            var _a = parseFloat(GetCurrency2(_prmBaseForex.value, _prmDecimalPlace.value));
            if (isNaN(_a) == true) {
                _a = 0;
            }
            var _b = parseFloat(GetCurrency2(_prmPPNForex.value, _prmDecimalPlace.value));
            if (isNaN(_b) == true) {
                _b = 0;
            }
            var _c = parseFloat(GetCurrency2(_prmPPN.value, _prmDecimalPlace.value));
            if (isNaN(_c) == true) {
                _c = 0;
            }

            var _ppnForex = _a * (_c / 100);
            _b = _ppnForex;

            var _totalForex = _a + _b;
            _prmPPNForex.value = (_b == 0) ? "0" : FormatCurrency2(_b, _prmDecimalPlace.value);
            _prmBaseForex.value = (_a == 0) ? "0" : FormatCurrency2(_a, _prmDecimalPlace.value);
            //              _prmTotalForex.value = _totalForex;
            _prmTotalForex.value = (_totalForex == 0) ? "0" : FormatCurrency2(_totalForex, _prmDecimalPlace.value);
        }

        function CalculateTotalForex(_prmBaseForex, _prmPPNForex, _prmTotalForex, _prmDecimalPlace) {
            var _a = parseFloat(GetCurrency(_prmBaseForex.value));
            if (isNaN(_a) == true) {
                _a = 0;
            }
            var _b = parseFloat(GetCurrency2(_prmPPNForex.value, _prmDecimalPlace.value));
            if (isNaN(_b) == true) {
                _b = 0;
            }

            var _totalForex = _a + _b;
            _prmPPNForex.value = (_b == 0) ? "0" : FormatCurrency2(_b, _prmDecimalPlace.value);
            _prmBaseForex.value = (_a == 0) ? "0" : FormatCurrency2(_a, _prmDecimalPlace.value);
            //_prmPPNForex.value = FormatCurrency(_b);
            //            _prmTotalForex.value = _totalForex;
            _prmTotalForex.value = (_totalForex == 0) ? "0" : FormatCurrency2(_totalForex, _prmDecimalPlace.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel DefaultButton="SaveButton" ID="Panel1" runat="server">
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
                                DP No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DPNoTextBox" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
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
                                            <b>Base Forex</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN %</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Forex</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Total Forex</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="BaseForexTextBox"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNForexTextBox"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TotalForexTextBox" BackColor="#CCCCCC"></asp:TextBox>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

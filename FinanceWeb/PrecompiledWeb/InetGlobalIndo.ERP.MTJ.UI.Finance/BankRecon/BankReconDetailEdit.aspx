<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.BankRecon.BankReconDetailEdit, App_Web_oti40ckn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Calculate(_prmForexRate, _prmAmountForex, _prmDecimalPlace) {
            var _forexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPlace.value));
            if (isNaN(_forexRate) == true) { _forexRate = 0; }
            var _amountForex = parseFloat(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value));
            if (isNaN(_amountForex) == true) { _amountForex = 0; }

            if (_forexRate < 1) {
                _prmForexRate.value = FormatCurrency2(1, _prmDecimalPlace.value);
            }
            else {
                _prmForexRate.value = FormatCurrency2(_forexRate, _prmDecimalPlace.value);
            }
            _prmAmountForex.value = FormatCurrency2(_amountForex, _prmDecimalPlace.value);
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
                                Account
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountNameTextBox" Width="350" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sub Ledger
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="SubLedgerDropDownList">
                                </asp:DropDownList>
                                <asp:HiddenField runat="server" ID="FgSubledHiddenField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Forex Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ForexRateTextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ForexRateRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Forex Rate Must Be Filled" ControlToValidate="ForexRateTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fg Value
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FgValueDDL" Width="40">
                                    <asp:ListItem Text="+" Value="+" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AmountForexTextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AmountForexRequiredFieldValidator" runat="server"
                                    Text="*" ErrorMessage="Amount Forex Must Be Filled" ControlToValidate="AmountForexTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
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

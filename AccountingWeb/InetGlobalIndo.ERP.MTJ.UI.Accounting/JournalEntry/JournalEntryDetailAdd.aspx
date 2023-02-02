<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="JournalEntryDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.JournalEntry.JournalEntryDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Clear(_prmForexRate, _prmDebitForex, _prmDebitHome, _prmCreditForex, _prmCreditHome, _prmDecimalPalce) {
            var _tempForexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPalce.value));
            if (isNaN(_tempForexRate) == true) {
                _tempForexRate = 0;
            }

            if (_tempForexRate < 1) {
                _prmForexRate.value = "1";
                _prmDebitForex.value = "0";
                _prmDebitHome.value = "0";
                _prmCreditForex.value = "0";
                _prmCreditHome.value = "0";

            }
            else {
                _prmForexRate.value = FormatCurrency2(_tempForexRate, _prmDecimalPalce.value);
                _prmDebitForex.value = FormatCurrency2(0, _prmDecimalPalce.value);
                _prmDebitHome.value = FormatCurrency2(0, _prmDecimalPalce.value);
                _prmCreditForex.value = FormatCurrency2(0, _prmDecimalPalce.value);
                _prmCreditHome.value = FormatCurrency2(0, _prmDecimalPalce.value);
            }
        }

        function Calculate(_prmForexRate, _prmDebitForex, _prmDebitHome, _prmCreditForex, _prmCreditHome, _prmDecimalPalce) {
            var _tempForexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPalce.value));
            if (isNaN(_tempForexRate) == true) {
                _tempForexRate = 0;
            }

            var _tempDebitForex = parseFloat(GetCurrency2(_prmDebitForex.value, _prmDecimalPalce.value));
            if (isNaN(_tempDebitForex) == true) {
                _tempDebitForex = 0;
            }

            var _tempCreditForex = parseFloat(GetCurrency2(_prmCreditForex.value, _prmDecimalPalce.value));
            if (isNaN(_tempCreditForex) == true) {
                _tempCreditForex = 0;
            }

            _prmDebitForex.value = FormatCurrency2(_tempDebitForex, _prmDecimalPalce.value);

            var _debitHome = _tempForexRate * _tempDebitForex;
            _prmDebitHome.value = FormatCurrency2(_debitHome, _prmDecimalPalce.value);

            _prmCreditForex.value = FormatCurrency2(_tempCreditForex, _prmDecimalPalce.value);

            var _creditHome = _tempForexRate * _tempCreditForex;
            _prmCreditHome.value = FormatCurrency2(_creditHome, _prmDecimalPalce.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Account
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountTextBox" Width="100" MaxLength="12" AutoPostBack="True"
                                            OnTextChanged="AccountTextBox_TextChanged"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDropDownList" AutoPostBack="True" OnSelectedIndexChanged="AccountDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="AccountCustomValidator" runat="server" ErrorMessage="Account Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="AccountDropDownList"></asp:CustomValidator>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Currency
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="CurrencyDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CurrencyDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ErrorMessage="Currency Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="CurrencyDropDownList"></asp:CustomValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
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
                                        <asp:TextBox runat="server" ID="ForexRateTextBox" Width="150" MaxLength="18"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td width="160px">
                                                    Forex
                                                </td>
                                                <td width="160px">
                                                    <asp:Literal runat="server" ID="DefaultLiteral"></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Debit
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox runat="server" ID="DebitForexTextBox" Width="150"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="DebitHomeTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Credit
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox runat="server" ID="CreditForexTextBox" Width="150"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="CreditHomeTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                                <asp:ImageButton ID="SaveAndNewDetailButton" runat="server" OnClick="SaveAndNewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

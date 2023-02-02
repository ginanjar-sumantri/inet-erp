<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PaymentTradeCrEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade.PaymentTradeCrEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function CalculateAmountTotal(_prmTextBox1, _prmTextBox2, _prmTextBox3, _prmDecimalPlace, _prmDecimalPlace2) {
            _prmTextBox3.value = FormatCurrency2(parseFloat(GetCurrency2(_prmTextBox1.value, _prmDecimalPlace.value)) * parseFloat(GetCurrency(_prmTextBox2.value, _prmDecimalPlace.value)), _prmDecimalPlace2.value);
            _prmTextBox2.value = FormatCurrency2(parseFloat(GetCurrency2(_prmTextBox2.value, _prmDecimalPlace.value)), _prmDecimalPlace.value);
            _prmTextBox1.value = FormatCurrency2(parseFloat(GetCurrency2(_prmTextBox1.value, _prmDecimalPlace.value)), _prmDecimalPlace.value)
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
                                Payment Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="PayTypeDropDownList" OnSelectedIndexChanged="PayTypeDropDownList_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PayTypeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="PayTypeDropDownList" ErrorMessage="Payment Type Must Be Filled"
                                    Text="*"></asp:CustomValidator>
                                <asp:HiddenField ID="FgGiroHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr id="DocTr" runat="server">
                            <td>
                                Document No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DocumentNoTextBox" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DocNoRequiredFieldValidator" runat="server" ControlToValidate="DocumentNoTextBox"
                                    Text="*" ErrorMessage="Document No must be filled" Enabled="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="DPTr" runat="server">
                            <td>
                                DP Supplier No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="DPSuppDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DPSuppDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="DPSuppCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="DPSuppDropDownList" ErrorMessage="Document No. Must Be Filled"
                                    Text="*"></asp:CustomValidator>
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
                                <asp:TextBox runat="server" ID="CurrRateTextBox" Width="100"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
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
                                <asp:TextBox runat="server" ID="AmountForexTextBox" Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Total
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AmountTotalTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="BankPaymentDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="BankPaymentCustomValidator" runat="server" ErrorMessage="Bank Payment Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="BankPaymentDropDownList"></asp:CustomValidator>
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
                                <%--<input id="headline_date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DueDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." runat="server" />--%>
                                <asp:Literal ID="DueDateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Expense
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BankExpenseTextBox" Width="150"></asp:TextBox>
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

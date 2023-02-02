<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade.ReceiptTradeDbEdit, App_Web_hbijsilb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
            function CalculateAmountHome(_prmCurrRate, _prmAmountForex, _prmBankExpensePercent, _prmBankExpenseAmount, _prmCustRevenuePercent,_prmCustRevenueAmount, _prmTotalCustPaid, _prmTotalReceiptForex, _prmTotalReceiptHome, _prmDecimalPlace, _prmDecimalPlace2) {
            _prmCurrRate.value = FormatCurrency2(GetCurrency2(_prmCurrRate.value, _prmDecimalPlace.value), _prmDecimalPlace.value);
            _prmAmountForex.value = FormatCurrency2(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value), _prmDecimalPlace.value);

            if (parseFloat(GetCurrency2(_prmCustRevenuePercent.value, _prmDecimalPlace.value)) > 0)  
            {
                _prmCustRevenueAmount.value = FormatCurrency2((parseFloat(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value)) * parseFloat(GetCurrency2(_prmCustRevenuePercent.value, _prmDecimalPlace.value)) / 100), _prmDecimalPlace.value); 
            }
            else
            {
                _prmCustRevenueAmount.value = FormatCurrency2(GetCurrency2(_prmCustRevenueAmount.value, _prmDecimalPlace.value), _prmDecimalPlace.value);
            }
            
             _prmTotalCustPaid.value = FormatCurrency2(parseFloat(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value)) + parseFloat(GetCurrency2(_prmCustRevenueAmount.value, _prmDecimalPlace.value)),_prmDecimalPlace.value) ;
             
            if (parseFloat(GetCurrency2(_prmBankExpensePercent.value, _prmDecimalPlace.value)) > 0)  
            {
                _prmBankExpenseAmount.value = FormatCurrency2((parseFloat(GetCurrency2(_prmTotalCustPaid.value, _prmDecimalPlace.value)) * parseFloat(GetCurrency2(_prmBankExpensePercent.value, _prmDecimalPlace.value)) / 100), _prmDecimalPlace.value); 
            }
            else
            {
                _prmBankExpenseAmount.value = FormatCurrency2(GetCurrency2(_prmBankExpenseAmount.value, _prmDecimalPlace.value), _prmDecimalPlace.value);
            }

            _prmTotalReceiptForex.value = FormatCurrency2((parseFloat(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value)) + parseFloat(GetCurrency2(_prmCustRevenueAmount.value, _prmDecimalPlace.value)) - parseFloat(GetCurrency2(_prmBankExpenseAmount.value, _prmDecimalPlace.value))), _prmDecimalPlace.value);
            _prmTotalReceiptHome.value = FormatCurrency2((parseFloat(GetCurrency2(_prmTotalReceiptForex.value, _prmDecimalPlace.value)) * parseFloat(GetCurrency2(_prmCurrRate.value, _prmDecimalPlace.value))), _prmDecimalPlace2.value);
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
                                Receipt Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="PayTypeDropDownList" AutoPostBack="true" OnSelectedIndexChanged="PayTypeDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PayTypeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="PayTypeDropDownList" ErrorMessage="Receipt Type Must Be Filled"
                                    Text="*"></asp:CustomValidator>
                                <asp:HiddenField ID="FgGiroHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
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
                                Customer Charge Revenue
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustRevenuePercentTextBox" runat="server" BackColor="#CCCCCC" Width="100px"></asp:TextBox>
                                %
                                <asp:TextBox ID="CustRevenueAmountTextBox" runat="server" BackColor="#CCCCCC" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Customer Paid
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalCustPaidTextBox" runat="server" BackColor="#CCCCCC" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Charge Expense
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="BankExpensePercentTextBox" runat="server" BackColor="#CCCCCC" Width="100px"></asp:TextBox>
                                %
                                <asp:TextBox ID="BankExpenseAmountTextBox" runat="server" BackColor="#CCCCCC" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Receipt Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalReceiptForexTextBox" runat="server" BackColor="#CCCCCC" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Receipt Home
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalReceiptHomeTextBox" runat="server" BackColor="#CCCCCC" Width="150px"></asp:TextBox>
                                <asp:HiddenField ID="DecimalPlaceHiddenField2" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Giro
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="BankGiroDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="BankGiroCustomValidator" runat="server" ErrorMessage="Bank Giro Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="BankGiroDropDownList"></asp:CustomValidator>
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

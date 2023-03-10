<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DPSuppReturPaymentEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur.DPSuppReturPaymentEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <%--<link href="../calendar/calendar-blue2.css" rel="stylesheet" type="text/css" media="all"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" src="../calendar/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../calendar/calendar-setup.js"></script>--%>
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Calculate(_prmCurrRateTextBox, _prmAmountForexTextBox, _prmAmountHomeTextBox, _prmDecimalPlace, _prmDecimalPlaceHome) {
            _prmCurrRateTextBox.value = FormatCurrency2(_prmCurrRateTextBox.value, _prmDecimalPlace.value);
            _prmAmountForexTextBox.value = FormatCurrency2(_prmAmountForexTextBox.value, _prmDecimalPlace.value);

            var _tempAmountHome = parseFloat(GetCurrency(_prmCurrRateTextBox.value)) * parseFloat(GetCurrency(_prmAmountForexTextBox.value))
            _prmAmountHomeTextBox.value = FormatCurrency2(_tempAmountHome, _prmDecimalPlaceHome.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Receipt Type
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ReceiptTypeDropDownList" AutoPostBack="true"
                        OnSelectedIndexChanged="ReceiptTypeDropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="ReceiptTypeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                        ControlToValidate="ReceiptTypeDropDownList" ErrorMessage="Receipt Type Must Be Filled"
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
                    <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                    <asp:HiddenField ID="DecimalPlaceHiddenFieldHome" runat="server" />
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
                    Amount Home
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AmountHomeTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                    <span id="ImageSpan" runat="server">
                        <%--<input id="date" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                            value="..." />--%>
                        <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                        <%--<img src="../calendar/img.gif" id="date" style="cursor: pointer; border: 1px solid red;"
                            title="Date selector" onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                            align="absmiddle" />

                        <script type="text/javascript">
	                        Calendar.setup({
		                        inputField     :    "ctl00_DefaultBodyContentPlaceHolder_DateTextBox",    	// id of the input field
		                        ifFormat       :    "%Y-%m-%d",        	// format of the input field
		                        showsTime      :    false,            	// will display a time selector
		                        button         :    "date", 	 	// trigger for the calendar (button ID)
		                        singleClick    :    true,           	// double-click mode
		                        step           :    1,              	// show all years in drop-down boxes (instead of every other year as default)
		                        showOthers 	   :	true
	                        });
                        </script>--%>
                    </span>
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
            <tr>
                <td colspan="3">
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

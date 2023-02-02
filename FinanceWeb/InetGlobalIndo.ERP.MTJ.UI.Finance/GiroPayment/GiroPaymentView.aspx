<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="GiroPaymentView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPayment.GiroPaymentView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset>
                    <legend>Giro</legend>
                    <asp:Panel runat="server" DefaultButton="BackButton" ID="Panel1">
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Giro No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="GiroNoTextBox" Width="150" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Payment No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="PaymentNoTextBox" Width="150" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Payment Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="PaymentDateTextBox" Width="100" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    Due Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="DueDateTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pay To
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="PayToTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="PayToNameTextBox" Width="250" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Bank Payment
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="BankPaymentTextBox" Width="250" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Currency/Rate
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="CurrencyTextBox" Width="50" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="CurrencyRateTextBox" Width="100" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Amount
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="AmountTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    Remark
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" MaxLength="500" Height="80"
                                        TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                    <asp:HiddenField ID="StatusHiddenField" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Action
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:DropDownList ID="ActionDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ActionDropDownList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
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
                <asp:Panel runat="server" ID="DrawnPanel" DefaultButton="DrawnSaveButton">
                    <fieldset>
                        <legend>Drawn</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="DrawnDateTextBox" runat="server" BackColor="#cccccc" Width="100"></asp:TextBox>&nbsp;
                                                <%--<input id="Button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DrawnDateTextBox,'yyyy-mm-dd',this)"
                                                    value="..." />--%>
                                                <asp:Literal ID="DrawnDateLiteral" runat="server"></asp:Literal>
                                                <%--</td>
                                            <td>
                                                <div runat="server" id="DrawnDateDiv">
                                                    <img src="../calendar/img.gif" id="drawn_date_start" style="cursor: pointer; border: 1px solid red;"
                                                        title="Date selector" onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                        align="absmiddle" />

                                                    <script type="text/javascript">
	                                                    Calendar.setup({
		                                                    inputField     :    "ctl00_DefaultBodyContentPlaceHolder_DrawnDateTextBox",    	// id of the input field
		                                                    ifFormat       :    "%Y-%m-%d",        	// format of the input field
		                                                    showsTime      :    false,            	// will display a time selector
		                                                    button         :    "drawn_date_start", 	 	// trigger for the calendar (button ID)
		                                                    singleClick    :    true,           	// double-click mode
		                                                    step           :    1,              	// show all years in drop-down boxes (instead of every other year as default)
		                                                    showOthers 	   :	true
	                                                    });
                                                    </script>

                                                </div>--%>
                                            </td>
                                        </tr>
                                    </table>
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
                                    <asp:TextBox runat="server" ID="DrawnBankPaymentTextBox" Width="250" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="DrawnRemarkTextBox" Width="300" TextMode="MultiLine"
                                        Height="80"></asp:TextBox>
                                    <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                    characters left
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="DrawnSaveButton" runat="server" OnClick="DrawnSaveButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="DrawnCancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="CancelPanel" DefaultButton="CancelSaveButton">
                    <fieldset>
                        <legend>Cancel</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="CancelDateTextBox" runat="server" BackColor="#cccccc" Width="100"></asp:TextBox>&nbsp;
                                                <%--<input id="Button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_CancelDateTextBox,'yyyy-mm-dd',this)"
                                                    value="..." />--%>
                                                <asp:Literal ID="CancelDateLiteral" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Reason
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CancelReasonTextBox" Width="300" TextMode="MultiLine"
                                        Height="80"></asp:TextBox>
                                    <asp:TextBox ID="CounterTextBox2" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                    characters left
                                    <asp:RequiredFieldValidator ID="CancelReasonRequiredFieldValidator" runat="server"
                                        ControlToValidate="CancelReasonTextBox" ErrorMessage="Reason Must Be Filled"
                                        Text="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="CancelSaveButton" runat="server" OnClick="CancelSaveButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="CancelCancelButton" runat="server" CausesValidation="False"
                                                    OnClick="CancelButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="UnpostingPanel" DefaultButton="UnpostingSaveButton">
                    <fieldset>
                        <legend>Unposting</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="UnpostingSaveButton" runat="server" OnClick="UnpostingSaveButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="UnpostingCancelButton" runat="server" CausesValidation="False"
                                                    OnClick="CancelButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

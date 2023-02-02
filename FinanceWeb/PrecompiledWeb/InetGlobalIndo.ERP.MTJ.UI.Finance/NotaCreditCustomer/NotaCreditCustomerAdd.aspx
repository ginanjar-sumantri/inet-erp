<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer.NotaCreditCustomerAdd, App_Web_lw0yydkz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <%-- <link href="../calendar/calendar-blue2.css" rel="stylesheet" type="text/css" media="all"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" src="../calendar/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../calendar/calendar-setup.js"></script>--%>
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
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
                    Trans Date
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                    <%--<input id="Button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                        value="..." />--%>
                    <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                    <%-- <img src="../calendar/img.gif" id="headline_date_start" style="cursor: pointer; border: 1px solid red;"
                        title="Date selector" onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                        align="absmiddle" />

                    <script type="text/javascript">
	                Calendar.setup({
		                inputField     :    "ctl00_DefaultBodyContentPlaceHolder_DateTextBox",    	// id of the input field
		                ifFormat       :    "%Y-%m-%d",        	// format of the input field
		                showsTime      :    false,            	// will display a time selector
		                button         :    "headline_date_start", 	 	// trigger for the calendar (button ID)
		                singleClick    :    true,           	// double-click mode
		                step           :    1,              	// show all years in drop-down boxes (instead of every other year as default)
		                showOthers 	   :	true
	                });
                    </script>--%>
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
                    <asp:DropDownList runat="server" ID="CustDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CustDropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                        ErrorMessage="Customer Must Be Filled" Text="*" ControlToValidate="CustDropDownList">
                    </asp:CustomValidator>
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
                    <asp:TextBox ID="AttnTextBox" runat="server" Width="150" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td>
                    Invoice No
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="InvoiceDDL">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="InvoiceCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                        ErrorMessage="Invoice Must Be Filled" Text="*" ControlToValidate="InvoiceDDL">
                    </asp:CustomValidator>
                </td>
            </tr>--%>
            <tr>
                <td>
                    CN Customer No.
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="CNCustNoTextBox" runat="server" MaxLength="30"></asp:TextBox>
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
                    <asp:DropDownList ID="TermDropDownList" runat="server">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="TermCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                        ErrorMessage="Term Must Be Filled" Text="*" ControlToValidate="TermDropDownList">
                    </asp:CustomValidator>
                </td>
            </tr>
            <asp:ScriptManager ID="scriptMgr" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <tr>
                        <td>
                            Currency / Rate
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="CurrCodeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CurrCodeDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CurrCodeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrCodeDropDownList">
                            </asp:CustomValidator>
                            <asp:TextBox runat="server" ID="CurrRateTextBox" Width="100">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="CurrRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                                Text="*" ControlToValidate="CurrRateTextBox" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
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
                                        <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100px" BackColor="#CCCCCC">
                                        </asp:TextBox>
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
            <tr>
                <td colspan="3">
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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

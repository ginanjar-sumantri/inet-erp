<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerBeginning.CustBeginningEdit, App_Web_rp-3s6hs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Calculate(_prmPPNPercent, _prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmPPNForex, _prmTotalForex, _prmDecimalPlace) {
        
            var _tempPPNPercent = parseFloat(GetCurrency2(_prmPPNPercent.value, _prmDecimalPlace.value));
            
            if (isNaN(_tempPPNPercent) == true) {
                _tempPPNPercent = 0;
            }

            var _tempAmountBase = parseFloat(GetCurrency2(_prmAmountBase.value, _prmDecimalPlace.value));
            if (isNaN(_tempAmountBase) == true) {
                _tempAmountBase = 0;
            }

            if (_tempPPNPercent > 0) {
                _prmPPNPercent.value = FormatCurrency2(_tempPPNPercent, _prmDecimalPlace.value);

                _prmPPNNo.readOnly = false;
                _prmPPNNo.style.background = '#FFFFFF';

                _prmPPNDate.readOnly = true;
                _prmCal.style.visibility = "visible";
            }
            else {
                _prmPPNPercent.value = FormatCurrency2(0, _prmDecimalPlace.value);

                _prmPPNNo.value = "";
                _prmPPNNo.readOnly = true;
                _prmPPNNo.style.background = '#CCCCCC';

                _prmPPNDate.value = "";
                _prmPPNDate.readOnly = true;
                _prmCal.style.visibility = "hidden";
            }

            _prmAmountBase.value = FormatCurrency2(_tempAmountBase, _prmDecimalPlace.value);

            if (_tempAmountBase > 0) {
                var _ppnForex = _tempAmountBase * _tempPPNPercent / 100;
                _prmPPNForex.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);

                var _totalForex = _tempAmountBase + GetCurrency2(_ppnForex, _prmDecimalPlace.value);
                _prmTotalForex.value = FormatCurrency2(_totalForex, _prmDecimalPlace.value);
            }
            else {
                _prmPPNForex.value = FormatCurrency2(0, _prmDecimalPlace.value);

                _prmTotalForex.value = FormatCurrency2(0, _prmDecimalPlace.value);
            }
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
                                Invoice No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="InvoiceNoTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <%--<input id="DateButton" runat="server" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>   
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer PO No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustPONoTextBox" runat="server" MaxLength="30"></asp:TextBox>
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
                                <asp:DropDownList ID="CustDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CustDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="CustDropDownList" ErrorMessage="Customer Must Be Filled" Text="*">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bill To
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="BillToDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="BillToCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="BillToDropDownList" ErrorMessage="Bill To Must Be Filled"
                                    Text="*">
                                </asp:CustomValidator>
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
                                <asp:TextBox ID="DueDateTextBox" runat="server" BackColor="#CCCCCC" Width="100"></asp:TextBox>
                                <%--<input id="DueDateButton" runat="server" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DueDateTextBox,'yyyy-mm-dd',this)"
                                    type="button" value="..." />--%>
                                 <asp:Literal ID="DueDateLiteral" runat="server"></asp:Literal>    
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
                                        <asp:DropDownList ID="CurrCodeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CurrCodeDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="CurrCodeDropDownList" ErrorMessage="Currency Must Be Filled"
                                            Text="*">
                                        </asp:CustomValidator>
                                        <asp:TextBox ID="CurrRateTextBox" runat="server" Width="100">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="CurrRateRequiredFieldValidator" runat="server" ControlToValidate="CurrRateTextBox"
                                            Display="Dynamic" ErrorMessage="Currency Rate Must Be Filled" Text="*">
                                        </asp:RequiredFieldValidator>
                                        <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="1">
                                            <tr class="bgcolor_gray" height="10px">
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <b>PPN %</b>
                                                </td>
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <b>No</b>
                                                </td>
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <b>Date</b>
                                                </td>
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <b>Rate</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PPN
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNTextBox" runat="server" Width="100px">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <asp:TextBox ID="PPNNoTextBox" runat="server" Width="100"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <asp:TextBox ID="PPNDateTextBox" runat="server" Width="70"></asp:TextBox>
                                                    <%--<input id="PPNDateButton" runat="server" style="visibility: hidden; width: 20px;"
                                                        onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_PPNDateTextBox,'yyyy-mm-dd',this)"
                                                        type="button" value="..." />--%>
                                                        <asp:Literal ID="PPNDateLiteral" runat="server"></asp:Literal>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <asp:TextBox ID="PPNRateTextBox" runat="server" BackColor="#CCCCCC" Width="100"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
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
                                        <table cellpadding="0" cellspacing="1">
                                            <tr class="bgcolor_gray" height="10px">
                                                <td style="width: 110px" align="center" class="tahoma_11_white">
                                                    <b>Currency</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>BaseForex</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPN Forex</b>
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
                                                    <asp:TextBox ID="CurrTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                                        Width="100">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="BaseForexTextBox" runat="server" Width="100px">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNForexTextBox" runat="server" BackColor="#CCCCCC" Width="100px">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="TotalForexTextBox" runat="server" BackColor="#CCCCCC" Width="100px">
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

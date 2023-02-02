<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditSupplier.NotaCreditSupplierAdd, App_Web_uhnr75_a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">

        function Calculate(_prmPPNPercent, _prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmDiscForex, _prmPPNForex, _prmTotalForex, _prmDecimalPlace) {
            var _tempPPNPercent = parseFloat(GetCurrency2(_prmPPNPercent.value, _prmDecimalPlace.value));
            if (isNaN(_tempPPNPercent) == true) {
                _tempPPNPercent = 0;
            }

            var _tempAmountBase = parseFloat(GetCurrency2(_prmAmountBase.value, _prmDecimalPlace.value));
            if (isNaN(_tempAmountBase) == true) {
                _tempAmountBase = 0;
            }

            var _tempDiscForex = parseFloat(GetCurrency2(_prmDiscForex.value, _prmDecimalPlace.value));
            if (isNaN(_tempDiscForex) == true) {
                _tempDiscForex = 0;
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

            _prmDiscForex.value = FormatCurrency2(_tempDiscForex, _prmDecimalPlace.value);

            if (_tempAmountBase > 0) {
                var _ppnForex = (_tempAmountBase - _tempDiscForex) * _tempPPNPercent / 100;
                _prmPPNForex.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);

                var _totalForex = _tempAmountBase + _ppnForex - _tempDiscForex;
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
                    <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                        value="..." />--%>
                    <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    Supplier
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="SuppDropDownList" AutoPostBack="True" OnSelectedIndexChanged="SuppDropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="SuppCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                        ErrorMessage="Supplier Must Be Filled" Text="*" ControlToValidate="SuppDropDownList">
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
            <tr>
                <td>
                    Supplier Invoice No.
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="SuppInvNoTextBox" runat="server" MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Supplier PO No.
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="SuppPONoTextBox" runat="server" MaxLength="30"></asp:TextBox>
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
                            <table width="0">
                                <tr class="bgcolor_gray" height="20">
                                    <td style="width: 110px" class="tahoma_11_white" align="center">
                                        <b>PPN %</b>
                                    </td>
                                    <td style="width: 110px" class="tahoma_11_white" align="center">
                                        <b>No.</b>
                                    </td>
                                    <td style="width: 110px" class="tahoma_11_white" align="center">
                                        <b>Date</b>
                                    </td>
                                    <td style="width: 110px" class="tahoma_11_white" align="center">
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
                                        <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100">
                                        </asp:TextBox>
                                    </td>
                                    <td style="width: 110px" align="center">
                                        <asp:TextBox ID="PPNNoTextBox" runat="server" Width="100" BackColor="#cccccc">
                                        </asp:TextBox>
                                    </td>
                                    <td style="width: 110px" align="center">
                                        <asp:TextBox ID="PPNDateTextBox" runat="server" Width="70" BackColor="#CCCCCC">
                                        </asp:TextBox>&nbsp;
                                        <%--<input id="ppn_date_start" runat="server" type="button" style="visibility: hidden;
                                            width: 20px;" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_PPNDateTextBox,'yyyy-mm-dd',this)"
                                            value="..." />--%>
                                        <asp:Literal ID="PPNDateLiteral" runat="server"></asp:Literal>
                                    </td>
                                    <td style="width: 110px" align="center">
                                        <asp:TextBox ID="PPNRateTextBox" runat="server" Width="100" BackColor="#cccccc">
                                        </asp:TextBox>
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
                            <table>
                                <tr class="bgcolor_gray" height="20">
                                    <td style="width: 110px" class="tahoma_11_white" align="center">
                                        <b>Currency</b>
                                    </td>
                                    <td style="width: 110px" class="tahoma_11_white" align="center">
                                        <b>Base Forex</b>
                                    </td>
                                    <td style="width: 110px" class="tahoma_11_white" align="center">
                                        <b>Discount Forex</b>
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
                                        <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                        </asp:TextBox>
                                    </td>
                                    <td style="width: 110px" align="center">
                                        <asp:TextBox ID="AmountBaseTextBox" runat="server" Width="100" BackColor="#CCCCCC">
                                        </asp:TextBox>
                                    </td>
                                    <td style="width: 110px" align="center">
                                        <asp:TextBox ID="DiscForexTextBox" runat="server" Width="100">
                                        </asp:TextBox>
                                    </td>
                                    <td style="width: 110px" align="center">
                                        <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" BackColor="#CCCCCC">
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

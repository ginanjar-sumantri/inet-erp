<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DPCustReceiptEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerReceipt.DPCustReceiptEdit" %>

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

            var _tempTotalForex = parseFloat(GetCurrency2(_prmTotalForex.value, _prmDecimalPlace.value));
            if (isNaN(_tempTotalForex) == true) {
                _tempTotalForex = 0;
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

            if (_tempTotalForex > 0) {
                var _baseForex = 100 / (100 + _tempPPNPercent) * _tempTotalForex;
                _prmAmountBase.value = FormatCurrency2(_baseForex, _prmDecimalPlace.value);

                var _ppnForex = _tempTotalForex - GetCurrency2(_baseForex, _prmDecimalPlace.value);
                _prmPPNForex.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);
            }
            else {
                _prmAmountBase.value = FormatCurrency2(0, _prmDecimalPlace.value);

                _prmPPNForex.value = FormatCurrency2(0, _prmDecimalPlace.value);
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
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TransNoTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                File No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FileNmbrTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="150">
                                </asp:TextBox>
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
                            </td>
                        </tr>
                        <%-- <tr>
                            <td>
                                Status
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label runat="server" ID="StatusLabel"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CustTextBox" BackColor="#CCCCCC" ReadOnly="true">
                                </asp:TextBox>
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
                                DP List No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DPListNoTextBox" runat="server" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                SO No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SONoTextBox" runat="server" BackColor="#CCCCCC" MaxLength="20">
                                </asp:TextBox>
                                <asp:HiddenField ID="SoNoHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency / Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="80px" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox ID="CurrRateTextBox" runat="server" Width="100">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="CurrRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                                    Text="*" ControlToValidate="CurrRateTextBox" Display="Dynamic">
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
                                <table>
                                    <tr class="bgcolor_gray" height="20">
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>PPN %</b>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>No</b>
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
                                            <asp:TextBox ID="PPNTextBox" runat="server" Width="100px">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <asp:TextBox runat="server" Width="100" ID="PPNNoTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <asp:TextBox runat="server" Width="70" ID="PPNDateTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                            <%--<input id="ppn_date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_PPNDateTextBox,'yyyy-mm-dd',this)"
                                                value="..." runat="server" />--%>
                                                <asp:Literal ID="PPNDateLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <asp:TextBox runat="server" Width="100" ID="PPNRateTextBox" BackColor="#CCCCCC"></asp:TextBox>
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
                                            <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="BaseForexTextBox" runat="server" Width="100px" BackColor="#CCCCCC">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100px" BackColor="#CCCCCC">
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
                                <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

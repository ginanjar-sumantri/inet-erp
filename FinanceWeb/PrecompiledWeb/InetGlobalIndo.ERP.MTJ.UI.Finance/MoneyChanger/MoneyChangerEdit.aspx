<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.MoneyChanger.MoneyChangerEdit, App_Web_h8xb3gvb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Count(_prmRate, _prmAmountExchange, _prmAmount,_prmDecimalPlace, _prmDecimalPlace2)
        {
            var _tempRate = parseFloat(GetCurrency2(_prmRate.value, "100000000"));
            if(isNaN(_tempRate) == true)
            {
                _tempRate = 0;
            }

            var _tempAmountExchange = parseFloat(GetCurrency2(_prmAmountExchange.value, _prmDecimalPlace2.value));
            if(isNaN(_tempAmountExchange) == true)
            {
                _tempAmountExchange = 0;
            }
            
            _prmRate.value = FormatCurrency2(_tempRate, "100000000");
            _prmAmountExchange.value = FormatCurrency2(_tempAmountExchange, _prmDecimalPlace2.value);
            
            var _amount = _tempRate * _tempAmountExchange;
            _prmAmount.value = FormatCurrency2(_amount, _prmDecimalPlace.value);
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
                    <asp:Label CssClass="warning" runat="server" ID="WarningLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TransNoTextBox" Width="200" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="FileNoTextBox" Width="200" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <%--<input id="headline_date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TypeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="petty_tr">
                            <td>
                                Petty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PettyTextBox" runat="server" Width="350" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="payment_tr">
                            <td>
                                Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PaymentTextBox" runat="server" Width="350" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CurrTextBox" BackColor="#cccccc" Width="50"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RateTextBox" runat="server"></asp:TextBox>
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
                                <asp:TextBox ID="AmountTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TypeExchangeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="petty_exchange_tr">
                            <td>
                                Petty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PettyExchangeTextBox" runat="server" Width="350" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="payment_exchange_tr">
                            <td>
                                Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" Width="350" BackColor="#CCCCCC" ReadOnly="true" ID="PaymentExchangeTextBox">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency Exchange
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CurrExchangeTextBox" runat="server" BackColor="#CCCCCC" Width="50px"
                                    ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Rate Exchange
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CurrRateExchangeTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Exchange
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountExchangeTextBox" runat="server"></asp:TextBox>
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

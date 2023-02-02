<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.MoneyChanger.MoneyChangerAdd, App_Web_h8xb3gvb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Count(_prmRate, _prmRateExchange, _prmAmountExchange, _prmAmount,_prmDecimalPlace, _prmDecimalPlace2)
        {
            var _tempRate = parseFloat(GetCurrency2(_prmRate.value, _prmDecimalPlace.value));
            if(isNaN(_tempRate) == true)
            {
                _tempRate = 0;
            }

            var _tempRateExchange = parseFloat(GetCurrency2(_prmRateExchange.value, _prmDecimalPlace2.value));
            if (isNaN(_tempRateExchange) == true) {
                _tempRateExchange = 0;
            }

            var _tempAmount = parseFloat(GetCurrency2(_prmAmount.value, _prmDecimalPlace.value));
            if(isNaN(_tempAmount) == true)
            {
                _tempAmount = 0;
            }
            
            var _amountExchange = (_tempRate * _tempAmount)/ _tempRateExchange;

            _prmRate.value = FormatCurrency2(_tempRate, _prmDecimalPlace.value);
            _prmRateExchange.value = FormatCurrency2(_tempRateExchange, _prmDecimalPlace2.value);
            _prmAmount.value = FormatCurrency2(_tempAmount, _prmDecimalPlace.value);
            _prmAmountExchange.value = FormatCurrency2(_amountExchange, _prmDecimalPlace2.value);
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
                                <asp:DropDownList ID="TypeDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TypeDDL_SelectedIndexChanged">
                                    <asp:ListItem Text="[Choose One]" Value="null"></asp:ListItem>
                                    <asp:ListItem Text="Petty" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Payment" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ErrorMessage="Type Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="TypeDDL"></asp:CustomValidator>
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
                                <asp:DropDownList ID="PettyDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PettyDDL_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PettyDDLCustomValidator" runat="server" ErrorMessage="Petty Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="PettyDDL"></asp:CustomValidator>
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
                                <asp:DropDownList ID="PaymentDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PaymentDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PaymentDropDownListCustomValidator" runat="server" ErrorMessage="Payment Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="PaymentDropDownList"></asp:CustomValidator>
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
                                <asp:DropDownList ID="TypeExchangeDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TypeExchangeDDL_SelectedIndexChanged">
                                    <asp:ListItem Text="[Choose One]" Value="null"></asp:ListItem>
                                    <asp:ListItem Text="Petty" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Payment" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CurrencyCustomValidator0" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="TypeExchangeDDL" ErrorMessage="Type Must Be Filled" Text="*"></asp:CustomValidator>
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
                                <asp:DropDownList ID="PettyExchangeDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PettyExchangeDDL_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PettyDDLCustomValidator0" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="PettyExchangeDDL" ErrorMessage="Petty Must Be Filled" Text="*"></asp:CustomValidator>
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
                                <asp:DropDownList ID="PaymentExchangeDropDownList" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="PaymentExchangeDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PaymentDropDownListCustomValidator0" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="PaymentExchangeDropDownList" ErrorMessage="Payment Must Be Filled"
                                    Text="*"></asp:CustomValidator>
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
                                <asp:TextBox ID="CurrExchangeTextBox" runat="server" BackColor="#CCCCCC" Width="50px"></asp:TextBox>
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

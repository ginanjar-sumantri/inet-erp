<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PriceGroupEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.PriceGroup.PriceGroupEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Clear(_prmForexRate, _prmAmountForex, _prmAmountHome, _prmDecimalPalce) {
            var _tempForexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPalce.value));
            if (isNaN(_tempForexRate) == true) {
                _tempForexRate = 0;
            }

            if (_tempForexRate < 1) {
                _prmForexRate.value = "1";
                _prmAmountForex.value = "0";
                _prmAmountHome.value = "0";

            }
            else {
                _prmForexRate.value = FormatCurrency2(_tempForexRate, _prmDecimalPalce.value);
                _prmAmountForex.value = FormatCurrency2(0, _prmDecimalPalce.value);
                _prmAmountHome.value = FormatCurrency2(0, _prmDecimalPalce.value);
            }
        }

        function Calculate(_prmForexRate, _prmDebitForex, _prmDebitHome, _prmDecimalPalce) {
            var _tempForexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPalce.value));
            if (isNaN(_tempForexRate) == true) {
                _tempForexRate = 0;
            }

            var _tempDebitForex = parseFloat(GetCurrency2(_prmDebitForex.value, _prmDecimalPalce.value));
            if (isNaN(_tempDebitForex) == true) {
                _tempDebitForex = 0;
            }

            _prmDebitForex.value = FormatCurrency2(_tempDebitForex, _prmDecimalPalce.value);

            var _debitHome = _tempForexRate * _tempDebitForex;
            _prmDebitHome.value = FormatCurrency2(_debitHome, _prmDecimalPalce.value);

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Price Group Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PriceGroupCodeTextBox" Width="100" MaxLength="5"
                                    BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Year
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="YearTextBox" Width="70" MaxLength="4" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:ScriptManager ID="scriptMgr" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Currency
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CurrencyDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="CurrencyDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrencyCustomValidator" Text="*" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="CurrencyDropDownList" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
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
                                        <asp:TextBox runat="server" ID="AmountForexTextBox" Width="150"></asp:TextBox>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Selling Currency Out of Warranty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="SellingCurrOWDropDownList"></asp:DropDownList>
                                <asp:CustomValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Selling Currency Out of Warranty must be chosen."
                                    Text="*" ControlToValidate="SellingCurrOWDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Selling Price Out of Warranty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="SellingPriceOWTextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SellingPriceOWRequiredFieldValidator" runat="server" ErrorMessage="Selling Price Out of Warranty must be filled."
                                    Text="*" ControlToValidate="SellingPriceOWTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Selling Currency Black Market
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="SellingCurrBMDropDownList"></asp:DropDownList>
                                <asp:CustomValidator ID="SellingCurrBMRequiredFieldValidator" runat="server" ErrorMessage="Selling Currency Black Market Must Be chosen"
                                    Text="*" ControlToValidate="SellingCurrBMDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Selling Price Black Market
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="SellingPriceBMTextBox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SellingPriceBMRequiredFieldValidator" runat="server" ErrorMessage="Selling Price Black Market Must Be Filled."
                                    Text="*" ControlToValidate="SellingPriceBMTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="IsActiveCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Start Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="StartDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                    <asp:Literal ID="StartDateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                End Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="EndDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input id="button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                    <asp:Literal ID="EndDateLiteral" runat="server"></asp:Literal>
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

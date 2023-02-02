<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FixedAssetAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FixedAsset.FixedAssetAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript">
        function CalculateHome(_prmForexRate, _prmBuyPriceForex, _prmBuyPriceHome, _prmBuyPriceLifeInMonths, _prmLifeInMonthsBeginDepr, _prmLifeInMonthsProcessDepr, _prmLifeInMonthsTotal, _prmAmountBeginDepr, _prmAmountProcessDepr, _prmAmountTotalDepr, _prmCurrentAmount, _prmDecimalPlace, _prmDecimalPlaceHome) {

            _prmForexRate.value = (_prmForexRate.value == 0 ? "0" : FormatCurrency2(_prmForexRate.value, _prmDecimalPlace.value));
            _prmBuyPriceForex.value = (_prmBuyPriceForex.value == 0 ? "0" : FormatCurrency2(_prmBuyPriceForex.value, _prmDecimalPlace.value));
            _prmBuyPriceHome.value = (_prmBuyPriceHome.value == 0 ? "0" : FormatCurrency2(_prmBuyPriceHome.value, _prmDecimalPlaceHome.value));

            var _buyPriceHome = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPlace.value)) * parseFloat(GetCurrency2(_prmBuyPriceForex.value, _prmDecimalPlace.value));
            _prmBuyPriceHome.value = (_buyPriceHome == 0 ? "0" : FormatCurrency2(_buyPriceHome, _prmDecimalPlaceHome.value));

            _prmLifeInMonthsBeginDepr.value = 0;
            _prmLifeInMonthsTotal.value = 0 + parseInt(_prmLifeInMonthsProcessDepr.value);

            _prmAmountBeginDepr.value = 0;
            var _amounTotalDepr = 0 + parseFloat(GetCurrency2(_prmAmountProcessDepr.value, _prmDecimalPlace.value));
            _prmAmountTotalDepr.value = (_amounTotalDepr == 0 ? "0" : FormatCurrency2(_amounTotalDepr, _prmDecimalPlace.value));

            var _currentAmount = parseFloat(GetCurrency(_prmBuyPriceHome.value)) - parseFloat(_prmAmountTotalDepr.value);
            _prmCurrentAmount.value = (_currentAmount == 0 ? "0" : FormatCurrency2(_currentAmount, _prmDecimalPlace.value));
        }

        function Calculate(_prmForexRate, _prmBuyPriceForex, _prmBuyPriceHome, _prmBuyPriceLifeInMonths, _prmLifeInMonthsBeginDepr, _prmLifeInMonthsProcessDepr, _prmLifeInMonthsTotal, _prmAmountBeginDepr, _prmAmountProcessDepr, _prmAmountTotalDepr, _prmCurrentAmount, _prmDecimalPlace, _prmDecimalPlaceHome) {
            if (parseInt(_prmLifeInMonthsBeginDepr.value) > parseInt(_prmBuyPriceLifeInMonths.value)) {
                _prmLifeInMonthsBeginDepr.value = 0;
            }

            var _temp = parseFloat(GetCurrency(_prmBuyPriceHome.value)) / parseFloat(GetCurrency(_prmBuyPriceLifeInMonths.value));

            if (isNaN(_temp) || _temp == Infinity) {
                _temp = 0;
            }

            var _buyPriceHome = parseFloat(GetCurrency(_prmForexRate.value)) * parseFloat(GetCurrency(_prmBuyPriceForex.value));
            _prmBuyPriceHome.value = (_buyPriceHome == 0 ? "0" : FormatCurrency2(_buyPriceHome, _prmDecimalPlaceHome.value));

            _prmLifeInMonthsTotal.value = parseInt(_prmLifeInMonthsBeginDepr.value) + parseInt(_prmLifeInMonthsProcessDepr.value);

            var _amountBeginDepr = _temp * parseFloat(GetCurrency(_prmLifeInMonthsBeginDepr.value));
            _prmAmountBeginDepr.value = (_amountBeginDepr == 0 ? "0" : FormatCurrency2(_amountBeginDepr, _prmDecimalPlace.value));

            var _amountProcessDepr = _temp * parseFloat(GetCurrency2(_prmLifeInMonthsProcessDepr.value, _prmDecimalPlace.value));
            _prmAmountProcessDepr.value = (_amountProcessDepr == 0 ? "0" : FormatCurrency2(_amountProcessDepr, _prmDecimalPlace.value));

            var _amountTotalDepr = parseFloat(GetCurrency2(_prmAmountBeginDepr.value, _prmDecimalPlace.value)) + parseFloat(GetCurrency2(_prmAmountProcessDepr.value, _prmDecimalPlace.value));
            _prmAmountTotalDepr.value = (_amountTotalDepr == 0 ? "0" : FormatCurrency2(_amountTotalDepr, _prmDecimalPlace.value));

            var _currentAmount = parseFloat(GetCurrency2(_prmBuyPriceHome.value, _prmDecimalPlace.value)) - parseFloat(GetCurrency2(_prmAmountTotalDepr.value, _prmDecimalPlace.value));
            _prmCurrentAmount.value = (_currentAmount == 0 ? "0" : FormatCurrency2(_currentAmount, _prmDecimalPlace.value));
        }

        function Selected(_prmDDL, _prmTextBox) {
            if (_prmDDL.value != "null") {
                _prmTextBox.value = _prmDDL.value;
            }
            else {
                _prmTextBox.value = "";
            }
        }

        function Blur(_prmDDL, _prmTextBox) {
            _prmDDL.value = _prmTextBox.value;

            if (_prmDDL.value == "") {
                _prmDDL.value = "null";
                _prmTextBox.value = "";
            }
        }
    </script>

    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

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
                                Fixed Asset Code
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="FACodeTextBox" Width="150" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FACodeRequiredFieldValidator" runat="server" ErrorMessage="Fixed Asset Code Must Be Filled"
                                    Text="*" ControlToValidate="FACodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="EnableCodeCounter">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <span class="tooltip">&quot;Fixed Asset Code&quot; will be automaticaly generated, if
                                    you leave it empty.</span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Name
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="FANameTextBox" Width="560" MaxLength="80"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FANameRequiredFieldValidator" runat="server" ErrorMessage="Fixed Asset Name Must Be Filled"
                                    Text="*" ControlToValidate="FANameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Spesification
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="SpesificationTextBox" Width="250" TextMode="MultiLine"
                                    Height="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Condition
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FAStatusDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="FAStatusCustomValidator" runat="server" ErrorMessage="Fixed Asset Status Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FAStatusDropDownList"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Fixed Asset Owner
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FAOwnerCheckBox" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Sub Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FASubGroupDropDownList" AutoPostBack="True"
                                    OnSelectedIndexChanged="FASubGroupDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="FASubGroupCustomValidator" runat="server" ErrorMessage="Fixed Asset Sub Group Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FASubGroupDropDownList"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Buying Date
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
                                Fixed Asset Location Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FALocationTypeDropDownList" AutoPostBack="True"
                                    OnSelectedIndexChanged="FALocationTypeDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Value="null">[Choose Item]</asp:ListItem>
                                    <asp:ListItem Value="GENERAL">GENERAL</asp:ListItem>
                                    <asp:ListItem Value="EMPLOYEE">EMPLOYEE</asp:ListItem>
                                    <asp:ListItem Value="CUSTOMER">CUSTOMER</asp:ListItem>
                                    <asp:ListItem Value="SUPPLIER">SUPPLIER</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="FALocationDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="FALocationCustomValidator" runat="server" ErrorMessage="Fixed Asset Location Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FALocationDropDownList"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
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
                                        <asp:DropDownList runat="server" ID="CurrencyDropDownList" OnSelectedIndexChanged="CurrencyDropDownList_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ErrorMessage="Curency Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="CurrencyDropDownList"></asp:CustomValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                        <asp:HiddenField ID="DecimalPlaceHiddenFieldHome" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        Forex Rate
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="ForexRateTextBox" Width="150" MaxLength="18"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                </table>
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                <tr class="bgcolor_gray">
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Forex</b>
                                                    </td>
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Home</b>
                                                    </td>
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Life In Months</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Buy Price
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="BuyPriceForexTextBox" Width="100"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="BuyPriceHomeTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="BuyPriceLifeInMonthsTextBox" Width="100"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                <tr class="bgcolor_gray">
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Begin Depr.</b>
                                                    </td>
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Process Depr.</b>
                                                    </td>
                                                    <td style="width: 110px" align="center" class="tahoma_11_white">
                                                        <b>Total Depr.</b>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Life In Months
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="LifeInMonthsBeginDeprTextBox" Width="100"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="LifeInMonthsProcessDeprTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="LifeInMonthsTotalDeprTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
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
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="AmountBeginDeprTextBox" Width="100"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="AmountProcessDeprTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="AmountTotalDeprTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Current Amount
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td style="width: 110px" align="center">
                                                        <asp:TextBox runat="server" ID="CurrentAmountTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Status Process
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="StatusProcessCheckBox" Enabled="False" Checked="false" />
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
                                <asp:CheckBox runat="server" ID="ActiveCheckBox" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sold
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="SoldCheckBox" />
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

<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetEntry.GLBudgetDetailAdd, App_Web_m9u2e1fu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function Count (_prmRate,_prmAmountForex, _prmAmountHome,_prmDecimalPlace, _prmDecimalPlaceHome)
    {
        var _amountHome = parseFloat(GetCurrency2(_prmRate.value, _prmDecimalPlace)) * parseFloat(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value));
        _prmAmountHome.value = (_amountHome == 0) ? "0" : FormatCurrency2(_amountHome, _prmDecimalPlaceHome.value);
        _prmAmountForex.value = (_prmAmountForex.value == 0) ? "0" : FormatCurrency2(_prmAmountForex.value, _prmDecimalPlace.value);
        _prmRate.value = (_prmRate.value == 0) ? "0" : FormatCurrency2(_prmRate.value, _prmDecimalPlace.value);
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" border="0" width="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Account
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="AccountTextBox" runat="server" Width="100" MaxLength="12" AutoPostBack="true"
                                            OnTextChanged="AccountTextBox_TextChanged"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDropDownList" AutoPostBack="True" OnSelectedIndexChanged="AccountDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="AccountCustomValidator" runat="server" ErrorMessage="Account Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="AccountDropDownList"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amount Budget Rate
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AmountBudgetRateTextBox" Width="200" MaxLength="23"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Amount Budget Rate Must Be Filled"
                                            ControlToValidate="AmountBudgetRateTextBox">*</asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                        <asp:HiddenField ID="DecimalPlaceHomeHiddenField" runat="server" />
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Amount Budget Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountBudgetForexTextBox" runat="server" Width="200" MaxLength="23"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AmountRequiredFieldValidator" runat="server" ErrorMessage="Amount Budget Forex Must Be Filled"
                                    ControlToValidate="AmountBudgetForexTextBox">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Budget Home
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountBudgetHomeTextBox" runat="server" Width="200" MaxLength="23"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Actual
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountActualTextBox" runat="server" Width="200" MaxLength="23" BackColor="#CCCCCC"></asp:TextBox>
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

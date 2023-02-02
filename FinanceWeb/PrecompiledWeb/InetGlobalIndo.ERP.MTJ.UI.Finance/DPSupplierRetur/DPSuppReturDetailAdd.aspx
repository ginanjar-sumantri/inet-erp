<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur.DPSuppReturDetailAdd, App_Web_z_knjefe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Calculate(_prmBaseForexTextBox, _prmPPNTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
            _prmBaseForexTextBox.value = FormatCurrency2(_prmBaseForexTextBox.value, _prmDecimalPlace.value);
            _prmPPNTextBox.value = FormatCurrency(_prmPPNTextBox.value);
            _prmPPNForexTextBox.value = FormatCurrency(_prmPPNForexTextBox.value);

            var _PPNForex = parseFloat(GetCurrency(_prmBaseForexTextBox.value)) * (parseFloat(GetCurrency(_prmPPNTextBox.value) / 100))
            _prmPPNForexTextBox.value = FormatCurrency2(_PPNForex, _prmDecimalPlace.value);

            var _totalForex = parseFloat(GetCurrency(_prmBaseForexTextBox.value)) + parseFloat(GetCurrency(_prmPPNForexTextBox.value))
            _prmTotalForexTextBox.value = FormatCurrency2(_totalForex, _prmDecimalPlace.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
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
                    DP No.
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="DPNoDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DPNoDropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="DPNoCustomValidator" runat="server" ErrorMessage="DP No. Must Be Filled"
                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="DPNoDropDownList"></asp:CustomValidator>
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
                    <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="40" BackColor="#CCCCCC"></asp:TextBox>
                    <asp:TextBox runat="server" ID="CurrRateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                    <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Base Forex
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="BaseForexTextBox" Width="150"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="PPNTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    PPN Forex
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="PPNForexTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Total Forex
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TotalForexTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
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
            <tr>
                <td colspan="3">
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

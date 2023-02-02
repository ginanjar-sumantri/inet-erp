<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FASalesDetailEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FASales.FASalesDetailEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Amount(_prmAmountForex, _prmAmountHome, _prmForexRate, _prmDecimalPlace, _prmDecimalPlaceHome)
        {
            var _tempAmountForex = parseFloat(GetCurrency2(_prmAmountForex.value, _prmDecimalPlace.value));
            if(isNaN(_tempAmountForex) == true)
            {
                _tempAmountForex = 0;
            }
            
            var _tempForexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPlace.value));
            if(isNaN(_tempForexRate) == true)
            {
                _tempForexRate = 0;
            }
            
            _prmAmountForex.value = FormatCurrency2(_tempAmountForex, _prmDecimalPlace.value);
            
            var _amountHome = _tempAmountForex * _tempForexRate;
            _prmAmountHome.value = FormatCurrency2(_amountHome, _prmDecimalPlaceHome.value);
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
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Fixed Asset
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="FADropDownList" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Forex Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ForexRateTextBox" Width="150" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountForexTextBox" runat="server" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ErrorMessage="AmountForex Must Be Filled"
                                    Text="*" ControlToValidate="AmountForexTextBox" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                                <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Home
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountHomeTextBox" runat="server" BackColor="#CCCCCC" Width="150"></asp:TextBox>
                                <asp:HiddenField ID="DecimalPlaceHomeHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Curr
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountCurrTextBox" runat="server" ReadOnly="true" Width="150" BackColor="#CCCCCC">
                                </asp:TextBox>
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

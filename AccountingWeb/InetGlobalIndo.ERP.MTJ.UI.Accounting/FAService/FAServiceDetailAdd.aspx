<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FAServiceDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FAService.FAServiceDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
       
        function Calculate(_prmQty, _prmPriceForex, _prmAmountForex, _prmDecimalPlace)
        {
            var _tempQty = parseFloat(GetCurrency(_prmQty.value));
            if(isNaN(_tempQty) == true)
            {
                _tempQty = 0;
            }
            
            var _tempPriceForex = parseFloat(GetCurrency2(_prmPriceForex.value, _prmDecimalPlace.value));
            if(isNaN(_tempPriceForex) == true)
            {
                _tempPriceForex = 0;
            }
            
            _prmQty.value = FormatCurrency(_tempQty);
            _prmPriceForex.value = FormatCurrency2(_tempPriceForex, _prmDecimalPlace.value);
            
            var _amountForex = _tempQty * _tempPriceForex;
            _prmAmountForex.value = FormatCurrency2(_amountForex, _prmDecimalPlace.value);
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
                                Fixed Asset Maintenance
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="FixedAssetMaintenanceDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="FixedAssetMaintenanceCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Fixed Asset Maintenance Must Be Filled" Text="*" ControlToValidate="FixedAssetMaintenanceDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Add Value
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="AddValueCheckBox" Enabled="true" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QtyTextBox" Width="150" MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" ErrorMessage="Qty Must Be Filled"
                                    Text="*" ControlToValidate="QtyTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Unit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="UnitDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="UnitCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Unit Must Be Filled" Text="*" ControlToValidate="UnitDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Price Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PriceForexTextBox" Width="150" MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PriceForexRequiredFieldValidator" runat="server"
                                    ErrorMessage="Price Forex Must Be Filled" Text="*" ControlToValidate="PriceForexTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
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
                                <asp:TextBox runat="server" ID="AmountForexTextBox" Width="150" MaxLength="18" BackColor="#CCCCCC"></asp:TextBox>
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

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RequestSalesReturDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur.RequestSalesReturDetailAdd" %>

<%@ Register Src="../ProductPicker.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function Count (_prmQty, _prmPrice, _prmAmount, _prmDecimalPlace)
    {
        var _amount = parseInt(_prmQty.value) * parseFloat(GetCurrency2(_prmPrice.value, _prmDecimalPlace.value));
        _prmAmount.value = (_amount == 0) ? "0" : FormatCurrency2(_amount, _prmDecimalPlace.value);
        
        _prmQty.value = ( _prmQty.value == 0) ? "0" : FormatCurrency( _prmQty.value);
        _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency2(_prmPrice.value, _prmDecimalPlace.value);
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
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ProductDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProductDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="CustomValidator1" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="ProductDropDownList" Text="*" ErrorMessage="Product Must Be Filled"></asp:CustomValidator>--%>
                                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                                </asp:ScriptManager>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <uc1:ProductPicker ID="ProductPicker1" runat="server" />
                                        <asp:HiddenField ID="tempProductCode" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product Scrap
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="ProductScrapRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                </asp:RadioButtonList>
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
                                <asp:TextBox ID="QtyTextBox" runat="server" MaxLength="18" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Qty Must Be Filled" ControlToValidate="QtyTextBox"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="UnitTextBox" BackColor="#cccccc" Width="210"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PriceTextBox" Width="150" MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PriceRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Price Must Be Filled" ControlToValidate="PriceTextBox"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="AmountTextBox" BackColor="#cccccc" Width="150" MaxLength="18"></asp:TextBox>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
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

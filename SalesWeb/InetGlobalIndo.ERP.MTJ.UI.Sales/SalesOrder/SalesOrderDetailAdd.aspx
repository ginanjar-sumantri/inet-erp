<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SalesOrderDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder.SalesOrderDetailAdd" %>

<%@ Register Src="../ProductPicker.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function BlurAmount(_prmQty, _prmPrice, _prmAmount, _prmDecimalPlace)
        {
            if (_prmQty.value == 0 || _prmPrice.value == 0)
            {
                _prmAmount.value = 0;
            }
            else
            {   
                _prmAmount.value = parseFloat(GetCurrency(_prmQty.value)) * parseFloat(GetCurrency2(_prmPrice.value,_prmDecimalPlace.value));
                _prmAmount.value =  (_prmAmount.value == 0) ? "0" : FormatCurrency2(_prmAmount.value,_prmDecimalPlace.value);
            }
             _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency2(_prmPrice.value,_prmDecimalPlace.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
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
                                <asp:CustomValidator runat="server" ID="ProductCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="ProductDropDownList" Text="*" ErrorMessage="Product Must Be Filled"></asp:CustomValidator>--%>
                                        <uc1:ProductPicker ID="ProductPicker1" runat="server" />
                                        <asp:HiddenField ID="tempProductCode" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Specification
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SpecificationTextBox" runat="server" Width="500"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Qty Order
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="QtyOrderTextBox" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="QtyOrderRequiredFieldValidator" runat="server" Text="*"
                                            ErrorMessage="Qty Order Must Be Filled" ControlToValidate="QtyOrderTextBox"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Unit Order
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="UnitOrderDDL" OnSelectedIndexChanged="UnitOrderDDL_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:CustomValidator runat="server" ID="UnitOrderCustomValidator" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="UnitOrderDDL" Text="*" ErrorMessage="Unit Order Must Be Filled"></asp:CustomValidator>
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
                                        <asp:TextBox ID="PriceTextBox" runat="server"></asp:TextBox>
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
                                        <asp:TextBox ID="AmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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
                                        <asp:TextBox ID="QtyTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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
                                        <asp:DropDownList runat="server" ID="UnitDDL" OnSelectedIndexChanged="UnitDDL_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:CustomValidator runat="server" ID="UnitCustomValidator" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="UnitDDL" Text="*" ErrorMessage="Unit Must Be Filled"></asp:CustomValidator>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
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

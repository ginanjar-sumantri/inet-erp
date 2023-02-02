<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.DirectPurchase.DirectPurchaseDetailAdd, App_Web_-xa4oz-a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>

    <script language="javascript" type="text/javascript">
        function BlurAmount(_prmQty, _prmPrice, _prmAmount, _prmDecimalPlace) {
            if (_prmQty.value == 0 || _prmPrice.value == 0) {
                _prmAmount.value = 0;
            }
            else {
                _prmAmount.value = parseFloat(GetCurrency(_prmQty.value)) * parseFloat(GetCurrency2(_prmPrice.value, _prmDecimalPlace.value));
                _prmAmount.value = (_prmAmount.value == 0) ? "0" : FormatCurrency2(_prmAmount.value, _prmDecimalPlace.value);
            }
            _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency2(_prmPrice.value, _prmDecimalPlace.value);
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                <tr>
                                    <td>
                                        Product
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ProductCodeTextBox" runat="server" AutoPostBack ="true"
                                            ontextchanged="ProductCodeTextBox_TextChanged" ></asp:TextBox>
                                        <asp:Button ID="btnSearchProductCode" runat="server" Text="..." Width="20px" />
                                        <asp:HiddenField ID="tempProductCode" runat="server" />
                                        <asp:TextBox ID="ProductNameTextBox" runat="server" Width="300"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Warehouse
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="WarehouseCodeDropDownList" AutoPostBack="true"
                                            OnSelectedIndexChanged="WarehouseCodeDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="FgSubLedHiddenField" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Warehouse Subled
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="WrhsSubledDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Warehouse Location
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="LocationNameDropDownList">
                                        </asp:DropDownList>
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
                                        <asp:TextBox ID="QtyOrderTextBox" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="QtyOrderRequiredFieldValidator" runat="server" Text="*"
                                            ErrorMessage="Qty Order Must Be Filled" ControlToValidate="QtyOrderTextBox"></asp:RequiredFieldValidator>
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
                                        <%--<asp:TextBox ID="UnitTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>--%>
                                        <asp:DropDownList ID="UnitDropDownList" runat ="server">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="UnitHiddenField" runat ="server" />
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
                                <tr valign="top">
                                    <td>
                                        Remark
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="RemarkTextBox" runat="server" Width="270" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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

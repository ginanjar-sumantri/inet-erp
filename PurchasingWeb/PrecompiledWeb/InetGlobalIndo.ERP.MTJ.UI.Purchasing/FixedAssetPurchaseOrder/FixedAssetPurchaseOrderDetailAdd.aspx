<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.FixedAssetPurchaseOrder.FixedAssetPurchaseOrderDetailAdd, App_Web_igl1jxa6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">

        function CalculateNetto(_prmQtyConvertionTextBox, _prmPriceTextBox, _prmAmountTextBox) {
            var _tempQtyConvertionTextBox = parseFloat(GetCurrency(_prmQtyConvertionTextBox.value));
            var _tempPriceTextBox = parseFloat(GetCurrency(_prmPriceTextBox.value));
            var _tempAmountTextBox = parseFloat(GetCurrency(_prmAmountTextBox.value));

            _prmQtyConvertionTextBox.value = FormatCurrency(_tempQtyConvertionTextBox);

            _prmPriceTextBox.value = FormatCurrency(_tempPriceTextBox);

            var _amount = _tempQtyConvertionTextBox * _tempPriceTextBox;
            _prmAmountTextBox.value = FormatCurrency(_amount);
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
                                Fixed Asset Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FANameTextBox" runat="server" Width="500"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Specification
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SpecificationTextBox" runat="server" Width="300" MaxLength="255"
                                    Height="80" TextMode="MultiLine"></asp:TextBox>
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
                                <asp:TextBox ID="QtyConvertionTextBox" runat="server" Width="80"></asp:TextBox>
                                <%-- <asp:TextBox ID="UnitConvertionTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>--%>
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
                                <asp:DropDownList ID="UnitDDL" runat="server">
                                </asp:DropDownList>
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
                                <asp:TextBox ID="PriceTextBox" runat="server" Width="120"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PriceRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Price Must Be Filled" ControlToValidate="PriceTextBox"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="AmountTextBox" runat="server" Width="120" BackColor="#CCCCCC"></asp:TextBox>
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

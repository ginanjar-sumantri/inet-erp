<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PurchaseOrderDetailEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder.PurchaseOrderDetailEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">

        function CalculateQty(_prmQtyTextBox, _prmQtyFreeTextBox, _prmQtyTotalTextBox) {
            var _tempQtyTextBox = parseFloat(GetCurrency(_prmQtyTextBox.value));
            var _tempQtyFreeTextBox = parseFloat(GetCurrency(_prmQtyFreeTextBox.value));

            _prmQtyTextBox.value = FormatCurrency(_tempQtyTextBox);
            _prmQtyFreeTextBox.value = FormatCurrency(_tempQtyFreeTextBox);

            var _qtyTotal = _tempQtyTextBox + _tempQtyFreeTextBox;
            _prmQtyTotalTextBox.value = FormatCurrency(_qtyTotal);
        }

        function CalculateDiscountPercent(_prmQtyConvertionTextBox, _prmPriceTextBox, _prmAmountTextBox, _prmDiscPercentTextBox, _prmDiscTextBox, _prmNettoTextBox) {
            var _tempAmountTextBox = parseFloat(GetCurrency(_prmAmountTextBox.value));
            var _tempDiscPercentTextBox = parseFloat(GetCurrency(_prmDiscPercentTextBox.value));

            _prmDiscPercentTextBox.value = FormatCurrency(_tempDiscPercentTextBox);

            var _discountForex = _tempAmountTextBox * _tempDiscPercentTextBox / 100;
            _prmDiscTextBox.value = FormatCurrency(_discountForex);

            CalculateNetto(_prmQtyConvertionTextBox, _prmPriceTextBox, _prmAmountTextBox, _prmDiscTextBox, _prmNettoTextBox);
        }

        function Calculate(_prmQtyConvertionTextBox, _prmPriceTextBox, _prmAmountTextBox, _prmDiscPercentTextBox, _prmDiscTextBox, _prmNettoTextBox) {
            var _tempAmountTextBox = parseFloat(GetCurrency((_prmAmountTextBox.value == "") ? "0" : _prmAmountTextBox.value));
            if (isNaN(_tempAmountTextBox) == true) {
                _tempAmountTextBox = 0;
            }
            var _tempDiscPercentTextBox = parseFloat(GetCurrency((_prmDiscPercentTextBox.value == "") ? "0" : _prmDiscPercentTextBox.value));
            if (isNaN(_tempDiscPercentTextBox) == true) {
                _tempDiscPercentTextBox = 0;
            }
            var _tempQtyConvertionTextBox = parseFloat(GetCurrency((_prmQtyConvertionTextBox.value == "") ? "0" : _prmQtyConvertionTextBox.value));
            if (isNaN(_tempQtyConvertionTextBox) == true) {
                _tempQtyConvertionTextBox = 0;
            }
            var _tempPriceTextBox = parseFloat(GetCurrency((_prmPriceTextBox.value == "") ? "0" : _prmPriceTextBox.value));
            if (isNaN(_tempPriceTextBox) == true) {
                _tempPriceTextBox = 0;
            }
            var _tempNettoTextBox = parseFloat(GetCurrency((_prmNettoTextBox.value == "") ? "0" : _prmNettoTextBox.value));
            if (isNaN(_tempNettoTextBox) == true) {
                _tempNettoTextBox = 0;
            }

            _prmDiscPercentTextBox.value = FormatCurrency(_tempDiscPercentTextBox);

            var _amount = _tempQtyConvertionTextBox * _tempPriceTextBox;
            _prmAmountTextBox.value = FormatCurrency(_amount);

            var _discountForex = _amount * _tempDiscPercentTextBox / 100;
            _prmDiscTextBox.value = FormatCurrency(_discountForex);

            _prmQtyConvertionTextBox.value = FormatCurrency(_tempQtyConvertionTextBox);

            _prmPriceTextBox.value = FormatCurrency(_tempPriceTextBox);

            var _netto = _amount - _discountForex;
            _prmNettoTextBox.value = FormatCurrency(_netto);
        }

        function CalculateDiscountForex(_prmQtyConvertionTextBox, _prmPriceTextBox, _prmAmountTextBox, _prmDiscPercentTextBox, _prmDiscTextBox, _prmNettoTextBox) {
            var _tempAmountTextBox = parseFloat(GetCurrency(_prmAmountTextBox.value));
            var _tempDiscTextBox = parseFloat(GetCurrency(_prmDiscTextBox.value));

            _prmDiscTextBox.value = FormatCurrency(_tempDiscTextBox);

            var _discountPercent = _tempDiscTextBox / _tempAmountTextBox * 100;
            _prmDiscPercentTextBox.value = FormatCurrency(_discountPercent);

            CalculateNetto(_prmQtyConvertionTextBox, _prmPriceTextBox, _prmAmountTextBox, _prmDiscTextBox, _prmNettoTextBox);
        }

        function CalculateNetto(_prmQtyConvertionTextBox, _prmPriceTextBox, _prmAmountTextBox, _prmDiscTextBox, _prmNettoTextBox) {
            var _tempQtyConvertionTextBox = parseFloat(GetCurrency(_prmQtyConvertionTextBox.value));
            var _tempPriceTextBox = parseFloat(GetCurrency(_prmPriceTextBox.value));
            var _tempAmountTextBox = parseFloat(GetCurrency(_prmAmountTextBox.value));
            var _tempDiscTextBox = parseFloat(GetCurrency(_prmDiscTextBox.value));
            var _tempNettoTextBox = parseFloat(GetCurrency(_prmNettoTextBox.value));

            _prmQtyConvertionTextBox.value = FormatCurrency(_tempQtyConvertionTextBox);

            _prmPriceTextBox.value = FormatCurrency(_tempPriceTextBox);

            var _amount = _tempQtyConvertionTextBox * _tempPriceTextBox;
            _prmAmountTextBox.value = FormatCurrency(_amount);

            var _netto = _amount - _tempDiscTextBox;
            _prmNettoTextBox.value = FormatCurrency(_netto);

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
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductTextBox" runat="server" Width="500" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                ETD
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ETDTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" value="..." id="Img1" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_ETDTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="ETDLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ETA
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ETATextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" value="..." id="Img2" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_ETATextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="ETALiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                Qty Wrhs
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyTextBox" runat="server" Width="80" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty Free Wrhs
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyFreeTextBox" runat="server" Width="80"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyFreeRequiredFieldValidator0" runat="server" ControlToValidate="QtyFreeTextBox"
                                    ErrorMessage="Qty Free Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty Total Wrhs / Unit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyTotalTextBox" runat="server" Width="80" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox ID="UnitTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Qty<%-- / Unit (Convertion)--%>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyConvertionTextBox" runat="server" Width="80"></asp:TextBox>
                                <%--<asp:DropDownList runat="server" ID="UnitDDL" AutoPostBack="True" OnSelectedIndexChanged="UnitDDL_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="UnitCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="UnitDDL" Text="*" ErrorMessage="Unit Convertion Must Be Filled"></asp:CustomValidator>--%>
                                <asp:TextBox ID="UnitConvertionTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
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
                        <tr>
                            <td>
                                Disc %
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DiscPercentTextBox" runat="server" Width="80"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DiscPercentRequiredFieldValidator" runat="server"
                                    ControlToValidate="DiscPercentTextBox" ErrorMessage="Disc Percent Must Be Filled"
                                    Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Disc
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DiscTextBox" runat="server" Width="120"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DiscRequiredFieldValidator" runat="server" ControlToValidate="DiscTextBox"
                                    ErrorMessage="Disc Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Netto
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="NettoTextBox" runat="server" Width="120" BackColor="#CCCCCC"></asp:TextBox>
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

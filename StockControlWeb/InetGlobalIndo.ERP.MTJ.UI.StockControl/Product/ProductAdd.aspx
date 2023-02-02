<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Product.ProductAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Clear(_prmForexRate, _prmAmountForex, _prmAmountHome, _prmDecimalPalce) {
            var _tempForexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPalce.value));
            if (isNaN(_tempForexRate) == true) {
                _tempForexRate = 0;
            }

            if (_tempForexRate < 1) {
                _prmForexRate.value = "1";
                _prmAmountForex.value = "0";
                _prmAmountHome.value = "0";

            }
            else {
                _prmForexRate.value = FormatCurrency2(_tempForexRate, _prmDecimalPalce.value);
                _prmAmountForex.value = FormatCurrency2(0, _prmDecimalPalce.value);
                _prmAmountHome.value = FormatCurrency2(0, _prmDecimalPalce.value);
            }
        }

        function Calculate(_prmForexRate, _prmDebitForex, _prmDebitHome, _prmDecimalPalce) {
            var _tempForexRate = parseFloat(GetCurrency2(_prmForexRate.value, _prmDecimalPalce.value));
            if (isNaN(_tempForexRate) == true) {
                _tempForexRate = 0;
            }

            var _tempDebitForex = parseFloat(GetCurrency2(_prmDebitForex.value, _prmDecimalPalce.value));
            if (isNaN(_tempDebitForex) == true) {
                _tempDebitForex = 0;
            }

            _prmDebitForex.value = FormatCurrency2(_tempDebitForex, _prmDecimalPalce.value);

            var _debitHome = _tempForexRate * _tempDebitForex;
            _prmDebitHome.value = FormatCurrency2(_debitHome, _prmDecimalPalce.value);

        }

        function CalculateTotal(_prmSellingPrice, _prmDisc, _prmTotal, _prmDecimalPlace) {
            var _tempSellingPrice = parseFloat(GetCurrency2(_prmSellingPrice.value, _prmDecimalPlace.value));
            if (isNaN(_tempSellingPrice) == true) {
                _tempSellingPrice = 0;
            }

            var _tempDisc = parseFloat(GetCurrency2(_prmDisc.value, _prmDecimalPlace.value));
            if (isNaN(_tempDisc) == true) {
                _tempDisc = 0;
            }

            _prmSellingPrice.value = FormatCurrency2(_tempSellingPrice, _prmDecimalPlace.value);
            _prmDisc.value = FormatCurrency2(_tempDisc, _prmDecimalPlace.value);

            var _total = _tempSellingPrice - _tempDisc;
            _prmTotal.value = FormatCurrency2(_total, _prmDecimalPlace.value);

        }
    </script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="NextButton" runat="server">
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
                                <asp:TextBox runat="server" ID="ProductCodeTextBox" Width="150" MaxLength="20" onKeyPress="return event.keyCode!=13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Product Code Must Be Filled"
                                    Text="*" ControlToValidate="ProductCodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" ID="ProductNameTextBox" Width="420" MaxLength="60"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ProductRequiredFieldValidator" runat="server" ErrorMessage="Product Name Must Be Filled"
                                    Text="*" ControlToValidate="ProductNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product Sub Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ProductSubGroupDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="ProductSubGroupRequiredFieldValidator" runat="server" ErrorMessage="Product Sub Group Must Be Filled"
                                    Text="*" ControlToValidate="ProductSubGroupDropDownList" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="ProductTypeDropDownList"
                                    OnSelectedIndexChanged="ProductTypeDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Product Type Must Be Filled"
                                    Text="*" ControlToValidate="ProductTypeDropDownList" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Specification 1
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Spec1TextBox" Width="420" MaxLength="60"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Specification 2
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Spec2TextBox" Width="420" MaxLength="60"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Specification 3
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Spec3TextBox" Width="420" MaxLength="60"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Specification 4
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Spec4TextBox" Width="420" MaxLength="60"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="PriceGroupTR" runat="server">
                            <td>
                                Price Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="PriceGroupDropDownList"
                                    OnSelectedIndexChanged="PriceGroupDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
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
                                        <asp:DropDownList ID="CurrencyDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="CurrencyDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrencyCustomValidator" Text="*" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="CurrencyDropDownList" runat="server" ErrorMessage="Currency Must Be Choosen"></asp:CustomValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Buying Price
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="BuyingPriceTextBox" Width="150"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Selling Price
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SellingPriceTextBox" runat="server" Width="150"></asp:TextBox>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<tr>
                            <td>
                                Discount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="DiscountDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DiscountDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Discount Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DiscAmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr class="bgcolor_gray" style="height: 20px">
                                        <td align="center" class="tahoma_11_white" style="width: 120px">
                                            <b>Min Qty</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="width: 120px">
                                            <b>Max Qty</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="width: 120px">
                                            <b>Unit</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="width: 120px">
                                            <b>Unit Order</b>
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
                                <table>
                                    <tr align="center">
                                        <td style="width: 120px">
                                            <asp:TextBox ID="MinQtyTextBox" runat="server" MaxLength="18" Width="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="MinQtyTextBox"
                                                Display="Dynamic" ErrorMessage="Min Qty Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="MaxQtyTextBox" runat="server" MaxLength="18" Width="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="MaxQtyTextBox"
                                                Display="Dynamic" ErrorMessage="Max Qty Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 120px">
                                            <asp:DropDownList ID="UnitDropDownList" runat="server">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="RequiredFieldValidator3" runat="server" ClientValidationFunction="DropDownValidation"
                                                ControlToValidate="UnitDropDownList" Display="Dynamic" ErrorMessage="Unit Must Be Filled"
                                                Text="*"></asp:CustomValidator>
                                        </td>
                                        <td style="width: 120px">
                                            <asp:DropDownList ID="UnitOrderDropDownList" runat="server">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DropDownValidation"
                                                ControlToValidate="UnitOrderDropDownList" Display="Dynamic" ErrorMessage="Unit Order Must Be Filled"
                                                Text="*"></asp:CustomValidator>
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
                                <table>
                                    <tr class="bgcolor_gray" style="height: 20px">
                                        <td style="width: 120px" align="center" class="tahoma_11_white">
                                            <b>Length (CM)</b>
                                        </td>
                                        <td style="width: 120px" align="center" class="tahoma_11_white">
                                            <b>Width (CM)</b>
                                        </td>
                                        <td style="width: 120px" align="center" class="tahoma_11_white">
                                            <b>Height (CM)</b>
                                        </td>
                                        <td style="width: 120px" align="center" class="tahoma_11_white">
                                            <b>Volume (M3)</b>
                                        </td>
                                        <td style="width: 120px" align="center" class="tahoma_11_white">
                                            <b>Weight (KGS)</b>
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
                                <table>
                                    <tr align="center">
                                        <td style="width: 120px">
                                            <asp:TextBox ID="LengthTextBox" runat="server" MaxLength="18" Width="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="LengthTextBox"
                                                Display="Dynamic" ErrorMessage="Length Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="WidthTextBox" runat="server" MaxLength="18" Width="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="WidthTextBox"
                                                Display="Dynamic" ErrorMessage="Width Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="HeightTextBox" runat="server" MaxLength="18" Width="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="HeightTextBox"
                                                Display="Dynamic" ErrorMessage="Height Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="VolumeTextBox" runat="server" MaxLength="18" Width="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="VolumeTextBox"
                                                Display="Dynamic" ErrorMessage="Volume Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 120px">
                                            <asp:TextBox ID="WeightTextBox" runat="server" MaxLength="18" Width="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="WeightTextBox"
                                                Display="Dynamic" ErrorMessage="Weight Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <table>
                                    <tr class="bgcolor_gray" style="height: 20px">
                                        <td style="width: 120px" align="center" class="tahoma_11_white">
                                            <b>Active</b>
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
                                <table>
                                    <tr>
                                        <td align="center" style="width: 120px">
                                            <asp:DropDownList ID="ActiveDropDownList" runat="server">
                                                <asp:ListItem Selected="True" Text="Y" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Barcode
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <table>
                                    <tr align="center">
                                        <td style="width: 110px">
                                            <asp:TextBox runat="server" ID="BarcodeTextBox" Width="150">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fg Package
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FgPackageCheckBox" OnCheckedChanged="FgPackageCheckBox_CheckedChanged"
                                    AutoPostBack="true" />
                                <%--<asp:CheckBox runat="server" ID="FgConsignmentCheckBox"/>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fg Assembly
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FgAssemblyCheckBox" OnCheckedChanged="FgAssemblyCheckBox_CheckedChanged"
                                    AutoPostBack="true" />
                                <%--<asp:CheckBox runat="server" ID="FgConsignmentCheckBox"/>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fg Consignment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FgConsignmentCheckBox" OnCheckedChanged="FgConsignment_OnCheckedBox"
                                    AutoPostBack="true" />
                                <%--<asp:CheckBox runat="server" ID="FgConsignmentCheckBox"/>--%>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Supplier
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SuppNmbrTextBox" runat="server" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Button ID="btnSearchSupplier" runat="server" Text="..." CausesValidation="False" />
                                <br />
                                <asp:TextBox ID="SupplierNameTextBox" runat="server" Width="300" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Supplier Must Be Filled"
                                    Text="*" ControlToValidate="SuppNmbrTextBox">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="ItemDurationRow">
                            <td>
                                Item Duration
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ItemDurationTextBox" runat="server" Width="80"></asp:TextBox>
                                min(s)
                            </td>
                        </tr>
                        <tr>
                            <td>
                                FgActive
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" MaxLength="500"
                                    TextMode="MultiLine"></asp:TextBox>
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
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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

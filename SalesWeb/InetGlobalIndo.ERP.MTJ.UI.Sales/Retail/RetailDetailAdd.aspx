<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RetailDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Retail.RetailDetailAdd" %>

<%@ Register src="../ProductPicker.ascx" tagname="ProductPicker" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function BlurAmount(_prmQty, _prmPrice, _prmDiscount, _prmAmount, _prmDecimalPlace)
        {
            if (_prmQty.value == 0 || _prmPrice.value == 0)
            {
                _prmAmount.value = 0;
            }
            else
            {   
                _prmAmount.value = (parseFloat(_prmQty.value) * parseFloat(GetCurrency2(_prmPrice.value,_prmDecimalPlace.value))) - parseFloat(GetCurrency2(_prmDiscount.value,_prmDecimalPlace.value));
                _prmAmount.value =  (_prmAmount.value == 0) ? "0" : FormatCurrency2(_prmAmount.value,_prmDecimalPlace.value);
            }
             _prmDiscount.value = (_prmDiscount.value == 0) ? "0" : FormatCurrency2(_prmDiscount.value,_prmDecimalPlace.value);
             _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency2(_prmPrice.value,_prmDecimalPlace.value);
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
                            <td width="80px">
                                Product
                            </td>
                            <td width="10px">
                                :
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ProductDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProductDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="ProductCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="ProductDropDownList" Text="*" ErrorMessage="Product Must Be Chosed"></asp:CustomValidator>--%>
                                <uc1:ProductPicker ID="ProductPicker1" runat="server" />
                                <asp:HiddenField ID="tempProductCode" runat="server" />
                            </td>
                        </tr>
                        <tr id="PhoneTypeTR" runat="server">
                            <td>
                                Phone Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="PhoneTypeDropDownList" runat="server">
                                </asp:DropDownList>
                                <%--<asp:CustomValidator runat="server" ID="CustomValidator1" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="PhoneTypeDropDownList" Text="*" ErrorMessage="Phone Type Must Be Chosed"></asp:CustomValidator>--%>
                            </td>
                        </tr>
                        <tr id="SerialNmbrTR" runat="server">
                            <td>
                                Serial Number
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="SerialNumberDropDownList" runat="server">
                                </asp:DropDownList>
                                <%--<asp:CustomValidator runat="server" ID="CustomValidator2" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="SerialNumberDropDownList" Text="*" ErrorMessage="Serial Number Must Be Chosed"></asp:CustomValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                IMEI
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="IMEITextBox" runat="server" Width="200" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                                    ErrorMessage="IMEI Must Be Filled" ControlToValidate="IMEITextBox"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="QtyTextBox" runat="server" MaxLength="10" Width="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Qty Must Be Filled" ControlToValidate="QtyTextBox"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="PriceTextBox" runat="server" MaxLength="23" Width="230" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Discount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DiscTextBox" runat="server" MaxLength="23" Width="230" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountTextBox" Width="230" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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

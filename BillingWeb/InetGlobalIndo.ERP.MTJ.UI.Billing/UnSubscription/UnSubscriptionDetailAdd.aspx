<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="UnSubscriptionDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.UnSubscription.UnSubscriptionDetailAdd" %>

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
                                        Customer Billing Account
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <%--<asp:DropDownList ID="ProductDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProductDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="ProductCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="ProductDropDownList" Text="*" ErrorMessage="Product Must Be Filled"></asp:CustomValidator>--%>
                                        <asp:TextBox ID="CustBillAccountTextBox" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnSearchProductCode" runat="server" Text="..." Width="20px" />
                                        <asp:HiddenField ID="CustBillCodeHiddenFiled" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Product Name
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ProductNameTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                        <asp:TextBox ID="CurrTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                        <asp:TextBox ID="AmountTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Type Payment
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TypePaymentTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="TypePaymentHiddenField" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Activate Date
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ActivateDateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Expired Date
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ExpiredDateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                            <%--<td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

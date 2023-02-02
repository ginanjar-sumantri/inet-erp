<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditSupplier.NotaCreditSupplierDetailEdit, App_Web_uhnr75_a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function Count (_prmQty,_prmPrice, _prmAmount, _prmDecimalPlace)
    {
        var _amount = parseInt(_prmQty.value) * parseFloat(GetCurrency2(_prmPrice.value, _prmDecimalPlace.value));
        _prmAmount.value = (_amount == 0 ? "0" : FormatCurrency2(_amount, _prmDecimalPlace.value));
        _prmPrice.value=(_prmPrice.value == 0 )? "0" : FormatCurrency2(_prmPrice.value, _prmDecimalPlace.value);
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
                        <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Account
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountTextBox" Width="80" MaxLength="12" AutoPostBack="True"
                                            OnTextChanged="AccountTextBox_TextChanged"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDropDownList" AutoPostBack="True" OnSelectedIndexChanged="AccountDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="AccountRequiredFieldValidator" runat="server" ErrorMessage="Account Must Be Filled"
                                            Text="*" ControlToValidate="AccountTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:HiddenField runat="server" ID="FgSubledHiddenField" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Subled
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="SubledDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Qty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QtyTextBox" Width="100"></asp:TextBox>
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
                                Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PriceTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PriceRequiredFieldValidator" runat="server" ErrorMessage="Price Must Be Filled"
                                    Text="*" ControlToValidate="PriceTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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

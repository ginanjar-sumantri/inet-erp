<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRetur.PurchaseReturDetailAdd, App_Web_l83mqzis" %>

<%@ Register Src="../ProductPicker2.ascx" TagName="ProductPicker2" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <%--    <script language="javascript" type="text/javascript">
        function ValidateQtyRemain(_prmQty, _prmQtyRemain) {
            if (_prmQty.value == "") {
                _prmQty.value = _prmQtyRemain.value;
            }
            else {
                if (_prmQty.value > _prmQtyRemain.value) _prmQty.value = _prmQtyRemain.value;
            }
        }
    </script>--%>
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
                                <uc2:ProductPicker2 ID="ProductPicker21" runat="server" />
                                <asp:HiddenField ID="tempProductCode" runat="server" OnValueChanged="tempProductCode_ValueChanged" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="LocationDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LocationDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="LocationCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="LocationDropDownList" Text="*" ErrorMessage="Location Must Be Filled"></asp:CustomValidator>
                            </td>
                        </tr>--%>
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
                                Qty Retur
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyTextBox" runat="server" MaxLength="18" OnTextChanged="QtyTextBox_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Qty Must Be Filled" ControlToValidate="QtyTextBox"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="QtyRemainHidden" runat="server" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                QTY SJ
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QTYSJTextBox" OnTextChanged="QtyTextBox_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                QTY Close
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QTYCloseTextBox" OnTextChanged="QtyTextBox_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PriceTextBox" OnTextChanged="PriceTextBox_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="AmountForexTextBox" BackColor="#CCCCCC"></asp:TextBox>
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

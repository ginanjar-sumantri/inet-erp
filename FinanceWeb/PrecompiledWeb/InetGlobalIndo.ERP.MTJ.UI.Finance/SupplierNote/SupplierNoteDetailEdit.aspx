<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote.SupplierNoteDetailEdit, App_Web_9oeaajfn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function Count(_prmQty, _prmPrice, _prmAmount) {
            var _tempQty = parseFloat(GetCurrency(_prmQty.value));
            if (isNaN(_tempQty) == true) {
                _tempQty = 0;
            }

            var _tempPrice = parseFloat(GetCurrency(_prmPrice.value));
            if (isNaN(_tempPrice) == true) {
                _tempPrice = 0;
            }

            _prmPrice.value = FormatCurrency(_tempPrice)

            var _amount = _tempQty * _tempPrice;
            _prmAmount.value = FormatCurrency(_amount);
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
                            <td>
                                PO No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PONoTextBox" Width="150" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                RR No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RRNoTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductTextBox" runat="server" Width="420" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="QtyTextBox" Width="150" ></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="UnitTextBox" Width="210" BackColor="#CCCCCC" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Price Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PriceTextBox" runat="server" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
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
                                <asp:TextBox ID="AmountTextBox" runat="server" BackColor="#CCCCCC" Width="150"></asp:TextBox>
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

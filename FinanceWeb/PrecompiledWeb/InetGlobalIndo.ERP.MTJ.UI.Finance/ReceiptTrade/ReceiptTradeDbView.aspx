<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade.ReceiptTradeDbView, App_Web_hbijsilb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function CalculateAmountHome(_prmTextBox1, _prmTextBox2, _prmTextBox3) {
        _prmTextBox3.value = FormatCurrency(parseFloat(GetCurrency( _prmTextBox1.value)) * parseFloat(GetCurrency(_prmTextBox2.value)));
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Receipt Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PayTypeDropDownList" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Document No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DocumentNoTextBox" Width="150" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="40" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                                <asp:TextBox runat="server" ID="CurrRateTextBox" Width="100" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="AmountForexTextBox" Width="150" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Charge Revenue
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustRevenuePercentTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC" Width="100px"></asp:TextBox>
                                %
                                <asp:TextBox ID="CustRevenueAmountTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Customer Paid
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalCustPaidTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Charge Expense
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="BankExpensePercentTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true" Width="100px"></asp:TextBox>
                                %
                                <asp:TextBox ID="BankExpenseAmountTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Receipt Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalReceiptForexTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Receipt Home
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalReceiptHomeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true" Width="150px"></asp:TextBox>
                                <asp:HiddenField ID="DecimalPlaceHiddenField2" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Giro
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BankGiroDropDownList" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Due Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DueDateTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" MaxLength="500" Height="80"
                                    TextMode="MultiLine" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
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
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

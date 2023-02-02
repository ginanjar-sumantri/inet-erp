<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade.ReceiptTradeCrView, App_Web_hbijsilb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                                Invoice No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="InvoiceNoTextBox" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="40" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                AR
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr style="height: 10px">
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AR Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AR Already Paid</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AR to be Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="ARInvoiceTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ARBalanceTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ARToBePaidTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                PPN
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr style="height: 10px">
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Rate</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNRateTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                            </td>
                            <td valign="top">
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Already Paid</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN to be Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNInvoiceTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNBalanceTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNToBePaidTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Total Amount
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr style="height: 10px">
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Already Paid</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount to be Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="AmountInvoiceTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="AmountBalanceTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="AmountToBePaidTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Receipt Amount AR
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Balance</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReceiptAmountForexTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReceiptAmountBalanceTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Receipt Amount PPN
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Balance</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReceiptPPNTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="ReceiptPPNBalanceTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Receipt Amount Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Balance</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="TotalAmountForexTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="TotalAmountBalanceTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Value
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FgValueTextBox" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                    TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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

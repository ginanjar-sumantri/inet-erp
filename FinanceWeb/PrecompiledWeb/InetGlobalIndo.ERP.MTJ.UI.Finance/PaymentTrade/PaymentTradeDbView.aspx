<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade.PaymentTradeDbView, App_Web_blmuo5uz" %>

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
                                Invoice No
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="InvoiceNoTextBox" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="50" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="ForexRateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="True"
                                    Width="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Amount
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Forex</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Balance</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>Amount Home</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="AmountForexTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="AmountInvoiceTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="AmountBalanceTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="AmountHomeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                    <tr>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Rate</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Balance</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>PPN Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNRateTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNInvoiceTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNBalanceTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="PPNPaidTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                AP
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr>
                                        <td style="width: 100%;">
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AP Invoice</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AP Balance</b>
                                        </td>
                                        <td align="center" class="tahoma_11_white" style="background-color: Gray">
                                            <b>AP Paid</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="APInvoiceTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="APBalanceTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="APPaidTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="FgValueTextBox" Width="50" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="300" MaxLength="500" Height="80" TextMode="MultiLine"></asp:TextBox>
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

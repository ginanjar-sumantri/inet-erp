<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur.DPSuppReturPaymentView, App_Web_z_knjefe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Receipt Type
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ReceiptTypeTextBox" Width="250" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="DocumentNoTextBox" Width="150" MaxLength="50" BackColor="#CCCCCC"
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
                    <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="40" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                    <asp:TextBox runat="server" ID="CurrRateTextBox" Width="100" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="AmountForexTextBox" Width="150" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Amount Home
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AmountHomeTextBox" Width="150" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
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
                        TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="BankGiroTextBox" Width="250" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Bank Expense
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="BankExpenseTextBox" Width="150" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
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

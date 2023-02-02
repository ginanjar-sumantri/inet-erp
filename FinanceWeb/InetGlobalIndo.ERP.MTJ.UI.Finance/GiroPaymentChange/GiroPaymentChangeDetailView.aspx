<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="GiroPaymentChangeDetailView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPaymentChange.GiroPaymentChangeDetailView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="CancelButton">
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
            <!--<tr>
                <td>
                    Transaction No.
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="TransNoTextBox" runat="server" Width="150" BackColor="#CCCCCC" 
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>-->
            <tr>
                <td>
                    Old Giro
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="OldGiroTextBox" runat="server" Width="150" BackColor="#CCCCCC" 
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Payment Date
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC" 
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
                    <asp:TextBox runat="server" ID="DueDateTextBox" Width="100" BackColor="#CCCCCC" 
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Bank Payment
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="BankPaymentTextBox" BackColor="#CCCCCC" 
                        Width="200" ReadOnly="True"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="CurrTextBox" Width="100" BackColor="#CCCCCC" 
                        ReadOnly="True"></asp:TextBox>
                    <asp:TextBox ID="RateTextBox" runat="server" BackColor="#CCCCCC" 
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
                    <asp:TextBox runat="server" ID="AmountForexTextBox" BackColor="#CCCCCC" 
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
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

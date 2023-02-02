<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerGroupAccountView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.CustomerGroup.CustomerGroupAccountView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td colspan="3" class="tahoma_14_black">
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
                                Customer Group Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CustGroupCodeTextBox" Width="50" MaxLength="5" BackColor="#CCCCCC"
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
                                <asp:TextBox runat="server" ID="CurrencyTextBox" ReadOnly="true" 
                                    BackColor="#CCCCCC" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account AR
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountARTextBox" Width="80" MaxLength="12" AutoPostBack="True"
                                    BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountARNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Disc
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountDiscTextBox" Width="80" MaxLength="12" AutoPostBack="True"
                                    BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountDiscNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Credit AR
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountCreditARTextBox" Width="80" MaxLength="12"
                                    AutoPostBack="True" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountCreditARNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account DP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountDPTextBox" Width="80" MaxLength="12" AutoPostBack="True"
                                    BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountDPNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account PPn(IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountPPNTextBox" Width="80" MaxLength="12" AutoPostBack="True"
                                    BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountPPNNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Other Sales
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountOtherTextBox" Width="80" MaxLength="12" AutoPostBack="True"
                                    ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountOtherNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
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

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="AccountView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Account.AccountView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
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
                                Branch Account
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BranchAccountTextBox" Width="560" BackColor="#CCCCCC"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Class Account
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CodeTextBox" Width="100" MaxLength="7" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                                <asp:TextBox runat="server" ID="CodeNameTextBox" Width="280" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Detail
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DetailTextBox" Width="100" MaxLength="5" BackColor="#CCCCCC"
                                    ReadOnly="True" AutoPostBack="True"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="100" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DescTextBox" BackColor="#CCCCCC" MaxLength="50" Width="350"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset title="Saldo Normal">
                        <legend>Normal Balance</legend>
                        <asp:RadioButtonList runat="server" ID="RadioButtonList1" Enabled="false" RepeatDirection="Horizontal"
                            RepeatColumns="2">
                            <asp:ListItem Value="D" Selected="True">Debit</asp:ListItem>
                            <asp:ListItem Value="C">Credit</asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset title="Subled Type">
                        <legend>Sub Ledger</legend>
                        <asp:RadioButtonList runat="server" Enabled="false" ID="SubledRBL" RepeatDirection="Horizontal"
                            RepeatColumns="2">
                        </asp:RadioButtonList>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="checkbox1" runat="server" Enabled="false" />
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
                                <%--</td>
                            <td>--%>
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False"
                                    OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

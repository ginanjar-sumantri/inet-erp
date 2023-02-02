<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FASubGroup.FAGroupSubAccView, App_Web_p6ocnxdl" %>

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
                                Currency
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CurrencyTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Asset
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AccAssetsTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountAssetNameTextBox" Width="350" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Acc Depreciation Expense
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AccDepreciationTextBox" runat="server" Width="100" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountDPNameTextBox" Width="350" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Acc Accumulated Depreciation
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AccAkumTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountAkumNameTextBox" Width="350" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Sales
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AccSalesTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountSalesNameTextBox" Width="350" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Lease Out
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AccTenancyTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountTenancyNameTextBox" Width="350" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
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

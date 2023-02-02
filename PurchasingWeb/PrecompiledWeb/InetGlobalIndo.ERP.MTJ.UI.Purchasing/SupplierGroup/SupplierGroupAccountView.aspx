<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.SupplierGroup.SupplierGroupAccountView, App_Web_1rdy5tvz" %>

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
                                <asp:TextBox runat="server" ID="CurrencyTextBox" ReadOnly="true" 
                                    BackColor="#CCCCCC" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account AP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountAPTextBox" Width="80" MaxLength="12" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountAPNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account AP Transit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountAPTransitTextBox" Width="80" MaxLength="12"
                                    ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountAPTransitNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Debit AP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountDebitAPTextBox" Width="80" MaxLength="12"
                                    ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountDebitAPNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
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
                                <asp:TextBox runat="server" ID="AccountDPTextBox" Width="80" MaxLength="12" ReadOnly="True"
                                    BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountDPNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Variant PO (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountVariantPOTextBox" Width="80" MaxLength="12"
                                    ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountVariantPONameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account PPn (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountPPnTextBox" Width="80" MaxLength="12" ReadOnly="True"
                                    BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountPPnNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account PPh (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountPPhTextBox" Width="80" MaxLength="12" ReadOnly="True"
                                    BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountPPhNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Other Purchase
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountOtherPurchaseTextBox" Width="80" MaxLength="12"
                                    ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountOtherPurchaseNameTextBox" ReadOnly="true"
                                    BackColor="#CCCCCC" Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Disc Purchase
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountDiscPurchaseTextBox" Width="80" MaxLength="12"
                                    ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountDiscPurchaseNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Duty Transit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountDutyTransitTextBox" Width="80" MaxLength="12"
                                    ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountDutyTransitNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Handling Transit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AccountHandlingTransitTextBox" Width="80" MaxLength="12"
                                    ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox runat="server" ID="AccountHandlingTransitNameTextBox" ReadOnly="true"
                                    BackColor="#CCCCCC" Width="300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                FgActive
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" Enabled="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" MaxLength="500"
                                    TextMode="MultiLine" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

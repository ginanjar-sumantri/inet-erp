<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierDifferenceRate.SuppDiffRateDetailEdit, App_Web_vh55nmb8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                                Invoice No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="InvoiceNoTextBox" runat="server" Width="200" BackColor="#CCCCCC"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="SuppNameTextBox" Width="420" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ForexRateTextBox" BackColor="#CCCCCC" Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Saldo Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AmountForexTextBox" BackColor="#CCCCCC" Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Old Saldo
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AmountHomeTextBox" BackColor="#CCCCCC" Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                New Saldo
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NewAmountHomeTextBox" BackColor="#CCCCCC" Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Adjust
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AdjustTextBox" BackColor="#CCCCCC" ReadOnly="True"
                                    Width="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Apply To PPN
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="IsApplyToPPNCheckBox" AutoPostBack="true" OnCheckedChanged="IsApplyToPPNCheckBox_CheckedChanged">
                                </asp:CheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PPN Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PPNRateTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Saldo PPN Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PPNForexTextBox" runat="server" BackColor="#cccccc"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Old Saldo PPN (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PPNHomeTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                New Saldo PPN (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="NewPPNHomeTextBox" runat="server" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Adjust PPN (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PPNAdjustTextBox" runat="server" BackColor="#CCCCCC">
                                </asp:TextBox>
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

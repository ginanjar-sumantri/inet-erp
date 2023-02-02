<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustDiffRateDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerDifferenceRate.CustDiffRateDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel DefaultButton="SaveButton" ID="Panel1" runat="server">
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
                                <asp:DropDownList ID="InvoiceNoDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="InvoiceNoDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="CustomValidator1" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="InvoiceNoDropDownList" Text="*" ErrorMessage="Invoice No. Must Be Filled"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustTextBox" Width="420" ReadOnly="true" BackColor="#CCCCCC" runat="server">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="ForexRateTextBox" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="AmountForexTextBox" BackColor="#cccccc"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Old Saldo (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountHomeTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                New Saldo (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NewAmountHomeTextBox" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Adjust (IDR)
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AdjustTextBox" BackColor="#CCCCCC">
                                </asp:TextBox>
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

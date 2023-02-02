<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCashReceive.PettyCashReceiveDetailView, App_Web_mn33juci" %>

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
                <td>
                    Account
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AccountTextBox" Width="80" MaxLength="12" ReadOnly="true"
                        BackColor="#cccccc"></asp:TextBox>
                    <asp:TextBox runat="server" ID="AccountNameTextBox" Width="300" MaxLength="50" ReadOnly="true"
                        BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Sub Ledger
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="SubledgerTextBox" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="FgSubledHiddenField" />
                </td>
            </tr>
            <tr>
                <td>
                    Amount
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AmountTextbox" MaxLength="20" ReadOnly="True" BackColor="#CCCCCC"></asp:TextBox>
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
                        TextMode="MultiLine" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
            <td>
                Currency / Rate
            </td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox runat="server" ID="CurrTextBox" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                <asp:TextBox runat="server" ID="RateTextbox" MaxLength="20" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
            </td>
        </tr>--%>
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

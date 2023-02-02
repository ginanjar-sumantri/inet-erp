<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RegistrationConfigView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.RegistrationConfig.RegistrationConfigView" %>

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
                    Registration Code
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="RegistrationCodeTextBox" Width="200" MaxLength="20"
                        ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Registration Name
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="RegistrationNameTextBox" Width="400" MaxLength="50"
                        ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Payment Status
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:RadioButtonList ID="PaymentStatusRadioButtonList" runat="server" RepeatDirection="Horizontal"
                        Enabled="false">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    Registration Product Code
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="RegProductCodeTextBox" runat="server" Width="400" ReadOnly="true"
                        BackColor="#CCCCCC">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Registration Fee
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="RegFeeTextBox" runat="server" Width="200" MaxLength="23" ReadOnly="true"
                        BackColor="#CCCCCC">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Installation Product Code
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="InstallationProductCodeTextBox" runat="server" Width="400" ReadOnly="true"
                        BackColor="#CCCCCC">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Instalation Fee
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="InstalationFeeTextBox" runat="server" Width="200" MaxLength="23"
                        ReadOnly="true" BackColor="#CCCCCC">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Deposit Product Code
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="DepositProductCodeTextBox" runat="server" Width="400" ReadOnly="true"
                        BackColor="#CCCCCC">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Deposit
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="DepositTextBox" runat="server" Width="200" MaxLength="23" ReadOnly="true"
                        BackColor="#CCCCCC">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Recurring First Charge
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="RecurringFirstChargeTextBox" runat="server" Width="100" MaxLength="5"
                        ReadOnly="true" BackColor="#CCCCCC">
                    </asp:TextBox>
                </td>
            </tr>
            <tr valign="top">
                <td>
                    Decription
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"
                        ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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

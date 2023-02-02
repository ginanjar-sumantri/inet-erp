<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.RegistrationConfig.RegistrationConfigEdit, App_Web_qdauerci" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
    <asp:Literal ID="javascriptReceiver1" runat="server"></asp:Literal>
    <asp:Literal ID="javascriptReceiver2" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td colspan="3" class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
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
                    <asp:TextBox runat="server" ID="RegistrationNameTextBox" Width="400" MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RegistrationNameRequiredFieldValidator" runat="server"
                        ErrorMessage="Registration Name Must Be Filled" Text="*" ControlToValidate="RegistrationNameTextBox"
                        Display="Dynamic"></asp:RequiredFieldValidator>
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
                    <asp:RadioButtonList ID="PaymentStatusRadioButtonList" runat="server" RepeatDirection="Horizontal">
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
                    <asp:TextBox ID="RegProductCodeTextBox" runat="server" Width="200" MaxLength="20"
                        AutoPostBack="true" OnTextChanged="RegProductCodeTextBox_TextChanged">
                    </asp:TextBox>
                    <asp:Button ID="btnRegProductCode" runat="server" Text="..." />
                    <asp:TextBox ID="RegProductNameTextBox" runat="server" Width="400" MaxLength="60">
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
                    <asp:TextBox ID="RegFeeTextBox" runat="server" Width="200" MaxLength="23">
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
                    <asp:TextBox ID="InstallationProductCodeTextBox" runat="server" Width="200" MaxLength="20"
                        AutoPostBack="true" OnTextChanged="InstallationProductCodeTextBox_TextChanged">
                    </asp:TextBox>
                    <asp:Button ID="btnInstallationProductCode" runat="server" Text="..." />
                    <asp:TextBox ID="InstallationProductNameTextBox" runat="server" Width="400" MaxLength="60">
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
                    <asp:TextBox ID="InstalationFeeTextBox" runat="server" Width="200" MaxLength="23">
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
                    <asp:TextBox ID="DepositProductCodeTextBox" runat="server" Width="200" MaxLength="20"
                        AutoPostBack="true" OnTextChanged="DepositProductCodeTextBox_TextChanged">
                    </asp:TextBox>
                    <asp:Button ID="btnDepositProductCode" runat="server" Text="..." />
                    <asp:TextBox ID="DepositProductNameTextBox" runat="server" Width="400" MaxLength="60">
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
                    <asp:TextBox ID="DepositTextBox" runat="server" Width="200" MaxLength="23">
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
                    <asp:TextBox ID="RecurringFirstChargeTextBox" runat="server" Width="100" MaxLength="5">
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
                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                    <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                    characters left
                </td>
            </tr>
            <tr>
                <td colspan="3">
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

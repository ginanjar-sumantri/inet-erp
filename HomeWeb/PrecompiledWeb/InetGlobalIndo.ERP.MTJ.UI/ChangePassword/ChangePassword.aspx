<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Home.ChangePassword.ChangePassword, App_Web_qxqtlhi5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SavePassButton" runat="server">
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
                                Old Password
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="OldPasswordTextBox" Width="120" MaxLength="128" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="OldPassRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Old Password Must Be Filled" ControlToValidate="OldPasswordTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                New Password
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PasswordTextBox" Width="120" MaxLength="128" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                                    ErrorMessage="New Password Must Be Filled" ControlToValidate="PasswordTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Confirm New Password
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PasswordCfmTextBox" Width="120" MaxLength="128" TextMode="Password"></asp:TextBox>
                                <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ErrorMessage="Confirmation password is different"
                                    Text="*" ControlToCompare="PasswordTextBox" ControlToValidate="PasswordCfmTextBox"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                <table cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="SavePassButton" runat="server" OnClick="SavePassButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

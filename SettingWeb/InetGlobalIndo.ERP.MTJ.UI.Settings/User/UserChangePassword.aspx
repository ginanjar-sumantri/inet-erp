<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="UserChangePassword.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.User.UserChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SavePassButton">
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
                    <table cellpadding="0" cellspacing="0" width="0">
                        <tr style="height: 20px">
                            <td>
                                User Name
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="UserNameLabel"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                Old Password
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="OldPasswordTextBox" Width="120" MaxLength="128" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:HiddenField ID="OldPassword" runat="server" />
                        --%>
                        <tr>
                            <td>
                                New Password
                            </td>
                            <td>
                                :&nbsp;
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PasswordTextBox" Width="120" MaxLength="128" TextMode="Password"></asp:TextBox>
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
                            <td colspan="3">
                                <table cellpadding="3" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="SavePassButton" runat="server" OnClick="SavePassButton_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ResetButton" runat="server" OnClick="ResetButton_Click" />
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

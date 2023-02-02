<%@ Page Title="" Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true"
    CodeFile="ManageUserAdd.aspx.cs" Inherits="SMS.SMSWeb.ManageUser.ManageUserAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    <asp:Label ID="PageLabel" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <table>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                <asp:Label ID="WarningLabel" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            User ID
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="UserIDTextBox" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="PasswordTextBox" runat="server" MaxLength="49" TextMode="Password">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Confirm Password
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" MaxLength="49" TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="PasswordTextBox"
                                ControlToValidate="ConfirmPasswordTextBox" ErrorMessage="Password is not identical"
                                Text="*"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="EmailTextBox" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:ImageButton ID="SaveImageButton" runat="server" OnClick="SaveImageButton_Click" />
                            &nbsp;&nbsp;
                            <asp:ImageButton ID="CancelImageButton" runat="server" OnClick="CancelImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

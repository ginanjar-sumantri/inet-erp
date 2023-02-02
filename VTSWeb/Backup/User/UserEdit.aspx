<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="VTSWeb.UI.UserEdit" %>

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
                    <fieldset>
                        <legend>User</legend>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td width="100px">
                                                UserName
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="UserTextBox" Width="150" MaxLength="100" BackColor="#CCCCCC"
                                                    ReadOnly="True"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="EmailTextBox" Width="200" MaxLength="200"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ErrorMessage="Email Must Be Filled"
                                                    Text="*" ControlToValidate="EmailTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Employee
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="EmpDropDownList" runat="server">
                                                </asp:DropDownList>
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
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <fieldset>
                        <legend>Password</legend>
                        <asp:Panel ID="Panel6" runat="server" DefaultButton="SavePasswordImageButton">
                            <table cellpadding="3" cellspacing="0" width="0">
                                <tr>
                                    <td width="100px">
                                        Password
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="PassTextBox" Width="150" MaxLength="200" TextMode="Password"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="PassRequiredFieldValidator" runat="server" ErrorMessage="Password Must Be Filled"
                                                        Text="*" ControlToValidate="PassTextBox" Display="Dynamic"></asp:RequiredFieldValidator>--%>
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
                                        <asp:TextBox ID="ConfirmTextBox" runat="server" Width="150" MaxLength="200" TextMode="Password"></asp:TextBox>
                                        <asp:CompareValidator ID="ConfirmCompareValidator" runat="server" ErrorMessage="Confirm Password Must Be Same With Password"
                                            Text="*" ControlToCompare="PassTextBox" ControlToValidate="ConfirmTextBox"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3">
                                        <table cellpadding="3" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SavePasswordImageButton" runat="server" OnClick="SavePasswordImageButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
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

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="UserAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.User.UserAdd" %>

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
                                User Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="UserNameTextBox" Width="120" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequiredFieldValidator" runat="server" ErrorMessage="User Name Must Be Filled"
                                    Text="*" ControlToValidate="UserNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="PasswordTextBox" Width="120" MaxLength="128" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ErrorMessage="Password Must Be Filled"
                                    Text="*" ControlToValidate="PasswordTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="PasswordCfmTextBox" Width="120" MaxLength="128" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordCfmRequiredFieldValidator" runat="server"
                                    ErrorMessage="Password Confirm Must Be Filled" Text="*" ControlToValidate="PasswordCfmTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="PasswordTextBox"
                                    ControlToValidate="PasswordCfmTextBox" ErrorMessage="Password is not identical"
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
                                <asp:TextBox runat="server" ID="EmailTextBox" Width="300" MaxLength="256"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ErrorMessage="Email must be filled"
                                    Text="*" ControlToValidate="EmailTextBox"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                    ErrorMessage="Email is not valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Security Question
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QuestionTextBox" Width="300" MaxLength="256"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequiredFieldValidator" runat="server" ErrorMessage="Security Question Must Be Filled"
                                    Text="*" ControlToValidate="QuestionTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Answer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AnswerTextBox" Width="300" MaxLength="256"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequiredFieldValidator" runat="server" ErrorMessage="Answer Must Be Filled"
                                    Text="*" ControlToValidate="AnswerTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:DropDownList runat="server" ID="EmployeeDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Roles
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:CheckBoxList runat="server" ID="RoleCheckBoxList" RepeatColumns="2" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
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

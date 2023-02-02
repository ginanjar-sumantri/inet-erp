<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="UserAdd.aspx.cs" Inherits="VTSWeb.UI.UserAdd" %>

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
                                UserName
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="UserTextBox" Width="150" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserRequiredFieldValidator" runat="server" ErrorMessage="UserName Must Be Filled"
                                    Text="*" ControlToValidate="UserTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="PassTextBox" Width="150" MaxLength="200" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PassRequiredFieldValidator" runat="server" ErrorMessage="Password Must Be Filled"
                                    Text="*" ControlToValidate="PassTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                    ErrorMessage="Email Not Valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
                                <asp:DropDownList ID="EmpDropDownList" runat="server"></asp:DropDownList>
                                <asp:CustomValidator ID="EmpCustomValidator" runat="server" ControlToValidate="EmpDropDownList"
                                    ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Employee Must Be Chosen"></asp:CustomValidator>
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


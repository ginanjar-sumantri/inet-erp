<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.User.UserEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="DummyButton">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
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
                        <legend>Header</legend>
                        <table width="0">
                            <tr>
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
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Change Email</legend>
                        <asp:Panel ID="Panel4" DefaultButton="SaveEmailButton" runat="server">
                            <table cellpadding="3" cellspacing="0" width="0">
                                <tr>
                                    <td>
                                        Email
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        &nbsp;<asp:TextBox runat="server" ID="EmailTextBox" Width="300" MaxLength="256"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email is not valid"
                                            Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3">
                                        <table cellpadding="3" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SaveEmailButton" runat="server" OnClick="SaveEmailButton_Click" />
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
                    <fieldset>
                        <legend>Change Employee</legend>
                        <asp:Panel ID="Panel5" runat="server" DefaultButton="SaveEmployeeButton">
                            <table cellpadding="3" cellspacing="0" width="0">
                                <tr>
                                    <td>
                                        Employee
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        &nbsp;<asp:DropDownList runat="server" ID="EmployeeDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3">
                                        <table cellpadding="3" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SaveEmployeeButton" runat="server" OnClick="SaveEmployeeButton_Click" />
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
                    <fieldset>
                        <legend>Change Roles</legend>
                        <asp:Panel ID="Panel6" runat="server" DefaultButton="SaveRoleImageButton">
                            <table cellpadding="3" cellspacing="0" width="0">
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
                                <tr>
                                    <td align="left" colspan="3">
                                        <table cellpadding="3" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SaveRoleImageButton" runat="server" OnClick="SaveRoleImageButton_Click" />
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
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" OnClick="BackButton_Click" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="DummyButton" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

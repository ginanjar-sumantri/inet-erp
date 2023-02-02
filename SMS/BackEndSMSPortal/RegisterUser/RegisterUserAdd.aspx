<%@ Page Language="C#" MasterPageFile="~/SMSAdminMasterPage.master" AutoEventWireup="true"
    CodeFile="RegisterUserAdd.aspx.cs" Inherits="SMS.BackEndSMSPortal.RegisterUser.RegisterUserAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    REGISTER USER ADD
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">--%>
    <table width="100%">
        <%--<tr>
            <td colspan="3">
                <asp:Label ID="PageLabel" runat="server" Font-Bold="true" Font-Size="15px"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                <asp:Label ID="WarningLabel" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="15px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            Organization Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="OrganizationNameTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="OrganizationNameValidator" runat="server" Text="*"
                                ErrorMessage="Organization Name must be filled" ControlToValidate="OrganizationNameTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Hosted
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="HostedCheckBox" Text="Hosted" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            User Limit
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="UserLimitTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserLimitValidator" runat="server" Text="*" ErrorMessage="User Limit must be filled"
                                ControlToValidate="UserLimitTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Balance Check Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="BalanceCheckCodeTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="BalanceCheckCodeValidator" runat="server" Text="*"
                                ErrorMessage="Balance Check Code must be filled" ControlToValidate="BalanceCheckCodeTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            UserID
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="UserIDTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserIDValidator" runat="server" Text="*" ErrorMessage="User ID must be filled"
                                ControlToValidate="UserIDTextBox"></asp:RequiredFieldValidator>
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
                            <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordValidator" runat="server" Text="*" ErrorMessage="Password must be filled"
                                ControlToValidate="PasswordTextBox"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Package Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="PackageNameDDL" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="PackageNameValidator" runat="server" Text="*" ControlToValidate="PackageNameDDL"
                                ErrorMessage="Package Name Must Be Selected" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
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
                            <asp:TextBox ID="EmailTextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click"
                                CausesValidation="true" />
                            &nbsp;
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click"
                                CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

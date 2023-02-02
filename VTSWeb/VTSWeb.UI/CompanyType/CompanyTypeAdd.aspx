<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CompanyTypeAdd.aspx.cs" Inherits="VTSWeb.UI.CompanyTypeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="WarningLabel" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            Company Type&nbsp; Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CompanyTypeCodeTextBox" MaxLength="5" Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyTypeCodeRequiredFieldValidator" runat="server"
                                ErrorMessage="CompanyType Code Must Be Filled" Text="*" ControlToValidate="CompanyTypeCodeTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Company Type Nama
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CompanyTypeNameTextBox" Width="250" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CompanyTypeNameRequiredFieldValidator" runat="server"
                                ErrorMessage="CompanyType Name Must Be Filled" Text="*" ControlToValidate="CompanyTypeNameTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
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
    </td> </tr> </table>
</asp:Content>

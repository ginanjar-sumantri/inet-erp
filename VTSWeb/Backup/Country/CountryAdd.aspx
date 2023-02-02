<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CountryAdd.aspx.cs" Inherits="VTSWeb.UI.MsCountryAdd" %>

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
                            Country Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CountryCodeTextBox" MaxLength="5" Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CountryCodeRequiredFieldValidator" runat="server"
                                ErrorMessage="Country Code Must Be Filled" Text="*" ControlToValidate="CountryCodeTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Country Nama
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CountryNameTextBox" Width="250" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CountryNameRequiredFieldValidator" runat="server"
                                ErrorMessage="Country Name Must Be Filled" Text="*" ControlToValidate="CountryNameTextBox"
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

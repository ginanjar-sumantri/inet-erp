<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CompanyEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.Company.CompanyEdit" %>

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
                                Company Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NameTextBox" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Company Name Must Be Filled"
                                    Text="*" ControlToValidate="NameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Logo
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="LogoTextBox" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Logo Must Be Filled"
                                    Text="*" ControlToValidate="LogoTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Primary Address
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AddressTextBox" Width="200" MaxLength="500" TextMode="MultiLine"
                                    Height="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AddressRequiredFieldValidator" runat="server" ErrorMessage="Address Must Be Filled"
                                    Text="*" ControlToValidate="AddressTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Company Tag
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CompanyTagTextBox" Width="50" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CompanyTagRequiredFieldValidator" runat="server" ErrorMessage="Company Tag Must Be Filled"
                                    Text="*" ControlToValidate="CompanyTagTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tax Branch No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TaxBranchNoTextBox" Width="50" MaxLength="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Default Company
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="DefaultCompanyCheckBox" />
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

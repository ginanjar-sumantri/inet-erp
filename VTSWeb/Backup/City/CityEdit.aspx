<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CityEdit.aspx.cs" Inherits="VTSWeb.UI.CityEdit" %>

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
                            City Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CityCodeTextBox" MaxLength="5" Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            City Nama
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="CityNameTextBox" Width="250" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="CityNameRequiredFieldValidator" runat="server" ErrorMessage="City Name Must Be Filled"
                                Text="*" ControlToValidate="CityNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            Regional
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="RegionalDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="RegionalCustomValidator" runat="server" ControlToValidate="RegionalDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Regional Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            Country
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="CountryDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CountryCustomValidator" runat="server" ControlToValidate="CountryDropDownList"
                                ClientValidationFunction="DropDownValidation" Text="*" ErrorMessage="Company Must Be Chosen"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
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
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SupplierContactAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier.SupplierContactAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table cellpadding="3" cellspacing="0" border="0" width="0">
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
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                Supplier Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SuppCodeTextBox" Width="100" ReadOnly="true" BackColor="#cccccc"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ContactNameTextBox" runat="server" Width="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ContactNameRequiredFieldValidator0" runat="server"
                                    ErrorMessage="Contact Name Must Be Filled" Text="*" ControlToValidate="ContactNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Title
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ContactTitleTextBox" runat="server" Width="200" MaxLength="100"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Postal Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PostalCodeTextBox" runat="server" Width="100" MaxLength="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address #1
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="Addr1TextBox" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Telephone
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PhoneTextBox" runat="server" Width="200" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address #2
                            </td>
                            <td>
                                &nbsp;:
                            </td>
                            <td>
                                <asp:TextBox ID="Addr2TextBox" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Fax
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FaxTextBox" runat="server" Width="200" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Country
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="CountryDropDownList" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Email
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="EmailTextBox" runat="server" Width="300" MaxLength="40"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                    Text="*" ErrorMessage="Email is not Valid" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                &nbsp;
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
                                <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" OnClick="ResetButton_Click" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

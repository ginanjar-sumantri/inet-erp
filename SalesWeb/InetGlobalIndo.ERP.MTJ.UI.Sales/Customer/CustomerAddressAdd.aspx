<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerAddressAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Customer.CustomerAddressAdd" %>

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
                                Customer Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustCodeTextBox" Width="100" ReadOnly="true" BackColor="#cccccc"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Delivery Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DeliveryCodeTextBox" runat="server" Width="100" MaxLength="12"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DeliveryCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Delivery Code Must Be Filled" Text="*" ControlToValidate="DeliveryCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Delivery Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DeliveryNameTextBox" runat="server" MaxLength="50" Width="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DeliveryNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Delivery Name Must Be Filled" Text="*" ControlToValidate="DeliveryNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="Address1TextBox" runat="server" Width="200" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address #2
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="Address2TextBox" runat="server" Width="200" TextMode="MultiLine"></asp:TextBox>
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
                        </tr>
                        <tr>
                            <td>
                                Postal Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PostalCodeTextBox" runat="server" Width="100" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
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

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SupplierContactView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier.SupplierContactView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                                No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ItemNoTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ItemNoRequiredFieldValidator" runat="server" ErrorMessage="No. Must Be Filled"
                                    Text="*" ControlToValidate="ItemNoTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Country
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CountryTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="ContactNameTextBox" runat="server" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ContactNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Contact Name Must Be Filled" Text="*" ControlToValidate="ContactNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="PostalCodeTextBox" runat="server" Width="100" MaxLength="5" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox ID="ContactTitleTextBox" runat="server" Width="200" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="PhoneTextBox" runat="server" Width="200" MaxLength="30" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="Addr1TextBox" runat="server" Width="250" TextMode="MultiLine" ReadOnly="true"
                                    BackColor="#cccccc"></asp:TextBox>
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
                                <asp:TextBox ID="FaxTextBox" runat="server" Width="200" MaxLength="30" ReadOnly="true"
                                    BackColor="#cccccc"></asp:TextBox>
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
                                <asp:TextBox ID="Addr2TextBox" runat="server" Width="250" TextMode="MultiLine" ReadOnly="true"
                                    BackColor="#cccccc"></asp:TextBox>
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
                                <asp:TextBox ID="EmailTextBox" runat="server" Width="300" MaxLength="40" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            <%--</td>
                            <td>--%>
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

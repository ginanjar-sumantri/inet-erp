<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerContactView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Customer.CustomerContactView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <table width="0" border="0" cellpadding="3" cellspacing="0">
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
                    <table width="0" border="0" cellpadding="3" cellspacing="0">
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
                                Contact Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ContactTypeDropDownList" runat="server" Enabled="false">
                                    <asp:ListItem Value="Mr" Text="Mr"></asp:ListItem>
                                    <asp:ListItem Value="Mrs" Text="Mrs"></asp:ListItem>
                                </asp:DropDownList>
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
                                <asp:TextBox ID="CountryTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
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
                                Birthday
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="BirthDateTextBox" runat="server" BackColor="#CCCCCC">
                                </asp:TextBox>
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
                                Religion
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ReligionTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
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
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" TextMode="MultiLine" Height="80"
                                    ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
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
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="0" border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RadiusAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.Radius.RadiusAdd" %>

<%@ Register Src="../ProductPicker.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Radius Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RadiusNameTextBox" runat="server" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustomerTextBox" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="custCode" runat="server"></asp:HiddenField>
                                <asp:Button ID="btnSearchCustomer" runat="server" Text="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Radius IP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RadiusIPTextBox" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Radius UserName
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RadiusUserNameTextBox" runat="server" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Radius Password
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RadiusPwdTextBox" runat="server" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Radius DB Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RadiusDbNameTextBox" runat="server" MaxLength="100"></asp:TextBox>
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

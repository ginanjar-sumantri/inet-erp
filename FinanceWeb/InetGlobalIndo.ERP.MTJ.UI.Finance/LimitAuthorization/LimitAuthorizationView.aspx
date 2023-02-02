<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="LimitAuthorizationView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.LimitAuthorization.LimitAuthorizationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Role
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RoleTextBox" Width="200" ReadOnly="true" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Transaction Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TransTypeTextBox" Width="200" ReadOnly="true" BackColor="#CCCCCC"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Limit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LimitTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

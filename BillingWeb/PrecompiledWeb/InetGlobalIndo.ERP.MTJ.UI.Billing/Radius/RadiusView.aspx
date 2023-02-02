<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.Radius.RadiusView, App_Web_8fiy9zdq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="CancelButton" runat="server">
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
                                <asp:TextBox runat="server" ID="RadiusNameTextBox" BackColor="#CCCCCC" Width="250"></asp:TextBox>
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
                                <asp:TextBox ID="RadiusIPTextBox" runat="server" BackColor="#CCCCCC" MaxLength="50"></asp:TextBox>
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
                                <asp:TextBox ID="RadiusUserNameTextBox" runat="server" BackColor="#CCCCCC" MaxLength="100"></asp:TextBox>
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
                                <asp:TextBox ID="RadiusPwdTextBox" runat="server" BackColor="#CCCCCC" MaxLength="100"></asp:TextBox>
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
                                <asp:TextBox ID="RadiusDbNameTextBox" runat="server" BackColor="#CCCCCC" MaxLength="100"></asp:TextBox>
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
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False"
                                    OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.EmailNotificationSetup.EmailNotificationSetupView, App_Web_ogdejpxk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table border="0" cellpadding="3" cellspacing="0" width="0">
        <tr>
            <td class="title">
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
                <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="3" cellspacing="0" width="0">
                    <tr>
                        <td>
                            Type
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="TypeTextBox" runat="server" MaxLength="100" Width="500px" ReadOnly="true"
                                BackColor="#CCCCCC"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sub Type
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SubTypeTextBox" runat="server" MaxLength="100" Width="500px" ReadOnly="true"
                                BackColor="#CCCCCC"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email From
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="EmailFromTextBox" runat="server" MaxLength="200" Width="500" ReadOnly="true"
                                BackColor="#CCCCCC"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email To
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="EmailToTextBox" runat="server" MaxLength="200" Width="500" ReadOnly="true"
                                BackColor="#CCCCCC"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Subject
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SubjectTextBox" runat="server" Width="500px" MaxLength="500" ReadOnly="true"
                                BackColor="#CCCCCC"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Isi Email
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:Label runat="server" ID="BodyMessageLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="3" cellspacing="0" width="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

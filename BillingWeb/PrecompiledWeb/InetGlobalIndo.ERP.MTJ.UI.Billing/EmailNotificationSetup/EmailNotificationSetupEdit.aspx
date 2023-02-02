<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.EmailNotificationSetup.EmailNotificationSetupEdit, App_Web_ogdejpxk" validaterequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script type="text/javascript" src="../nicEdit.js"></script>

    <script type="text/javascript">
        bkLib.onDomLoaded(function() {
            new nicEditor().panelInstance('ctl00_DefaultBodyContentPlaceHolder_BodyMessageTextBox');
        });
    </script>

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
                            <asp:TextBox ID="TypeTextBox" runat="server" MaxLength="100" Width="500px" BackColor="#CCCCCC"></asp:TextBox>
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
                            <asp:TextBox ID="SubTypeTextBox" runat="server" MaxLength="100" Width="500px" BackColor="#CCCCCC"></asp:TextBox>
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
                            <asp:TextBox ID="EmailFromTextBox" runat="server" MaxLength="200" Width="500"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="EmailFromRegularExpressionValidator" runat="server"
                                ErrorMessage="Email tidak valid" Text="*" ControlToValidate="EmailFromTextBox"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
                            <asp:TextBox ID="EmailToTextBox" runat="server" MaxLength="200" Width="500"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="EmailToRegularExpressionValidator" runat="server"
                                ErrorMessage="Email tidak valid" Text="*" ControlToValidate="EmailToTextBox"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Subject Email
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="SubjectTextBox" runat="server" Width="500px" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SubjectRequiredFieldValidator" runat="server" ControlToValidate="SubjectTextBox"
                                Display="Dynamic" ErrorMessage="Subject harus diisi" Text="*"></asp:RequiredFieldValidator>
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
                            <asp:TextBox runat="server" ID="BodyMessageTextBox" Width="600" Height="100" TextMode="MultiLine"></asp:TextBox>
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
</asp:Content>

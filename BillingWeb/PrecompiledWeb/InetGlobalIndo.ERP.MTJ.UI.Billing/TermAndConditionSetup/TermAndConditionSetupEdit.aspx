<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.TermAndConditionSetup.TermAndConditionSetupEdit, App_Web_2zgdmafc" validaterequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script type="text/javascript" src="../nicEdit.js"></script>

    <script type="text/javascript">
        bkLib.onDomLoaded(function() {
            new nicEditor().panelInstance('ctl00_DefaultBodyContentPlaceHolder_BodyTextBox');
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
                        <td valign="top">
                            Text
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="BodyTextBox" Width="600" Height="100" TextMode="MultiLine"></asp:TextBox>
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

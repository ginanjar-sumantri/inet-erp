<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.DefaultAccount.DefaultAccountAdd, App_Web_b6fxii-d" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                            Setup Code
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="SetCodeTextBox" Width="70" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SetCodeRequiredFieldValidator" runat="server" ErrorMessage="Setup Code Must Be Filled"
                                Text="*" ControlToValidate="SetCodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Setup Name
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="SetDescriptionTextBox" Width="420" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="SetDescriptionRequiredFieldValidator" runat="server"
                                ErrorMessage="Setup Description Must Be Filled" Text="*" ControlToValidate="SetDescriptionTextBox"
                                Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td>
                            Account
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="AccountRBL" RepeatDirection="Vertical" RepeatColumns="2">
                            </asp:RadioButtonList>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" border="0" width="0">
                    <tr>
                        <td>
                            <asp:ImageButton runat="server" ID="SaveButton" Text="Save" OnClick="SaveButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="CancelButton" Text="Cancel" OnClick="CancelButton_Click"
                                CausesValidation="False" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="ResetButton" Text="Reset" OnClick="ResetButton_Click"
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

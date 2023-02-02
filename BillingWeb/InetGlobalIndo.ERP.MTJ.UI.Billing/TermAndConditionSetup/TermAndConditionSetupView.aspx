<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="TermAndConditionSetupView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.TermAndConditionSetup.TermAndConditionSetupView" %>

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
                        <td valign="top">
                            Text
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <asp:Label runat="server" ID="BodyLabel"></asp:Label>
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

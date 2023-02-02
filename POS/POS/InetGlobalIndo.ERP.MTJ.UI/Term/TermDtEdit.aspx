<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="TermDtEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Home.Term.TermDtEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
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
                                Period
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PeriodTextBox" Width="50" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type Range
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="TypeRangeDropDownList" runat="server">
                                    <asp:ListItem Text="Month" Value="month" />
                                    <asp:ListItem Text="Week" Value="week" />
                                    <asp:ListItem Text="Day" Value="day" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Range
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RangeTextBox" runat="server" Width="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Range Must Be Filled"
                                    Text="*" ControlToValidate="RangeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Percent Base
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PercentBaseTextBox" runat="server"></asp:TextBox>
                                &nbsp;%
                                <asp:RequiredFieldValidator ID="PercentBaseRequiredFieldValidator" runat="server"
                                    ErrorMessage="Percent Base Must Be Filled" Text="*" ControlToValidate="PercentBaseTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Percent PPn
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PercentPPnTextBox" runat="server"></asp:TextBox>
                                &nbsp;%
                                <asp:RequiredFieldValidator ID="PercentPPnRequiredFieldValidator" runat="server"
                                    ErrorMessage="Percent PPn Must Be Filled" Text="*" ControlToValidate="PercentPPnTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
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
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Home.Term.TermAdd, App_Web_rjxa5pz4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
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
                                Term Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TermCodeTextBox" Width="70" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TermCodeRequiredFieldValidator" runat="server" ErrorMessage="Term Code Must Be Filled"
                                    Text="*" ControlToValidate="TermCodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Term Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TermNameTextBox" Width="350" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TermNameRequiredFieldValidator" runat="server" ErrorMessage="Term Name Must Be Filled"
                                    Text="*" ControlToValidate="TermNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Periode
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PeriodTextBox" runat="server" Width="50" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PeriodRequiredFieldValidator" runat="server" ErrorMessage="Period Must Be Filled"
                                    Text="*" ControlToValidate="PeriodTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="RangeTextBox" runat="server" Width="50" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RangeRequiredFieldValidator" runat="server" ErrorMessage="Range Must Be Filled"
                                    Text="*" ControlToValidate="RangeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                &nbsp;X&nbsp;
                                <asp:DropDownList ID="TypeRangeDropDownList" runat="server">
                                    <asp:ListItem Text="Month" Value="month" />
                                    <asp:ListItem Text="Week" Value="week" />
                                    <asp:ListItem Text="Day" Value="day" />
                                </asp:DropDownList>
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
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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

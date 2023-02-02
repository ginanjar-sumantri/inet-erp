<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CurrencyAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Currency.CurrencyAdd" %>

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
                                Currency Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="50" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CurrCodeRequiredFieldValidator" runat="server" ErrorMessage="Currency Code Must Be Filled"
                                    Text="*" ControlToValidate="CurrCodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CurrNameTextBox" Width="210" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CurrNameRequiredFieldValidator" runat="server" ErrorMessage="Currency Name Must Be Filled"
                                    Text="*" ControlToValidate="CurrNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Decimal Place
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DecimalPlaceTextBox" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DecimalPlaceRequiredFieldValidator" runat="server"
                                    ErrorMessage="Decimal Place Must Be Filled" Text="*" ControlToValidate="DecimalPlaceTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                Decimal Place Report
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DecimalPlaceReportTextBox" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DecimalPlaceReportRequiredFieldValidator" runat="server"
                                    ErrorMessage="Decimal Place Report Must Be Filled" Text="*" ControlToValidate="DecimalPlaceReportTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Value Tolerance
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ValueToleranceTextBox" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ValueToleranceRequiredFieldValidator" runat="server" ErrorMessage="Value Tolerance Must Be Filled"
                                    Text="*" ControlToValidate="ValueToleranceTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Default Currency
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="CurrDefaultCheckBox" />
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
                                <asp:ImageButton runat="server" ID="SaveButton" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="CancelButton" OnClick="CancelButton_Click" CausesValidation="False" />
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="ResetButton" OnClick="ResetButton_Click" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="TaxTypeAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.TaxType.TaxTypeAdd" %>

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
                                Tax Type Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TaxTypeCodeTextBox" Width="50" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TaxTypeCodeRequiredFieldValidator" runat="server" ErrorMessage="Tax Type Code Must Be Filled"
                                    Text="*" ControlToValidate="TaxTypeCodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tax Type Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TaxTypeNameTextBox" Width="210" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TaxTypeNameRequiredFieldValidator" runat="server" ErrorMessage="Tax Type Name Must Be Filled"
                                    Text="*" ControlToValidate="TaxTypeNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Value
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ValueTextBox" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ValueRequiredFieldValidator" runat="server"
                                    ErrorMessage="Value Must Be Filled" Text="*" ControlToValidate="ValueTextBox"
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
                                Account
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="AccountDropDownList" runat ="server" ></asp:DropDownList>
                                <%--<asp:TextBox runat="server" ID="ValueToleranceTextBox" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ValueToleranceRequiredFieldValidator" runat="server" ErrorMessage="Value Tolerance Must Be Filled"
                                    Text="*" ControlToValidate="ValueToleranceTextBox" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="fgActiveCheckBox" />
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

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SalesConfirmationDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.SalesConfirmation.SalesConfirmationDetailAdd" %>

<%@ Register Src="../ProductPickerIgnoreStock.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
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
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <uc1:ProductPicker ID="ProductPicker1" runat="server" />
                                <asp:HiddenField ID="tempProductCode" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Product Specification
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductSpecTextBox" runat="server" Width="300" Height="80" MaxLength="170"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                                </asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="CurrDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CurrDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator runat="server" ID="CustomValidator2" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="CurrDropDownList" Text="*" ErrorMessage="Currency Must Be Choosed"></asp:CustomValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountTextBox" runat="server" MaxLength="23" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AmountRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Amount Must Be Filled" ControlToValidate="AmountTextBox"></asp:RequiredFieldValidator>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

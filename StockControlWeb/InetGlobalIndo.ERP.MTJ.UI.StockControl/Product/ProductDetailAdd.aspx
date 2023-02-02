<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Product.ProductDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Unit Convert
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="UnitConvertDropDownList" AutoPostBack="true"
                                    OnSelectedIndexChanged="UnitConvertDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Unit Convert Must Be Filled"
                                    Text="*" ControlToValidate="UnitConvertDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RateTextBox" MaxLength="18" Width="150" AutoPostBack="true"
                                    OnTextChanged="RateTextBox_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RateRequiredFieldValidator" runat="server" ErrorMessage="Rate Must Be Filled"
                                    Text="*" ControlToValidate="RateTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Unit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="UnitTextBox" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UnitRequiredFieldValidator" runat="server" ErrorMessage="Unit Must Be Filled"
                                    Text="*" ControlToValidate="UnitTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                <%--  <asp:DropDownList runat="server" ID="UnitDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="UnitCustomValidator" runat="server" ErrorMessage="Unit Must Be Filled"
                                    Text="*" ControlToValidate="UnitDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="UnitLabel" runat="server"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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

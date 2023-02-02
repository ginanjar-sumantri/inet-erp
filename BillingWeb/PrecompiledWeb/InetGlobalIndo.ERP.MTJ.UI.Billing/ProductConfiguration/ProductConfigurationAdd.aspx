<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.ProductConfiguration.ProductConfigurationAdd, App_Web_x_njlvbj" %>

<%@ Register Src="../ProductPicker.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ProductDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="ProductCustomValidator" runat="server" ErrorMessage="Product Must Be Choosed"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ProductDropDownList"></asp:CustomValidator>--%>
                                <uc1:ProductPicker ID="ProductPicker1" runat="server" />
                                <asp:HiddenField ID="tempProductCode" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Is Postpone
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="IsPostponeCheckbox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Is Allow Change Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="ChangePriceCheckBox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Monthly Recuring Charge
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="MRCCheckBox" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contract Month
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ContractMonthTextBox" runat="server" Width="100"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="ContrctMonthRequiredFieldValidator" Text="*" runat="server" ControlToValidate="ContractMonthTextBox"
                                    ErrorMessage="Contract Month Must Be Filled"></asp:RequiredFieldValidator>
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

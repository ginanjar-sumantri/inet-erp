<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CompanyUserAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.Company.CompanyUserAdd" %>

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
                                User Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="UserDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UserDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="User Name Must Be Filled"
                                    Text="*" ControlToValidate="UserDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Database
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:CheckBoxList ID="DatabaseCheckBoxList" runat="server">
                                </asp:CheckBoxList>
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

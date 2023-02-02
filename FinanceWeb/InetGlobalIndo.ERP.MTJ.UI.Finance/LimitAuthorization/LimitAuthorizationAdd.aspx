<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="LimitAuthorizationAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.LimitAuthorization.LimitAuthorizationAdd" %>

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
                                Role
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="RoleDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Role Must Be Choosed"
                                    ClientValidationFunction="DropDownValidation" Text="*" ControlToValidate="RoleDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Transaction Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="TransTypeDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Transaction Type Must Be Choosed" ControlToValidate="TransTypeDropDownList"
                                    Text="*"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Limit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LimitTextBox" runat="server"></asp:TextBox>
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

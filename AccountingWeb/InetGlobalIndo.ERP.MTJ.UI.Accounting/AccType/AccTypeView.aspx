<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="AccTypeView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.AccType.AccTypeView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Account Type Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CodeTextBox" Width="50" MaxLength="1" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CodeRequiredFieldValidator" runat="server" ErrorMessage="Account Type Code Must Be Filled"
                                    Text="*" ControlToValidate="CodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Account Type Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NameTextBox" Width="280" MaxLength="40" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Account Type Name Must Be Filled"
                                    Text="*" ControlToValidate="NameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="AccTypeRBL" runat="server" RepeatColumns="2" Width="100px"
                                    Enabled="false">
                                    <asp:ListItem Value="BS" Text="BS"></asp:ListItem>
                                    <asp:ListItem Value="PL" Text="PL"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Side
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="FgSideDropDownList" runat="server" Enabled="false">
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
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                <%--</td>
                            <td>--%>
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click"
                                    CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

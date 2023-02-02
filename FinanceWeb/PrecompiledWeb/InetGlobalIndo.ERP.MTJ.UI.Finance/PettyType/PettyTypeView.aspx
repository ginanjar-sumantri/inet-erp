<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PettyType.PettyTypeView, App_Web_aob-oe2b" %>

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
                    Petty Type Code
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CodeTextBox" Width="70" MaxLength="10" ReadOnly="true"
                        BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Petty Type Name
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="NameTextBox" Width="150" MaxLength="50" ReadOnly="true"
                        BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Account
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="AccountCodeTextBox" runat="server" Width="80" MaxLength="12" ReadOnly="true"
                        BackColor="#CCCCCC"></asp:TextBox>
                    <%--<asp:DropDownList runat="server" ID="PettyDDL" Enabled="false">
                </asp:DropDownList>--%>
                    <asp:TextBox ID="AccountNameCodeTextBox" runat="server" Width="300" MaxLength="50"
                        ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

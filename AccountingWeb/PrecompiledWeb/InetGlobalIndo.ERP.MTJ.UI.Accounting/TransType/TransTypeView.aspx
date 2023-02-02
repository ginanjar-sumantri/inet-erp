<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.TransType.TransTypeView, App_Web_3qatxh-h" %>

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
                                Transaction Type Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CodeTextBox" Width="50" MaxLength="3" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CodeRequiredFieldValidator" runat="server" ErrorMessage="Account Group Code Must Be Filled"
                                    Text="*" ControlToValidate="CodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Transaction Type Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NameTextBox" Width="210" MaxLength="30" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Account Group Name Must Be Filled"
                                    Text="*" ControlToValidate="NameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Using Account
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="ActiveCheckBox" runat="server" Enabled="false" />
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
                            &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

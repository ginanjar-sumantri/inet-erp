<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.TaxType.TaxTypeView, App_Web_sumr5hw1" %>

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
                                Tax Type Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TaxTypeCodeTextBox" Width="50" MaxLength="5" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="TaxTypeNameTextBox" Width="210" MaxLength="30" ReadOnly ="true" BackColor ="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="ValueTextBox" Width="150" AutoPostBack ="true" BackColor ="#CCCCCC" ></asp:TextBox>
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
                                <asp:TextBox ID="AccountTaxBox" runat ="server" Width ="250px" ReadOnly ="true" BackColor ="#CCCCCC" ></asp:TextBox>
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
                                <asp:Label ID="FgActiveLabel" runat ="server" ></asp:Label>
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
                                <asp:ImageButton runat="server" ID="EditButton" OnClick="EditButton_Click" />
                                <%--</td>
                                <td>--%>
                                &nbsp;<asp:ImageButton runat="server" ID="CancelButton" OnClick="CancelButton_Click"
                                    CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

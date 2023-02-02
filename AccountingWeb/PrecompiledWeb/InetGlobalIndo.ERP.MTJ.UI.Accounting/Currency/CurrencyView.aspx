<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Currency.CurrencyView, App_Web_09amfek9" %>

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
                                Currency Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="50" MaxLength="5" ReadOnly="True"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CurrNameTextBox" Width="210" MaxLength="30" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Decimal Place
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DecimalPlaceTextBox" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="DecimalPlaceReportTextBox" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Value Tolerance
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ValueToleranceTextBox" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Default Currency
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="CurrDefaultCheckBox" Enabled="False" />
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

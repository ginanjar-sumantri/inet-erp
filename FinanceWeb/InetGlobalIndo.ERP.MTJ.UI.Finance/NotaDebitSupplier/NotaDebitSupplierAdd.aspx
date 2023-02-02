<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="NotaDebitSupplierAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.NotaDebitSupplier.NotaDebitSupplierAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Trans Date
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                    <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                        value="..." />--%>
                        <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    Supplier
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="SuppDropDownList" AutoPostBack="True" OnSelectedIndexChanged="SuppDropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="SuppCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                        ErrorMessage="Supplier Must Be Filled" Text="*" ControlToValidate="SuppDropDownList">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Attn
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="AttnTextBox" runat="server" Width="150" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Term
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="TermDropDownList" runat="server">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="TermCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                        ErrorMessage="Term Must Be Filled" Text="*" ControlToValidate="TermDropDownList">
                    </asp:CustomValidator>
                </td>
            </tr>
            <asp:ScriptManager ID="scriptMgr" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <tr>
                        <td>
                            Currency / Rate
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="CurrCodeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CurrCodeDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CurrCodeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrCodeDropDownList">
                            </asp:CustomValidator>
                            <asp:TextBox runat="server" ID="CurrRateTextBox" Width="100">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="CurrRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                                Text="*" ControlToValidate="CurrRateTextBox" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            Amount
                        </td>
                        <td>
                            <br />
                            :
                        </td>
                        <td>
                            <table width="0" cellpadding="3" cellspacing="1">
                                <tr class="bgcolor_gray">
                                    <td style="width: 105px" class="tahoma_11_white" align="center">
                                        <b>Currency</b>
                                    </td>
                                    <td style="width: 105px" class="tahoma_11_white" align="center">
                                        <b>Total Forex</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#cccccc"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </ContentTemplate>
            </asp:UpdatePanel>
            <tr valign="top">
                <td>
                    Remark
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                    <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                    characters left
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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

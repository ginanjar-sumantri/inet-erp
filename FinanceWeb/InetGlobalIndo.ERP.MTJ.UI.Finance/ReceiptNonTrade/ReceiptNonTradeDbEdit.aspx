<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ReceiptNonTradeDbEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptNonTrade.ReceiptNonTradeDbEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

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
                                Receipt Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ReceiptTypeDropDownList" AutoPostBack="True"
                                    OnSelectedIndexChanged="ReceiptTypeDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PaymentTypeCustomValidator" runat="server" ErrorMessage="Receipt Type Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ReceiptTypeDropDownList"></asp:CustomValidator>
                                <asp:HiddenField runat="server" ID="FgGiroHiddenField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Document No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DocumentNoTextBox" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DocumentNoRequiredFieldValidator" runat="server"
                                    ErrorMessage="Document No. Must Be Filled" Text="*" ControlToValidate="DocumentNoTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Nominal
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NominalTextBox" Width="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NominalRequiredFieldValidator" runat="server" ErrorMessage="Nominal Must Be Filled"
                                    Text="*" ControlToValidate="NominalTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Giro
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="BankGiroDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="BankGiroCustomValidator" runat="server" Text="*" ErrorMessage="Bank Giro Must Be Choosed"
                                    ClientValidationFunction="DropDownValidation" ControlToValidate="BankGiroDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Due Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input id="headline_date_start" runat="server" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>    
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Expense
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BankExpenseTextBox" Width="100"></asp:TextBox>
                            </td>
                        </tr>
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

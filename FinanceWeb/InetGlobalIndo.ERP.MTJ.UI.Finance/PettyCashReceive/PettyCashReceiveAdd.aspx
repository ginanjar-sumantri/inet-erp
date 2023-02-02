<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PettyCashReceiveAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCashReceive.PettyCashReceiveAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
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
                    <asp:Label runat="server" ID="WarningLabel"></asp:Label>
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
                    <%--<input id="headline_date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                        value="..." />--%>
                    <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
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
                    <asp:DropDownList ID="TypeDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TypeDDL_SelectedIndexChanged">
                        <asp:ListItem Text="[Choose One]" Value="null"></asp:ListItem>
                        <asp:ListItem Text="Petty" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Payment" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ErrorMessage="Type Must Be Filled"
                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="TypeDDL"></asp:CustomValidator>
                </td>
            </tr>
            <tr runat="server" id="petty_tr">
                <td>
                    Petty
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="PettyDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PettyDDL_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="PettyDDLCustomValidator" runat="server" ErrorMessage="Petty Must Be Filled"
                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="PettyDDL"></asp:CustomValidator>
                </td>
            </tr>
            <tr runat="server" id="payment_tr">
                <td>
                    Payment
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="PaymentDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PaymentDropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="PaymentDropDownListCustomValidator" runat="server" ErrorMessage="Payment Must Be Filled"
                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="PaymentDropDownList"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Currency / Rate
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CurrTextBox" BackColor="#cccccc" Width="50"></asp:TextBox>
                    <asp:TextBox runat="server" ID="RateTextbox" MaxLength="20"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                </td>
            </tr>
            <tr>
                <td>
                    Pay To
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="PayToText" MaxLength="50"></asp:TextBox>
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

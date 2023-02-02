<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceipt.GiroReceiptView, App_Web_m6v-5kps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" language="javascript">
        function AmountForex_OnBlur (_prmAmountForex)
        {
             _prmAmountForex.value = FormatCurrency(_prmAmountForex.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="BackButton">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="7">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Giro Receipt</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Giro No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="GiroNoTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Receipt No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="ReceiptNoTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Receipt Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ReceiptDateTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Due Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="DueDateTextBox" runat="server" Width="100" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Receipt Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="ReceiptCodeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                    <asp:TextBox ID="ReceiptNameTextBox" runat="server" Width="300" BackColor="#CCCCCC"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Bank Giro
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="BankGiroTextBox" BackColor="#CCCCCC" ReadOnly="true" Width="300"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Currency / Rate
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="CurrCodeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                        Width="100"></asp:TextBox>
                                    <asp:TextBox ID="RateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Amount
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="AmountTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    Remark
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="RemarkTextBox" Width="300" MaxLength="500" Height="80" TextMode="MultiLine"
                                        runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                    <asp:HiddenField ID="StatusHiddenField" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Action
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:DropDownList ID="ActionDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ActionDropDownList_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" OnClick="BackButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Panel ID="SetorPanel" runat="server" DefaultButton="SaveSetorButton">
                        <fieldset>
                            <legend>Setor</legend>
                            <table width="100%">
                                <tr>
                                    <td width="150">
                                        Date
                                    </td>
                                    <td width="10px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SetorDateTextBox" runat="server" BackColor="#cccccc" Width="100"></asp:TextBox>&nbsp;
                                        <%--<input id="Button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_SetorDateTextBox,'yyyy-mm-dd',this)"
                                            value="..." />--%>
                                        <asp:Literal ID="SetorDateLiteral" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bank Receipt
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SetorBankReceiptDropDownList" runat="server">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="SetorBankReceiptDropDownList" Text="*" ErrorMessage="Bank Receipt Must Be Filled"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bank Setor
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="BankSetorDropDownList" runat="server">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="BankSetorDropDownList" Text="*" ErrorMessage="Bank Setor Must Be Filled"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bank Charge (IDR)
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SetorBankChangeTextBox" runat="server">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="SetorBankChangeTextBox"
                                            ErrorMessage="Bank Change Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SaveSetorButton" runat="server" OnClick="SaveSetorButton_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="CancelSetorButton" runat="server" CausesValidation="False" OnClick="CancelSetorButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Panel ID="DrawnPanel" runat="server" DefaultButton="SaveDrawnButton">
                        <fieldset>
                            <legend>Drawn</legend>
                            <table width="100%">
                                <tr>
                                    <td width="150">
                                        Date
                                    </td>
                                    <td width="10px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DrawnDateTextBox" runat="server" BackColor="#cccccc" Width="100"></asp:TextBox>&nbsp;
                                        <%--<input id="Button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DrawnDateTextBox,'yyyy-mm-dd',this)"
                                            value="..." />--%>
                                        <asp:Literal ID="DrawnDateLiteral" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bank Receipt
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DrawnBankReceiptDropDownList" runat="server">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="DrawnBankReceiptDropDownList" Text="*" ErrorMessage="Bank Receipt Must Be Filled"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bank Charge (IDR)
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DrawnBankChangeTextBox" runat="server">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DrawnBankChangeTextBox"
                                            ErrorMessage="Bank Change Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Remark
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DrawnRemarkTextBox" Width="300" TextMode="MultiLine" Height="80"
                                            runat="server"></asp:TextBox>
                                        <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                        characters left
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SaveDrawnButton" runat="server" OnClick="SaveDrawnButton_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="CancelDrawnButton" runat="server" CausesValidation="False" OnClick="CancelDrawnButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Panel ID="CancelPanel" runat="server" DefaultButton="SaveCancelButton">
                        <fieldset>
                            <legend>Cancel</legend>
                            <table width="100%">
                                <tr>
                                    <td width="150">
                                        Date
                                    </td>
                                    <td width="10px">
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CancelDateTextBox" runat="server" BackColor="#cccccc" Width="100"></asp:TextBox>&nbsp;
                                        <%--<input id="Button3" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_CancelDateTextBox,'yyyy-mm-dd',this)"
                                            value="..." />--%>
                                        <asp:Literal ID="CancelDateLiteral" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Reason
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ReasonTextBox" runat="server" TextMode="MultiLine" Width="300" Height="80">
                                        </asp:TextBox>
                                        <asp:TextBox ID="CounterTextBox2" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                        characters left
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ReasonTextBox"
                                            ErrorMessage="Reason Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SaveCancelButton" runat="server" OnClick="SaveCancelButton_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="CancelCancelButton" runat="server" CausesValidation="False"
                                                        OnClick="CancelCancelButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Panel ID="UnpostPanel" runat="server" DefaultButton="SaveUnpostButton">
                        <fieldset>
                            <legend>Unposting</legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="SaveUnpostButton" runat="server" OnClick="SaveUnpostButton_Click" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="CancelUnpostButton" runat="server" CausesValidation="False"
                                                        OnClick="CancelUnpostButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

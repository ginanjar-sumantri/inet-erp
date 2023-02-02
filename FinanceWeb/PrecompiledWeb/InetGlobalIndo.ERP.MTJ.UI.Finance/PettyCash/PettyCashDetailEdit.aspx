<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCash.PettyCashDetailEdit, App_Web_rhllso80" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script type="text/javascript" language="javascript">
        function AmountForex_OnBlur (_prmAmountForex)
        {
             _prmAmountForex.value = FormatCurrency(_prmAmountForex.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
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
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <tr>
                        <td>
                            Account
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="AccountTextBox" Width="80" MaxLength="12" AutoPostBack="True"
                                OnTextChanged="AccountTextBox_TextChanged"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="AccountDropDownList" AutoPostBack="True" OnSelectedIndexChanged="AccountDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="AccountCustomValidator" runat="server" ErrorMessage="Account Must Be Filled"
                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="AccountDropDownList"></asp:CustomValidator>
                            <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sub Ledger
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="SubLedgerDropDownList">
                            </asp:DropDownList>
                            <asp:HiddenField runat="server" ID="FgSubledHiddenField" />
                        </td>
                    </tr>
                </ContentTemplate>
            </asp:UpdatePanel>
            <tr>
                <td>
                    Amount
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="AmountTextbox" MaxLength="20"></asp:TextBox>
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
            <%--<tr>
            <td>
                Currency / Rate</td>
            <td>
                :
            </td>
            <td>
                <asp:TextBox runat="server" ID="CurrTextBox" ReadOnly="true" BackColor="#cccccc" ></asp:TextBox>
                <asp:TextBox runat="server" ID="RateTextbox" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
       --%>
            <tr>
                <td colspan="3">
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

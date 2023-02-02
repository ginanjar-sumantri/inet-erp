<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DPCustomerListEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerList.DPCustomerListEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <%--<link href="../calendar/calendar-blue2.css" rel="stylesheet" type="text/css" media="all"
        title="win2k-cold-1" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" src="../calendar/lang/calendar-en.js"></script>

    <script type="text/javascript" src="../calendar/calendar-setup.js"></script>--%>
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Calculate(_prmPPNPercent, _prmAmountBase, _prmPPNForex, _prmTotalForex, _prmDecimalPlace) {
            var _tempPPNPercent = parseFloat(GetCurrency2(_prmPPNPercent.value, _prmDecimalPlace.value));
            if (isNaN(_tempPPNPercent) == true) {
                _tempPPNPercent = 0;
            }

            var _tempAmountBase = parseFloat(GetCurrency2(_prmAmountBase.value, _prmDecimalPlace.value));
            if (isNaN(_tempAmountBase) == true) {
                _tempAmountBase = 0;
            }

            if (_tempPPNPercent > 0) {
                _prmPPNPercent.value = FormatCurrency2(_tempPPNPercent, _prmDecimalPlace.value);
            }
            else {
                _prmPPNPercent.value = FormatCurrency2(0, _prmDecimalPlace.value);
            }

            _prmAmountBase.value = FormatCurrency2(_tempAmountBase, _prmDecimalPlace.value);

            if (_tempAmountBase > 0) {
                var _ppnForex = _tempAmountBase * _tempPPNPercent / 100;
                _prmPPNForex.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);

                var _totalForex = _tempAmountBase + GetCurrency2(_ppnForex, _prmDecimalPlace.value);
                _prmTotalForex.value = FormatCurrency2(_totalForex, _prmDecimalPlace.value);
            }
            else {
                _prmPPNForex.value = FormatCurrency2(0, _prmDecimalPlace.value);

                _prmTotalForex.value = FormatCurrency2(0, _prmDecimalPlace.value);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel DefaultButton="SaveButton" ID="Panel1" runat="server">
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
                    Trans No.
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="DPCustListCodeTextBox" MaxLength="20" BackColor="#CCCCCC"
                        Width="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    File No.
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="FileNmbrTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                        Width="150">
                    </asp:TextBox>
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
                    <asp:TextBox runat="server" ID="DPCustListDateTextBox" Width="100" MaxLength="30"
                        BackColor="#CCCCCC"></asp:TextBox>
                    <%--<input id="Button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DPCustListDateTextBox,'yyyy-mm-dd',this)"
                        value="..." />--%>
                        <asp:Literal ID="DPCustListDateLiteral" runat="server"></asp:Literal> 
                </td>
            </tr>
            <%--  <tr>
                <td>
                    Status
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:Label runat="server" ID="StatusLabel"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td>
                    Customer
                </td>
                <td>
                    :
                </td>
                <td>
                    <%--<asp:DropDownList runat="server" ID="CustCodeDropDownList" AutoPostBack="true" OnSelectedIndexChanged="CustCodeDropDownList_SelectedIndexChanged">
                </asp:DropDownList>--%>
                    <asp:TextBox runat="server" ID="CustTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="AttnTextBox" MaxLength="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    SO No
                </td>
                <td>
                    :
                </td>
                <td>
                    <%--<asp:DropDownList runat="server" ID="SONoDropDownList">
                </asp:DropDownList>--%><asp:TextBox runat="server" ID="SONoTextBox" MaxLength="20"
                    BackColor="#CCCCCC"></asp:TextBox>
                </td>
            </tr>
            <asp:ScriptManager ID="scriptMgr" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <tr>
                        <td>
                            Currency
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="CurrCodeDropDownList" AutoPostBack="true" OnSelectedIndexChanged="CurrCodeDropDownList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CurrCodeCustomValidator" runat="server" ErrorMessage="Currency Must be filled"
                                Text="*" ControlToValidate="CurrCodeDropDownList" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Amount
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="1">
                                <tr style="background-color: Gray">
                                    <td align="center" class="tahoma_11_white">
                                        <b>Base Forex</b>
                                    </td>
                                    <td align="center" class="tahoma_11_white">
                                        <b>PPN %</b>
                                    </td>
                                    <td align="center" class="tahoma_11_white">
                                        <b>PPN Forex</b>
                                    </td>
                                    <td align="center" class="tahoma_11_white">
                                        <b>Total Forex</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="BaseForexTextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="BaseForexRequiredFieldValidator" runat="server" ControlToValidate="BaseForexTextBox"
                                            ErrorMessage="Base Forex Must be filled" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="PPNTextBox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PPNRequiredFieldValidator" runat="server" ControlToValidate="PPNTextBox"
                                            ErrorMessage="PPN Must be filled" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="PPNForexTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="TotalForexTextBox" BackColor="#CCCCCC"></asp:TextBox>
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

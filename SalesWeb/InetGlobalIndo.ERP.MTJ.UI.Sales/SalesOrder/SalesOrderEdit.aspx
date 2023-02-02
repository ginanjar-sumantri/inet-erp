<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SalesOrderEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder.SalesOrderEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
    
    function Calculate(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace)
    {
        var _tempDPPercentTextBox = parseFloat(GetCurrency2((_prmDPPercentTextBox.value == "") ? 0 : _prmDPPercentTextBox.value, _prmDecimalPlace.value));
        var _tempDPForexTextBox = parseFloat(GetCurrency2((_prmDPForexTextBox.value == "") ? 0 : _prmDPForexTextBox.value, _prmDecimalPlace.value));
        var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? 0 : _prmBaseForexTextBox.value, _prmDecimalPlace.value));
        var _tempDiscForexTextBox = parseFloat(GetCurrency2((_prmDiscForexTextBox.value == "") ? 0 : _prmDiscForexTextBox.value, _prmDecimalPlace.value));
        var _tempPPNPercentTextBox = parseFloat(GetCurrency2((_prmPPNPercentTextBox.value == "") ? 0 : _prmPPNPercentTextBox.value, _prmDecimalPlace.value));
        var _tempPPNForexTextBox = parseFloat(GetCurrency2((_prmPPNForexTextBox.value == "") ? 0 : _prmPPNForexTextBox.value, _prmDecimalPlace.value));
        var _tempTotalForexTextBox = parseFloat(GetCurrency2((_prmTotalForexTextBox.value == "") ? 0 : _prmTotalForexTextBox.value, _prmDecimalPlace.value));
        
        _prmBaseForexTextBox.value = FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);
        
        _prmDPPercentTextBox.value = _tempDPPercentTextBox;
        var _dpForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempDPPercentTextBox / 100;
        _prmDPForexTextBox.value = FormatCurrency2(_dpForex, _prmDecimalPlace.value);
        
        _prmPPNPercentTextBox.value = _tempPPNPercentTextBox;
        var _ppnForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempPPNPercentTextBox / 100;
        _prmPPNForexTextBox.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);
        
        var _totalForex = _tempBaseForexTextBox - _tempDiscForexTextBox + _ppnForex;
        _prmTotalForexTextBox.value = FormatCurrency2(_totalForex, _prmDecimalPlace.value);
    }
    
    function CalculateDisc(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace)
    {
        var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? 0 : _prmBaseForexTextBox.value, _prmDecimalPlace.value));
        var _tempDiscTextBox = parseFloat(GetCurrency2((_prmDiscTextBox.value == "") ? 0 : _prmDiscTextBox.value, _prmDecimalPlace.value));
        
        _prmBaseForexTextBox.value = FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);
        
        _prmDiscTextBox.value = _tempDiscTextBox;
        var _discForex = _tempBaseForexTextBox * _tempDiscTextBox / 100;
        _prmDiscForexTextBox.value = FormatCurrency2(_discForex, _prmDecimalPlace.value);
        
        Calculate(_prmDPPercentTextBox,_prmDPForexTextBox,_prmBaseForexTextBox,_prmDiscForexTextBox,_prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace);
    }
    
    function CalculateDiscForex(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace)
    {
        var _tempBaseForexTextBox = parseFloat(GetCurrency2(((_prmBaseForexTextBox.value == "") ? 0 : _prmBaseForexTextBox.value), _prmDecimalPlace.value));
        var _tempDiscTextBox = parseFloat(GetCurrency2(((_prmDiscTextBox.value == "") ? 0 : _prmDiscTextBox.value), _prmDecimalPlace.value));
        var _tempDiscForexTextBox = parseFloat(GetCurrency2(((_prmDiscForexTextBox.value == "") ? 0 : _prmDiscForexTextBox.value), _prmDecimalPlace.value));
        
        _prmDiscForexTextBox.value = FormatCurrency2(_tempDiscForexTextBox, _prmDecimalPlace.value);
        if (_tempBaseForexTextBox > 0)
        {
            _prmDiscTextBox.value = FormatCurrency2((_tempDiscForexTextBox / _tempBaseForexTextBox * 100), _prmDecimalPlace.value);
        }
        else
        {
            _prmDiscTextBox.value = "0";
        }
        Calculate(_prmDPPercentTextBox,_prmDPForexTextBox,_prmBaseForexTextBox,_prmDiscForexTextBox,_prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox,_prmDecimalPlace);
    }
    </script>

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
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td width="310px">
                                <asp:TextBox runat="server" ID="TransNoTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="160"></asp:TextBox>
                                <%--<asp:DropDownList ID="RevisiDropDownList" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="RevisiDropDownList_SelectedIndexChanged">
                </asp:DropDownList>--%>
                                <asp:TextBox runat="server" ID="RevisiTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="50"></asp:TextBox>
                                <asp:HiddenField ID="CheckHidden" runat="server" />
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                File No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FileNmbrTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="160"></asp:TextBox>
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
                                <%--<input type="button" id="date_start" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)" />--%>
                                 <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                            </td>
                            <td>
                                Customer PO No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustPONoTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="CustDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CustDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Customer Must Be Filled" Text="*" ControlToValidate="CustDropDownList">
                                </asp:CustomValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                                Customer PO Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CustPODateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" value="..." id="cust_date" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_CustPODateTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="CalendarScriptLiteral" runat="server"></asp:Literal>
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
                                <asp:TextBox ID="AttnTextBox" runat="server" Width="150"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Delivery To
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="DeliveryToDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="DeliveryCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Delivery To Must Be Filled" Text="*" ControlToValidate="DeliveryToDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bill To
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="BillToDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="BillToCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Bill To Must Be Filled" Text="*" ControlToValidate="BillToDropDownList">
                                </asp:CustomValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                                Delivery Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DeliveryDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" id="delivery_date" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DeliveryDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                    <asp:Literal ID="DeliveryDateLiteral" runat="server"></asp:Literal>
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
                            <td>
                            </td>
                            <td>
                                Due Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DueDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" value="..." id="due_date" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DueDateTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="DueDateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <asp:ScriptManager ID="scriptMgr" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr valign="top">
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
                                        <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ForexRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                                            Text="*" ControlToValidate="ForexRateTextBox" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                    <td rowspan="4">
                                        Remark
                                    </td>
                                    <td rowspan="4">
                                        :
                                    </td>
                                    <td rowspan="4">
                                        <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                        <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                        characters left
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr class="bgcolor_gray" style="height: 8px">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>DP %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>DP Forex</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        DP
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DPPercentTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DPForexTextBox" runat="server" Width="100" BackColor="#cccccc"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="5">
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr class="bgcolor_gray" style="height: 8px">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Currency</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Base Forex</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Discount</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Discount Forex</b>
                                                </td>
                                            </tr>
                                        </table>
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
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="BaseForexTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DiscTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DiscForexTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="5">
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr class="bgcolor_gray" style="height: 8px">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPN %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPN Forex</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Total Forex</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PPN
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="5">
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" BackColor="#cccccc"></asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100" BackColor="#cccccc"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                            <td>
                                <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

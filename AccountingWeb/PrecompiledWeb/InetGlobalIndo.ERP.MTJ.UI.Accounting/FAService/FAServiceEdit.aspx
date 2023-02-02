<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FAService.FAServiceEdit, App_Web_5v1xy6hu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Calculate(_prmPPNPercent,_prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmPPNForex, _prmTotalForex, _prmDecimalPlace)
        {
            var _tempPPNPercent = parseFloat(GetCurrency2(_prmPPNPercent.value, _prmDecimalPlace.value));
            if(isNaN(_tempPPNPercent) == true)
            {
                _tempPPNPercent = 0;
            }
            
            var _tempAmountBase = parseFloat(GetCurrency2(_prmAmountBase.value, _prmDecimalPlace.value));
            if(isNaN(_tempAmountBase) == true)
            {
                _tempAmountBase = 0;
            }
          
            if(_tempPPNPercent > 0)
            {
                _prmPPNPercent.value = FormatCurrency2(_tempPPNPercent, _prmDecimalPlace.value);
                
                _prmPPNNo.readOnly = false;
                _prmPPNNo.style.background = '#FFFFFF';
                
                _prmPPNDate.readOnly = true;
                _prmCal.style.visibility = "visible";
            }
            else
            {
                _prmPPNPercent.value =  FormatCurrency2(0, _prmDecimalPlace.value);
                
                _prmPPNNo.value = "";            
                _prmPPNNo.readOnly = true;
                _prmPPNNo.style.background = '#CCCCCC';
                
                _prmPPNDate.value = "";
                _prmPPNDate.readOnly = true;
                _prmCal.style.visibility = "hidden";
            }
                
            if(_tempAmountBase > 0)
            {
                var _ppnForex = (_tempAmountBase)  * _tempPPNPercent / 100;
                _prmPPNForex.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);
                
                var _totalForex = _tempAmountBase + _ppnForex;
                _prmTotalForex.value = FormatCurrency2(_totalForex, _prmDecimalPlace.value);
            }
            else
            {
                _prmPPNForex.value = FormatCurrency2(0, _prmDecimalPlace.value);
                
                _prmTotalForex.value = FormatCurrency2(0, _prmDecimalPlace.value);
            }
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
                            <td>
                                <asp:TextBox ID="TransNoTextBox" Width="150" runat="server" BackColor="#CCCCCC" ReadOnly="true">
                                </asp:TextBox>
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
                                <asp:TextBox ID="FileNmbrTextBox" Width="150" runat="server" BackColor="#CCCCCC"
                                    ReadOnly="true">
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
                                <asp:TextBox ID="TransDateTextBox" Width="100" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_TransDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                    <asp:Literal ID="TransDateLiteral" runat="server"></asp:Literal>
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
                                <asp:DropDownList ID="SupplierDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SupplierDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="SupplierDDLCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Supplier Must Be Filled" Text="*" ControlToValidate="SupplierDropDownList">
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
                                <asp:TextBox ID="AttnTextBox" runat="server" Width="280" MaxLength="40">
                                </asp:TextBox>
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
                                        <asp:DropDownList ID="CurrDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CurrDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrDropDownList">
                                        </asp:CustomValidator>
                                        <asp:TextBox ID="ForexRateTextBox" Width="150" runat="server" BackColor="#CCCCCC">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ForexRateRequiredFieldValidator" runat="server" ErrorMessage="Forex Rate Must Be Filled"
                                            Text="*" ControlToValidate="ForexRateTextBox" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
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
                                <tr>
                                    <td>
                                        Due Date
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DueDateTextBox" Width="100" runat="server" BackColor="#CCCCCC">
                                        </asp:TextBox>&nbsp;
                                        <%--<input id="DueDateButton" runat="server" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DueDateTextBox,'yyyy-mm-dd',this)"
                                            value="..." />--%>
                                            <asp:Literal ID="DueDateLiteral" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Fixed Asset
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="FixedAssetDropDownList" runat="server">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="FixedAssetCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="Fixed Asset Must Be Filled" Text="*" ControlToValidate="FixedAssetDropDownList">
                                        </asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Start Period
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="PeriodDropDownList" runat="server">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="PeriodCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="Period Must Be Filled" Text="*" ControlToValidate="PeriodDropDownList">
                                        </asp:CustomValidator>
                                        <asp:DropDownList ID="YearDropDownList" runat="server">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="YearCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="Year Must Be Filled" Text="*" ControlToValidate="YearDropDownList">
                                        </asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Add Life Time
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="AddLifeTimeTextBox" Width="50" runat="server">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="AddLifeTimeRequiredFieldValidator" runat="server"
                                            ErrorMessage="Add Life Time Must Be Filled" Text="*" ControlToValidate="AddLifeTimeTextBox"
                                            Display="Dynamic">
                                        </asp:RequiredFieldValidator>
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
                                        <table width="0">
                                            <tr class="bgcolor_gray" height="20">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPN %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>No.</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Date</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Rate</b>
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
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ErrorMessage="PPN % Must Be Filled"
                                                        Text="*" ControlToValidate="PPNPercentTextBox" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNNoTextBox" runat="server" Width="100" BackColor="#cccccc">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNDateTextBox" runat="server" Width="70" BackColor="#CCCCCC">
                                                    </asp:TextBox>&nbsp;
                                                    <%--<input runat="server" id="ppn_date_start" type="button" style="visibility: hidden;
                                                        width: 20px;" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_PPNDateTextBox,'yyyy-mm-dd',this)"
                                                        value="..." />--%>
                                                        <asp:Literal ID="PPNDateLiteral" runat="server"></asp:Literal>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNRateTextBox" runat="server" Width="100" BackColor="#cccccc">
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
                                    <td>
                                        <table>
                                            <tr class="bgcolor_gray" height="20">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Currency</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Base Forex</b>
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
                                        Amount
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="AmountBaseTextBox" runat="server" Width="100" BackColor="#CCCCCC">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" BackColor="#CCCCCC">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100px" BackColor="#CCCCCC">
                                                    </asp:TextBox>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" TextMode="MultiLine" Height="80" Width="300">
                                </asp:TextBox>
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
                                <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="False" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" OnClick="ResetButton_Click" CausesValidation="False" />
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

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FATenancyEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FATenancy.FATenancyEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Calculate(_prmPPNPercent,_prmPPNNo, _prmPPNDate, _prmCal, _prmAmountBase, _prmDiscForex, _prmPPNForex, _prmTotalForex, _prmDecimalPlace)
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
            
            var _tempDiscForex = parseFloat(GetCurrency2(_prmDiscForex.value, _prmDecimalPlace.value));
            if(isNaN(_tempDiscForex) == true)
            {
                _tempDiscForex = 0;
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
            
            _prmDiscForex.value = FormatCurrency2(_tempDiscForex, _prmDecimalPlace.value);
                
            if(_tempAmountBase > 0)
            {
                var _ppnForex = (_tempAmountBase - _tempDiscForex)  * _tempPPNPercent / 100;
                _prmPPNForex.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);
                
                var _totalForex = _tempAmountBase + _ppnForex - _tempDiscForex;
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
                    <asp:Label runat="server" ID="WarningLabel"></asp:Label>
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
                                <asp:TextBox runat="server" ID="TransNumberTextBox" Width="150" MaxLength="20" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="FileNmbrTextBox" Width="150" MaxLength="20" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="CustCodeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CustCodeDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustRequiredFieldValidator" runat="server" ErrorMessage="Customer harus diisi"
                                    Text="*" ControlToValidate="CustCodeDropDownList"></asp:CustomValidator>
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
                                <asp:TextBox runat="server" ID="AttnTextBox" Width="280" MaxLength="40"></asp:TextBox>
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
                                        <table cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="CurrencyDropDownList" MaxLength="100" AutoPostBack="True"
                                                        OnSelectedIndexChanged="CurrencyDropDownList_SelectedIndexChanged" Enabled="False">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CurrencyRequiredFieldValidator" runat="server" ErrorMessage="Currency harus diisi"
                                                        Text="*" ControlToValidate="CurrencyDropDownList"></asp:CustomValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="CurrencyRateTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                    <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
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
                                        <asp:DropDownList runat="server" ID="TermDropDownList">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="TermRequiredFieldValidator" runat="server" ErrorMessage="Term harus diisi"
                                            Text="*" ControlToValidate="TermDropDownList"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <table cellpadding="3" cellspacing="1">
                                            <tr class="bgcolor_gray">
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
                                                    <b>PPN %</b>
                                                </td>
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
                                                    <b>PPN No.</b>
                                                </td>
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
                                                    <b>Date</b>
                                                </td>
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
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
                                        <table cellpadding="3" cellspacing="1">
                                            <tr>
                                                <td align="center" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="PPNTextBox" Width="100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="PPNRequiredFieldValidator" runat="server" ErrorMessage="PPN % Must Be Filled"
                                                        Text="*" ControlToValidate="PPNTextBox" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td align="center" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="PPNNoTextBox" BackColor="#CCCCCC" Width="100"></asp:TextBox>
                                                </td>
                                                <td align="center" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="PPNDateTextBox" BackColor="#CCCCCC" Width="70"></asp:TextBox>
                                                    <%--<input id="ppn_date_start" runat="server" type="button" style="visibility: hidden;
                                                        width: 20px;" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_PPNDateTextBox,'yyyy-mm-dd',this)"
                                                        value="..." />--%>
                                                        <asp:Literal ID="PPNDateLiteral" runat="server"></asp:Literal>
                                                </td>
                                                <td align="center" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="PPNRateTextBox" BackColor="#CCCCCC" Width="100"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <table cellpadding="3" cellspacing="1">
                                            <tr class="bgcolor_gray">
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
                                                    <b>Currency</b>
                                                </td>
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
                                                    <b>Base Forex</b>
                                                </td>
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
                                                    <b>Discount Forex</b>
                                                </td>
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
                                                    <b>PPN Forex</b>
                                                </td>
                                                <td class="tahoma_11_white" align="center" style="width: 110px">
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
                                        <table cellpadding="3" cellspacing="1">
                                            <tr>
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="CurrencyForexTextBox" Width="100px" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="BaseForexTextBox" Width="100px" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="DiscountTextBox" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="DiscountRequiredFieldValidator" runat="server" ErrorMessage="Discount Must Be Filled"
                                                        Text="*" ControlToValidate="DiscountTextBox" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="PPNForexTextBox" Width="100px" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                                <td align="center" class="tahoma_11_white" style="width: 110px">
                                                    <asp:TextBox runat="server" ID="TotalForexTextBox" Width="100px" BackColor="#CCCCCC"></asp:TextBox>
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

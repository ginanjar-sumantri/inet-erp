<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Retail.RetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
    
    function Calculate(_prmBaseForexTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace, _prmAddFee)
    {
        var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? 0 : _prmBaseForexTextBox.value, _prmDecimalPlace.value));
        var _tempDiscForexTextBox = parseFloat(GetCurrency2((_prmDiscForexTextBox.value == "") ? 0 : _prmDiscForexTextBox.value, _prmDecimalPlace.value));
        var _tempPPNPercentTextBox = parseFloat(GetCurrency2((_prmPPNPercentTextBox.value == "") ? 0 : _prmPPNPercentTextBox.value, _prmDecimalPlace.value));
        var _tempPPNForexTextBox = parseFloat(GetCurrency2((_prmPPNForexTextBox.value == "") ? 0 : _prmPPNForexTextBox.value, _prmDecimalPlace.value));
        var _tempAddFeeTextBox = parseFloat(GetCurrency2((_prmAddFee.value == "") ? 0 : _prmAddFee.value, _prmDecimalPlace.value));
        var _tempTotalForexTextBox = parseFloat(GetCurrency2((_prmTotalForexTextBox.value == "") ? 0 : _prmTotalForexTextBox.value, _prmDecimalPlace.value));
        
        _prmBaseForexTextBox.value = FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);
        _prmAddFee.value = FormatCurrency2(_tempAddFeeTextBox, _prmDecimalPlace.value);
        _prmPPNPercentTextBox.value = _tempPPNPercentTextBox;
        
        var _ppnForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempPPNPercentTextBox / 100;
        _prmPPNForexTextBox.value = FormatCurrency2(_ppnForex, _prmDecimalPlace.value);
        
        var _totalForex = _tempBaseForexTextBox - _tempDiscForexTextBox + _ppnForex + _tempAddFeeTextBox;
        _prmTotalForexTextBox.value = FormatCurrency2(_totalForex, _prmDecimalPlace.value);
    }
    
    function CalculateDisc( _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace, _prmAddFee)
    {
        var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? 0 : _prmBaseForexTextBox.value, _prmDecimalPlace.value));
        var _tempDiscTextBox = parseFloat(GetCurrency2((_prmDiscTextBox.value == "") ? 0 : _prmDiscTextBox.value, _prmDecimalPlace.value));
        
        _prmBaseForexTextBox.value = FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);
        
        _prmDiscTextBox.value = _tempDiscTextBox;
        var _discForex = _tempBaseForexTextBox * _tempDiscTextBox / 100;
        _prmDiscForexTextBox.value = FormatCurrency2(_discForex, _prmDecimalPlace.value);
        
        Calculate(prmBaseForexTextBox,_prmDiscForexTextBox,_prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace, _prmAddFee);
    }
    
    function CalculateDiscForex(_prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace, _prmAddFee)
    {
        var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? 0 : _prmBaseForexTextBox.value, _prmDecimalPlace.value));
        var _tempDiscTextBox = parseFloat(GetCurrency2((_prmDiscTextBox.value == "") ? 0 : _prmDiscTextBox.value, _prmDecimalPlace.value));
        var _tempDiscForexTextBox = parseFloat(GetCurrency2((_prmDiscForexTextBox.value == "") ? 0 : _prmDiscForexTextBox.value, _prmDecimalPlace.value));
        
        _prmBaseForexTextBox.value = FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);
        _prmDiscForexTextBox.value = FormatCurrency2(_tempDiscForexTextBox, _prmDecimalPlace.value);
//        if (_tempBaseForexTextBox > 0)
//        {
//            _prmDiscTextBox.value = FormatCurrency2((_tempDiscForexTextBox / _tempBaseForexTextBox * 100), _prmDecimalPlace.value);
//        }
//        else
//        {
            _prmDiscTextBox.value = "0";
//        }
        
        Calculate(_prmBaseForexTextBox,_prmDiscForexTextBox,_prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox,_prmDecimalPlace, _prmAddFee);
    }
    
    function a(_prmDDL, _prmNoTextBox, _prmNameTextBox)
    {
        if (_prmDDL.value == "Cash")
        {
            document.getElementById("NoTD").style.visibility = "hidden";
            document.getElementById("NameTD").style.visibility = "hidden";
            _prmNoTextBox.style.visibility = "hidden";
            _prmNameTextBox.style.visibility = "hidden";
        }
        else 
        {
            document.getElementById("NoTD").style.visibility = "visible";
            document.getElementById("NameTD").style.visibility = "visible";
            _prmNoTextBox.style.visibility = "visible";
            _prmNameTextBox.style.visibility = "visible";
            _prmNoTextBox.value = "";
            _prmNameTextBox.value = "";
        }
    }
    </script>

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
                            <td width="10px">
                            </td>
                            <td width="90px">
                                Payment Type
                            </td>
                            <td width="5px">
                                :
                            </td>
                            <td width="500px">
                                <asp:DropDownList ID="PayTypeDropDownList" runat="server">
                                    <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                    <asp:ListItem Text="Debit" Value="Debit"></asp:ListItem>
                                    <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustNameTextBox" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CustNameRequiredFieldValidator" runat="server" ErrorMessage="Customer Name Must Be Filled"
                                    Text="*" ControlToValidate="CustNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td width="10px">
                            </td>
                            <td colspan="3" id="NoTD">
                                <table cellpadding="0" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td width="100px">
                                            Card No.
                                        </td>
                                        <td width="10px">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="CardNoTextBox" Width="300px" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr valign="middle">
                            <td>
                                Sales Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="EmpNumbDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator" runat="server" ErrorMessage="Sales Name Must Be Chosed"
                                    Text="*" ControlToValidate="EmpNumbDropDownList" ClientValidationFunction="DropDownValidation"
                                    Display="Dynamic"></asp:CustomValidator>
                            </td>
                            <td width="10px">
                            </td>
                            <td colspan="3" id="NameTD">
                                <table cellpadding="0" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td width="100px">
                                            Card Name
                                        </td>
                                        <td width="10px">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="CardNameTextBox" Width="300px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
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
                                    <td colspan="4">
                                        <asp:DropDownList runat="server" ID="CurrCodeDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CurrCodeDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrCodeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrCodeDropDownList">
                                        </asp:CustomValidator>
                                        <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="CurrRateRequiredFieldValidator" runat="server" ErrorMessage="Currency Rate Must Be Filled"
                                            Text="*" ControlToValidate="ForexRateTextBox" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="4">
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr class="bgcolor_gray" style="height: 8px">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Currency</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Base Forex</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Discount %</b>
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
                                    <td colspan="4">
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
                                    <td colspan="4">
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr class="bgcolor_gray" style="height: 8px">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPN %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPN Forex</b>
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
                                    <td colspan="4">
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" BackColor="#cccccc"></asp:TextBox>
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
                                    <td colspan="4">
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr class="bgcolor_gray" style="height: 8px">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>Additional Fee</b>
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
                                        Additional Fee
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="4">
                                        <table cellpadding="0" cellspacing="1" width="0" border="0">
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="AddFeeTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
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
                        <tr valign="top">
                            <td>
                                Description
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="4">
                                <asp:TextBox ID="RemarkTextBox" runat="server" Height="80" TextMode="MultiLine" Width="300"></asp:TextBox>
                                <br />
                                <asp:TextBox ID="CounterTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="50"></asp:TextBox>
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

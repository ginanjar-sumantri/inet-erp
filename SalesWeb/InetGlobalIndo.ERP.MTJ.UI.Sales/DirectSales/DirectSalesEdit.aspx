<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DirectSalesEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales.DirectSalesEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function numericInput(x) {
            if (isNaN(x.value)) 
                x.value = "0";
        }
    </script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
    <%--<script language="javascript" type="text/javascript">

        function Calculate(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
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

        function CalculateDisc(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
            var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? 0 : _prmBaseForexTextBox.value, _prmDecimalPlace.value));
            var _tempDiscTextBox = parseFloat(GetCurrency2((_prmDiscTextBox.value == "") ? 0 : _prmDiscTextBox.value, _prmDecimalPlace.value));

            _prmBaseForexTextBox.value = FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);

            _prmDiscTextBox.value = _tempDiscTextBox;
            var _discForex = _tempBaseForexTextBox * _tempDiscTextBox / 100;
            _prmDiscForexTextBox.value = FormatCurrency2(_discForex, _prmDecimalPlace.value);

            Calculate(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace);
        }

        function CalculateDiscForex(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
            var _tempBaseForexTextBox = parseFloat(GetCurrency2(((_prmBaseForexTextBox.value == "") ? 0 : _prmBaseForexTextBox.value), _prmDecimalPlace.value));
            var _tempDiscTextBox = parseFloat(GetCurrency2(((_prmDiscTextBox.value == "") ? 0 : _prmDiscTextBox.value), _prmDecimalPlace.value));
            var _tempDiscForexTextBox = parseFloat(GetCurrency2(((_prmDiscForexTextBox.value == "") ? 0 : _prmDiscForexTextBox.value), _prmDecimalPlace.value));

            _prmDiscForexTextBox.value = FormatCurrency2(_tempDiscForexTextBox, _prmDecimalPlace.value);
            //        if (_tempBaseForexTextBox > 0)
            //        {
            //            _prmDiscTextBox.value = FormatCurrency2((_tempDiscForexTextBox / _tempBaseForexTextBox * 100), _prmDecimalPlace.value);
            //        }
            //        else
            //        {
            _prmDiscTextBox.value = "0";
            //        }
            Calculate(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace);
        }
    </script>--%>
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
                            <td colspan="3">
                                <asp:Label runat="server" ID="Label" CssClass="warning"></asp:Label>
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
                                <asp:TextBox runat="server" ID="TransNoTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="160"></asp:TextBox>
                                <%--<asp:DropDownList ID="RevisiDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RevisiDropDownList_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                <asp:HiddenField ID="StatusHiddenField" runat="server" />
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
                                <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
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
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                            </td>
                            <td>
                                Payment Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="PayTypeDropDownList" runat="server">
                                </asp:DropDownList>
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
                                <asp:TextBox runat="server" ID="CustTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="300px"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Base Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BaseForexTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                <asp:HiddenField ID="BaseForexHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CurrCodeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Disc Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DiscPercentTextBox" runat="server" Width="50"></asp:TextBox>
                                %
                                <asp:TextBox ID="DiscAmountTextBox" runat="server" Width="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Forex Rate   
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ForexRateTextBox" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                PPN Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="50"></asp:TextBox>
                                %
                                <asp:TextBox runat="server" ID="PPNAmountTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td rowspan="4">
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="250" MaxLength="500" Height="60"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Stamp Fee
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="StampFeeTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td colspan="4">
                                &nbsp;
                            </td>
                            <td>
                                Other Fee
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="OtherFeeTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                            <td>
                                Total Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalAmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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

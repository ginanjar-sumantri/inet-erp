<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PurchaseOrderEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder.PurchaseOrderEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>

    <script language="javascript" type="text/javascript">

        function CalculateDPPercent(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPHPercentTextBox, _prmPPHForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
            var _tempDPPercentTextBox = parseFloat(GetCurrency2((_prmDPPercentTextBox.value == "") ? "0" : _prmDPPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDPPercentTextBox) == true) {
                _tempDPPercentTextBox = 0;
            }
            var _tempDPForexTextBox = parseFloat(GetCurrency2((_prmDPForexTextBox.value == "") ? "0" : _prmDPForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDPForexTextBox) == true) {
                _tempDPForexTextBox = 0;
            }
            var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? "0" : _prmBaseForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempBaseForexTextBox) == true) {
                _tempBaseForexTextBox = 0;
            }
            var _tempDiscTextBox = parseFloat(GetCurrency2((_prmDiscTextBox.value == "") ? "0" : _prmDiscTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscTextBox) == true) {
                _tempDiscTextBox = 0;
            }
            var _tempDiscForexTextBox = parseFloat(GetCurrency2((_prmDiscForexTextBox.value == "") ? "0" : _prmDiscForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscForexTextBox) == true) {
                _tempDiscForexTextBox = 0;
            }
            var _tempPPHPercentTextBox = parseFloat(GetCurrency2((_prmPPHPercentTextBox.value == "") ? "0" : _prmPPHPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPHPercentTextBox) == true) {
                _tempPPHPercentTextBox = 0;
            }
            var _tempPPHForexTextBox = parseFloat(GetCurrency2((_prmPPHForexTextBox.value == "") ? "0" : _prmPPHForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPHForexTextBox) == true) {
                _tempPPHForexTextBox = 0;
            }
            var _tempPPNPercentTextBox = parseFloat(GetCurrency2((_prmPPNPercentTextBox.value == "") ? "0" : _prmPPNPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPNPercentTextBox) == true) {
                _tempPPNPercentTextBox = 0;
            }
            var _tempPPNForexTextBox = parseFloat(GetCurrency2((_prmPPNForexTextBox.value == "") ? "0" : _prmPPNForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPNForexTextBox) == true) {
                _tempPPNForexTextBox = 0;
            }
            var _tempTotalForexTextBox = parseFloat(GetCurrency2((_prmTotalForexTextBox.value == "") ? "0" : _prmTotalForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempTotalForexTextBox) == true) {
                _tempTotalForexTextBox = 0;
            }

            //        _prmDPPercentTextBox.value = (_tempDPTextBox == 0) ? "0" : FormatCurrency(_tempDPTextBox);
            //        
            //        var _dpForex = (_tempBaseForex-_tempDiscForexTextBox) * _tempDPTextBox.value / 100 ;
            //        _prmDPForexTextBox.value = (_dpForex == 0) ? "0" : FormatCurrency(_dpForex);
            //

            _prmBaseForexTextBox.value = (_tempBaseForexTextBox.value == 0) ? "0" : FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);

            _prmDPPercentTextBox.value = (_tempDPPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDPPercentTextBox, _prmDecimalPlace.value);
            var _dpForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempDPPercentTextBox / 100;
            _prmDPForexTextBox.value = (_dpForex == 0) ? "0" : FormatCurrency2(_dpForex, _prmDecimalPlace.value);

            _prmPPHPercentTextBox.value = (_tempPPHPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempPPHPercentTextBox, _prmDecimalPlace.value);
            var _pphForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempPPHPercentTextBox / 100;
            _prmPPHForexTextBox.value = (_pphForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_pphForex, _prmDecimalPlace.value);

            _prmPPNPercentTextBox.value = (_tempPPNPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempPPNPercentTextBox, _prmDecimalPlace.value);
            var _ppnForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempPPNPercentTextBox / 100;
            _prmPPNForexTextBox.value = (_ppnForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_ppnForex, _prmDecimalPlace.value);

            var _totalForex = _tempBaseForexTextBox - _tempDiscForexTextBox + _pphForex + _ppnForex -_dpForex;
            _prmTotalForexTextBox.value = (_totalForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_totalForex, _prmDecimalPlace.value);
        }

        function CalculateDPForex(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPHPercentTextBox, _prmPPHForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
            var _tempDPPercentTextBox = parseFloat(GetCurrency2((_prmDPPercentTextBox.value == "") ? "0" : _prmDPPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDPPercentTextBox) == true) {
                _tempDPPercentTextBox = 0;
            }
            var _tempDPForexTextBox = parseFloat(GetCurrency2((_prmDPForexTextBox.value == "") ? "0" : _prmDPForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDPForexTextBox) == true) {
                _tempDPForexTextBox = 0;
            }
            var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? "0" : _prmBaseForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempBaseForexTextBox) == true) {
                _tempBaseForexTextBox = 0;
            }
            var _tempDiscTextBox = parseFloat(GetCurrency2((_prmDiscTextBox.value == "") ? "0" : _prmDiscTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscTextBox) == true) {
                _tempDiscTextBox = 0;
            }
            var _tempDiscForexTextBox = parseFloat(GetCurrency2((_prmDiscForexTextBox.value == "") ? "0" : _prmDiscForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscForexTextBox) == true) {
                _tempDiscForexTextBox = 0;
            }
            var _tempPPHPercentTextBox = parseFloat(GetCurrency2((_prmPPHPercentTextBox.value == "") ? "0" : _prmPPHPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPHPercentTextBox) == true) {
                _tempPPHPercentTextBox = 0;
            }
            var _tempPPHForexTextBox = parseFloat(GetCurrency2((_prmPPHForexTextBox.value == "") ? "0" : _prmPPHForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPHForexTextBox) == true) {
                _tempPPHForexTextBox = 0;
            }
            var _tempPPNPercentTextBox = parseFloat(GetCurrency2((_prmPPNPercentTextBox.value == "") ? "0" : _prmPPNPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPNPercentTextBox) == true) {
                _tempPPNPercentTextBox = 0;
            }
            var _tempPPNForexTextBox = parseFloat(GetCurrency2((_prmPPNForexTextBox.value == "") ? "0" : _prmPPNForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPNForexTextBox) == true) {
                _tempPPNForexTextBox = 0;
            }
            var _tempTotalForexTextBox = parseFloat(GetCurrency2((_prmTotalForexTextBox.value == "") ? "0" : _prmTotalForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempTotalForexTextBox) == true) {
                _tempTotalForexTextBox = 0;
            }

            _prmDPForexTextBox.value = (_tempDPForexTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDPForexTextBox, _prmDecimalPlace.value);

            var _dpPercent = _tempDPForexTextBox / (_tempBaseForexTextBox - _tempDiscForexTextBox) * 100;
            if ((_tempBaseForexTextBox - _tempDiscForexTextBox) != 0) {
                _prmDPPercentTextBox.value = (_dpPercent == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_dpPercent, _prmDecimalPlace.value);
            }
            else {
                _prmDPPercentTextBox.value = FormatCurrency2(0, _prmDecimalPlace.value);
            }

            _prmBaseForexTextBox.value = (_tempBaseForexTextBox.value == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);

            _prmDPPercentTextBox.value = (_tempDPPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDPPercentTextBox, _prmDecimalPlace.value);
            var _dpForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempDPPercentTextBox / 100;
            _prmDPForexTextBox.value = (_dpForex == 0) ? "0" : FormatCurrency2(_dpForex, _prmDecimalPlace.value);

            _prmPPHPercentTextBox.value = (_tempPPHPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempPPHPercentTextBox, _prmDecimalPlace.value);
            var _pphForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempPPHPercentTextBox / 100;
            _prmPPHForexTextBox.value = (_pphForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_pphForex, _prmDecimalPlace.value);

            _prmPPNPercentTextBox.value = (_tempPPNPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempPPNPercentTextBox, _prmDecimalPlace.value);
            var _ppnForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempPPNPercentTextBox / 100;
            _prmPPNForexTextBox.value = (_ppnForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_ppnForex, _prmDecimalPlace.value);

            var _totalForex = _tempBaseForexTextBox - _tempDiscForexTextBox + _pphForex + _ppnForex -_dpForex;
            _prmTotalForexTextBox.value = (_totalForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_totalForex, _prmDecimalPlace.value);
        }

        function CalculateDiscountPercent(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPHPercentTextBox, _prmPPHForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
            var _tempDiscTextBox = parseFloat(GetCurrency2((_prmDiscTextBox.value == "") ? "0" : _prmDiscTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscTextBox) == true) {
                _tempDiscTextBox = 0;
            }
            var _tempDPTextBox = parseFloat(GetCurrency2((_prmDPPercentTextBox.value == "") ? "0" : _prmDPPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDPTextBox) == true) {
                _tempDPTextBox = 0;
            }
            var _tempBaseForex = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? "0" : _prmBaseForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempBaseForex) == true) {
                _tempBaseForex = 0;
            }
            var _tempDiscForexTextBox = parseFloat(GetCurrency2((_prmDiscForexTextBox.value == "") ? "0" : _prmDiscForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscForexTextBox) == true) {
                _tempDiscForexTextBox = 0;
            }

            _prmDiscTextBox.value = (_tempDiscTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDiscTextBox, _prmDecimalPlace.value);

            var _discountForex = _tempBaseForex * _prmDiscTextBox.value / 100;
            _prmDiscForexTextBox.value = (_discountForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_discountForex, _prmDecimalPlace.value);

            _prmDPPercentTextBox.value = (_tempDPTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDPTextBox, _prmDecimalPlace.value);

            var _dpForex = (_tempBaseForex - _discountForex) * _tempDPTextBox.value / 100;
            _prmDPForexTextBox.value = (_dpForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_dpForex, _prmDecimalPlace.value);

            Calculate(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPHPercentTextBox, _prmPPHForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace);
        }


        function CalculateDiscountForex(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPHPercentTextBox, _prmPPHForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
            var _tempBaseForex = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? "0" : _prmBaseForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempBaseForex) == true) {
                _tempBaseForex = 0;
            }
            var _tempDiscForexTextBox = parseFloat(GetCurrency2((_prmDiscForexTextBox.value == "") ? "0" : _prmDiscForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscForexTextBox) == true) {
                _tempDiscForexTextBox = 0;
            }
            var _tempDPForexTextBox = parseFloat(GetCurrency2((_prmDPForexTextBox.value == "") ? "0" : _prmDPForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDPForexTextBox) == true) {
                _tempDPForexTextBox = 0;
            }

            _prmDiscForexTextBox.value = (_tempDiscForexTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDiscForexTextBox, _prmDecimalPlace.value);


            _prmDiscTextBox.value = "0";


            _prmDPForexTextBox.value = (_tempDPForexTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDPForexTextBox, _prmDecimalPlace.value);

            //        var _dpPercent = _tempDPForexTextBox / (_tempBaseForex-_tempDiscForexTextBox) * 100;
            //        if((_tempBaseForex-_tempDiscForexTextBox) != 0)
            //        {
            //            _prmDPPercentTextBox.value = (_dpPercent == 0) ? "0" : FormatCurrency(_dpPercent);
            //        }
            //        else
            //        {
            //            _prmDPPercentTextBox.value = FormatCurrency(0);
            //        }

            Calculate(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPHPercentTextBox, _prmPPHForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace);
        }


        function Calculate(_prmDPPercentTextBox, _prmDPForexTextBox, _prmBaseForexTextBox, _prmDiscTextBox, _prmDiscForexTextBox, _prmPPHPercentTextBox, _prmPPHForexTextBox, _prmPPNPercentTextBox, _prmPPNForexTextBox, _prmTotalForexTextBox, _prmDecimalPlace) {
            var _tempDPPercentTextBox = parseFloat(GetCurrency2((_prmDPPercentTextBox.value == "") ? "0" : _prmDPPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDPPercentTextBox) == true) {
                _tempDPPercentTextBox = 0;
            }
            var _tempDPForexTextBox = parseFloat(GetCurrency2((_prmDPForexTextBox.value == "") ? "0" : _prmDPForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDPForexTextBox) == true) {
                _tempDPForexTextBox = 0;
            }
            var _tempBaseForexTextBox = parseFloat(GetCurrency2((_prmBaseForexTextBox.value == "") ? "0" : _prmBaseForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempBaseForexTextBox) == true) {
                _tempBaseForexTextBox = 0;
            }
            var _tempDiscTextBox = parseFloat(GetCurrency2((_prmDiscTextBox.value == "") ? "0" : _prmDiscTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscTextBox) == true) {
                _tempDiscTextBox = 0;
            }
            var _tempDiscForexTextBox = parseFloat(GetCurrency2((_prmDiscForexTextBox.value == "") ? "0" : _prmDiscForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempDiscForexTextBox) == true) {
                _tempDiscForexTextBox = 0;
            }
            var _tempPPHPercentTextBox = parseFloat(GetCurrency2((_prmPPHPercentTextBox.value == "") ? "0" : _prmPPHPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPHPercentTextBox) == true) {
                _tempPPHPercentTextBox = 0;
            }
            var _tempPPHForexTextBox = parseFloat(GetCurrency2((_prmPPHForexTextBox.value == "") ? "0" : _prmPPHForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPHForexTextBox) == true) {
                _tempPPHForexTextBox = 0;
            }
            var _tempPPNPercentTextBox = parseFloat(GetCurrency2((_prmPPNPercentTextBox.value == "") ? "0" : _prmPPNPercentTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPNPercentTextBox) == true) {
                _tempPPNPercentTextBox = 0;
            }
            var _tempPPNForexTextBox = parseFloat(GetCurrency2((_prmPPNForexTextBox.value == "") ? "0" : _prmPPNForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempPPNForexTextBox) == true) {
                _tempPPNForexTextBox = 0;
            }
            var _tempTotalForexTextBox = parseFloat(GetCurrency2((_prmTotalForexTextBox.value == "") ? "0" : _prmTotalForexTextBox.value), _prmDecimalPlace.value);
            if (isNaN(_tempTotalForexTextBox) == true) {
                _tempTotalForexTextBox = 0;
            }

            _prmDPPercentTextBox.value = (_tempDPPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempDPPercentTextBox, _prmDecimalPlace.value);
            var _dpForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempDPPercentTextBox / 100;
            _prmDPForexTextBox.value = (_dpForex == 0) ? "0" : FormatCurrency2(_dpForex, _prmDecimalPlace.value);

            _prmBaseForexTextBox.value = (_tempBaseForexTextBox.value == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempBaseForexTextBox, _prmDecimalPlace.value);

            _prmPPHPercentTextBox.value = (_tempPPHPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempPPHPercentTextBox, _prmDecimalPlace.value);
            var _pphForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempPPHPercentTextBox / 100;
            _prmPPHForexTextBox.value = (_pphForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_pphForex, _prmDecimalPlace.value);

            _prmPPNPercentTextBox.value = (_tempPPNPercentTextBox == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_tempPPNPercentTextBox, _prmDecimalPlace.value);
            var _ppnForex = (_tempBaseForexTextBox - _tempDiscForexTextBox) * _tempPPNPercentTextBox / 100;
            _prmPPNForexTextBox.value = (_ppnForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_ppnForex, _prmDecimalPlace.value);

            var _totalForex = _tempBaseForexTextBox - _tempDiscForexTextBox + _pphForex + _ppnForex -_dpForex;
            _prmTotalForexTextBox.value = (_totalForex == 0) ? FormatCurrency2(0, _prmDecimalPlace.value) : FormatCurrency2(_totalForex, _prmDecimalPlace.value);
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
                                <asp:TextBox runat="server" ID="TransNoTextBox" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<asp:TextBox runat="server" ID="RevisiLabel" Width="50px" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>--%>
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
                                <asp:TextBox runat="server" ID="FileNmbrTextBox" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                Supplier Reference
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SupplierPONoTextBox" runat="server" MaxLength="30" Width="150"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Shipment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ShipmentDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="ShipmentCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Shipment Must Be Filled" Text="*" ControlToValidate="ShipmentDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Supplier
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <%--<asp:DropDownList runat="server" ID="SupplierDropDownList" AutoPostBack="True" OnSelectedIndexChanged="SupplierDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="SUpplierCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Supplier Must Be Filled" Text="*" ControlToValidate="SupplierDropDownList">
                                </asp:CustomValidator>--%>
                                <asp:TextBox ID="SupplierTextBox" runat="server" AutoPostBack="true" OnTextChanged="SupplierTextBox_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnSearchSupplier" runat="server" Text="..." /><br />
                                &nbsp;
                                <asp:Label ID="SupplierLabel" runat="server"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Supplier Must Be Filled"
                                    Text="*" ControlToValidate="SupplierTextBox" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                                Shipment Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ShipmentNameTextBox" runat="server" MaxLength="60" Width="200"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="AttnTextBox" Width="150">
                                </asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Shipping Curr / Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ShippingCurrCodeDropDownList" AutoPostBack="True"
                                    OnSelectedIndexChanged="ShippingCurrCodeDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="ShippingCurrRateTextBox" Width="100"></asp:TextBox>
                                <asp:HiddenField ID="DecimalPlaceHiddenField2" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Delivery Site
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="DeliveryDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="DeliveryCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="DeliveryDropDownList" ErrorMessage="Delivery Must Be Filled"
                                    Text="*">
                                </asp:CustomValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                                Shipping Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ShippingForexTextBox" runat="server" Width="100" OnTextChanged="ShippingForexTextBox_TextChanged"
                                    AutoPostBack="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Delivery Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DeliveryDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" id="Img1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DeliveryDateTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="DeliveryDateLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                            </td>
                            <td>
                                Subject
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="SubjectTextBox" Width="400" MaxLength="500"></asp:TextBox>
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
                                    <td>
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
                                    <td>
                                    </td>
                                    <td valign="top" rowspan="3">
                                        Remark
                                    </td>
                                    <td valign="top" rowspan="3">
                                        :
                                    </td>
                                    <td valign="top" rowspan="3">
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
                                    <td colspan="5">
                                        <table width="0">
                                            <tr class="bgcolor_gray" height="20">
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
                                    <td colspan="5">
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DPPercentTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="DPForexTextBox" runat="server" Width="100"></asp:TextBox>
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
                                        <table>
                                            <tr class="bgcolor_gray" height="20">
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
                                    <td colspan="5">
                                        <table>
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
                                                    <asp:TextBox ID="DiscForexTextBox" runat="server" Width="100"></asp:TextBox>
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
                                        <table width="0">
                                            <tr class="bgcolor_gray" height="20">
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPh %</b>
                                                </td>
                                                <td style="width: 110px" class="tahoma_11_white" align="center">
                                                    <b>PPh Forex</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PPh
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td colspan="5">
                                        <table>
                                            <tr>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPHPercentTextBox" runat="server" Width="100">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="width: 110px" align="center">
                                                    <asp:TextBox ID="PPHForexTextBox" runat="server" Width="100" BackColor="#cccccc"></asp:TextBox>
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
                                        <table width="0">
                                            <tr class="bgcolor_gray" height="20">
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
                                        <table>
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

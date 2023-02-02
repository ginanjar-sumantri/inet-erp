<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckStatus.aspx.cs" Inherits="InetGlobalIndo.ERP.MTJ.UI.POSInterface.General.CheckStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POS Check Status</title>

    <script src="../CSS/orange/jquery-1.5.2.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../CSS/orange/scrollsync.js"></script>

    <script type="text/javascript" src="../CSS/orange/dragscrollable.js"></script>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/orange/style2.css" media="all" rel="Stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

    <asp:Literal ID="JScriptLiteral" runat="server"></asp:Literal>

    <script type="text/javascript" language="javascript">
        var dateObject = new Date();
        var Hari = new Array("SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY");
        var Bulan = new Array("JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER");
        var hour = dateObject.getHours();
        var minute = dateObject.getMinutes();
        var second = dateObject.getSeconds();
        if (hour < 10) {
            hour = "0" + hour;
        }
        if (minute < 10) {
            minute = "0" + minute;
        }
        if (second < 10) {
            second = "0" + second;
        }
        $('.container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('.container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        function updateJam() {
            var dateObject = new Date();
            var Hari = new Array("SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY");
            var Bulan = new Array("JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER");
            var hour = dateObject.getHours();
            var minute = dateObject.getMinutes();
            var second = dateObject.getSeconds();
            if (hour < 10) {
                hour = "0" + hour;
            }
            if (minute < 10) {
                minute = "0" + minute;
            }
            if (second < 10) {
                second = "0" + second;
            }
            $('.container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('.container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);
    </script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#dialogHold").draggable();
            setTimeout(function() {
                $("#dialogHold").fadeOut("fast")
            }, 1000);
        });
    </script>

    <script language="javascript" type="text/javascript">
        function editQty(_nourut, _code, _nourutHidden, _qtyTextBox, _barcodeTextBox) {
            nourutHidden = document.getElementById(_nourutHidden);
            qtyTextBox = document.getElementById(_qtyTextBox);
            barcodeTextBox = document.getElementById(_barcodeTextBox);

            nourutHidden.value = _nourut;

            barcodeTextBox.value = _code;
            barcodeTextBox.readOnly = true;
            barcodeTextBox.style.backgroundColor = '#CCCCCC';

            qtyTextBox.focus();
        }

        function deleteItem(x, y, z) {
            _boughtItems = document.getElementById(x).value.split("^");
            _newBoughtItems = "";
            _ctrItems = 1;
            for (i = 0; i < _boughtItems.length; i++) {
                _boughtItem = _boughtItems[i].split('|');
                if (_boughtItem[0] != y) {
                    if (_newBoughtItems == "") {
                        _newBoughtItems = _ctrItems + "|";
                    } else {
                        _newBoughtItems += "^" + _ctrItems + "|";
                    }
                    _newBoughtItems += _boughtItem[1] + "|" + _boughtItem[2] + "|" + _boughtItem[3] + "|" + _boughtItem[4] + "|" + _boughtItem[5] + "|" + _boughtItem[6];
                    _ctrItems++;
                }
            }
            document.getElementById(x).value = _newBoughtItems;
            document.getElementById(z).value = document.getElementById(z).value - 1;
            document.forms[0].submit();
        }
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $('.table-container-scroll').dragscrollable({ dragSelector: '.dragger td', acceptPropagatedEvent: false });
            $("#KeyboardToggle").click(function() {
                if ($("#KeyboardSlider").css("display") == "none")
                    $("#KeyboardSlider").slideDown("slow");
                else
                    $("#KeyboardSlider").slideUp("slow");
            });

            var inputselected;
            $("#CustIDTextBox").click(function() {
                inputselected = "#CustIDTextBox";
            });
            $("#SearchFieldTextBox").click(function() {
                inputselected = "#SearchFieldTextBox";
            });

            $(".KeyboardDiv input").click(function() {
                if (this.value == "^^^^^") {
                    if ($("#KeyBoardDivID0").css("display") == "none")
                        $("#KeyBoardDivID1").fadeOut("fast", function() { $("#KeyBoardDivID0").fadeIn("fast"); });
                    else
                        $("#KeyBoardDivID0").fadeOut("fast", function() { $("#KeyBoardDivID1").fadeIn("fast"); });
                } else if (this.value == "ENTER") {
                    $("#KeyboardSlider").slideUp("slow");
                } else if (this.value == "SPACE") {
                    $(inputselected).val($(inputselected).val() + " ");
                } else if (this.value == "BACKSPACE") {
                    if ($(inputselected).val().length > 0)
                        $(inputselected).val($(inputselected).val().substr(0, $(inputselected).val().length - 1));
                } else {
                    $(inputselected).val($(inputselected).val() + this.value);
                }
            });
        });
    </script>

    <script language="javascript" type="text/javascript">
        function CancelOrder() {
            var _result = false;

            if (confirm("Are you sure want to deliver this order ?") == true) {
                _result = true;
            }
            else {
                _result = false;
            }

            return _result
        }
    </script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
    <asp:Literal ID="javaScriptDeclaration" runat="server"></asp:Literal>
</head>
<body id="bodyPosCheckStatus">
    <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        POS Check Status</div>
                    <div class="sep">
                    </div>
                </div>
            </div>
            <div class="right">
                <div class="date">
                    <div id="tanggal">
                    </div>
                    <div id="jam">
                    </div>
                </div>
            </div>
        </div>
        <div class="containerBox" style="clear: both">
            <div class="totalItemBox">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            Check Status
                        </td>
                    </tr>
                </table>
            </div>
            <div class="transactionRefBox">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        Reference No.
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="CustIDTextBox" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Search Field
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="SearchFieldDDL" runat="server" Width="205px">
                                            <%--<asp:ListItem Text="Job Order" Value="JobOrder" Selected="True"></asp:ListItem>--%>
                                            <asp:ListItem Value="JobOrder">Job Order</asp:ListItem>
                                            <asp:ListItem Value="CustName">Member</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <asp:TextBox ID="SearchFieldTextBox" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="padding-left: 5px">
                                        <asp:ImageButton ID="SearchButton" OnClick="btnSearch_Click" runat="server" Text="search"
                                            CausesValidation="false" />
                                    </td>
                                    <td style="padding-left: 5px">
                                        <asp:ImageButton runat="server" ID="CloseButton" />
                                    </td>
                                    <td style="padding-left: 5px">
                                        <asp:ImageButton runat="server" ID="ResetButton" OnClick="btnReset_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="float: left; padding-left: 12px; width: 99%;">
            <div class="productListBox table-container-scroll">
                <table class="dragger">
                    <tr class="bgproductListBox">
                        <th align="center">
                            <b>No</b>
                        </th>
                        <th align="center">
                            <b>Reference</b>
                        </th>
                        <th align="center">
                            <b>Settlement No</b>
                        </th>
                        <th align="center">
                            <b>Transnumber</b>
                        </th>
                        <th align="center">
                            <b>Payment</b>
                        </th>
                        <th align="center">
                            <b>Member</b>
                        </th>
                        <th align="center">
                            <b>Datetime</b>
                        </th>
                        <th align="center">
                            <b>DP Paid</b>
                        </th>
                        <th align="center">
                            <b>Change</b>
                        </th>
                    </tr>
                    <asp:Repeater ID="CheckStatusListRepeater" runat="server" OnItemDataBound="CheckStatusListRepeater_ItemDataBound"
                        OnItemCommand="CheckStatusListRepeater_ItemCommand">
                        <ItemTemplate>
                            <tr id="RepeaterTemplate" runat="server" style="font-size: 12px;">
                                <td align="center" style="width: 15px">
                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                </td>
                                <td align="left" style="width: 61px">
                                    <asp:Literal runat="server" ID="ReferenceNoLiteral"></asp:Literal>
                                </td>
                                <td align="left" style="width: 133px">
                                    <asp:Literal runat="server" ID="JobOrderLiteral"></asp:Literal>
                                </td>
                                <td align="left" style="width: 133px">
                                    <asp:Literal runat="server" ID="TransNumLiteral"></asp:Literal>
                                </td>
                                <td align="center" style="width: 47px">
                                    <asp:Literal runat="server" ID="PaymentStatusLiteral"></asp:Literal>
                                </td>
                                <td align="center" style="width: 75px">
                                    <asp:Literal runat="server" ID="MemberNameLiteral"></asp:Literal>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:Literal runat="server" ID="DatetimeLiteral"></asp:Literal>
                                </td>
                                <td align="right" style="width: 50px">
                                    <asp:Literal runat="server" ID="DPPaidLiteral"></asp:Literal>
                                </td>
                                <td align="center" style="width: 146px">
                                    <div style="float: left;">
                                        <asp:ImageButton ID="ChangeImageButton" class="ChangeImageButton" runat="server" />
                                    </div>
                                    <div style="float: left;">
                                        <asp:ImageButton ID="ViewImageButton" class="ViewImageButton" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<tr><td colspan="9">Test1</td></tr>--%>
                </table>
            </div>
            <div class="navigation table-navigation-scroll">
                <div class="up">
                </div>
                <div class="down">
                </div>
            </div>
        </div>
        <table>
            <tr>
                <td colspan="3">
                    <asp:HiddenField ID="TransNmbrHiddenField" runat="server" />
                    <div class="detailcashier">
                        <div class="toplabel">
                            Settlement No
                            <asp:Literal runat="server" ID="JobOrderNoLiteral">
                            </asp:Literal>
                        </div>
                    </div>
                    <table cellpadding="3" cellspacing="0">
                        <tr class="toptable">
                            <td align="center" style="width: 120px">
                                Product Code
                            </td>
                            <td align="center" style="width: 288px">
                                Description
                            </td>
                            <td align="center" style="width: 50px">
                                Qty
                            </td>
                            <td align="center" style="width: 150px">
                                Disc
                            </td>
                            <td align="center" style="width: 150px">
                                Price
                            </td>
                            <td align="center" style="width: 175px">
                                SubTotal
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="ContentTable1">
                    <asp:HiddenField ID="DetailTypeHiddenField" runat="server" Visible="False" />
                    <div class="table-container-scroll2">
                        <table>
                            <asp:Repeater ID="ListRepeaterDetail" runat="server" OnItemDataBound="ListRepeaterDetail_ItemDataBound">
                                <ItemTemplate>
                                    <tr id="DetailRepeaterItemTemplate" runat="server" class="repeater">
                                        <td style="width: 120px">
                                            <asp:Literal ID="ProductCodeLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td style="width: 288px">
                                            <asp:Literal ID="DescriptionLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:Literal ID="QtyLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Literal ID="DiscLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td style="width: 150px">
                                            <asp:Literal ID="PriceLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td style="width: 175px">
                                            <div style="float: left; margin-top: 10px;">
                                                <asp:Literal ID="LineTotalLiteral" runat="server"></asp:Literal>
                                            </div>
                                            <div style="float: left;">
                                                <asp:ImageButton ID="PickPrintPreview" runat="server" CssClass="PickPrintPreview" />
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </td>
                <td>
                    <div class="table-navigation-scroll2">
                        <div class="up">
                        </div>
                        <div class="down">
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="panelKeyboard">
        <div style="background-color: #cccccc;">
            <div id="KeyboardToggle" style="padding: 3px; cursor: pointer;">
                KEYBOARD</div>
        </div>
        <div id="KeyboardSlider" style="display: none">
            <div id="KeyBoardDivID0" class="KeyboardDiv">
                <div class="KeyboardDiv1">
                    <div class="number" id="nmbr1">
                        <input type="button" value="1" /></div>
                    <div class="number" id="nmbr2">
                        <input type="button" value="2" /></div>
                    <div class="number" id="nmbr3">
                        <input type="button" value="3" /></div>
                    <div class="number" id="nmbr4">
                        <input type="button" value="4" /></div>
                    <div class="number" id="nmbr5">
                        <input type="button" value="5" /></div>
                    <div class="number" id="nmbr6">
                        <input type="button" value="6" /></div>
                    <div class="number" id="nmbr7">
                        <input type="button" value="7" /></div>
                    <div class="number" id="nmbr8">
                        <input type="button" value="8" /></div>
                    <div class="number" id="nmbr9">
                        <input type="button" value="9" /></div>
                    <div class="number" id="nmbr0">
                        <input type="button" value="0" /></div>
                    <div class="number" id="nmbrMin">
                        <input type="button" value="-" /></div>
                    <div class="number" id="nmbrEqual">
                        <input type="button" value="=" /></div>
                    <div class="number" id="nmbrBack">
                        <input type="button" value="BACKSPACE" style="margin-right: 43px; width: 120px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrQ">
                        <input type="button" value="q" style="margin-left: 0px" /></div>
                    <div class="number" id="nmbrW">
                        <input type="button" value="w" /></div>
                    <div class="number" id="nmbrE">
                        <input type="button" value="e" /></div>
                    <div class="number" id="nmbrR">
                        <input type="button" value="r" /></div>
                    <div class="number" id="nmbrT">
                        <input type="button" value="t" /></div>
                    <div class="number" id="nmbrY">
                        <input type="button" value="y" /></div>
                    <div class="number" id="nmbrU">
                        <input type="button" value="u" /></div>
                    <div class="number" id="nmbrI">
                        <input type="button" value="i" /></div>
                    <div class="number" id="nmbrO">
                        <input type="button" value="o" /></div>
                    <div class="number" id="nmbrP">
                        <input type="button" value="p" /></div>
                    <div class="number" id="nmbrOpenKurva">
                        <input type="button" value="[" /></div>
                    <div class="number" id="nmbrCloseKurva">
                        <input type="button" value="]" style="margin-right: 83px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrA">
                        <input type="button" value="a" style="margin-left: 34px;" /></div>
                    <div class="number" id="nmbrS">
                        <input type="button" value="s" /></div>
                    <div class="number" id="nmbrD">
                        <input type="button" value="d" /></div>
                    <div class="number" id="nmbrF">
                        <input type="button" value="f" /></div>
                    <div class="number" id="nmbrG">
                        <input type="button" value="g" /></div>
                    <div class="number" id="nmbrH">
                        <input type="button" value="h" /></div>
                    <div class="number" id="nmbrJ">
                        <input type="button" value="j" /></div>
                    <div class="number" id="nmbrK">
                        <input type="button" value="k" /></div>
                    <div class="number" id="nmbrL">
                        <input type="button" value="l" /></div>
                    <div class="number" id="nmbrTitikKoma">
                        <input type="button" value=";" /></div>
                    <div class="number" id="nmbrPetik">
                        <input type="button" value="'" style="margin-right: 73px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrZ">
                        <input type="button" value="z" style="margin-left: 60px;" /></div>
                    <div class="number" id="nmbrX">
                        <input type="button" value="x" /></div>
                    <div class="number" id="nmbrC">
                        <input type="button" value="c" /></div>
                    <div class="number" id="nmbrV">
                        <input type="button" value="v" /></div>
                    <div class="number" id="nmbrB">
                        <input type="button" value="b" /></div>
                    <div class="number" id="nmbrN">
                        <input type="button" value="n" /></div>
                    <div class="number" id="nmbrM">
                        <input type="button" value="m" /></div>
                    <div class="number" id="nmbrKoma">
                        <input type="button" value="," /></div>
                    <div class="number" id="nmbrTitik">
                        <input type="button" value="." /></div>
                    <div class="number" id="nmbrSlash">
                        <input type="button" value="/" style="margin-right: 79.5px;" /></div>
                </div>
                <div class="KeyboardDiv1">
                    <div class="number" id="nmbrSift">
                        <input type="button" value="^^^^^" style="width: 125px;" /></div>
                    <div class="number" id="nmbrSpace">
                        <input type="button" value="SPACE" style="width: 600px;" /></div>
                    <div class="number" id="nmbrEnter">
                        <input type="button" value="ENTER" style="width: 125px;" /></div>
                </div>
            </div>
            <%--KEYBOARD SIFT--%>
            <div id="KeyBoardDivID1" class="KeyboardDiv" style="display: none">
                <div class="KeyboardDiv1">
                    <div class="number" id="nmbrSeru">
                        <input type="button" value="!" /></div>
                    <div class="number" id="nmbrAdd">
                        <input type="button" value="@" /></div>
                    <div class="number" id="nmbrSharp">
                        <input type="button" value="#" /></div>
                    <div class="number" id="nmbrDolar">
                        <input type="button" value="$" /></div>
                    <div class="number" id="nmbrPersen">
                        <input type="button" value="%" /></div>
                    <div class="number" id="nmbrPangkat">
                        <input type="button" value="^" /></div>
                    <div class="number" id="nmbrAnd">
                        <input type="button" value="&" /></div>
                    <div class="number" id="nmbrBintang">
                        <input type="button" value="*" /></div>
                    <div class="number" id="nmbrBukaKurung">
                        <input type="button" value="(" /></div>
                    <div class="number" id="nmbrTutupKurung">
                        <input type="button" value=")" /></div>
                    <div class="number" id="nmbrUnderCross">
                        <input type="button" value="_" /></div>
                    <div class="number" id="nmbrPlus">
                        <input type="button" value="+" /></div>
                    <div class="number" id="nmbrBack2">
                        <input type="button" value="BACKSPACE" style="width: 120px; margin-right: 43px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrQ2">
                        <input type="button" value="Q" /></div>
                    <div class="number" id="nmbrW2">
                        <input type="button" value="W" /></div>
                    <div class="number" id="nmbrE2">
                        <input type="button" value="E" /></div>
                    <div class="number" id="nmbrR2">
                        <input type="button" value="R" /></div>
                    <div class="number" id="nmbrT2">
                        <input type="button" value="T" /></div>
                    <div class="number" id="nmbrY2">
                        <input type="button" value="Y" /></div>
                    <div class="number" id="nmbrU2">
                        <input type="button" value="U" /></div>
                    <div class="number" id="nmbrI2">
                        <input type="button" value="I" /></div>
                    <div class="number" id="nmbrO2">
                        <input type="button" value="O" /></div>
                    <div class="number" id="nmbrP2">
                        <input type="button" value="P" /></div>
                    <div class="number" id="nmbrKurvaOpen2">
                        <input type="button" value="{" /></div>
                    <div class="number" id="nmbrKurvaClose2">
                        <input type="button" value="}" style="margin-right: 83px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrA2">
                        <input type="button" value="A" style="margin-left: 34px;" /></div>
                    <div class="number" id="nmbrS2">
                        <input type="button" value="S" /></div>
                    <div class="number" id="nmbrD2">
                        <input type="button" value="D" /></div>
                    <div class="number" id="nmbrF2">
                        <input type="button" value="F" /></div>
                    <div class="number" id="nmbrG2">
                        <input type="button" value="G" /></div>
                    <div class="number" id="nmbrH2">
                        <input type="button" value="H" /></div>
                    <div class="number" id="nmbrJ2">
                        <input type="button" value="J" /></div>
                    <div class="number" id="nmbrK2">
                        <input type="button" value="K" /></div>
                    <div class="number" id="nmbrL2">
                        <input type="button" value="L" /></div>
                    <div class="number" id="nmbrTitikDua">
                        <input type="button" value=":" /></div>
                    <div class="number" id="nmbrBackSlash">
                        <input type="button" value="\" style="margin-right: 73px;" /></div>
                </div>
                <div class="KeyboardDiv2">
                    <div class="number" id="nmbrZ2">
                        <input type="button" value="Z" style="margin-left: 60px;" /></div>
                    <div class="number" id="nmbrX2">
                        <input type="button" value="X" /></div>
                    <div class="number" id="nmbrC2">
                        <input type="button" value="C" /></div>
                    <div class="number" id="nmbrV2">
                        <input type="button" value="V" /></div>
                    <div class="number" id="nmbrB2">
                        <input type="button" value="B" /></div>
                    <div class="number" id="nmbrN2">
                        <input type="button" value="N" /></div>
                    <div class="number" id="nmbrM2">
                        <input type="button" value="M" /></div>
                    <div class="number" id="nmbrLebihKecil">
                        <input type="button" value="<" /></div>
                    <div class="number" id="nmbrLebihBesar">
                        <input type="button" value=">" /></div>
                    <div class="number" id="nmbrTanya">
                        <input type="button" value="?" style="margin-right: 79.5px;" /></div>
                </div>
                <div class="KeyboardDiv1">
                    <div class="number" id="nmbrSift2">
                        <input type="button" value="^^^^^" style="width: 125px;" /></div>
                    <div class="number" id="nmbrSpace2">
                        <input type="button" value="SPACE" style="width: 600px;" /></div>
                    <div class="number" id="nmbrEnter2">
                        <input type="button" value="ENTER" style="width: 125px;" /></div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

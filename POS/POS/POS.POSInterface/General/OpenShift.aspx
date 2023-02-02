<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenShift.aspx.cs" Inherits="POS.POSInterface.General.OpenShift" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Open Shift</title>
    <link href="../CSS/orange/style4.css" media="all" rel="Stylesheet" type="text/css" />
    <link href="../CSS/orange/style2.css" media="all" rel="Stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

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

    <%----- JS KEYBOARD SLIDER ------%>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
           $("#KeyboardToggle").click(function() {
                if ($("#KeyboardSlider").css("display") == "none")
                    $("#KeyboardSlider").slideDown("slow");
                else
                    $("#KeyboardSlider").slideUp("slow");
            });
           

            var inputselected;
            $("#CashTextBoxt").click(function() {
                inputselected = "#CashTextBoxt";
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

</head>
<body id="OpenShift">
    <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        Open Shift</div>
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
        <div style="color:Red; font-size: 20px">
            <asp:Label runat="server" ID="WarningLabel"></asp:Label>
        </div>
        <div class="formCash">
            <div style="margin: 10px;">
                Open Shift
            </div>
            <div style="margin-bottom: 50px; margin-left: 25px; margin-top: 50px;">
                <table>
                    <tr>
                        <td style="font-size: 30px;">
                            CASH
                        </td>
                        <td style="width: 25px;">
                        </td>
                        <td>
                            <asp:TextBox ID="CashTextBoxt" runat="server" CssClass="widthText"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-left: 10px;">
                <div>
                    <asp:ImageButton ID="OkButton" runat="server" OnClick="OkButton_Click" />
                </div>
                <div>
                    <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" />
                </div>
                <div>
                    <asp:ImageButton ID="BackButton" runat="server" PostBackUrl="~/Login.aspx" />
                </div>
            </div>
        </div>
    </div>
    <div id="panelKeyboard">
        <div style="background-color: #cccccc;">
            <div id="KeyboardToggle" style="padding: 3px; cursor: pointer;">
                KEYBOARD</div>
        </div>
        <div id="KeyboardSlider">
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

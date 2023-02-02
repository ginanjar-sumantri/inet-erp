<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <asp:Literal ID="AppNameLiteral" runat="server"></asp:Literal>
    </title>
    <asp:Literal ID="StyleSheetLiteral" runat="server" />

    <script src="CSS/orange/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="CSS/orange/style.js" type="text/javascript"></script>

    <link href="CSS/orange/style.css" media="all" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            /*
            $(".btnInputNumber").click(function() {
            alert("a");
            if ($("#currActiveInput").val() != "") {
            if (this.value == '.') {
            if (($("#" + $("#currActiveInput").val()).val()).indexOf('.') < 0) {
            $("#" + $("#currActiveInput").val()).val($("#" + $("#currActiveInput").val()).val() + this.value);
            }
            }
            }
            else {
            $("#" + $("#currActiveInput").val()).val($("#" + $("#currActiveInput").val()).val() + this.value);
            }
            });
            $("#btnClearNumber").click(function() 
            {
            if ($("#currActiveInput").val() != "") 
            {
            $("#" + $("#currActiveInput").val()).val("");
            }
            });
            */
            $(".btnInputNumber").click(function() {
                if ($("#currActiveInput").val() != "") {
                    if (this.value == '.') {
                        if (($("#" + $("#currActiveInput").val()).val()).indexOf('.') < 0)
                            $("#" + $("#currActiveInput").val()).val($("#" + $("#currActiveInput").val()).val() + this.value);
                    }
                    else {
                        if ($("#" + $("#currActiveInput").val()).val() == "0") {
                            $("#" + $("#currActiveInput").val()).val(this.value * 1);
                        }
                        else
                            $("#" + $("#currActiveInput").val()).val($("#" + $("#currActiveInput").val()).val() + this.value);
                    }
                }
            });
            $("#btnClearNumber").click(function() {
                if ($("#currActiveInput").val() != "") {
                    $("#" + $("#currActiveInput").val()).val("");
                }
            });
        });
    </script>

    <%-- Keyboard--%>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $('.table-container-scroll').dragscrollable({ dragSelector: '.dragger td', acceptPropagatedEvent: false });
            $('.table-container-scroll2').dragscrollable({ dragSelector: '.dragger2 td', acceptPropagatedEvent: false });
            $('.table-container-scroll3').dragscrollable({ dragSelector: '.dragger3 td', acceptPropagatedEvent: false });
            $("#KeyboardToggle").click(function() {
                if ($("#KeyboardSlider").css("display") == "none")
                    $("#KeyboardSlider").slideDown("slow");
                else
                    $("#KeyboardSlider").slideUp("slow");
            });

            var inputselected;
            $("#Login1_UserName").click(function() {
                inputselected = "#Login1_UserName";
            });
            $("#Login1_Password").click(function() {
                inputselected = "#Login1_Password";
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

    <script type="text/javascript" src="CSS/orange/dragscrollable.js"></script>

    <script src="CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <link href="CSS/orange/style2.css" media="all" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function deleteCookie() {
            var d = new Date();
            document.cookie = "v0=1;expires=" + d.toGMTString() + ";" + ";";
            alert(document.cookie);
        }
    </script>

    <asp:Literal runat="server" ID="javascriptReceiver"></asp:Literal>
</head>
<body id="bodyLogin" runat="server">
    <%--onunload="deleteCookie()"--%>
    <form id="form1" runat="server">
    <asp:Login ID="Login1" runat="server" DestinationPageUrl="Default.aspx" OnLoggedIn="Login1_LoggedIn">
        <LayoutTemplate>
            <div class="container">
                <div class="content">
                    <div class="left-content">
                        <asp:Panel ID="GeneralPanel" runat="server">
                            <div class="link-button" id="button1">
                                <asp:Button Text="Delivery" ID="DeliveryOrderButton" runat="server" OnClick="DeliveryOrderButton_Click" /></div>
                            <%--PostBackUrl="~/DeliveryOrder/ListDeliveryOrder.aspx"--%>
                            <%--<asp:Button Text="Member" ID="MemberButton" runat="server" PostBackUrl="~/DeliveryOrder/ListDeliveryOrder.aspx" /></div> --%>
                            <div class="link-button" id="button2">
                                <asp:Button Text="Tour" ID="TourButton" runat="server" OnClick="TourButton_Click" /></div>
                            <div class="link-button" id="button3">
                                <asp:Button Text="Printing" ID="PrintingButton" runat="server" OnClick="PrintingButton_Click" /></div>
                            <div class="link-button" id="button4">
                                <asp:Button Text="PhotoCopy" ID="PhotoCopyButton" runat="server" OnClick="PhotoCopyButton_Click" /></div>
                            <div class="link-button" id="button5">
                                <asp:Button Text="Shipping" ID="ShippingButton" runat="server" PostBackUrl="~/Shipping/POSShipping.aspx?code=&itemno=" /></div>
                            <div class="link-button" id="button6">
                                <asp:Button Text="Internet" ID="InternetButton" runat="server" OnClick="InternetButton_Click" /></div>
                            <div class="link-button" id="button7">
                                <asp:Button Text="Stationary" ID="StationaryButton" runat="server" PostBackUrl="~/Retail/POSRetail.aspx?referenceNo=" /></div>
                            <div class="link-button" id="button8">
                                <asp:Button Text="Graphic Design" ID="GrafikDesainButton" runat="server" OnClick="GrafikDesainButton_Click" /></div>
                            <div class="link-button" id="button9">
                                <asp:Button Text="Voucher" ID="VoucherButton" runat="server" /></div>
                            <div class="link-button" id="button10">
                                <asp:Button Text="Cafe" ID="CafeButton" runat="server" OnClick="CafeButton_Click" /></div>
                            <div class="link-button" id="button11">
                                <asp:Button Text="Cashier" ID="CashierButton" runat="server" PostBackUrl="~/General/Cashier.aspx" /></div>
                            <div class="link-button" id="button12">
                                <asp:Button Text="Monitoring" ID="MonitoringButton" runat="server" PostBackUrl="~/General/Monitoring.aspx" /></div>
                        </asp:Panel>
                        <asp:Panel ID="TourPanel" runat="server">
                            <div class="link-button" id="button13">
                                <asp:Button Text="Ticketing" ID="TicketingButton" runat="server" PostBackUrl="~/Ticketing/Ticketing.aspx?referenceNo=&code=" /></div>
                            <div class="link-button" id="button14">
                                <asp:Button Text="Hotel" ID="HotelButton" runat="server" PostBackUrl="~/THotel/Hotel.aspx?referenceNo=&code=" /></div>
                            <div class="link-button" id="button15">
                                <asp:Button Text="Back" ID="BackButton" runat="server" OnClick="BackButton_Click" /></div>
                        </asp:Panel>
                    </div>
                    <div id="Div1" class="right-content" runat="server">
                        <div class="WelcomeLabel">
                            Welcome To<%--<asp:Label ID="WalcomeLabel" runat="server" Text="WELCOME TO"></asp:Label>--%></div>
                        <div class="CompanyImage">
                            <%--<asp:Image ID="CompanyImage" runat="server" />--%></div>
                        <asp:HiddenField ID="CompanyIDHiddenField" runat="server" />
                        <asp:HiddenField ID="CompanyNameHiddenField" runat="server" />
                        <input type="hidden" id="currActiveInput" value="0" />
                        <div class="login">
                            <div class="input-form" id="instance">
                                <div class="label">
                                    Instance</div>
                                <div class="input">
                                    <asp:DropDownList ID="ConnModeDropDownList" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="input-form" id="username">
                                <div class="label">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username</asp:Label></div>
                                <div class="input">
                                    <asp:TextBox ID="UserName" runat="server" Width="120px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required." 

ValidationGroup="Login1">*</asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="input-form" id="password">
                                <div class="label">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label></div>
                                <div class="input">
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Password is required." ToolTip="Password is required." 

ValidationGroup="Login1">*</asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="message-error">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                <asp:Literal ID="FailureText2" runat="server" EnableViewState="false"></asp:Literal>
                            </div>
                            <div class="submit">
                                <div class="btn_login">
                                    <asp:Button ID="LoginButton" runat="server" ValidationGroup="Login1" CommandName="Login"
                                        Text="Login" />
                                </div>
                                <div class="btn_logout">
                                    <asp:LoginStatus ID="LoginStatus1" runat="server" LoginText="" OnLoggingOut="LoginStatus1_LoggingOut" />
                                </div>
                            </div>
                            <div class="OpenShift" id="button16">
                                <asp:Button ID="OpenShiftButton" runat="server" PostBackUrl="~/General/OpenShift.aspx" />
                            </div>
                        </div>
                        <div class="numpad">
                            <div class="left-container">
                                <div class="number" id="nmbr1">
                                    <input type="button" value="1" class="btnInputNumber"></div>
                                <div class="number" id="nmbr2">
                                    <input type="button" value="2" class="btnInputNumber"></div>
                                <div class="number" id="nmbr3">
                                    <input type="button" value="3" class="btnInputNumber"></div>
                                <div class="number" id="nmbr4">
                                    <input type="button" value="4" class="btnInputNumber"></div>
                                <div class="number" id="nmbr5">
                                    <input type="button" value="5" class="btnInputNumber"></div>
                                <div class="number" id="nmbr6">
                                    <input type="button" value="6" class="btnInputNumber"></div>
                                <div class="number" id="nmbr7">
                                    <input type="button" value="7" class="btnInputNumber"></div>
                                <div class="number" id="nmbr8">
                                    <input type="button" value="8" class="btnInputNumber"></div>
                                <div class="number" id="nmbr9">
                                    <input type="button" value="9" class="btnInputNumber"></div>
                                <div class="number" id="nmbrdot">
                                    <input type="button" value="." class="btnInputNumber"></div>
                                <div class="number" id="nmbr0">
                                    <input type="button" value="0" class="btnInputNumber"></div>
                                <div class="number" id="nmbr00">
                                    <input type="button" value="00" class="btnInputNumber"></div>
                            </div>
                            <div class="right-container">
                                <div class="clear">
                                    <input type="button" value="CLR" class="btnConfirmNumber" id="btnClearNumber" /></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="footer">
                    KEEP SMILING TO THE CUSTOMER...
                </div>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <%--<div style="position: absolute; top: 197px; width: 10%; height: 24px;">
        <asp:Panel ID="UserPanel" runat="server">
        <asp:TextBox ID="UserNameTextBox" runat="server" Style="width: 194px; height: 17px;"></asp:TextBox>
        <%--</asp:Panel>
    </div>
    <div style="position: absolute; top: 235px; width: 10%; height: 24px;">
        <%--<asp:Panel ID="PasswordPanel" runat="server">
        <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" Style="width: 194px;
            height: 17px;"></asp:TextBox>
        <%--</asp:Panel>--%>
    </div>--%>
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

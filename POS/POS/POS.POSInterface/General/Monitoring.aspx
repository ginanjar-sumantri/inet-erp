<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Monitoring.aspx.cs" Inherits="POS.POSInterface.General.Monitoring" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<asp:scriptmanager id="ScriptManager1" runat="server" loadscriptsbeforeui="false">
                    </asp:scriptmanager>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<asp:literal id="javascriptReceiver" runat="server"></asp:literal>
<head runat="server">
    <link href="../CSS/orange/style2.css" media="all" rel="Stylesheet" type="text/css" />
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" src="../CSS/orange/JScript.js"></script>

    <script src="../CSS/orange/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../CSS/orange/dragscrollable.js"></script>

    <style type="text/css">
        .fixedTable
        {
            font-size: small;
            width: 971px;
        }
        input, .ddl
        {
            border: medium double SeaGreen;
            border-radius: 4px 4px 4px 4px;
            color: Black;
        }
    </style>

    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#dialogHold").draggable();
            setTimeout(function() {
                $("#dialogHold").fadeOut("fast")
            }, 1000);
        });
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $('.table-container-scroll').dragscrollable({ dragSelector: '.dragger td', acceptPropagatedEvent: false });
        });
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $('.table-container-scrollcheck2').dragscrollable({ dragSelector: '.dragger td', acceptPropagatedEvent: false });
        });
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
            $("#RefNoTextBox").click(function() {
                inputselected = "#RefNoTextBox";
            });
            $("#SearchFieldTextBox").click(function() {
                inputselected = "#SearchFieldTextBox";
            });
            $("#PasswordTextBox").click(function() {
                inputselected = "#PasswordTextBox";
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

    <title>POS - Monitoring</title>
</head>
<body id="bodyMonitoring">
    <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        Pos Monitoring
                    </div>
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
        <asp:Panel ID="PasswordPanel" runat="server">
            <div>
                <table cellpadding="3" cellspacing="1" width="0" border="0">
                    <tr>
                        <td style="width: 190px; font-size: 16px; text-align: left; color: White;">
                            <b>Password Required : </b>
                        </td>
                        <td>
                            <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" Width="135px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="OKButton" runat="server" Text="OK" OnClick="OKButton_Click" />
                            <asp:ImageButton ID="CancellButton" runat="server" Text="OK" OnClick="CancellButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="FormPanel" runat="server">
            <div class="content">
                <div class="top" style="border-radius: 10px;">
                    <div class="table">
                        <div class="table-inner-top">
                            <div class="input-form" id="referencenumber">
                                <div style="float: left; padding-left: 3px" id="resetbtn">
                                    <asp:Button ID="CashierButton" runat="server" Text="Reset" OnClick="CashierButton_Click" />
                                </div>
                                <div style="float: left; padding-left: 3px" id="searchbtn">
                                    <asp:Button ID="CheckStatusButton" runat="server" Text="Search" OnClick="CheckStatusButton_Click" />
                                </div>
                                <div style="float: left; padding-left: 3px" id="MemberBtn">
                                    <asp:Button Text="Member" ID="MemberButton" runat="server" OnClick="MemberButton_Click" />
                                </div>
                                <div style="float: left; padding-left: 3px" id="RegsitrasiBtn">
                                    <asp:Button Text="Registration" ID="RegistrationButton" runat="server" OnClick="RegistrationButton_Click" />
                                </div>
                                <div style="float: left; padding-left: 3px" id="backbtn">
                                    <asp:ImageButton runat="server" ID="BackImageButton" OnClick="BackImageButton_Click" />
                                </div>
                                <asp:Panel runat="server" ID="searchPanel">
                                    <div style="float: left;">
                                        <asp:HiddenField ID="TypeHiddenField" runat="server" />
                                        <table>
                                            <tr>
                                                <td valign="middle">
                                                    Search Field
                                                </td>
                                                <td valign="middle">
                                                    :
                                                </td>
                                                <td valign="middle">
                                                    <asp:DropDownList ID="SearchFieldDDL" runat="server" AutoPostBack="true" CssClass="ddl">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnSearch" CssClass="SearchButtonForm" runat="server" CausesValidation="false"
                                                        OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                            <asp:Panel ID="SearchFieldPanel" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:Literal ID="SearchFieldLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="3" align="right">
                                                        <asp:TextBox ID="SearchFieldTextBox" runat="server" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="SearchFieldPanel2" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:Literal ID="SearchTitle2Literal" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox ID="SearchField2TextBox" runat="server" Width="75px" BackColor="#CCCCCCC"></asp:TextBox>
                                                        <input id="DateButton" type="button" onclick="displayCalendar(SearchField2TextBox,'yyyy-mm-dd',this)"
                                                            value="..." />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Literal ID="SearchTitle3Literal" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox ID="SearchField3TextBox" runat="server" Width="75px" BackColor="#CCCCCCC"></asp:TextBox>
                                                        <input id="Date2Button" type="button" onclick="displayCalendar(SearchField3TextBox,'yyyy-mm-dd',this)"
                                                            value="..." />
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="CashierPanel" runat="server">
                                    <div>
                                        <table class="tableRepater">
                                            <tr valign="top">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td valign="top">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellpadding="3" cellspacing="0" class="fixedTable">
                                                                                <tr class="toptable">
                                                                                    <td align="center" style="width: 40px;">
                                                                                        No
                                                                                    </td>
                                                                                    <td align="center" style="width: 180px;">
                                                                                        TransNmbr
                                                                                    </td>
                                                                                    <td align="center" style="width: 100px">
                                                                                        Transdate
                                                                                    </td>
                                                                                    <td align="center" style="width: 120px">
                                                                                        Divisi
                                                                                    </td>
                                                                                    <td align="center" style="width: 150px">
                                                                                        Reference No
                                                                                    </td>
                                                                                    <td align="center" style="width: 150px">
                                                                                        Member ID
                                                                                    </td>
                                                                                    <td align="center" style="width: 150px">
                                                                                        Name
                                                                                    </td>
                                                                                    <td align="center" style="width: 147px">
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <tr>
                                                                        <td class="ContentTable">
                                                                            <div class="table-container-scroll">
                                                                                <table class="dragger">
                                                                                    <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound"
                                                                                        OnItemCommand="ListRepeater_ItemCommand">
                                                                                        <ItemTemplate>
                                                                                            <tr id="RepeaterItemTemplate" runat="server" class="repeater" align="left">
                                                                                                <td align="center" style="width: 40px;">
                                                                                                    <asp:Literal ID="NoLiteral2" runat="server"></asp:Literal>
                                                                                                </td>
                                                                                                <td style="width: 180px">
                                                                                                    <asp:Literal ID="TransNmbrLiteral" runat="server"></asp:Literal>
                                                                                                </td>
                                                                                                <td style="width: 100px">
                                                                                                    <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                                                                                                </td>
                                                                                                <td style="width: 120px">
                                                                                                    <asp:Literal ID="DivisiLiteral" runat="server"></asp:Literal>
                                                                                                </td>
                                                                                                <td style="width: 150px">
                                                                                                    <asp:Literal ID="ReferenceNoLiteral" runat="server"></asp:Literal>
                                                                                                </td>
                                                                                                <td style="width: 150px">
                                                                                                    <asp:Literal ID="MemberIDLiteral" runat="server"></asp:Literal>
                                                                                                </td>
                                                                                                <td style="width: 150px">
                                                                                                    <asp:Literal ID="MemberNameLiteral" runat="server"></asp:Literal>
                                                                                                </td>
                                                                                                <td style="width: 147px">
                                                                                                    <div class="ViewDetailImageButton" style="float: left;">
                                                                                                        <asp:ImageButton ID="ViewDetailImageButton" runat="server" />
                                                                                                    </div>
                                                                                                    <div class="CancelImageButton" style="float: left;">
                                                                                                        <asp:ImageButton ID="CancelImageButton" runat="server" />
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </table>
                                                                            </div>
                                                                            <asp:HiddenField ID="List1HiddenField" runat="server" />
                                                                            <asp:HiddenField ID="List3HiddenField" runat="server" />
                                                                            <asp:HiddenField ID="List4HiddenField" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <div class="table-navigation-scroll">
                                                                                <div class="up">
                                                                                </div>
                                                                                <div class="down">
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:HiddenField ID="List2HiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:HiddenField ID="TransNmbrHiddenField" runat="server" />
                                                    <div class="detailcashier">
                                                        <div class="toplabel">
                                                            Job Order No
                                                            <asp:Literal runat="server" ID="JobOrderNoLiteral">
                                                            </asp:Literal>
                                                        </div>
                                                        <div style="margin-left: 409px; margin-top: -29px; font-size: 13px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Start Date
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="StartDateCashierTextBox" runat="server" Width="75px" BackColor="#CCCCCCC"></asp:TextBox>
                                                                        <input id="StartDateCashierButton" type="button" onclick="displayCalendar(StartDateCashierTextBox,'yyyy-mm-dd',this)"
                                                                            value="..." />
                                                                    </td>
                                                                    <td>
                                                                        End Date
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="EndDateCashierTextBox" runat="server" Width="75px" BackColor="#CCCCCCC"></asp:TextBox>
                                                                        <input id="EndDateCashierButton" type="button" onclick="displayCalendar(EndDateCashierTextBox,'yyyy-mm-dd',this)"
                                                                            value="..." />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div style="margin-left: 850px;">
                                                            <asp:ImageButton ID="CashierPrintPreviewButton" runat="server" OnClick="CashierPrintPreviewButton_Click" />
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
                                                <td>
                                                    <asp:HiddenField ID="DetailTypeHiddenField" runat="server" />
                                                    <div class="table-container-scroll2">
                                                        <table>
                                                            <%--<asp:Repeater ID="ListRepeaterDetail" runat="server">--%>
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
                                                                            <asp:Literal ID="LineTotalLiteral" runat="server"></asp:Literal>
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
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="CheckStatusPanel" runat="server">
                                    <div>
                                        <table class="tableRepater">
                                            <tr valign="top">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellpadding="3" cellspacing="0" class="fixedTable">
                                                                                <tr class="toptable">
                                                                                    <td align="center" style="width: 40px;">
                                                                                        <b>No</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 100px;">
                                                                                        <b>Reference</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 170px;">
                                                                                        <b>Settlement No</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 170px;">
                                                                                        <b>Transnumber</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 78px;">
                                                                                        <b>Divisi</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 80px;">
                                                                                        <b>Payment</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 111px;">
                                                                                        <b>Member</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 122px;">
                                                                                        <b>Datetime</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 116px;">
                                                                                        <b>DP Paid</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 82px;">
                                                                                        <b>View</b>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <tr>
                                                                        <td class="ContentTable">
                                                                            <div class="table-container-scroll">
                                                                                <table class="dragger">
                                                                                    <asp:Repeater ID="CheckStatusListRepeater" runat="server" OnItemDataBound="CheckStatusListRepeater_ItemDataBound"
                                                                                        OnItemCommand="CheckStatusListRepeater_ItemCommand">
                                                                                        <ItemTemplate>
                                                                                            <tr id="RepeaterTemplate" class="repeater" runat="server" style="font-size: 16px;">
                                                                                                <td align="center" style="width: 40px">
                                                                                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="left" style="width: 100px">
                                                                                                    <asp:Literal runat="server" ID="ReferenceNoLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="left" style="width: 170px">
                                                                                                    <asp:Literal runat="server" ID="JobOrderLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="left" style="width: 170px">
                                                                                                    <asp:Literal runat="server" ID="TransNmbrLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="left" style="width: 78px">
                                                                                                    <asp:Literal runat="server" ID="DivisiLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="center" style="width: 80px">
                                                                                                    <asp:Literal runat="server" ID="PaymentStatusLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="center" style="width: 111px">
                                                                                                    <asp:Literal runat="server" ID="MemberNameLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="center" style="width: 122px">
                                                                                                    <asp:Literal runat="server" ID="DatetimeLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="right" style="width: 107px">
                                                                                                    <asp:Literal runat="server" ID="DPPaidLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="center" class="ViewImageButton" style="width: 40px">
                                                                                                    <asp:ImageButton ID="ViewImageButton" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="table-navigation-scroll">
                                                                                <div class="up">
                                                                                </div>
                                                                                <div class="down">
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <div class="detailcashier">
                                                        <div class="toplabel">
                                                            <div>
                                                                <div>
                                                                    Job Order No
                                                                </div>
                                                                <div style="margin-top: -25px;">
                                                                    <asp:Literal runat="server" ID="JobOrderNoCheckStatus">
                                                                    </asp:Literal>
                                                                </div>
                                                            </div>
                                                            <div style="margin-top: 10px;">
                                                                <div style="margin-left: 187px; margin-top: -56px; font-size: 13px; font-family: caption;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                Start Date
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="StartDateCheckStatusTextBox" runat="server" Width="75px" BackColor="#CCCCCCC"></asp:TextBox>
                                                                                <input id="StartDateCheckStatusButton" type="button" onclick="displayCalendar(StartDateCheckStatusTextBox,'yyyy-mm-dd',this)"
                                                                                    value="..." />
                                                                            </td>
                                                                            <td>
                                                                                End Date
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="EndDateCheckStatusTextBox" runat="server" Width="75px" BackColor="#CCCCCCC"></asp:TextBox>
                                                                                <input id="EndDateCheckStatusButton" type="button" onclick="displayCalendar(EndDateCheckStatusTextBox,'yyyy-mm-dd',this)"
                                                                                    value="..." />
                                                                            </td>
                                                                            <td>
                                                                                Report Type
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ReportTypeDDL" runat="server" CssClass="ddl">
                                                                                    <asp:ListItem Value="0" Text="Down Payment"></asp:ListItem>
                                                                                    <asp:ListItem Value="1" Text="All Transactions"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div style="margin-left: 850px; margin-top: -10px;">
                                                                    <asp:ImageButton ID="CheckStatusPrintPreviewButton" runat="server" OnClick="CheckStatusPrintPreviewButton_Click" />
                                                                </div>
                                                            </div>
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
                                                    <asp:HiddenField ID="HiddenField2" runat="server" Visible="False" />
                                                    <div class="table-container-scroll2">
                                                        <table>
                                                            <asp:Repeater ID="CheckStatusListRepeaterDt" runat="server" OnItemDataBound="CheckStatusListRepeaterDt_ItemDataBound">
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
                                                                            <asp:Literal ID="LineTotalLiteral" runat="server"></asp:Literal>
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
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Panel ID="MemberPanel" runat="server">
                                    <div>
                                        <table class="tableRepater">
                                            <tr valign="top">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellpadding="3" cellspacing="0" class="fixedTable">
                                                                                <tr class="toptable">
                                                                                    <td align="center" style="width: 60px;">
                                                                                        <b>No</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 180px;">
                                                                                        <b>Member Name</b>
                                                                                    </td>
                                                                                    <td align="center" style="width: 251px;">
                                                                                        <b>Address</b>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <b>Telephone</b>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <b>Handphone</b>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <b>Barcode</b>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table>
                                                                    <tr>
                                                                        <td class="ContentTable">
                                                                            <div class="table-container-scroll3">
                                                                                <table class="dragger">
                                                                                    <asp:Repeater ID="MemberRepeater" runat="server" OnItemDataBound="MemberRepeater_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <tr id="RepeaterTemplate" class="repeater" runat="server" style="font-size: 16px;">
                                                                                                <td align="center" style="width: 60px">
                                                                                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="left" style="width: 180px">
                                                                                                    <asp:Literal runat="server" ID="MemberNameLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="left" style="width: 249px">
                                                                                                    <asp:Literal runat="server" ID="AddressLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="left" style="width: 152px">
                                                                                                    <asp:Literal runat="server" ID="TelephoneLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="center" style="width: 170px">
                                                                                                    <asp:Literal runat="server" ID="HandPhoneLiteral"></asp:Literal>
                                                                                                </td>
                                                                                                <td align="center" style="width: 120px">
                                                                                                    <asp:Literal runat="server" ID="BarcodeLiteral"></asp:Literal>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="table-navigation-scroll3">
                                                                                <div class="up">
                                                                                </div>
                                                                                <div class="down">
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <asp:Panel runat="server" ID="DetailPanel">
                                        <div>
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                        <div class="detailcashier">
                                                            <div class="toplabel">
                                                                Job Order No
                                                                <asp:Literal runat="server" ID="Literal1">
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
                                                        <asp:HiddenField ID="HiddenField4" runat="server" Visible="False" />
                                                        <div class="table-container-scroll2">
                                                            <table>
                                                                <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="CheckStatusListRepeaterDt_ItemDataBound">
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
                                                                                <asp:Literal ID="LineTotalLiteral" runat="server"></asp:Literal>
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
                                    </asp:Panel>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="ReasonListPanel">
            <div class="reasonList">
                <table cellpadding="3" cellspacing="1" width="0" border="0">
                    <tr class="bgproductListBox">
                        <td style="width: 40px" class="tahoma_11_white" align="center">
                            <b>Pick</b>
                        </td>
                        <td style="width: 345px" class="tahoma_11_white" align="center">
                            <b>Reason</b>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="table-container-scroll2">
                <table class="dragger2">
                    <%--<asp:HiddenField ID="TransNmbrHiddenField" runat="server" />--%>
                    <asp:Repeater ID="ReasonListRepeater" runat="server" OnItemCommand="ReasonListRepeater_ItemCommand"
                        OnItemDataBound="ReasonListRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr style="width: 40px" class="tahoma_11_white" id="RepeaterTemplate" runat="server">
                                <td align="center" class="pickReasonButton">
                                    <asp:ImageButton ID="PickReasonImageButton" runat="server" />
                                </td>
                                <td style="width: 345px" class="tahoma_11_white">
                                    <asp:Literal runat="server" ID="ReasonLiteral"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="table-navigation-scroll2">
                <div class="up">
                </div>
                <div class="down">
                </div>
            </div>
            <div class="reasonback">
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            <asp:ImageButton runat="server" ID="Back2ImageButton" text="" OnClick="Back2ImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <div style="background-color: white; background-image: none;">
            <asp:Panel runat="server" ID="CashierPrintPreviewPanel">
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td style="text-align: right;">
                            <input type="button" value="Go Back" onclick="history.back()" style="height: 40px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="CashierReportViewer" Width="100%" runat="server" Height="650px">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="background-color: white; background-image: none;">
            <asp:Panel runat="server" ID="CheckStatusPrintPreviewPanel">
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td style="text-align: right;">
                            <input type="button" value="Go Back" onclick="history.back()" style="height: 40px;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="CheckStatusReportViewer" Width="100%" runat="server" Height="650px">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
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

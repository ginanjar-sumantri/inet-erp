<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POSPhotocopy.aspx.cs" Inherits="POS.POSInterface.Photocopy.POSPhotocopy"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>POS Photocopy</title>
    <link href="../CSS/orange/style4.css" media="all" rel="Stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery-1.4.4.min.js" type="text/javascript"></script>

    <script src="../CSS/orange/style.js" type="text/javascript"></script>

    <script src="../JQuery/jquery-ui.min.js" type="text/javascript"></script>

    <link href="../JQuery/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../CSS/orange/jquery.corner.js" type="text/javascript"></script>

    <script type="text/javascript" src="../CSS/orange/dragscrollable.js"></script>

    <script src="../CSS/orange/JScript.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            $(".productBox").click(function() {
                $(".productBox").css("background-color", "#EEE");
                this.style.backgroundColor = "Red";
            });

        });

        function deleteBoughtItem(x) {
            var tempBoughtItem = "";
            var BoughtItemHidden = $("#BoughtItemHiddenField").val();
            BoughtItemRows = BoughtItemHidden.split("^");
            if (BoughtItemHidden != "") {
                for (i = 0; i < BoughtItemRows.length; i++) {
                    if (i != x) tempBoughtItem += "^" + BoughtItemRows[i];
                }
                $("#BoughtItemHiddenField").val(tempBoughtItem.substr(1));
            }
            document.forms[0].submit();
        }

        function editBoughtItem(x) {
            $("#EditRowHiddenField").val(x);
            document.forms[0].submit();
        }

        function harusAngka(x) { if (isNaN(x.value)) x.value = "0"; }
    </script>

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
        $('#bodyPhotocopy .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
        $('#bodyPhotocopy .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
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
            $('#bodyPhotocopy .container .header .right .date #tanggal').text(Hari[dateObject.getDay()] + ', ' + dateObject.getDate() + ' ' + Bulan[dateObject.getMonth()] + ' ' + (dateObject.getYear() + 1900));
            $('#bodyPhotocopy .container .header .right .date #jam').text(hour + ':' + minute + ':' + second);
        }
        var x = setInterval("updateJam();", 1000);
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
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
                    $("#" + $("#currActiveInput").val()).val("0");
                }
            });
        });
    </script>

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
            $('.table-container-scroll2').dragscrollable({ dragSelector: '.dragger2 td', acceptPropagatedEvent: false });
        });
    </script>

    <script language="javascript" type="text/javascript">
        function editQyt(_qytTextbox) {
            qtyProductTextBox = document.getElementById(_qytTextbox);

            qtyProductTextBox.focus();

        }
    </script>

    <asp:Literal runat="server" ID="javascriptReceiver"></asp:Literal>
</head>
<body id="bodyPhotocopy">
    <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <div class="left">
                <div class="title">
                    <div class="text">
                        POS Photocopy
                    </div>
                    <div class="sep">
                    </div>
                </div>
            </div>
            <div class="right">
                <%--<div style="float:left"><img src="../images/logoBizxpress.gif" /></div>--%>
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
                            <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="OKButton" runat="server" Text="OK" OnClick="OKButton_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="FormPanel">
            <div class="content">
                <div class="left">
                    <div class="table">
                        <div class="table-inner-left">
                            <div class="input-form" id="trans-ref">
                                <div class="input">
                                    <asp:TextBox runat="server" ID="TransRefTextBox"></asp:TextBox>
                                    <asp:Label ID="WarningLabel" runat="server" CssClass="warning" ForeColor="Red"></asp:Label>
                                    <asp:HiddenField ID="TransRefHiddenField" runat="server" />
                                    <asp:HiddenField ID="IsEditedHiddenField" runat="server" />
                                    <asp:HiddenField ID="ReferenceNoHiddenField" runat="server" />
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="input-form" id="cashier">
                                <div class="label">
                                    Operator</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:Label ID="CashierLabel" runat="server"></asp:Label></div>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="input-form" id="reference">
                                <div class="label">
                                    Reference</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:TextBox runat="server" ID="ReferenceTextBox" onfocus="$('#currActiveInput').val(this.id); $(this).select();"></asp:TextBox></div>
                                <%--<asp:RequiredFieldValidator ID="ReferenceRequiredFieldValidator" runat="server" ErrorMessage="Reference Must Be Filled"
                                Text="*" ControlToValidate="ReferenceTextBox" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="input-form" id="member">
                                <div class="label">
                                    Member</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:TextBox runat="server" ID="MemberNoTextBox" onKeyPress="return event.keyCode!=13"
                                        OnTextChanged="MemberNoTextBox_TextChanged" AutoPostBack="true" onfocus="$('#currActiveInput').val(this.id); $(this).select();"></asp:TextBox>
                                </div>
                                <%--<asp:Button runat="server" ID="SearchMemberButton" style="float:left;" Text="..." />--%>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="input-form" id="name">
                                <div class="label">
                                    Name</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:TextBox runat="server" ID="MemberNameTextBox"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="input-form" id="phone">
                                <div class="label">
                                    Phone</div>
                                <div class="sep">
                                    :</div>
                                <div class="input">
                                    <asp:TextBox ID="CustPhoneTextBox" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="input">
                            <asp:Button ID="NewMemberButton" ToolTip="NewMember" runat="server" 
                                onclick="NewMemberButton_Click" />
                        </div>
                    </div>
                    <div class="order">
                        <asp:HiddenField runat="server" ID="BoughtItemHiddenField" />
                        <div class="table-header">
                            <table>
                                <tr>
                                    <td id="qty">
                                        Qty
                                    </td>
                                    <td id="product">
                                        Product
                                    </td>
                                    <td id="price">
                                        Price
                                    </td>
                                    <td id="disc">
                                        Disc
                                    </td>
                                    <td id="total">
                                        Total
                                    </td>
                                    <td id="edit">
                                        Edit
                                    </td>
                                    <td id="cancel">
                                        Cancel
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="table-body table-container-scroll">
                            <table class="dragger">
                                <asp:Literal runat="server" ID="BoughtItemLiteral"></asp:Literal>
                            </table>
                        </div>
                    </div>
                    <div class="total">
                        <div class="send">
                            <div class="text">
                                <div class="input-form" id="discount">
                                    <div class="label">
                                        Discount</div>
                                    <div class="value">
                                        <asp:Label runat="server" ID="DiscountLabel"></asp:Label></div>
                                </div>
                                <div class="clear">
                                </div>
                                <div class="input-form" id="subtotal">
                                    <div class="label">
                                        Sub Total</div>
                                    <div class="value">
                                        <asp:Label runat="server" ID="SubTotalLabel"></asp:Label></div>
                                </div>
                                <div class="clear">
                                </div>
                                <div class="input-form" id="service-charge">
                                    <div class="label">
                                        Service Charge</div>
                                    <div class="value">
                                        <asp:Label runat="server" ID="ServiceChargeLabel"></asp:Label></div>
                                </div>
                                <div class="clear">
                                </div>
                                <div class="input-form" id="tax">
                                    <div class="label">
                                        Tax</div>
                                    <div class="value">
                                        <asp:Label runat="server" ID="TaxLabel"></asp:Label><asp:HiddenField ID="TaxHiddenField"
                                            runat="server" />
                                        <asp:HiddenField ID="PB1HiddenField" runat="server" />
                                    </div>
                                </div>
                                <div class="clear">
                                </div>
                                <div class="input-form" id="Div1">
                                    <div class="label">
                                        Total</div>
                                    <div class="value">
                                        <asp:Label runat="server" ID="TotalLabel"></asp:Label></div>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="button-submit">
                                <asp:Button runat="server" ID="SendToCashierButton" Text="SEND TO CASHIER" OnClick="SendToCashierButton_Click" />
                            </div>
                        </div>
                        <div class="navigator">
                            <asp:HiddenField runat="server" ID="EditRowHiddenField" />
                            <asp:HiddenField runat="server" ID="EditPostBackHiddenField" />
                            <div class="scroll table-navigation-scroll">
                                <div class="btn_up up">
                                </div>
                                <div class="btn_down down">
                                </div>
                            </div>
                            <div class="note">
                                Count Counter Product
                                <asp:TextBox runat="server" ID="NotesTextBox" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="right">
                    <div class="item">
                        <div class="item-inner-top">
                            <asp:ImageButton runat="server" ID="PrevProductGroupButton" OnClick="PrevProductGroupButton_Click" />
                            <div class="list-item">
                                <asp:HiddenField runat="server" ID="ProductGroupHiddenField" />
                                <asp:HiddenField runat="server" ID="ProductGroupPageHiddenField" />
                                <asp:Repeater runat="server" ID="ProductGroupRepeater" OnItemDataBound="ProductGroupRepeater_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="ProductGroupButton" class="productGroupBox" OnClick="ProductGroupButton_Click" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <asp:ImageButton runat="server" ID="NextProductGroupButton" OnClick="NextProductGroupButton_Click" />
                        </div>
                        <div class="clear">
                        </div>
                        <div class="item-inner-bottom">
                            <div class="item-inner-bottom-left">
                                <asp:ImageButton runat="server" ID="PrevProductSubGroupButton" OnClick="PrevProductSubGroupButton_Click" />
                                <div class="list-item">
                                    <asp:HiddenField runat="server" ID="ProductSubGroupHiddenField" />
                                    <asp:HiddenField runat="server" ID="ProductSubGroupPageHiddenField" />
                                    <asp:Repeater runat="server" ID="ProductSubGroupRepeater" OnItemDataBound="ProductSubGroupRepeater_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="ProductSubGroupButton" class="productSubGroupBox"
                                                OnClick="ProductSubGroupButton_Click" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <asp:ImageButton runat="server" ID="NextProductSubGroupButton" OnClick="NextProductSubGroupButton_Click" />
                            </div>
                            <div class="item-inner-bottom-right">
                                <asp:HiddenField runat="server" ID="ProductHiddenField" />
                                <asp:HiddenField runat="server" ID="ProductPageHiddenField" />
                                <asp:Repeater runat="server" ID="ProductRepeater" OnItemDataBound="ProductRepeater_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="ProductButton" class="productBox" OnClick="ProductButton_Click" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                        <div class="navigator-left-right">
                            <asp:Button runat="server" ID="ProductPrevButton" Text="Prev" OnClick="ProductPrevButton_Click" />
                            <asp:Button runat="server" ID="ProductNextButton" Text="Next" OnClick="ProductNextButton_Click" />
                        </div>
                    </div>
                    <div class="navigator">
                        <div class="navigator-left">
                            <asp:ImageButton runat="server" class="button-submit" ID="SearchImageButton" />
                            <asp:ImageButton runat="server" class="button-submit" ID="JoinJobOrderImageButton" />
                            <asp:ImageButton runat="server" class="button-submit" ID="CheckStatusImageButton" />
                            <asp:ImageButton runat="server" class="button-submit" ID="CancelAllImageButton" OnClick="CancelAllImageButton_Click" />
                            <asp:ImageButton runat="server" class="button-submit" ID="BackImageButton" OnClick="BackImageButton_Click" />
                            <asp:ImageButton runat="server" class="button-submit" ID="CashierButton" PostBackUrl="../General/Cashier.aspx" />
                            <asp:ImageButton runat="server" class="button-submit" ID="CashierAbuButton" />
                        </div>
                        <div class="navigator-right">
                            <div class="navigator-right-inner">
                                <div class="input-form">
                                    <asp:TextBox runat="server" ID="qtyProductTextBox" Text="0"></asp:TextBox>
                                </div>
                                <div id="panelInputNumber">
                                    <input type="hidden" id="currActiveInput" value="" />
                                    <div class="number">
                                        <div id="nmbr1">
                                            <input type="button" value="1" class="btnInputNumber"></div>
                                        <div id="nmbr2">
                                            <input type="button" value="2" class="btnInputNumber"></div>
                                        <div id="nmbr3">
                                            <input type="button" value="3" class="btnInputNumber"></div>
                                        <div id="nmbr4">
                                            <input type="button" value="4" class="btnInputNumber"></div>
                                        <div id="nmbr5">
                                            <input type="button" value="5" class="btnInputNumber"></div>
                                        <div id="nmbr6">
                                            <input type="button" value="6" class="btnInputNumber"></div>
                                        <div id="nmbr7">
                                            <input type="button" value="7" class="btnInputNumber"></div>
                                        <div id="nmbr8">
                                            <input type="button" value="8" class="btnInputNumber"></div>
                                        <div id="nmbr9">
                                            <input type="button" value="9" class="btnInputNumber"></div>
                                        <div id="nmbrdot">
                                            <input type="button" value="." class="btnInputNumber"></div>
                                        <div id="nmbr0">
                                            <input type="button" value="0" class="btnInputNumber"></div>
                                        <div id="nmbr00">
                                            <input type="button" value="00" class="btnInputNumber"></div>
                                    </div>
                                    <div class="button-submit">
                                        <input type="button" value="CLR" class="btnConfirmNumber" id="btnClearNumber" />
                                        <%--<asp:button runat="server" id="btnOKPanelNumber" class="btnConfirmNumber" text="OK" onclick="btnOKPanelNumber_Click" />--%>
                                        <asp:Button runat="server" ID="UpdateButton" Text="OK" OnClick="UpdateButton_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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
    </div>
    </form>
</body>
</html>

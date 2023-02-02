<%@ page title="" language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.THotel.HotelDetailAdd, App_Web_24wapq3p" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
    
         function Calculate (_prmQtyRoom, _prmBasicFare, _prmTotal, _prmSellPrice, _prmDiscount)
         {
         _prmTotal.value = _prmQtyRoom.value * _prmBasicFare.value;
         _prmSellPrice.value = _prmTotal.value - _prmDiscount.value;
         }
         
         function Calculate2 (_prmDiscount, _prmTotal, _prmSellPrice)
         {
         _prmSellPrice.value = _prmTotal.value - _prmDiscount.value;
         }
    </script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
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
                                Voucher No
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="VoucherNoTextBox" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="VoucherNoRequiredFieldValidator" runat="server" ErrorMessage="Voucher No Must Be Filled"
                                    Text="*" ControlToValidate="VoucherNoTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Guest
                            </td>
                            <td>
                                :
                            </td>
                            <td rowspan="2">
                                <asp:TextBox ID="GuestTextBox" runat="server" TextMode="MultiLine" Width="300" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Hotel
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="HotelTextBox" MaxLength="20" Width="200px" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Button ID="btnSearchHotel" runat="server" Text="..." CausesValidation="False" />
                                <asp:RequiredFieldValidator ID="HotelRequiredFieldValidator" runat="server" ErrorMessage="Hotel Must Be Filled"
                                    Text="*" ControlToValidate="HotelTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="HotelHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Check In
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CheckInDateTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Literal ID="CheckInDateLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Voucher Information
                            </td>
                            <td>
                                :
                            </td>
                            <td rowspan="3">
                                <asp:TextBox ID="VoucherInformationTextBox" runat="server" TextMode="MultiLine" Width="300"
                                    Height="80" MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Check Out
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CheckOutDateTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Literal ID="CheckOutDateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty Room
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QtyRoomTextBox" MaxLength="23" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRoomRequiredFieldValidator" runat="server" ErrorMessage="Qty Room Must Be Filled"
                                    Text="*" ControlToValidate="QtyRoomTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Basic Fare
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BasicFareTextBox" MaxLength="23" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="BasicFareRequiredFieldValidator" runat="server" ErrorMessage="Basic Fare Must Be Filled"
                                    Text="*" ControlToValidate="BasicFareTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TotalTextBox" MaxLength="23" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Discount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DiscountTextBox" runat="server" MaxLength="23" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DiscountRequiredFieldValidator" runat="server" ErrorMessage="Discount Must Be Filled"
                                    Text="*" ControlToValidate="DiscountTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td rowspan="3">
                                <asp:TextBox ID="RemarkTextBox" runat="server" TextMode="MultiLine" Width="300" Height="80"
                                    MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Selling Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SellingPriceTextBox" runat="server" MaxLength="23" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SellingPriceRequiredFieldValidator" runat="server"
                                    ErrorMessage="Selling Price Must Be Filled" Text="*" ControlToValidate="SellingPriceTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Buying Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BuyingPriceTextBox" MaxLength="23" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="BuyingPriceRequiredFieldValidator" runat="server"
                                    ErrorMessage="Buying Price Must Be Filled" Text="*" ControlToValidate="BuyingPriceTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

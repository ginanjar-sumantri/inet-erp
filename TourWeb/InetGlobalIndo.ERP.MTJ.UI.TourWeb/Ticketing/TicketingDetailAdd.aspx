<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="TicketingDetailAdd.aspx.cs" Inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.Ticketing.TicketingDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Calculate(_prmQty, _prmBasic, _prmTotal, _prmSellPrice, _prmDiscount) 
        {

            var _tempQty = _prmQty.value
            if (isNaN(_tempQty) == true) {
                _tempQty = 0
            }

            var _tempBasic = _prmBasic.value
            if (isNaN(_tempBasic) == true) {
                _tempBasic = 0
            }

            _prmTotal.value     = _tempQty * _tempBasic;
            _prmSellPrice.value = _prmTotal.value - _prmDiscount.value;
            
        }
        
           function Calculate2 (_prmDiscount, _prmTotal, _prmSellPrice)
        {
                _prmSellPrice.value = _prmTotal.value - _prmDiscount.value ;
                
        }  

            //            if (isN_tempPPNPercentQty) == true) {
            //                _tempPPNPercent = 0;
            //            }

            //            var _tempQty = _prmQty.value;

            //            if (isNaN(_tempQty) == true) {
            //                _tempQty = 0;
            //            }
            //            _prmQty.value = _tempQty;           
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
                                Booking Code
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="BookingCodeTextBox" runat="server" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="BookingCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Booking Code Must Be Filled" Text="*" ControlToValidate="BookingCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ticket Type
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:RadioButtonList ID="TicketTypeRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Domestic" Value="Domestic"></asp:ListItem>
                                    <asp:ListItem Text="International" Value="International"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Airline
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AirlineTextBox" MaxLength="20" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Button ID="btnSearchAirline" runat="server" Text="..." CausesValidation="False" />
                                <asp:RequiredFieldValidator ID="AirlineRequiredFieldValidator" runat="server" ErrorMessage="Airline Must Be Filled"
                                    Text="*" ControlToValidate="AirlineTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="AirlineHiddenField" runat="server" />
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
                            <td rowspan="3">
                                <asp:TextBox ID="GuestTextBox" runat="server" TextMode="MultiLine" Width="300" Height="80"
                                    MaxLength="100"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="GuestRequiredFieldValidator" runat="server" ErrorMessage="Guest Must Be Filled"
                                    Text="*" ControlToValidate="GuestTextBox" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ticket Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty Guest
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="QtyGuestTextBox" MaxLength="23" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyGuestRequiredFieldValidator" runat="server" ErrorMessage="Qty Guest Must Be Filled"
                                    Text="*" ControlToValidate="QtyGuestTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="TotalTextBox" MaxLength="23" Width="150" BackColor="#CCCCCC"></asp:TextBox>
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
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Flight Information
                            </td>
                            <td>
                                :
                            </td>
                            <td rowspan="3">
                                <asp:TextBox ID="FlightInformationTextBox" runat="server" TextMode="MultiLine" Width="300"
                                    Height="80" MaxLength="500"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="FlightInformationRequiredFieldValidator" runat="server"
                                    ErrorMessage="Flight Information Must Be Filled" Text="*" ControlToValidate="FlightInformationTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
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
                                <asp:RequiredFieldValidator ID="SellingPriceRequiredFieldValidator1" runat="server"
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

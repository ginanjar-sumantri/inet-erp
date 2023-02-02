<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="TicketingDetailView.aspx.cs" Inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.Ticketing.TicketingDetailView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Calculate(_prmQty, _prmBasic, _prmTotal) {

            var _tempQty = _prmQty.value
            if (isNaN(_tempQty) == true) {
                _tempQty = 0
            }

            var _tempBasic = _prmBasic.value
            if (isNaN(_tempBasic) == true) {
                _tempBasic = 0
            }

            _prmTotal.value = _tempQty * _tempBasic;

            //            if (isN_tempPPNPercentQty) == true) {
            //                _tempPPNPercent = 0;
            //            }

            //            var _tempQty = _prmQty.value;

            //            if (isNaN(_tempQty) == true) {
            //                _tempQty = 0;
            //            }
            //            _prmQty.value = _tempQty;            

        }
    </script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="CancelButton">
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
                                <asp:HiddenField ID="CodeHiddenField" runat="server" />
                                <asp:TextBox ID="BookingCodeTextBox" runat="server" MaxLength="20" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox ID="TicketTypeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="AirlineTextBox" MaxLength="20" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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
                                    MaxLength="100" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="DateTextBox" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="QtyGuestTextBox" MaxLength="23" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="QtyGuestRequiredFieldValidator" runat="server" ErrorMessage="Qty Guest Must Be Filled"
                                    Text="*" ControlToValidate="QtyGuestTextBox" Display="Dynamic"></asp:RequiredFieldValidator>--%>
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
                                <asp:TextBox runat="server" ID="BasicFareTextBox" MaxLength="23" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox ID="DiscountTextBox" runat="server" MaxLength="23" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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
                                    Height="80" MaxLength="500" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox ID="SellingPriceTextBox" runat="server" MaxLength="23" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="BuyingPriceTextBox" MaxLength="23" Width="150" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <%--<td>
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>--%>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                           <%-- <td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

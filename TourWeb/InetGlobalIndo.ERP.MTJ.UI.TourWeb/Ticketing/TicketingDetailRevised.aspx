<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="TicketingDetailRevised.aspx.cs" Inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.Ticketing.TicketingDetailRevised" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="YesButton">
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
                    <fieldset>
                        <legend>Revised</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Booking Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:HiddenField ID="CodeHiddenField" runat="server" />
                                    <asp:Label ID="BookingCodeLabel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    Selling Price
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="SellingPriceTextBox" runat="server" MaxLength="23" Width="150"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="SellingPriceRequiredFieldValidator1" runat="server"
                                    ErrorMessage="Selling Price Must Be Filled" Text="*" ControlToValidate="SellingPriceTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr valign="top">
                                <td>
                                    Buying Price
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="BuyingPriceTextBox" MaxLength="23" Width="150"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="BuyingPriceRequiredFieldValidator" runat="server"
                                    ErrorMessage="Buying Price Must Be Filled" Text="*" ControlToValidate="BuyingPriceTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    <asp:HiddenField ID="SellingPriceHiddenField" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <%--<td>
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                            </td>--%>
                            <td>
                                <asp:Button ID="YesButton" runat="server" Text="Yes" OnClick="YesButton_Click" />
                            </td>
                            <td>
                                <asp:Button ID="NoButton" runat="server" Text="No" CausesValidation="False" OnClick="NoButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

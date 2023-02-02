<%@ page title="" language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.THotel.HotelDetailRevised, App_Web_24wapq3p" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                        <legend>Revised Detail</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Voucher No
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:HiddenField ID="CodeHiddenField" runat="server" />
                                    <asp:Label runat="server" ID="VoucherNoLabel"></asp:Label>
                                </td>
                            </tr>
                            <%--     <tr>
                                <td>
                                    Selling Price
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="SellingPriceTextBox" runat="server" MaxLength="23" Width="150"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="SellingPriceRequiredFieldValidator" runat="server"
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
                                    <asp:RequiredFieldValidator ID="BuyingPriceRequiredFieldValidator" runat="server"
                                        ErrorMessage="Buying Price Must Be Filled" Text="*" ControlToValidate="BuyingPriceTextBox"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
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
                            <td>
                                <asp:Button Text="Yes" ID="YesButton" runat="server" OnClick="YesButton_Click" />
                            </td>
                            <td>
                                <asp:Button Text="No" ID="NoButton" runat="server" OnClick="NoButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

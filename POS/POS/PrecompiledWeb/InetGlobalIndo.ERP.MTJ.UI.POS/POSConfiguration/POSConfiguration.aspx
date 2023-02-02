<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.POSConfiguration.POSConfigurationAdd, App_Web_2ru0-bmz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton1">
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
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="IgnoreItemDiscountLabel"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="IgnoreItemDiscountTextBox">
                                        <asp:ListItem Value="1" Text="Yes"> </asp:ListItem>
                                        <asp:ListItem Value="0" Text="No"> </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:ImageButton ID="SaveButton1" runat="server" OnClick="SaveButton_Click1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="POSBookingTimeLimitBeforeLabel"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="POSBookingTimeLimitBeforeTextBox" Width="30" MaxLength="99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="POSBookingTimeLimitBeforeRequiredFieldValidator"
                                        runat="server" ErrorMessage="POS Booking Time Limit Before Must Be Filled" Text="*"
                                        ControlToValidate="POSBookingTimeLimitBeforeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:ImageButton ID="SaveButton3" runat="server" OnClick="SaveButton_Click3" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="POSBookingTimeLimitAfterLabel"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="POSBookingTimeLimitAfterTextBox" Width="30" MaxLength="99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="POSBookingTimeLimitAfterRequiredFieldValidator" runat="server"
                                        ErrorMessage="POS Booking Time Limit After Must Be Filled" Text="*" ControlToValidate="POSBookingTimeLimitAfterTextBox"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:ImageButton ID="SaveButton2" runat="server" OnClick="SaveButton_Click2" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="POSDefaultCustCodeLabel"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="POSDefaultCustCodeValueLabel"></asp:Label>
                                    <%--<asp:RequiredFieldValidator ID="POSDefaultCustCodeRequiredFieldValidator" runat="server"
                                        ErrorMessage="POS Default Customer Code Must Be Filled" Text="*" ControlToValidate="POSDefaultCustCodeTextBox"
                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td>
                                    <asp:ImageButton ID="SaveButton4" runat="server" OnClick="SaveButton_Click4" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="POSRoundingLabel"></asp:Label>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="POSRoundingTextBox" Width="35" MaxLength="99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="POSRoundingRequiredFieldValidator" runat="server"
                                        ErrorMessage="POS Rounding Must Be Filled" Text="*" ControlToValidate="POSRoundingTextBox"
                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:ImageButton ID="SaveButton5" runat="server" OnClick="SaveButton_Click5" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

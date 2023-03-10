<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.RadiusUpdateVoucher.RadiusUpdateVoucherAdd, App_Web_im-wepyp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <asp:Literal ID="JSCaller" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
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
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <input id="DateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                Radius
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="RadiusDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="RadiusCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="RadiusDropDownList" Text="*" ErrorMessage="Radius Must be Chosen"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Expired Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ExpiredDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <input id="ExpiredDateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_ExpiredDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />
                            </td>
                            <td>
                            </td>
                            <td>
                                Batch Series
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SeriesTextBox" runat="server" Width="200" MaxLength="11"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SeriesRequiredFieldValidator" runat="server" ControlToValidate="SeriesTextBox"
                                    Display="Dynamic" ErrorMessage="Series Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Series No From
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SeriesNoFromTextBox" runat="server" Width="120" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SeriesNoFromRequiredFieldValidator" runat="server" ControlToValidate="SeriesNoFromTextBox"
                                    Display="Dynamic" ErrorMessage="Series No From Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                                <asp:Button ID="btnSearchFrom" runat="server" Text="..." />
                            </td>
                            <td>
                            </td>
                            <td>
                                Associated Service
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AssociatedServiceTextBox" runat="server" Width="200" MaxLength="50"></asp:TextBox>                                
                                <asp:RequiredFieldValidator ID="AssociatedServiceRequiredFieldValidator" runat="server" ControlToValidate="AssociatedServiceTextBox"
                                    Display="Dynamic" ErrorMessage="Associated Service Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Series No To
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SeriesNoToTextBox" runat="server" Width="120" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SeriesNoToRequiredFieldValidator" runat="server" ControlToValidate="SeriesNoToTextBox"
                                    Display="Dynamic" ErrorMessage="Series No To Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                                <asp:Button ID="btnSearchTo" runat="server" Text="..." />
                            </td>
                            <td>
                            </td>
                            <td>
                                Expire Time
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ExpireTimeTextBox" runat="server" Width="50" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ExpireTimeRequiredFieldValidator" runat="server" ControlToValidate="ExpireTimeTextBox"
                                    Display="Dynamic" ErrorMessage="Expire Time Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Selling Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SellingAmountTextBox" runat="server" Width="120" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SellingAmountTextBoxRequiredFieldValidator" runat="server" ControlToValidate="SellingAmountTextBox"
                                    Display="Dynamic" ErrorMessage="Selling Amount Must Be Filled" Text="*">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                                Expire Time Unit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ExpireTimeUnitDropDownList" runat="server">
                                    <asp:ListItem Value="0">Hour</asp:ListItem>
                                    <asp:ListItem Value="1">Day</asp:ListItem>
                                    <asp:ListItem Value="2">Month</asp:ListItem>
                                </asp:DropDownList>
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
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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

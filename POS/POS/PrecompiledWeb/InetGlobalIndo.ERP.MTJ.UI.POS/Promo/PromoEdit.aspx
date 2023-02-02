<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.Promo.PromoEdit, App_Web_bxcwfygp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <style type="text/css">
        .width
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
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
                    <asp:UpdatePanel ID="ButtonUpdatePanel" runat="server">
                        <ContentTemplate>
                            <div id="ChooseButton" runat="server">
                                <tr>
                                    <td>
                                        <asp:Button ID="YesButton" Text="Yes" runat="server" OnClick="Yes1Button_Click" />
                                        <asp:Button ID="NoButton" Text="No" runat="server" OnClick="No1Button_Click" />
                                    </td>
                                </tr>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td class="width">
                                Promo Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PromoCodeTextBox" Width="120" MaxLength="99" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                Start Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="StartDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <input type="button" id="Button2" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)" />
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                End Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="EndDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <input type="button" id="Button1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)" />
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                FgMember
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgMemberCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                Payment Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="PaymentTypeDDL" runat="server" OnSelectedIndexChanged="PaymentTypeDDL_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Kredit" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Debit" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Voucher" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Cash" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="CreditCardTypeDDL" runat="server" OnSelectedIndexChanged="CreditCardTypeDDL_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:DropDownList ID="CreditCardDDL" runat="server">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DebitCardDDL" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                Promo Interval Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="ServiceTypeDDLUpdatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="PromoIntervalTypeDropDownList" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="PromoIntervalTypeDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Text="Daily" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Weekly" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Monthly" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table id="MonthlyTable" runat="server" width="100%" visible="false">
                                            <tr>
                                                <td class="width">
                                                    Date
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="DateTextBox" runat="server" MaxLength="2" Width="35px"></asp:TextBox>
                                                    <asp:RangeValidator ControlToValidate="DateTextBox" runat="server" Text="*" ErrorMessage="The Date Must be From 1 to 31"
                                                        MinimumValue="1" MaximumValue="31" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table id="WeeklyTable" runat="server" width="100%" visible="false">
                                            <tr>
                                                <td class="width">
                                                    Monday
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="fgMondayCheckBox" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width">
                                                    Tuesday
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="fgTuesdayCheckBox" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width">
                                                    Wednesday
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="fgWedCheckBox" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width">
                                                    Thursday
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="fgThurCheckBox" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width">
                                                    Friday
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="fgFridayCheckBox" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width">
                                                    Saturday
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="fgSatCheckBox" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width">
                                                    Sunday
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="fgSundayCheckBox" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <table id="DailyTable" runat="server" width="100%" visible="true">
                                            <tr>
                                                <td class="width">
                                                    Start Time
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="StartTimeHourDDL" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="StartTimeMinuteDDL" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width">
                                                    End Time
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="EndTimeHourDDL" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="EndTimeMinuteDDL" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                FgPayment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgPaymentCheckBox" runat="server" AutoPostBack="true"
                                    oncheckedchanged="FgPaymentCheckBox_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                Minimum Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="MinimumPaymentTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                FgActive
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" MaxLength="500"
                                    TextMode="MultiLine"></asp:TextBox>
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

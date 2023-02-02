<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DiscountView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.POS.Discount.DiscountView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <style type="text/css">
        .width
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Discount Config</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td class="width">
                                                Discount Config Code
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="DiscountConfigCodeTextBox" Width="120" MaxLength="99"
                                                    BackColor="#CCCCCC"></asp:TextBox>
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
                                                <%--<input type="button" id="Button2" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)" />--%>
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
                                                <%--<input type="button" id="Button1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="width">
                                                Amount Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="AmountTypeLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="width">
                                                Member Discount
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="fgMemberDiscountCheckBox" runat="server" Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:UpdatePanel ID="DiscountAmountPanel" runat="server">
                                                    <ContentTemplate>
                                                        <table id="DiscountAmountTable" runat="server">
                                                            <tr>
                                                                <td class="width">
                                                                    Discount Amount
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="DiscountAmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="width">
                                                Discount Calc Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="DiscountCalcTypeLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="DetailUpdatePanel" runat="server">
                                                    <ContentTemplate>
                                                        <div id="MinimumPaymentPanel" runat="server">
                                                            <tr>
                                                                <td class="width">
                                                                    Minimum Payment
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="MinimumPaymentTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Force Print On Demand
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ForcePrintOnDemandCheckBox" runat="server" Enabled="false" />
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
                                                <asp:TextBox ID="PaymentTypeTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox ID="DebitCardTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox ID="CreditCardTypeTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox ID="CreditCardTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="width">
                                                Discount Interval Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DiscountIntervalTypeTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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
                                                                    <asp:TextBox ID="DateTextBox" runat="server" BackColor="#CCCCCC" Width="35px"></asp:TextBox>
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
                                                                    <asp:CheckBox ID="fgMondayCheckBox" runat="server" Enabled="false" />
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
                                                                    <asp:CheckBox ID="fgTuesdayCheckBox" runat="server" Enabled="false" />
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
                                                                    <asp:CheckBox ID="fgWedCheckBox" runat="server" Enabled="false" />
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
                                                                    <asp:CheckBox ID="fgThurCheckBox" runat="server" Enabled="false" />
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
                                                                    <asp:CheckBox ID="fgFridayCheckBox" runat="server" Enabled="false" />
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
                                                                    <asp:CheckBox ID="fgSatCheckBox" runat="server" Enabled="false" />
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
                                                                    <asp:CheckBox ID="fgSundayCheckBox" runat="server" Enabled="false" />
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
                                                                    <asp:TextBox runat="server" ID="StartTimeHourTextBox" Width="25" BackColor="#CCCCCC"></asp:TextBox>
                                                                    &nbsp : &nbsp
                                                                    <asp:TextBox runat="server" ID="StartTimeMinuteTextBox" Width="25" BackColor="#CCCCCC"></asp:TextBox>
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
                                                                    <asp:TextBox runat="server" ID="EndTimeHourTextBox" Width="25" BackColor="#CCCCCC"></asp:TextBox>
                                                                    &nbsp : &nbsp
                                                                    <asp:TextBox runat="server" ID="EndTimeMinuteTextBox" Width="25" BackColor="#CCCCCC"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="width">
                                                Status
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                                <asp:HiddenField runat="server" ID="StatusHiddenField" />
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
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Discount Member</legend>
                        <table cellpadding="3" cellspacing="0" border="0" width="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                                &nbsp;
                                                <asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <asp:HiddenField ID="DescriptionHiddenField" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Discount Config Code</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Member Type</b>
                                            </td>
                                            <td style="width: 180px" class="tahoma_11_white" align="center">
                                                <b>Discount Amount</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <%--<td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>--%>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="DiscountConfigCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="MemberTypeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="DiscountAmountLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Discount Product</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="AddButton2" runat="server" OnClick="AddButton2_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="WarningLabel2" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden2" runat="server" />
                                    <asp:HiddenField ID="TempHidden2" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox2" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <%--<td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>--%>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Discount Config Code</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Product Code</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Product Name</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Selling Price</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Unit</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater2" OnItemDataBound="ListRepeater2_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox2" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral2"></asp:Literal>
                                                    </td>
                                                    <%--<td align="left">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton2" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton2" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>--%>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="DiscountConfigCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="SellingPriceLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="UnitLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.Notification.NotifyPembayaran, App_Web_p4-ylffz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
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
                                <asp:UpdatePanel ID="WarningPanel" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="0" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100px">
                                                        Trans Date
                                                    </td>
                                                    <td style="width: 10px">
                                                        :
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                        <input id="DateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                                            value="..." />
                                                    </td>
                                                    <td>
                                                        Customer Name
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CustNameTextBox" Width="150"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Year
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="YearTextBox" runat="server" Width="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                                                            ControlToValidate="YearTextBox" ErrorMessage="Year Must Be Filled"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        Customer Type
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="CustTypeDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Period
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="PeriodTextBox" runat="server" Width="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*"
                                                            ControlToValidate="PeriodTextBox" ErrorMessage="Period Must Be Filled"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="SearchImageButton" runat="server" OnClick="SearchImageButton_Click" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="right">
                                                                    <asp:UpdatePanel runat="server" ID="PagingUpdatePanel">
                                                                        <ContentTemplate>
                                                                            <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                                                <table border="0" cellpadding="2" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Button ID="DataPagerButton" runat="server" OnClick="DataPagerButton_Click" />
                                                                                        </td>
                                                                                        <td valign="middle">
                                                                                            <b>Page :</b>
                                                                                        </td>
                                                                                        <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                                                            OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                                                                    <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                                                                </td>
                                                                                            </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel runat="server" ID="ListUpdatePanel">
                                                <ContentTemplate>
                                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                                    <asp:HiddenField ID="AllHidden" runat="server" />
                                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                        <tr class="bgcolor_gray">
                                                            <td style="width: 5px">
                                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                                            </td>
                                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                                <b>No.</b>
                                                            </td>
                                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                                <b>Cust Code</b>
                                                            </td>
                                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                                <b>Customer</b>
                                                            </td>
                                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                                <b>Cust Type</b>
                                                            </td>
                                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                                <b>Cust Bill Account</b>
                                                            </td>
                                                            <td style="width: 130px" class="tahoma_11_white" align="center">
                                                                <b>Contact Name</b>
                                                            </td>
                                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                                <b>Customer Email</b>
                                                            </td>
                                                            <td style="width: 250px" class="tahoma_11_white" align="center">
                                                                <b>Cust Bill Desc</b>
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr id="RepeaterItemTemplate" runat="server">
                                                                    <td align="center">
                                                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Literal ID="NoLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="CustCodeLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="CustNameLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="CustTypeLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="CustBillAccountLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="ContactNameLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="EmailLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="CustBillDescriptionLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr class="bgcolor_gray">
                                                            <td style="width: 1px" colspan="9">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="9">
                                                                <asp:CheckBox ID="GrapAllCheckBox" runat="server" Text="Grab data from all pages." />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="9">
                                                                <asp:ImageButton ID="SendEmailImageButton" runat="server" OnClick="SendEmailImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel runat="server" ID="TriggerUpdatePanel">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="SearchImageButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="SendEmailImageButton" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.Notification.NotifyBilInvNYSend, App_Web_p4-ylffz" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SearchImageButton">
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
                                <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="0" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="80px">
                                                        Year
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td width="80px">
                                                        <asp:TextBox ID="YearTextBox" runat="server" Width="50"></asp:TextBox>
                                                    </td>
                                                    <td width="120px">
                                                        Invoice No
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="InvoiceNoTextBox" Width="150"></asp:TextBox>
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
                                                    </td>
                                                    <td>
                                                        Customer Name
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="CustNameTextBox" Width="150"></asp:TextBox>&nbsp;
                                                        <asp:ImageButton ID="SearchImageButton" runat="server" OnClick="SearchImageButton_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="right">
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
                                                    <td style="width: 120px" class="tahoma_11_white" align="center">
                                                        <b>Invoice No</b>
                                                    </td>
                                                    <td style="width: 80px" class="tahoma_11_white" align="center">
                                                        <b>Period</b>
                                                    </td>
                                                    <td style="width: 80px" class="tahoma_11_white" align="center">
                                                        <b>Year</b>
                                                    </td>
                                                    <td style="width: 150px" class="tahoma_11_white" align="center">
                                                        <b>Customer Name</b>
                                                    </td>
                                                    <td style="width: 150px" class="tahoma_11_white" align="center">
                                                        <b>Customer Email</b>
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
                                                                <asp:Literal ID="InvoiceNoLiteral" runat="server"></asp:Literal>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Literal ID="PeriodLiteral" runat="server"></asp:Literal>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Literal ID="YearLiteral" runat="server"></asp:Literal>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Literal ID="CustNameLiteral" runat="server"></asp:Literal>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Literal ID="EmailLiteral" runat="server"></asp:Literal>
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
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel2">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="440">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PettyCashPrintPreview.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCash.PettyCashPrintPreview" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="GoButton">
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
                    <asp:Panel runat="server" ID="MenuPanel" DefaultButton="GoButton">
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="3">
                                                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                    <tr>
                                                        <td>
                                                            Begin Date
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="BeginDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <%--<input id="Button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_BeginDateTextBox,'yyyy-mm-dd',this)"
                                                                value="..." />--%>
                                                                <asp:Literal ID="BeginDateLiteral" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            End Date
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="EndDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <%--<input id="Button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_BeginDateTextBox,'yyyy-mm-dd',this)"
                                                                value="..." />--%>
                                                                <asp:Literal ID="EndDateLiteral" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table cellpadding="3" cellspacing="0" border="0" width="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="GoButton" runat="server" OnClick="GoButton_Click" />
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
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                                                        </td>
                                                        <td align="right" width="100%">
                                                            <table border="0" cellpadding="2" cellspacing="0" width="0">
                                                                <tr>
                                                                    <td>
                                                                        Page :
                                                                    </td>
                                                                    <asp:Repeater ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerRepeater_ItemCommand"
                                                                        OnItemDataBound="DataPagerRepeater_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <td>
                                                                                <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                                                <asp:Label ID="PageNumberLabel" runat="server"></asp:Label>
                                                                            </td>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField runat="server" ID="DescriptionHiddenField" />
                                                <asp:HiddenField ID="CheckHidden" runat="server" />
                                                <asp:HiddenField ID="TempHidden" runat="server" />
                                                <table cellpadding="3" cellspacing="1" width="0" border="0">
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
                                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                                            <b>Trans No.</b>
                                                        </td>
                                                        <td style="width: 80px" class="tahoma_11_white" align="center">
                                                            <b>Date</b>
                                                        </td>
                                                        <td style="width: 50px" class="tahoma_11_white" align="center">
                                                            <b>Status</b>
                                                        </td>
                                                        <td style="width: 200px" class="tahoma_11_white" align="center">
                                                            <b>Remark</b>
                                                        </td>
                                                        <td style="width: 80px" class="tahoma_11_white" align="center">
                                                            <b>Pay To</b>
                                                        </td>
                                                        <td style="width: 50px" class="tahoma_11_white" align="center">
                                                            <b>Req Print</b>
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
                                                                            <td>
                                                                                <asp:ImageButton runat="server" ID="ViewButton" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Literal runat="server" ID="TransactionNoLiteral"></asp:Literal>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Literal runat="server" ID="TransactionDateLiteral"></asp:Literal>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Literal runat="server" ID="PayToLiteral"></asp:Literal>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Literal runat="server" ID="ReqPrintLiteral"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="PreviewButton2" runat="server" OnClick="PreviewButton_Click" />
                                                        </td>
                                                        <td align="right" width="100%">
                                                            <table border="0" cellpadding="2" cellspacing="0" width="0">
                                                                <tr>
                                                                    <td>
                                                                        Page :
                                                                    </td>
                                                                    <asp:Repeater ID="DataPagerBottomRepeater" runat="server" OnItemCommand="DataPagerRepeater_ItemCommand"
                                                                        OnItemDataBound="DataPagerRepeater_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <td>
                                                                                <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                                                <asp:Label ID="PageNumberLabel" runat="server"></asp:Label>
                                                                            </td>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
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
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td>
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="1000px">
                                </rsweb:ReportViewer>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

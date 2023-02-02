<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.Report.CashFlowCashSummary, App_Web_ixfvgxv7" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td class="warning">
                <asp:Literal ID="Literal1" runat="server" Text="* This Report is Best Printed on Legal Size Paper" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="MenuPanel">
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                    <tr>
                                        <td>
                                            Start Date
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="StartDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                            &nbsp;
                                            <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                                value="..." />--%>
                                             <asp:Literal ID="StartDateLiteral" runat="server"></asp:Literal>   
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            End Date
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EndDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                            &nbsp;
                                            <%--<input id="button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)"
                                                value="..." />--%>
                                             <asp:Literal ID="EndDateLiteral" runat="server"></asp:Literal>   
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Filter Account
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="FilterDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterDropDownList_SelectedIndexChanged">
                                                <asp:ListItem Text="By Range" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="By Check Selection" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="RangePanel" runat="server">
                                        <tr>
                                            <td>
                                                Account From
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="FromTextBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                To
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ToTextBox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="SelectionPanel" runat="server">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td colspan="5" align="right">
                                                <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <table>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:CheckBox runat="server" ID="AllCheckBox" Text="Check All" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:HiddenField ID="CheckHidden" runat="server" />
                                                                            <asp:HiddenField ID="TempHidden" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="DataPagerButton" runat="server" CausesValidation="false" OnClick="DataPagerButton_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <b>Page :</b>
                                                                        </td>
                                                                        <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                                            OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <td>
                                                                                    <asp:LinkButton ID="PageNumberLinkButton" runat="server" CausesValidation="false"></asp:LinkButton>
                                                                                    <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                                                </td>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
                                                    <table cellpadding="0" cellspacing="2" border="0">
                                                        <tr>
                                                            <td>
                                                                <b>Quick Search :</b>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                                    <asp:ListItem Value="Code" Text="Account Code"></asp:ListItem>
                                                                    <asp:ListItem Value="Name" Text="Account Name"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click"
                                                                    CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                Account
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td colspan="5">
                                                <asp:CheckBoxList ID="AccountCheckBoxList" RepeatColumns="2" runat="server" RepeatDirection="Vertical">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td colspan="7">
                                            <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ViewButton" runat="server" OnClick="ViewButton_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="false" OnClick="ResetButton_Click" />
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
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ShowPrintButton="true"
                        ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProFormaInvPerCust.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Report.ProFormaInvPerCust" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../HeaderReportList.ascx" TagName="HeaderReportList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="ViewButton" runat="server">
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
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td width="135px">
                                                Report
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td colspan="7">
                                                <div>
                                                    <uc1:HeaderReportList ID="HeaderReportList1" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="FgReportDropDownList">
                                                    <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                    <asp:ListItem Value="0">Summary</asp:ListItem>
                                                    <asp:ListItem Value="1">Detail</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Type Must Be Choosed"
                                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FgReportDropDownList"></asp:CustomValidator>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                Date Range
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td colspan="5">
                                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="StartDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <%--<input id="date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                                                value="..." />--%>
                                                            <asp:Literal ID="CalendarScriptLiteral" runat="server"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            to
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="EndDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <%--<input id="date_end" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)"
                                                                value="..." />--%>
                                                            <asp:Literal ID="CalendarScriptLiteral2" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Date Range
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td colspan="5">
                                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="TextBox1" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <%--<input id="Button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                                                value="..." />--%>
                                                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                                        </td>
                                                        <td>
                                                            to
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox2" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                            <%--<input id="Button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)"
                                                                value="..." />--%>
                                                            <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Group by
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="GroupByDropDownList" runat="server">
                                                    <asp:ListItem Text="Forex Currency" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Default Currency" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                By
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="SelectionDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectionDropDownList_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Text="By Range"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="By Check"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="RangePanel" runat="server">
                                            <tr>
                                                <td>
                                                    Customer Code From
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CustCodeFromTextBox" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    To
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CustCodeToTextBox" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="SelectionPanel" runat="server" Visible="false">
                                            <tr>
                                                <td colspan="8">
                                                    <fieldset>
                                                        <legend>Customer</legend>
                                                        <table>
                                                            <tr>
                                                                <td valign="top">
                                                                    Customer Group Type
                                                                </td>
                                                                <td valign="top">
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="CustGroupTypeDropDownList">
                                                                        <asp:ListItem Text="LOKAL" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="EXPORT" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="ALL" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Customer Group
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="CustGroupDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CustGroupDropDownList_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Customer Type
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="CustTypeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CustTypeDropDownList_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Panel ID="Panel5" DefaultButton="GoImageButton" runat="server">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                                                        <asp:ListItem Value="Code" Text="Customer Code"></asp:ListItem>
                                                                                        <asp:ListItem Value="Name" Text="Customer Name"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="KeywordTextBox" runat="server" Width="100"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:ImageButton ID="GoImageButton" runat="server" CausesValidation="false" OnClick="GoImageButton_Click" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                                <asp:CheckBox runat="server" ID="AllCheckBox" Text="Check All" />
                                                                                                <asp:CheckBox runat="server" ID="GrabAllCheckBox" Text="Grab All" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:HiddenField ID="CheckHidden" runat="server" />
                                                                                                <asp:HiddenField ID="TempHidden" runat="server" />
                                                                                                <asp:HiddenField ID="AllHidden" runat="server" />
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
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td class="note">
                                                                    * Use Check All to select all checkbox visible on this page<br />
                                                                    * Use Grab All to get all data regarding filter selected
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    Customer
                                                                </td>
                                                                <td valign="top">
                                                                    :
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBoxList runat="server" ID="CustCodeCheckBoxList" RepeatColumns="3">
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td colspan="3">
                                                <table cellpadding="3" cellspacing="0" width="0" border="0">
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
                    <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" ShowPrintButton="true"
                        ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

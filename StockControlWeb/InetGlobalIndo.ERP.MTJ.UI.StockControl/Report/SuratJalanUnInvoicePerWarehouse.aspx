<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SuratJalanUnInvoicePerWarehouse.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Report.SuratJalanUnInvoicePerWarehouse" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Src="../HeaderReportList.ascx" TagName="HeaderReportList" TagPrefix="uc1" %>    
    
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
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
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
                                            Date Range
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="StartDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                        <%--<input id="date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                                            value="..." />--%>
                                                            <asp:Literal ID="CalendarScriptLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Report Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <div>
                                                <uc1:HeaderReportList ID="HeaderReportList1" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td>
                                            Report Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ReportTypeDropDownList">
                                                <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                <asp:ListItem Value="0">Summary Per Transaction </asp:ListItem>
                                                <asp:ListItem Value="0">Summary Per Product </asp:ListItem>
                                                <asp:ListItem Value="1">Detail Per Transaction</asp:ListItem>
                                                <asp:ListItem Value="2">Detail Per  Product</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="ReportTypeCustomValidator" runat="server" ErrorMessage="Report Type By Must Be Choosed"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ReportTypeDropDownList"></asp:CustomValidator>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            Report Status
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ReportStatusDropDownList">
                                                <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                <asp:ListItem Value="0">Price HPP</asp:ListItem>
                                                <asp:ListItem Value="0">Price SO</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="ReportStatusCustomValidator" runat="server" ErrorMessage="Report Type By Must Be Choosed"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ReportStatusDropDownList"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Filter Product
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="FilterDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterDropDownList_SelectedIndexChanged">
                                                <asp:ListItem Text="By Range" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="By Check Selection" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="SelectionPanel" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="3">
                                                <fieldset>
                                                    <legend>Product</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Product Group
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ProductGroupDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductGroupDDL_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Product Sub Group
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ProductSubGroupDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductSubGroupDDL_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Product Type
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ProductTypeDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ProductTypeDDL_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                                                    <asp:ListItem Value="Code" Text="Product Code"></asp:ListItem>
                                                                                    <asp:ListItem Value="Name" Text="Product Name"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="SearchImageButton" runat="server" CausesValidation="false" OnClick="SearchImageButton_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
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
                                                                Product
                                                            </td>
                                                            <td valign="top">
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList runat="server" ID="ProductCodeCheckBoxList" RepeatColumns="3">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="RangePanel" runat="server">
                                        <tr>
                                            <td colspan="3">
                                                <fieldset>
                                                    <legend>Product</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Product From
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td colspan="7">
                                                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="ProductFromTextBox" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            To
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="ProductToTextBox" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td colspan="3">
                                            <fieldset>
                                                <legend>Warehouse</legend>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Warehouse Group
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="WrhsGroupDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="WrhsGroupDropDownList_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Warehouse Area
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="WrhsAreaDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="WrhsAreaDropDownList_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Panel DefaultButton="DataPagerButton2" ID="DataPagerPanel2" runat="server">
                                                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <asp:CheckBox runat="server" ID="AllCheckBox2" Text="Check All" />
                                                                                        <asp:CheckBox runat="server" ID="GrabAllCheckBox2" Text="Grab All" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:HiddenField ID="CheckHidden2" runat="server" />
                                                                                        <asp:HiddenField ID="TempHidden2" runat="server" />
                                                                                        <asp:HiddenField ID="AllHidden2" runat="server" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="DataPagerButton2" runat="server" CausesValidation="false" OnClick="DataPagerButton2_Click" />
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
                                                                                    <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater2" runat="server" OnItemCommand="DataPagerTopRepeater2_ItemCommand"
                                                                                        OnItemDataBound="DataPagerTopRepeater2_ItemDataBound">
                                                                                        <ItemTemplate>
                                                                                            <td>
                                                                                                <asp:LinkButton ID="PageNumberLinkButton2" runat="server" CausesValidation="false"></asp:LinkButton>
                                                                                                <asp:TextBox Visible="false" ID="PageNumberTextBox2" runat="server" Width="30px"></asp:TextBox>
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
                                                    <tr valign="top">
                                                        <td>
                                                            Warehouse
                                                        </td>
                                                        <td>
                                                            :
                                                        </td>
                                                        <td>
                                                            <asp:CheckBoxList ID="WarehouseCheckBoxList" RepeatColumns="2" runat="server" RepeatDirection="Vertical">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
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

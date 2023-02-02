<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SuratJalanSummaryPerCustomer.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Report.SuratJalanSummaryPerCustomer" %>

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
                                            Filter Report
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="FilterReportRadioButtonList1" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="FilterReportRadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="By Month" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="By Year" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
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
                                    <tr>
                                        <td>
                                            Start Period
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="StartPeriodTextBox" runat="server" Width="50" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="StartPeriodRequiredFieldValidator" runat="server"
                                                ErrorMessage="Start Period Must Be Filled" Text="*" ControlToValidate="StartPeriodTextBox"
                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                            &nbsp; Year
                                            <asp:TextBox runat="server" ID="StartYearTextBox" Width="50" MaxLength="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="StartYearRequiredFieldValidator" runat="server" ErrorMessage="Start Year Must Be Filled"
                                                Text="*" ControlToValidate="StartYearTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            End Period
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EndPeriodTextBox" runat="server" Width="50" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EndPeriodRequiredFieldValidator" runat="server" ErrorMessage="End Period Must Be Filled"
                                                Text="*" ControlToValidate="EndPeriodTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                            &nbsp; Year
                                            <asp:TextBox runat="server" ID="EndYearTextBox" Width="50" MaxLength="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EndYearRequiredFieldValidator" runat="server" ErrorMessage="End Year Must Be Filled"
                                                Text="*" ControlToValidate="EndYearTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                                <asp:ListItem Value="0">Summary By Customer </asp:ListItem>
                                                <asp:ListItem Value="1">Detail by Customer</asp:ListItem>
                                                <asp:ListItem Value="2">Detail By Product, Customer</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="ReportTypeCustomValidator" runat="server" ErrorMessage="Report Type By Must Be Choosed"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ReportTypeDropDownList"></asp:CustomValidator>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            By
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="SelectionDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectionDropDownList_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="By Range"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="By Check"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="RangePanel" runat="server">
                                        <tr>
                                            <td colspan="3">
                                                <fieldset>
                                                    <legend>Customer</legend>
                                                    <table>
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
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <fieldset>
                                                    <legend>Product</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Product Sub Group From
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ProductSubGroupFromTextBox" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                To
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ProductSubGroupToTextBox" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="SelectionPanel" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="9">
                                                <fieldset>
                                                    <legend>Customer</legend>
                                                    <table>
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
                                                            <td align="left">
                                                                <asp:Panel ID="Panel3" DefaultButton="GoImageButton2" runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="CategoryDropDownList2" runat="server">
                                                                                    <asp:ListItem Value="Code" Text="Customer Code"></asp:ListItem>
                                                                                    <asp:ListItem Value="Name" Text="Customer Name"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="KeywordTextBox2" runat="server" Width="100"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="GoImageButton2" runat="server" CausesValidation="false" OnClick="GoImageButton2_Click" />
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
                                                                <asp:Panel DefaultButton="DataPagerButton2" ID="DataPagerPanel" runat="server">
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
                                                <fieldset>
                                                    <legend>Product Sub Group</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Panel ID="Panel1" DefaultButton="GoImageButton1" runat="server">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="CategoryDropDownList1" runat="server">
                                                                                    <asp:ListItem Value="Code" Text="Product Code"></asp:ListItem>
                                                                                    <asp:ListItem Value="Name" Text="Product Name"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="KeywordTextBox1" runat="server" Width="100"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="GoImageButton1" runat="server" CausesValidation="false" OnClick="GoImageButton1_Click" />
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
                                                            <td colspan="5" align="left">
                                                                <asp:Panel DefaultButton="DataPagerButton1" ID="Panel2" runat="server">
                                                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <asp:CheckBox runat="server" ID="AllCheckBox1" Text="Check All" />
                                                                                            <asp:CheckBox runat="server" ID="GrabAllCheckBox1" Text="Grab All" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:HiddenField ID="CheckHidden1" runat="server" />
                                                                                            <asp:HiddenField ID="TempHidden1" runat="server" />
                                                                                            <asp:HiddenField ID="AllHidden1" runat="server" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Button ID="DataPagerButton1" runat="server" CausesValidation="false" OnClick="DataPagerButton1_Click" />
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
                                                                                        <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater1" runat="server" OnItemCommand="DataPagerTopRepeater1_ItemCommand"
                                                                                            OnItemDataBound="DataPagerTopRepeater1_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="PageNumberLinkButton1" runat="server" CausesValidation="false"></asp:LinkButton>
                                                                                                    <asp:TextBox Visible="false" ID="PageNumberTextBox1" runat="server" Width="30px"></asp:TextBox>
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
                                                                Product Sub Group
                                                            </td>
                                                            <td valign="top">
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList runat="server" ID="ProductSubGroupCheckBoxList" RepeatColumns="3">
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

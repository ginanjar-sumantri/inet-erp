<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.Report.PurchaseOrderPerProduct, App_Web_3cmzf388" %>

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
                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td colspan="3">
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="125px">
                                    Type
                                </td>
                                <td width="5px">
                                    :
                                </td>
                                <td>
                                    <uc1:HeaderReportList ID="HeaderReportList1" runat="server" />
                                </td>
                                <%--<td>
                                    <asp:DropDownList ID="FgTypeDropDownList" runat="server">
                                        <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                        <asp:ListItem Text="Summary Per Product" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Summary Per Product and Supplier" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Detail Per Product and Supplier" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Summary" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Type Must Be Choosed"
                                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FgTypeDropDownList"></asp:CustomValidator>
                                </td>--%>
                            </tr>
                            <tr>
                                <td>
                                    Date Range
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="StartDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                    <%--<input id="date_start" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                        value="..." />--%>
                                    <asp:Literal ID="StartDateLiteral" runat="server"></asp:Literal>
                                    &nbsp;to&nbsp;
                                    <asp:TextBox ID="EndDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                    <%--<input id="date_end" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)"
                                        value="..." />--%>
                                    <asp:Literal ID="EndDateLiteral" runat="server"></asp:Literal>
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
                                    <asp:DropDownList ID="SelectDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SelectDropDownList_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Range"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Chack"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <asp:Panel ID="RangePanel" runat="server">
                                <tr>
                                    <td>
                                        Supplier Code From
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SuppCodeFromTextBox" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp; To &nbsp;&nbsp; : &nbsp;&nbsp;
                                        <asp:TextBox ID="SuppCodeToTextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Product Code From
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ProductFromTextBox" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp; To &nbsp;&nbsp; : &nbsp;&nbsp;
                                        <asp:TextBox ID="ProductToTextBox" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="SelectionPanel" runat="server" Visible="false">
                                <tr>
                                    <td colspan="3">
                                        <fieldset>
                                            <legend>Filter Criteria by Supplier</legend>
                                            <table>
                                                <tr>
                                                    <td width="112px">
                                                        Supplier Group
                                                    </td>
                                                    <td width="5px">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="SuppGroupDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SuppGroupDropDownList_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Supplier Type
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="SuppTypeDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SuppTypeDropDownList_SelectedIndexChanged">
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
                                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox runat="server" ID="AllCheckBox" Text="Check All" />
                                                                        <asp:CheckBox runat="server" ID="GrabAllCheckBox" Text="Grab All" />
                                                                        <asp:HiddenField ID="CheckHidden" runat="server" />
                                                                        <asp:HiddenField ID="TempHidden" runat="server" />
                                                                        <asp:HiddenField ID="AllHidden" runat="server" />
                                                                        <asp:Button ID="DataPagerButton" runat="server" CausesValidation="false" OnClick="DataPagerButton_Click" />
                                                                    </td>
                                                                    <td valign="middle" align="right">
                                                                        <b>Page :</b>
                                                                        <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                                            OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <td>
                                                                                    <asp:LinkButton ID="PageNumberLinkButton" runat="server" CausesValidation="false"></asp:LinkButton>
                                                                                    <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                                                </td>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
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
                                                        Supplier
                                                    </td>
                                                    <td valign="top">
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList runat="server" ID="SuppCodeCheckBoxList" RepeatColumns="3">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">
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
                                                    <td align ="left">
                                                        <asp:Panel DefaultButton="DataPagerButton" ID="Panel2" runat="server">
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
                                                                        <asp:ImageButton ID="SearchImageButton" runat="server" CausesValidation="false" OnClick="SearchImageButton1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        <table>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:CheckBox runat="server" ID="AllCheckBox1" Text="Check All" />
                                                                                    <asp:CheckBox runat="server" ID="GrabAllCheckBox1" Text="Grab All" />
                                                                                    <asp:HiddenField ID="CheckHidden1" runat="server" />
                                                                                    <asp:HiddenField ID="TempHidden1" runat="server" />
                                                                                    <asp:HiddenField ID="AllHidden1" runat="server" />
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

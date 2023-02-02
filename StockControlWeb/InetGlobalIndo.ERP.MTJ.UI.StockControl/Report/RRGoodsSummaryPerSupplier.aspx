<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RRGoodsSummaryPerSupplier.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Report.RRGoodsSummaryPerSupplier" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../HeaderReportList.ascx" TagName="HeaderReportList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function ValidatePeriod(_prmPeriod) {
            var _tempPeriod = parseInt(_prmPeriod.value);
            if (isNaN(_tempPeriod) == true) {
                _tempPeriod = 0;
            }

            if (_tempPeriod < 1 || _tempPeriod > 12) {
                _prmPeriod.value = "";
            }
        }

        function ValidateYear(_prmYear) {
            var _tempYear = parseInt(_prmYear.value);
            if (isNaN(_tempYear) == true) {
                _tempYear = 0;
            }

            if (_tempYear < 1000 || _tempYear > 9999) {
                _prmYear.value = "";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                                        <td>
                                            Report Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <uc1:HeaderReportList ID="HeaderReportList1" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
                                            <asp:DropDownList ID="ReportTypeDropDownList" runat="server">
                                                <asp:ListItem Text="Per Supplier" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Per Supplier - Product" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Per Product - Supplier" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>--%>
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
                                    <tr>
                                        <td>
                                            Filter Supplier
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
                                                    <legend>Supplier</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Supplier Group
                                                            </td>
                                                            <td>
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
                                                            <td colspan="5" align="right">
                                                                <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                                                                <asp:ListItem Value="Code" Text="Supplier Code"></asp:ListItem>
                                                                                                <asp:ListItem Value="Name" Text="Supplier Name"></asp:ListItem>
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
                                    </asp:Panel>
                                    <asp:Panel ID="RangePanel" runat="server">
                                        <tr>
                                            <td colspan="3">
                                                <fieldset>
                                                    <legend>Supplier</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Supplier From
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td colspan="7">
                                                                <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="SupplierFromTextBox" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            To
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="SupplierToTextBox" runat="server"></asp:TextBox>
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
                                                <legend>Product</legend>
                                                <table>
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
                                                    <tr>
                                                        <td colspan="4">
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
                                            </fieldset>
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

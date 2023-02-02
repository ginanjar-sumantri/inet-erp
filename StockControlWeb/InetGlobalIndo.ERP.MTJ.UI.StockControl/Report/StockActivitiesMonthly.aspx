<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockActivitiesMonthly.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Report.StockActivitiesMonthly" %>

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
                                            Type
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
                                            Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="FgReportDropDownList" runat="server">
                                                <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                <asp:ListItem Text="Product" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Product Group" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Product Sub Group" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Product Type" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Type Must Be Choosed"
                                                Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FgReportDropDownList"></asp:CustomValidator>
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
                                        <td valign="top">
                                            Report Type
                                        </td>
                                        <td valign="top">
                                            :
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="FgQtyRadioButtonList" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="FgQtyRadioButtonList_SelectedIndexChanged">
                                                <asp:ListItem Text="Qty" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="Amount" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Divide By
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="FgDivideDropDownList" runat="server">
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                <asp:ListItem Text="1,000" Value="1000"></asp:ListItem>
                                                <asp:ListItem Text="10,000" Value="10000"></asp:ListItem>
                                                <asp:ListItem Text="100,000" Value="100000"></asp:ListItem>
                                                <asp:ListItem Text="1,000,000" Value="1000000"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Print
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="WrhsCheckBox" runat="server" Text="Warehouse" />
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
                                                            <td valign="top">
                                                                Product Sub Group
                                                            </td>
                                                            <td valign="top">
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList runat="server" ID="ProductSubGrpCheckBoxList" RepeatColumns="3">
                                                                </asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                Product Type
                                                            </td>
                                                            <td valign="top">
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList runat="server" ID="ProductTypeCheckBoxList" RepeatColumns="3">
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

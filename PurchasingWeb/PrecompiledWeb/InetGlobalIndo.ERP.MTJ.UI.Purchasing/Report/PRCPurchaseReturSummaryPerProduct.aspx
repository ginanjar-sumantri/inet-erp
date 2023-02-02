<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.Report.PRCPurchaseReturSummaryPerProduct, App_Web__mankk1m" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                            <td width="125px">
                                Type
                            </td>
                            <td width="5px">
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
                            <td colspan="3">
                                <fieldset>
                                    <legend>Filter Criteria by Product Sub Group</legend>
                                    <table>
                                        <tr>
                                            <td width="112px" valign="top">
                                                Product Sub Group
                                            </td>
                                            <td width="5px" valign="top">
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBoxList runat="server" ID="ProductSubGrpCheckBoxList" RepeatColumns="3">
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <fieldset>
                                    <legend>Filter Criteria by Product Type</legend>
                                    <table>
                                        <tr>
                                            <td width="112px" valign="top">
                                                Product Type
                                            </td>
                                            <td width="5px" valign="top">
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

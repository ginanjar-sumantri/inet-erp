<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.Report.CustomerBillingAccountReport, App_Web_af1gntuk" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function ValidatePeriod(_prmPeriod) {
            var _tempPeriod = _prmPeriod.value;
            if (parseInt(_tempPeriod) < 1 || parseInt(_tempPeriod) > 12) {
                _prmPeriod.value = "";
            }
        }

        function ValidateYear(_prmYear) {
            var _tempYear = _prmYear.value;
            if (parseInt(_tempYear) < 1 || parseInt(_tempYear) > 9999) {
                _prmYear.value = "";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="PreviewButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
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
                            <td valign="top">
                                Type
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="TypeDDL" runat="server">
                                    <asp:ListItem Value="0">Without Price</asp:ListItem>
                                    <asp:ListItem Value="1">With Price</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="TypeCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="TypeDDL" ErrorMessage="Type Must Be Choosed" Text="*"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Customer Group
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:CheckBoxList ID="CustGroupCheckBoxList" runat="server" RepeatColumns="3">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Customer Type
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:CheckBoxList ID="CustTypeCheckBoxList" runat="server" RepeatColumns="3">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
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
                    <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" Height="1550px" ShowPrintButton="true"
                        ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

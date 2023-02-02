<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Report.TransactionJournal, App_Web_o0azb2_0" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="scriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
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
                                            <td>
                                                Year
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="YearTextBox" runat="server" Width="50" MaxLength="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="YearRequiredFieldValidator" runat="server" ControlToValidate="YearTextBox"
                                                    ErrorMessage="Year Must be Filled" Text="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Period
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="PeriodTextBox" Width="50" MaxLength="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PeriodRequiredFieldValidator" runat="server" ControlToValidate="PeriodTextBox"
                                                    ErrorMessage="Period Must be Filled" Text="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Transaction Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="TransTypeDDL" runat="server">
                                                </asp:DropDownList>
                                               <%-- <asp:CustomValidator ID="TransTypeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                                    Text="*" ControlToValidate="TransTypeDDL" ErrorMessage="Transaction Type Must Be Choosed"></asp:CustomValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:RadioButtonList ID="FgTypeRadioBtnList" runat="server" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="FgTypeRadioBtnList_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="TransNmbr" Value="TransNmbr">Trans No.</asp:ListItem>
                                                    <asp:ListItem Text="FileNmbr" Value="FileNmbe">File No.</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="TransIdUpdate" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Literal runat="server" ID="TransIdLiteral"></asp:Literal>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox Width="200" ID="TransIdTextbox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
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
                    <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" Height="1500px"
                        ShowPrintButton="true" ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

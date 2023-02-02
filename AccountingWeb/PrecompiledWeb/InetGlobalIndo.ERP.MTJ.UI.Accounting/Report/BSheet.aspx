<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.Report.BSheetDetail, App_Web_o0azb2_0" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                                            Year
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="YearTextBox" runat="server" Width="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*"
                                                ControlToValidate="YearTextBox" ErrorMessage="Year Must Be Filled"></asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="PeriodTextBox" runat="server" Width="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*"
                                                ControlToValidate="PeriodTextBox" ErrorMessage="Period Must Be Filled"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Layout Form
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="FormDropDownList" runat="server">
                                                <asp:ListItem Text="T Form" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Standard" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mode
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="FgModeDropDownList" runat="server">
                                                <asp:ListItem Text="With 0 (Zero)" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Without 0 (Zero)" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="TypeRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0" Text="Summary" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Detail"></asp:ListItem>
                                            </asp:RadioButtonList>
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
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="1500px" ShowPrintButton="true"
                    ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>

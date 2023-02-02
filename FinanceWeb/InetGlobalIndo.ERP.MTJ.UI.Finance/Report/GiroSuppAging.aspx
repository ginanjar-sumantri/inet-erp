<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="GiroSuppAging.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.Report.GiroSuppAging" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript" language="javascript">

        function CekAndFillRange(_prmLowestValue, _prmCurrValue, _prmNextValue) {
            if (_prmCurrValue.value != "") {
                if (parseInt(_prmCurrValue.value) >= parseInt(_prmLowestValue.value)) {
                    _prmNextValue.value = (parseInt(_prmCurrValue.value) + 1);
                }
                else {
                    _prmCurrValue.value = "";
                    _prmNextValue.value = "";
                }
            }
        }

        function CekRange(_prmLowestValue, _prmCurrValue) {
            if (_prmCurrValue.value != "") {
                if (parseInt(_prmCurrValue.value) < parseInt(_prmLowestValue.value)) {
                    _prmCurrValue.value = "";
                }
            }
        }
    
    </script>

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
                                            <td>
                                                Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="FgReportDropDownList">
                                                    <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                    <asp:ListItem Value="0">Summary Per Supplier</asp:ListItem>
                                                    <asp:ListItem Value="1">Summary Per Giro Supplier</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Type Must Be Choosed"
                                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="FgReportDropDownList"></asp:CustomValidator>
                                                <asp:DropDownList ID="BackDropDownList" runat="server">
                                                    <asp:ListItem Value="1" Text="Backward"></asp:ListItem>
                                                    <asp:ListItem Value="-1" Text="Forward"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Date
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
                                                            <asp:Literal ID="StartDateLiteral" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Range Period
                                            </td>
                                            <td valign="top">
                                                :
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            Range 1
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Period0TextBox" BackColor="#CCCCCC" runat="server" Width="30"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Until
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Period1TextBox" runat="server" Width="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Period1TextBox"
                                                                runat="server" ErrorMessage="Range 1 Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Range 2
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Period2TextBox" BackColor="#CCCCCC" runat="server" Width="30"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Until
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Period3TextBox" runat="server" Width="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="Period3TextBox"
                                                                runat="server" ErrorMessage="Range 2 Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Range 3
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Period4TextBox" BackColor="#CCCCCC" runat="server" Width="30"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Until
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Period5TextBox" runat="server" Width="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="Period5TextBox"
                                                                runat="server" ErrorMessage="Range 3 Must Be Filled" Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
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
                                        <tr>
                                            <td valign="top">
                                                Bank Payment
                                            </td>
                                            <td valign="top">
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBoxList runat="server" ID="BankCodeCheckBoxList" RepeatColumns="3">
                                                </asp:CheckBoxList>
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
                    <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="100%" Height="1600px"
                        ShowPrintButton="true" ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

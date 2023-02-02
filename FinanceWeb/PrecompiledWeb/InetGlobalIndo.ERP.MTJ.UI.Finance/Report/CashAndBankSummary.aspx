<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.Report.CashAndBankSummary, App_Web_0hq3v3m6" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                                        <td width="100px">
                                            Start Date
                                        </td>
                                        <td width="10px">
                                            :
                                        </td>
                                        <td width="150px">
                                            <asp:TextBox ID="StartDateTextBox" runat="server" Width="100px" BackColor="#CCCCCC"></asp:TextBox>
                                            &nbsp;
                                            <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_StartDateTextBox,'yyyy-mm-dd',this)"
                                                value="..." />--%>
                                                <asp:Literal ID="StartDateLiteral" runat="server"></asp:Literal>
                                        </td>
                                        <td width="10px">
                                        </td>
                                        <td width="100px">
                                            End Date
                                        </td>
                                        <td width="10px">
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EndDateTextBox" runat="server" Width="100px" BackColor="#CCCCCC"></asp:TextBox>
                                            &nbsp;
                                            <%--<input id="button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_EndDateTextBox,'yyyy-mm-dd',this)"
                                                value="..." />--%>
                                                <asp:Literal ID="EndDateLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Order By
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="OrderByDDL" runat="server">
                                                <asp:ListItem Text="Transaction Date" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Transaction Class" Value="1"></asp:ListItem>
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
                                                        <td>
                                                            <asp:Button ID="DataPagerButton" runat="server" CausesValidation="false" OnClick="DataPagerButton_Click" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td align="right">
                                                            <table border="0" cellpadding="2" cellspacing="0" width="0">
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
                                    <tr valign="top">
                                        <td>
                                            Payment Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td colspan="5">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:HiddenField ID="CheckHidden" runat="server" />
                                                        <asp:HiddenField ID="TempHidden" runat="server" />
                                                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                            <tr class="bgcolor_gray">
                                                                <td style="width: 5px">
                                                                    <asp:CheckBox runat="server" ID="AllCheckBox" />
                                                                </td>
                                                                <td style="width: 5px" class="tahoma_11_white" align="center">
                                                                    <b>No.</b>
                                                                </td>
                                                                <td style="width: 150px" class="tahoma_11_white" align="center">
                                                                    <b>Payment Type Code</b>
                                                                </td>
                                                                <td style="width: 400px" class="tahoma_11_white" align="center">
                                                                    <b>Payment Type Name</b>
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr id="RepeaterItemTemplate" runat="server">
                                                                        <td align="center">
                                                                            <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Literal runat="server" ID="PaymentCodeLiteral"></asp:Literal>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Literal runat="server" ID="PaymentNameLiteral"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr class="bgcolor_gray">
                                                                <td style="width: 1px" colspan="25">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--<asp:CheckBoxList ID="PaymentTypeCheckBoxList" RepeatColumns="2" runat="server" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td colspan="5" align="right">
                                            <asp:Panel DefaultButton="DataPagerButton2" ID="DataPagerPanel2" runat="server">
                                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="DataPagerButton2" runat="server" CausesValidation="false" OnClick="DataPagerButton2_Click" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td align="right">
                                                            <table border="0" cellpadding="2" cellspacing="0" width="0">
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
                                    <tr valign="top">
                                        <td>
                                            Petty Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td colspan="5">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:HiddenField ID="CheckHidden2" runat="server" />
                                                        <asp:HiddenField ID="TempHidden2" runat="server" />
                                                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                            <tr class="bgcolor_gray">
                                                                <td style="width: 5px">
                                                                    <asp:CheckBox runat="server" ID="AllCheckBox2" />
                                                                </td>
                                                                <td style="width: 5px" class="tahoma_11_white" align="center">
                                                                    <b>No.</b>
                                                                </td>
                                                                <td style="width: 150px" class="tahoma_11_white" align="center">
                                                                    <b>Petty Type Code</b>
                                                                </td>
                                                                <td style="width: 400px" class="tahoma_11_white" align="center">
                                                                    <b>Petty Type Name</b>
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater runat="server" ID="ListRepeater2" OnItemDataBound="ListRepeater2_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr id="RepeaterItemTemplate2" runat="server">
                                                                        <td align="center">
                                                                            <asp:CheckBox runat="server" ID="ListCheckBox2" />
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Literal runat="server" ID="NoLiteral2"></asp:Literal>
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Literal runat="server" ID="PettyCodeLiteral"></asp:Literal>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Literal runat="server" ID="PettyNameLiteral"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <tr class="bgcolor_gray">
                                                                <td style="width: 1px" colspan="25">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--<asp:CheckBoxList ID="PettyTypeCheckBoxList" RepeatColumns="2" runat="server" RepeatDirection="Vertical">
                                            </asp:CheckBoxList>--%>
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
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="1760px"
                    ShowPrintButton="true" ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>

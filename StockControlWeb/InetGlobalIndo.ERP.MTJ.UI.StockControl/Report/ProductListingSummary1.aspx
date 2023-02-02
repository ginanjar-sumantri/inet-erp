<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductListingSummary1.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Report.ProductListing" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
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
                                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <tr>
                                                <td>
                                                    View By
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ViewByDDL" OnSelectedIndexChanged="ViewByDDL_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Value="null" Text="[Choose One]"></asp:ListItem>
                                                        <asp:ListItem Value="0">Stock Balance</asp:ListItem>
                                                        <asp:ListItem Value="1">Stock Mutation</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="ViewByCustomValidator" runat="server" ErrorMessage="View By Must Be Choosed"
                                                        Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ViewByDDL"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Product Code
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="ProductCodeTextBox" runat="server" MaxLength="30" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                                                    <asp:Button ID="btnSearchPINo" runat="server" Text="..." />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Product Name
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="ProductNameTextBox" runat="server" MaxLength="30" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Product Spesification
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="ProductSpesificationTextBox" runat="server" MaxLength="30" Width="160"
                                                        BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Product Type
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="ProductTypeTextBox" runat="server" MaxLength="30" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Product Sub Group
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="ProductSubGroupTextBox" runat="server" MaxLength="30" Width="160"
                                                        BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Price Group
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="PriceGroupTextBox" runat="server" MaxLength="30" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Selling Price
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="4">
                                                    <asp:TextBox ID="SellingPriceTextBox" runat="server" MaxLength="30" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="DateTr" runat="server">
                                                <td>
                                                    Date From
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="DateFromTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                    <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateFromTextBox,'yyyy-mm-dd',this)"
                                                        value="..." />--%>
                                                        <asp:Literal ID="DateFromLiteral" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    To
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="DateToTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                    <%--<input id="button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateToTextBox,'yyyy-mm-dd',this)"
                                                        value="..." />--%>
                                                        <asp:Literal ID="DateToLiteral" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <tr>
                                        <td colspan="3">
                                            <table border="0" cellpadding="3" cellspacing="0" width="0">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ViewStokButton" runat="server" OnClick="ViewStokButton_Click" />
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
                <asp:Panel ID="StokBalancePanel" runat="server">
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr>
                            <td align="right" colspan="6">
                                <asp:Panel DefaultButton="DataPagerButton" ID="Panel1" runat="server">
                                    <table border="0" cellpadding="2" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Button ID="DataPagerButton" runat="server" OnClick="DataPagerButton_Click" />
                                            </td>
                                            <td valign="middle">
                                                <b>Page :</b>
                                            </td>
                                            <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                <ItemTemplate>
                                                    <td>
                                                        <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                        <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr class="bgcolor_gray">
                            <td style="width: 260px" class="tahoma_11_white" align="center">
                                <b>Product Name</b>
                            </td>
                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                <b>Location Name</b>
                            </td>
                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                <b>Wrhs Code</b>
                            </td>
                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                <b>Wrhs Name</b>
                            </td>
                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                <b>Qty</b>
                            </td>
                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                <b>Unit</b>
                            </td>
                        </tr>
                        <asp:Repeater ID="StokBalanceRepeater" runat="server" OnItemDataBound="StokBalanceRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr id="RepeaterStokBalance" runat="server">
                                    <td align="left">
                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="LocationNameLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="WrhsCodeLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="WrhsNameLiteral"></asp:Literal>
                                    </td>
                                    <td align="right">
                                        <asp:Literal runat="server" ID="QtyLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="UnitLiteral"></asp:Literal>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr class="bgcolor_gray">
                            <td style="width: 1px" colspan="20">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BackButton" runat="server" Text="Back" OnClick="BackButton_OnClick" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%--</asp:Repeater>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ShowPrintButton="true"
                    ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="StokBalancePanel1" runat="server">
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr>
                            <td align="right" colspan="8">
                                <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                    <table border="0" cellpadding="2" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:Button ID="DataPagerButton1" runat="server" OnClick="DataPagerButton1_Click" />
                                            </td>
                                            <td valign="middle">
                                                <b>Page :</b>
                                            </td>
                                            <asp:Repeater EnableViewState="true" ID="DataPagerTop1Repeater" runat="server" OnItemCommand="DataPagerTop1Repeater_ItemCommand"
                                                OnItemDataBound="DataPagerTop1Repeater_ItemDataBound">
                                                <ItemTemplate>
                                                    <td>
                                                        <asp:LinkButton ID="PageNumberLinkButton1" runat="server"></asp:LinkButton>
                                                        <asp:TextBox Visible="false" ID="PageNumberTextBox1" runat="server" Width="30px"></asp:TextBox>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="ProductLabel" runat="server" Width="500px" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr class="bgcolor_gray">
                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                <b>Trans Date</b>
                            </td>
                            <td style="width: 180px" class="tahoma_11_white" align="center">
                                <b>Trans Number</b>
                            </td>
                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                <b>Trans Type</b>
                            </td>
                            <td style="width: 180px" class="tahoma_11_white" align="center">
                                <b>File No</b>
                            </td>
                            <td style="width: 180px" class="tahoma_11_white" align="center">
                                <b>Reff Ins Name</b>
                            </td>
                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                <b>Wrhs Code</b>
                            </td>
                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                <b>Qty In</b>
                            </td>
                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                <b>Qty Out</b>
                            </td>
                        </tr>
                        <asp:Repeater ID="ProductInformationRepeater" runat="server" OnItemDataBound="ProductInformationRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr id="RepeaterProductInformation" runat="server">
                                    <td align="left">
                                        <asp:Literal runat="server" ID="TransDateLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="TransNmbrLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="TransTypeLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="FileNoLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="ReffInsNameLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="WrhsCodeLiteral"></asp:Literal>
                                    </td>
                                    <td align="right">
                                        <asp:Literal runat="server" ID="QtyInLiteral"></asp:Literal>
                                    </td>
                                    <td align="right">
                                        <asp:Literal runat="server" ID="QtyOutLiteral"></asp:Literal>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr class="bgcolor_gray">
                            <td style="width: 1px" colspan="20">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BackButton1" runat="server" Text="Back" OnClick="BackButton1_OnClick" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%--</asp:Repeater>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" ShowPrintButton="true"
                    ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>--%>
            </td>
        </tr>
    </table>
</asp:Content>

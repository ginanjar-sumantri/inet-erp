<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ReportCustomerGoodInTransactionByCustomer.aspx.cs" Inherits="VTSWeb.UI.ReportCustomerGoodInTransactionByCustomer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="PanelSearch">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                             &nbsp; Company Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="CustomerDDL" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                                <asp:ImageButton ID="ViewImageButton" runat="server" OnClick="ViewImageButton_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr align="center">
            <td align="center" class="style1">
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="200%" ShowPrintButton="true"
                    ShowZoomControl="true" ZoomMode="Percent" ZoomPercent="100">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>

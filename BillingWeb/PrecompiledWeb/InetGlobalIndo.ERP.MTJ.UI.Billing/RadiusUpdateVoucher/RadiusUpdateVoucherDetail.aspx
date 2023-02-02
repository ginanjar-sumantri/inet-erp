<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.RadiusUpdateVoucher.RadiusUpdateVoucherDetail, App_Web_im-wepyp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td colspan="7">
                                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TransNoTextBox" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                File No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FileNmbrTextBox" runat="server" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                Radius
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RadiusTextBox" runat="server" Width="200px" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Expired Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ExpiredDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Series
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SeriesTextBox" runat="server" Width="200" MaxLength="50" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Series No From
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SeriesNoFromTextBox" runat="server" Width="120" MaxLength="20" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Associated Service
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AssociatedServiceTextBox" runat="server" Width="200" MaxLength="50"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Series No To
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SeriesNoToTextBox" runat="server" Width="120" MaxLength="20" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Expire Time
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ExpireTimeTextBox" runat="server" Width="50" MaxLength="5" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Selling Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SellingAmountTextBox" runat="server" Width="120" MaxLength="15"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Expire Time Unit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ExpireTimeUnitTextBox" runat="server" Width="120" MaxLength="5"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                <asp:HiddenField ID="StatusHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <table cellpadding="3" cellspacing="0" border="0" width="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                            &nbsp;<asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                            &nbsp;<asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
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
</asp:Content>

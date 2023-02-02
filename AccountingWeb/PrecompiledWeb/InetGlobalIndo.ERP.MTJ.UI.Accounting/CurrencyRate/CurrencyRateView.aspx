<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.CurrencyRate.CurrencyRateView, App_Web_7bi8oamz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="CancelButton">
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
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td style="width: 50px">
                                <asp:ImageButton runat="server" ID="CancelButton" Text="Cancel" OnClick="CancelButton_Click" />
                            </td>
                            <td align="right">
                                Page :
                                <asp:Label ID="PageLabel" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr class="bgcolor_gray" height="20px">
                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                <b>No.</b>
                            </td>
                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                <b>Date</b>
                            </td>
                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                <b>Currency Code</b>
                            </td>
                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                <b>Rate</b>
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <td align="center">
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="DateLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="CurrCodeLiteral"></asp:Literal>
                                    </td>
                                    <td align="right">
                                        <asp:Literal runat="server" ID="CurrRateLiteral"></asp:Literal>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td style="width: 50px">
                                <asp:ImageButton runat="server" ID="CancelButton2" OnClick="CancelButton_Click" />
                            </td>
                            <td align="right">
                                Page :
                                <asp:Label ID="PageLabel2" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.SupplierBuyingPrice.SuppBuyingPrice, App_Web_ny5hv5e2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel1">
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
                                        <td width="90px">
                                            Supplier
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="SupplierTextBox" runat="server" MaxLength="30" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                                            <asp:Button ID="btnSearchSuppNo" runat="server" Text="..." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Product
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ProductTextBox" runat="server" MaxLength="30" Width="160" BackColor="#CCCCCC"></asp:TextBox>
                                            <asp:Button ID="btnSearchProdNo" runat="server" Text="..." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:ImageButton ID="ViewStokButton" runat="server" OnClick="ViewStokButton_Click" />
                                            <asp:ImageButton ID="ResetButton" runat="server" OnClick="ResertButton_Click" />
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
                <asp:Panel runat="server" ID="Panel2">
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Customer Selling Price</legend>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td align="right">
                                                <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
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
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="WarningLabel1" CssClass="warning"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="CheckHidden" runat="server" />
                                                <asp:HiddenField ID="TempHidden" runat="server" />
                                                <asp:HiddenField ID="sortField" runat="server" />
                                                <asp:HiddenField ID="ascDesc" Value="false" runat="server" />
                                                <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                    <tr class="bgcolor_gray">
                                                        <td style="width: 5px" class="tahoma_11_white" align="center">
                                                            <b>No.</b>
                                                        </td>
                                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                                            <asp:LinkButton Text="Supplier Code" ID="field1" class="SortLinkButton" runat="server"
                                                                OnClick="field1_Click"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                                            <asp:LinkButton Text="Product Code" ID="field2" class="SortLinkButton" runat="server"
                                                                OnClick="field2_Click"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 200px" class="tahoma_11_white" align="center">
                                                            <asp:LinkButton Text="Trans Number" ID="field3" class="SortLinkButton" runat="server"
                                                                OnClick="field3_Click"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 200px" class="tahoma_11_white" align="center">
                                                            <asp:LinkButton Text="Trans Date" ID="field4" class="SortLinkButton" runat="server"
                                                                OnClick="field4_Click"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 80px" class="tahoma_11_white" align="center">
                                                            <asp:LinkButton Text="Unit Code" ID="field5" class="SortLinkButton" runat="server"
                                                                OnClick="field5_Click"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 80px" class="tahoma_11_white" align="center">
                                                            <asp:LinkButton Text="Curr Code" ID="field6" class="SortLinkButton" runat="server"
                                                                OnClick="field6_Click"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 80px" class="tahoma_11_white" align="center">
                                                            <asp:LinkButton Text="Amount Forex" ID="field7" class="SortLinkButton" runat="server"
                                                                OnClick="field7_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr id="RepeaterListTemplate" runat="server">
                                                                <td align="center">
                                                                    <asp:Literal ID="NoLiteral" runat="server"></asp:Literal>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Literal ID="SuppCodeLiteral" runat="server"></asp:Literal>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Literal ID="ProductCodeLiteral" runat="server"></asp:Literal>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Literal ID="TransNmbrLiteral" runat="server"></asp:Literal>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Literal ID="TransDateLiteral" runat="server"></asp:Literal>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Literal ID="UnitCodeLiteral" runat="server"></asp:Literal>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Literal ID="CurrCodeLiteral" runat="server"></asp:Literal>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Literal ID="AmountForexLiteral" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr class="bgcolor_gray">
                                                        <td style="width: 1px" colspan="8">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.SupplierInvoice.SupplierInvoiceDetail, App_Web_yxiwhesb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="BackButton" runat="server">
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
                    <fieldset>
                        <legend>Header</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                Trans No.
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TransactionNoTextBox" Width="150" ReadOnly="true"
                                                    BackColor="#CCCCCC"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Supplier
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="SupplierTextBox" Width="420" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Term
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TermTextBox" Width="350" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Currency / Rate
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="80" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="ForexRateTextBox" Width="150" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <table width="0">
                                                    <tr class="bgcolor_gray" height="20">
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>PPN %</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>No.</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Date</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Rate</b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                PPN
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                                ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="PPNNoTextBox" runat="server" Width="100" BackColor="#cccccc" ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="PPNDateTextBox" runat="server" Width="70" BackColor="#CCCCCC" ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="PPNRateTextBox" runat="server" Width="100" BackColor="#cccccc" ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <table>
                                                    <tr class="bgcolor_gray" height="20">
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Currency</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Base Forex</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Discount %</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Discount Forex</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>PPN Forex</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Other Fee</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Stamp Fee</b>
                                                        </td>
                                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                                            <b>Total Forex</b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Amount
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="AmountBaseTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                                ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="DiscPercentTextBox" MaxLength="23" runat="server" Width="100" BackColor="#CCCCCC"
                                                                ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="DiscForexTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                                ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                                ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="OtherFeeTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                                ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="StampFeeTextBox" runat="server" Width="100" BackColor="#CCCCCC"
                                                                ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="width: 110px" align="center">
                                                            <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100px" BackColor="#CCCCCC"
                                                                ReadOnly="true">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td>
                                                Remark
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" MaxLength="500" Height="80"
                                                    TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Status
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                                <asp:HiddenField ID="StatusHiddenField" runat="server" />
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
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Detail</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                                            </td>
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
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Account</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Item Desc</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Amount Forex</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Remark</b>
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
                                                    <td align="left">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton ID="EditButton" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="AccountLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ItemDescLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="AmountForexLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

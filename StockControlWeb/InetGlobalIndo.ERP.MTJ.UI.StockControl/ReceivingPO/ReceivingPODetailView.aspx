<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ReceivingPODetailView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO.ReceivingPODetailView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="420">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LocationTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="350">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyTextBox" runat="server" MaxLength="18" Width="150" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Unit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="UnitTextBox" BackColor="#cccccc" ReadOnly="true"
                                    Width="210"></asp:TextBox>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"
                                    ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                    &nbsp;<asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="SerialNumberPanel">
        <table>
            <tr>
                <td>
                    <fieldset>
                        <legend>Serial Number</legend>
                        <table width="0" cellpadding="3" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                                &nbsp;<asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                                                &nbsp;<asp:Literal runat="server" ID="GetFormatExcelLiteral" />
                                                &nbsp;<asp:ImageButton runat="server" ID="ImportButton" OnClick="ImportButton_Click" />
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
                                    <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
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
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <%--<td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Trans No.</b>
                                            </td>--%>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Serial Number</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>PIN</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Manufacture ID</b>
                                            </td>
                                            <%--<td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Product Code</b>
                                            </td>--%>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Expiration Date</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Currency</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Forex Rate</b>
                                            </td>
                                            <%--<td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Country</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Bulk Mode</b>
                                            </td>
                                            
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Sold</b>
                                            </td>--%>
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
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="EditButton" />
                                                    </td>
                                                    <%--<td align="center">
                                                        <asp:Literal runat="server" ID="TransNoLiteral"></asp:Literal>
                                                    </td>--%>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="SerialNumberLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="PINLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ManufactureIDLiteral"></asp:Literal>
                                                    </td>
                                                    <%--<td align="center">
                                                        <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                                    </td>--%>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ExpirationDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="CurrLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="ForexRateLiteral"></asp:Literal>
                                                    </td>
                                                    <%--<td align="center">
                                                        <asp:Literal runat="server" ID="CountryLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="BulkModeLiteral"></asp:Literal>
                                                    </td>
                                                    
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="SoldLiteral"></asp:Literal>
                                                    </td>--%>
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

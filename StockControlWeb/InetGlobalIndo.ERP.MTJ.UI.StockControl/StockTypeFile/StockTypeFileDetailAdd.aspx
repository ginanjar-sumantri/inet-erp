<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockTypeFileDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTypeFile.StockTypeFileDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
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
                        <legend>Detail</legend>
                        <table border="0" cellpadding="3" cellspacing="0" width="0">
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
                                    <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px" align="center">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 210px" class="tahoma_11_white" align="center">
                                                <b>Transaction Type</b>
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
                                                        <asp:Literal runat="server" ID="TransactionTypeLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="GrabAllCheckBox" runat="server" Text="Grab data from all pages." />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" />
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
</asp:Content>

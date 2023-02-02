<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="GiroReceipt.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.GiroReceipt.GiroReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <%--<asp:Panel DefaultButton="AddButton" ID="Panel1" runat="server">--%>
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td class="tahoma_14_black">
                            <b>
                                <asp:Literal ID="PageTitleLiteral" runat="server" />
                            </b>
                        </td>
                        <td align="right">
                            <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
                                <table cellpadding="0" cellspacing="2" border="0">
                                    <tr>
                                        <td>
                                            <b>Quick Search :</b>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                <asp:ListItem Value="TransNmbr" Text="Giro No."></asp:ListItem>
                                                <asp:ListItem Value="FileNo" Text="File No."></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="SetorAllImageButton" runat="server" 
                                onclick="SetorAllImageButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td>
                            <table cellpadding="3" cellspacing="0" width="100%" border="0">
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
                            <table cellpadding="3" cellspacing="1" width="0" border="0">
                                <tr class="bgcolor_gray" height="20px">
                                    <td style="width: 5px" class="tahoma_11_white" align="center">
                                        <b>No.</b>
                                    </td>
                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                        <b>Action</b>
                                    </td>
                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                        <b>Giro No.</b>
                                    </td>
                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                        <b>File No.</b>
                                    </td>
                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                        <b>Status</b>
                                    </td>
                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                        <b>Receipt No.</b>
                                    </td>
                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                        <b>Receipt Date</b>
                                    </td>
                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                        <b>Due Date</b>
                                    </td>
                                </tr>
                                <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="RepeaterItemTemplate" runat="server">
                                            <td align="center">
                                                <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                            </td>
                                            <td align="center">
                                                <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton runat="server" ID="ViewButton" />
                                                        </td>
                                                        <%--<td style="padding-left: 4px">
                                                <asp:ImageButton runat="server" ID="EditButton" />
                                            </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="center">
                                                <asp:Literal runat="server" ID="GiroNoLiteral"></asp:Literal>
                                            </td>
                                            <td align="center">
                                                <asp:Literal runat="server" ID="FileNoLiteral"></asp:Literal>
                                            </td>
                                            <td align="center">
                                                <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                            </td>
                                            <td align="center">
                                                <asp:Literal runat="server" ID="ReceiptNoLiteral"></asp:Literal>
                                            </td>
                                            <td align="center">
                                                <asp:Literal runat="server" ID="ReceiptDateLiteral"></asp:Literal>
                                            </td>
                                            <td align="center">
                                                <asp:Literal runat="server" ID="DueDateLiteral"></asp:Literal>
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
                    <tr>
                        <td>
                            <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Panel DefaultButton="DataPagerBottomButton" ID="Panel3" runat="server">
                                            <table border="0" cellpadding="2" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="DataPagerBottomButton" runat="server" OnClick="DataPagerBottomButton_Click" />
                                                    </td>
                                                    <td>
                                                        <b>Page :</b>
                                                    </td>
                                                    <asp:Repeater EnableViewState="true" ID="DataPagerBottomRepeater" runat="server"
                                                        OnItemCommand="DataPagerTopRepeater_ItemCommand" OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
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
                </table>
            </td>
        </tr>
    </table>
    <%--</asp:Panel>--%>
</asp:Content>

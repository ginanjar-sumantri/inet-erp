<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PurchaseRequestXMLCompile.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest.PurchaseRequestXMLCompile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td colspan="2">
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
                                                    <asp:ListItem Value="TransNmbr" Text="Trans No."></asp:ListItem>
                                                    <asp:ListItem Value="FileNmbr" Text="File No."></asp:ListItem>
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
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />&nbsp;
                    <asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                    <asp:ImageButton runat="server" ID="CompileButton" OnClick="CompileButton_Click" />
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
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:HiddenField ID="CheckHidden" runat="server" />
                    <asp:HiddenField ID="TempHidden" runat="server" />
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr class="bgcolor_gray">
                            <td style="width: 5px">
                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                            </td>
                            <th width="5" class="tahoma_11_white">
                                No.
                            </th>
                            <th class="tahoma_11_white">
                                Action
                            </th>
                            <th class="tahoma_11_white">
                                Sender Code
                            </th>
                            <th class="tahoma_11_white">
                                XML File Name
                            </th>
                            <th class="tahoma_11_white">
                                Remark
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="ListRepeater" 
                            OnItemDataBound="ListRepeater_ItemDataBound" >
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ViewButton" runat="server" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="SenderCodeLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="XmlFileNameLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
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
                    <asp:ImageButton runat="server" ID="AddButton2" OnClick="AddButton_Click" />
                    &nbsp;<asp:ImageButton runat="server" ID="DeleteButton2" OnClick="DeleteButton_Click" />
                    &nbsp;<asp:ImageButton runat="server" ID="CompileButton2" OnClick="CompileButton_Click" />
                </td>
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
    </asp:Panel>
</asp:Content>

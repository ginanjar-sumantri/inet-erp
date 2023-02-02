<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest.PurchaseRequestXMLApprove, App_Web_4j1qazt0" maintainscrollpositiononpostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <fieldset>
            <legend>Purchase Request List</legend>
            <table cellpadding="3" cellspacing="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td class="tahoma_14_black">
                                    <b>
                                        <asp:Literal ID="PageTitleLiteral0" runat="server" />
                                    </b>
                                </td>
                                <td align="right">
                                    <asp:Panel ID="Panel4" DefaultButton="GoImageButton0" runat="server">
                                        <table cellpadding="0" cellspacing="2" border="0">
                                            <tr>
                                                <td>
                                                    <b>Quick Search :</b>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CategoryDropDownList0" runat="server">
                                                        <asp:ListItem Value="TransNmbr" Text="Trans No."></asp:ListItem>
                                                        <asp:ListItem Value="FileNo" Text="File No."></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="KeywordTextBox0" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="GoImageButton0" runat="server" OnClick="GoImageButton0_Click" />
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
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Panel DefaultButton="DataPagerButton0" ID="Panel5" runat="server">
                                                    <table border="0" cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="DataPagerButton0" runat="server" OnClick="DataPagerButton0_Click" />
                                                            </td>
                                                            <td valign="middle">
                                                                <b>Page :</b>
                                                            </td>
                                                            <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater0" runat="server" OnItemCommand="DataPagerTopRepeater0_ItemCommand"
                                                                OnItemDataBound="DataPagerTopRepeater0_ItemDataBound">
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
                                    <asp:Label runat="server" ID="WarningLabel0" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="TempHidden0" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 160px" class="tahoma_11_white" align="center">
                                                <b>Trans No.</b>
                                            </td>
                                            <td style="width: 160px" class="tahoma_11_white" align="center">
                                                <b>File No.</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Trans Date</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Status</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Organization Unit</b>
                                            </td>                                            
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Currency</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Request By</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater0" OnItemDataBound="ListRepeater0_ItemDataBound"
                                            OnItemCommand="ListRepeater0_ItemCommand">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="CreateXMLButton" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="TransNoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="FileNmbrLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="TransDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="OrgUnitDescLiteral"></asp:Literal>
                                                    </td>                                                    
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="CurrCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="RequestByLiteral"></asp:Literal>
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
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Panel DefaultButton="DataPagerBottomButton0" ID="Panel6" runat="server">
                                                    <table border="0" cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="DataPagerBottomButton0" runat="server" OnClick="DataPagerBottomButton0_Click" />
                                                            </td>
                                                            <td>
                                                                <b>Page :</b>
                                                            </td>
                                                            <asp:Repeater EnableViewState="true" ID="DataPagerBottomRepeater0" runat="server"
                                                                OnItemCommand="DataPagerTopRepeater0_ItemCommand" OnItemDataBound="DataPagerTopRepeater0_ItemDataBound">
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
        </fieldset>
        <fieldset>
            <legend>XML List</legend>
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
                                                        <asp:ListItem Value="SenderCode" Text="Sender Code"></asp:ListItem>
                                                        <asp:ListItem Value="XMLFileName" Text="XML File Name"></asp:ListItem>
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
                        <asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
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
                            </tr>
                            <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound"
                                OnItemCommand="ListRepeater_ItemCommand">
                                <ItemTemplate>
                                    <tr id="RepeaterItemTemplate" runat="server">
                                        <td align="center">
                                            <asp:CheckBox runat="server" ID="ListCheckBox" />
                                        </td>
                                        <td align="center">
                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnApprove" runat="server" />
                                            <asp:ImageButton ID="btnView" runat="server" />
                                        </td>
                                        <td align="center">
                                            <asp:Literal runat="server" ID="SenderCodeLiteral"></asp:Literal>
                                        </td>
                                        <td align="center">
                                            <asp:Literal runat="server" ID="XmlFileNameLiteral"></asp:Literal>
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
                        <asp:ImageButton runat="server" ID="DeleteButton2" OnClick="DeleteButton_Click" />
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
        </fieldset>
    </asp:Panel>
</asp:Content>

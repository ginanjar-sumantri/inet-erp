<%@ Page Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true"
    CodeFile="Outbox.aspx.cs" Inherits="SMS.SMSWeb.Message.Outbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    <asp:Literal ID="SubPageTitleLiteral" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
        <table cellpadding="0" cellspacing="2" border="0">
            <tr>
                <td>
                    <b>Quick Search :</b>
                </td>
                <td>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                        <asp:ListItem Value="DestinationNo" Text="Destination Phone No"></asp:ListItem>
                        <asp:ListItem Value="Message" Text="Message"></asp:ListItem>
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
    <table cellpadding="3px" cellspacing="0">
        <tr>
            <td style="width: 100px">
                Category
            </td>
            <td style="width: 15px">
                :
            </td>
            <td>
                <asp:DropDownList ID="CategoryDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CategoryDDL_SelectedIndexChanged">
                </asp:DropDownList>
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
                <asp:DropDownList ID="StatusDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="StatusDropDownList_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="ALL"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Queued"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Failed"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
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
    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
    <asp:HiddenField ID="CheckHidden" runat="server" />
    <asp:HiddenField ID="TempHidden" runat="server" />
    <table cellpadding="3" cellspacing="1" border="0">
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
            <td style="width: 120px" class="tahoma_11_white" align="center">
                <b>Category</b>
            </td>
            <td style="width: 120px" class="tahoma_11_white" align="center">
                <b>Destination No</b>
            </td>
            <td style="width: 50px" class="tahoma_11_white" align="center">
                <b>Status</b>
            </td>
            <td style="width: 100px" class="tahoma_11_white" align="center">
                <b>Username</b>
            </td>
            <td style="width: 100px" class="tahoma_11_white" align="center">
                <b>Date Sent</b>
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
                        <asp:ImageButton ID="ViewButton" runat="server" />
                    </td>
                    <td align="left">
                        <asp:Literal runat="server" ID="CategoryLiteral"></asp:Literal>
                    </td>
                    <td align="left">
                        <asp:Literal runat="server" ID="DestinationLiteral"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                    </td>
                    <td align="left">
                        <asp:Literal runat="server" ID="UserNameLiteral"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="DateSentLiteral"></asp:Literal>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr class="bgcolor_gray">
            <td style="width: 1px" colspan="25">
            </td>
        </tr>
    </table>
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton_Click" />
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
 </asp:Content>

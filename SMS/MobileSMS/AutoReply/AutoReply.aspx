<%@ Page Title="" Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true" CodeFile="AutoReply.aspx.cs" Inherits="SMS.SMSWeb.AutoReply.AutoReply_AutoReply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle" Runat="Server">
    AUTO REPLY
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" Runat="Server">
    <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
        <table cellpadding="0" cellspacing="2" border="0">
            <tr>
                <td>
                    <b>Quick Search :</b>
                </td>
                <td>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                        <asp:ListItem Value="KeyWord" Text="Key Word"></asp:ListItem>
                        <asp:ListItem Value="ReplyMessage" Text="Reply Message"></asp:ListItem>
                        <asp:ListItem Value="PhoneNumber" Text="Phone Number"></asp:ListItem>
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
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <asp:ImageButton ID="AddButton" runat="server" onclick="AddButton_Click"/>
                <asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                <%--<asp:ImageButton runat="server" ID="UploadButton" onclick="UploadButton_Click" />--%>
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
    <asp:Label runat="server" ID="WarningLabel" ForeColor="Red" Font-Bold="true"></asp:Label>
    <asp:HiddenField ID="TempHidden" runat="server" />
    <asp:HiddenField ID="CheckHidden" runat="server" />
    <table cellpadding="3" cellspacing="1" border="0">
        <tr class="bgcolor_gray">
            <td align="center" style="width: 5px">
                <asp:CheckBox runat="server" ID="AllCheckBox" />
            </td>
            <td align="center" style="width: 5px;font-weight:bold;" >
                No.
            </td>
            <td align="center" style="width: 60px;font-weight:bold;" >
                Action
            </td>
            <td align="center" style="width: 120px;font-weight:bold;">
                Sender Phone No
            </td>
            <td align="center" style="width: 100px;font-weight:bold;">
                Key Word
            </td>
            <td align="center" style="width: 150px;font-weight:bold;">
                Reply Message
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
                    <td align="center">
                        <asp:ImageButton ID="EditButton" runat="server" />
                    </td>
                    <td align="left">
                        <asp:Literal runat="server" ID="SenderPhoneLiteral"></asp:Literal>
                    </td>
                    <td align="left">
                        <asp:Literal runat="server" ID="KeyWordLiteral"></asp:Literal>
                    </td>
                    <td align="left">
                        <asp:Literal runat="server" ID="ReplyMessageLiteral"></asp:Literal>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr class="bgcolor_gray">
            <td style="width: 1px" colspan="6">
            </td>
        </tr>
    </table>
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <asp:ImageButton ID="AddButton2" runat="server" onclick="AddButton_Click"/>
                <asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton_Click" />
                <%--<asp:ImageButton runat="server" ID="UploadButton2" onclick="UploadButton_Click" />--%>
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


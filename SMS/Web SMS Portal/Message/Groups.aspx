<%@ Page Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true"
    CodeFile="Groups.aspx.cs" Inherits="SMS.SMSWeb.Message.Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    <asp:Literal ID="SubPageTitleLiteral" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <%--<asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                    &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />--%>
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
                    <b>Group Name</b>
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
                                        <asp:ImageButton ID="ViewButton" runat="server" />
                                    </td>
                                    <td style="padding-left: 4px">
                                        <asp:ImageButton ID="EditButton" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left">
                            <asp:Literal runat="server" ID="PhoneBookGroupLiteral"></asp:Literal>
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
                    <%--<asp:ImageButton runat="server" ID="AddButton2" OnClick="AddButton_Click" />
                    &nbsp;<asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton_Click" />--%>
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

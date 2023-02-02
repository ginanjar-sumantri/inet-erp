<%@ page title="" language="C#" masterpagefile="~/SMSMasterPage.master" autoeventwireup="true" inherits="SMS.SMSWeb.ManageUser.ManageUser, App_Web__eq3z4mv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    <asp:Label ID="PageLabel" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
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
                        <asp:ListItem Value="NameListItem" Text="Name"></asp:ListItem>
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
                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                &nbsp;
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
    </table>
    <asp:Label runat="server" ID="WarningLabel" ForeColor="Red" Font-Bold="true"></asp:Label>
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
            <td style="width: 60px" class="tahoma_11_white" align="center">
                <b>Action</b>
            </td>
            <td style="width: 250px" class="tahoma_11_white" align="center">
                <b>User ID</b>
            </td>
            <td style="width: 150px" class="tahoma_11_white" align="center">
                <b>Expire Date</b>
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
                        <asp:ImageButton runat="server" Text="Edit" ID="EditButton" />
                    </td>
                    <td align="left">
                        <asp:Literal runat="server" ID="NameLiteral"></asp:Literal>
                    </td>
                    <td align="center">
                        <asp:Literal runat="server" ID="ExpireLiteral"></asp:Literal>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr class="bgcolor_gray">
            <td style="width: 1px" colspan="7">
            </td>
        </tr>
    </table>    
</asp:Content>

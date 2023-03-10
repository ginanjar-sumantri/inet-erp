<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="Purpose.aspx.cs" Inherits="VTSWeb.UI.Purpose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
                    <table cellpadding="0" cellspacing="2" border="0">
                        <tr>
                            <td>
                                <b>Quick Search :</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="PurposeDropDownList" runat="server">
                                    <asp:ListItem Value="PurposeCode" Text="Purpose Code"></asp:ListItem>
                                    <asp:ListItem Value="PurposeName" Text="Purpose Name"></asp:ListItem>
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
                <table cellpadding="3" cellspacing="0" border="0" style="width: 102%">
                    <tr>
                        <td align="left">
                            <%--</td>
                                <td>--%>
                            <asp:ImageButton ID="AddButton" runat="server" CausesValidation="False" OnClick="AddButton_Click" />&nbsp
                            <asp:ImageButton ID="DeleteButton" runat="server" CausesValidation="False" OnClick="DeleteButton_Click" />
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
                <b>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="CheckHidden" runat="server" />
                <asp:HiddenField ID="TempHidden" runat="server" />
                <table cellpadding="3" cellspacing="1" width="0" border="0">
                    <tr class="bgcolor_gray">
                        <td>
                            <asp:CheckBox runat="server" ID="AllCheckBox" />
                        </td>
                        <td style="width: 5px" class="tahoma_11_white" align="center">
                            <b>No.</b>
                        </td>
                        <td style="width: 100px" class="tahoma_11_white" align="center">
                            <b>Action</b>
                        </td>
                        <td style="width: 120px" class="tahoma_11_white" align="center">
                            <b>Purpose Code</b>
                        </td>
                        <td style="width: 200px" class="tahoma_11_white" align="center">
                            <b>Purpose Name</b>
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
                                    <table cellpadding="0" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <%--<td>
                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                </td>--%>
                                            <td style="padding-left: 4px">
                                                <asp:ImageButton runat="server" ID="EditButton" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="PurposeCodeLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="PurposeNamaLiteral"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="bgcolor_gray">
                        <td style="width: 1px" colspan="5">
                        </td>
                    </tr>
                </table>
                <tr>
                    <td>
                        <table cellpadding="3" cellspacing="0" border="0" style="width: 100%">
                            <tr>
                                <td align="left">
                                    <%--</td>
                                <td>--%>
                                    <asp:ImageButton runat="server" ID="AddButton2" CausesValidation="False" OnClick="AddButton_Click" />&nbsp
                                    <asp:ImageButton runat="server" ID="DeleteButton2" CausesValidation="False" OnClick="DeleteButton_Click" />
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
                    </td>
                </tr>
            </td>
        </tr>
    </table>
</asp:Content>

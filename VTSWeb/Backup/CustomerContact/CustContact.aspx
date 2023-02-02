<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CustContact.aspx.cs" Inherits="VTSWeb.UI.CustContact" %>

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
                                <asp:DropDownList ID="CompanyDropDownList" runat="server">
                                    <asp:ListItem Value="CustCode" Text="Company Name"></asp:ListItem>
                                    <asp:ListItem Value="ContactName" Text="Contact Name"></asp:ListItem>
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
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
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
            <td align="left">
                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:HiddenField ID="CheckHidden" runat="server" />
                <asp:HiddenField ID="TempHidden" runat="server" />
                <table cellpadding="3" cellspacing="1" width="0" border="0">
                    <tr class="bgcolor_gray">
                        <%--<td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>--%>
                        <td class="tahoma_11_white" class="style14">
                            <asp:CheckBox runat="server" ID="AllCheckBox" />
                        </td>
                        <td class="tahoma_11_white" align="center">
                            <b>No</b>
                        </td>
                        <td style="width: 10px" class="tahoma_11_white" align="center">
                            <b>Action</b>
                        </td>
                        <td style="width: 100px" class="tahoma_11_white" align="left">
                            <b>Company Name</b>
                        </td>
                        <td style="width: 100px" class="tahoma_11_white" align="left">
                            <b>Contact Type </b>
                        </td>
                        <td style="width: 150px" class="tahoma_11_white" align="left">
                            <b>Contact Name</b>
                        </td>
                        <td style="width: 100px" class="tahoma_11_white" align="left">
                            <b>Phone </b>
                        </td>
                        <td style="width: 80px" class="tahoma_11_white" align="left">
                            <b>Email </b>
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                        <ItemTemplate>
                            <tr id="RepeaterItemTemplate" runat="server">
                                <td>
                                    <asp:CheckBox runat="server" ID="ListCheckBox" />
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                </td>
                                <td>
                                    <table class="action_table" cellpadding="0" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton runat="server" ID="ViewImageButton" />
                                            </td>
                                            <td>
                                                <asp:ImageButton runat="server" ID="EditImageButton" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="CompanyNameLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="ContactTypeLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="ContactNameLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="PhoneLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="EmailLiteral"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="bgcolor_gray">
                        <td style="width: 1px" colspan="20">
                        </td>
                </table>
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
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

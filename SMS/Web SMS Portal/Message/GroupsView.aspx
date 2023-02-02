<%@ Page Language="C#" MasterPageFile="~/SMSMasterPage.master" AutoEventWireup="true"
    CodeFile="GroupsView.aspx.cs" Inherits="SMS.SMSWeb.Message.ContactsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAtas" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderContentTitle"
    runat="Server">
    <asp:Literal ID="SubPageTitleLiteral" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderContent" runat="Server">
    <table cellpadding="3" cellspacing="0" border="0" width="0">
        <tr>
            <td valign="top">
                <asp:Panel runat="server" ID="Panel1">
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td>
                                            Group Name
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="GroupNameTextBox" Width="250" MaxLength="50" ReadOnly="true"
                                                BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <fieldset>
                                                <legend>Contact List</legend>
                                                <table cellpadding="3" cellspacing="0" border="0" width="0">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                                                <tr>
                                                                    <td>
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
                                                    <%--<tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                                                <tr class="bgcolor_gray">
                                                                    <%--<td style="width: 5px">
                                                                        <asp:CheckBox runat="server" ID="AllCheckBox" />
                                                                    </td>--%>
                                                                    <td style="width: 5px" class="tahoma_11_white" align="center">
                                                                        <b>No.</b>
                                                                    </td>
                                                                    <td style="width: 250px" class="tahoma_11_white" align="center">
                                                                        <b>Contact Name</b>
                                                                    </td>
                                                                    <td style="width: 250px" class="tahoma_11_white" align="center">
                                                                        <b>Phone No</b>
                                                                    </td>
                                                                </tr>
                                                                <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr id="RepeaterItemTemplate" runat="server">
                                                                            <%--<td align="center">
                                                                                <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                                            </td>--%>
                                                                            <td align="center">
                                                                                <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Literal runat="server" ID="ContactNameLiteral"></asp:Literal>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:Literal runat="server" ID="PhoneNoLiteral"></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" border="0" width="0">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

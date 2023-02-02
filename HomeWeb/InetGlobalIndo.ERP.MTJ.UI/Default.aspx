<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Home._Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="DefaultHeadContentPlaceHolder"
    runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="DefaultBodyContentPlaceHolder"
    runat="Server">
<asp:Panel runat="server" ID="Panel1">
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
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="1" width="0" border="0">
                                    <tr class="bgcolor_gray">
                                        <td style="width: 5px" class="tahoma_11_white" align="center">
                                            <b>No.</b>
                                        </td>
                                        <td style="width: 60px" class="tahoma_11_white" align="center">
                                            <b>Action</b>
                                        </td>
                                        <td style="width: 240px" class="tahoma_11_white" align="center">
                                            <b>Reminder Name</b>
                                        </td>
                                        <td style="width: 50px" class="tahoma_11_white" align="center">
                                            <b>Count</b>
                                        </td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="ListRepeater" 
                                        onitemdatabound="ListRepeater_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="RepeaterItemTemplate" runat="server">
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                </td>
                                                <td align="left">
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
                                                <td>
                                                    <asp:Literal runat="server" ID="ReminderNameLiteral"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:Literal runat="server" ID="CountLiteral"></asp:Literal>
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
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

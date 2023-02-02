<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductDefaultValType.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.ProductDefaultValType.ProductDefaultValType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" runat="server">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="100%">
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
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="CheckHidden" runat="server" />
                                <asp:HiddenField ID="TempHidden" runat="server" />
                                <table cellpadding="3" cellspacing="1" width="0" border="0">
                                    <tr class="bgcolor_gray">
                                        <td style="width: 5px" class="tahoma_11_white" align="center">
                                            <b>No.</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Action</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Default Product Value Type</b>
                                        </td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="RepeaterListTemplate" runat="server">
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                </td>
                                                <td align="left">
                                                    <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                        <tr>
                                                            <td style="padding-left: 4px">
                                                                <asp:ImageButton runat="server" ID="EditButton" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="ProductDefaultValLiteral"></asp:Literal>
                                                </td>
                                                <%-- <td align="left">
                                                    <asp:Literal runat="server" ID="ProductGroupNameLiteral"></asp:Literal>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

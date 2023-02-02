<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="TermAndConditionSetup.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.TermAndConditionSetup.TermAndConditionSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
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
                        <tr>
                            <td>
                                <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel runat="server" ID="ListUpdatePanel">
                                                <ContentTemplate>
                                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                                        <tr class="bgcolor_gray">
                                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                                <b>No.</b>
                                                            </td>
                                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                                <b>Action</b>
                                                            </td>
                                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                                <b>Type</b>
                                                            </td>
                                                        </tr>
                                                        <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr id="RepeaterItemTemplate" runat="server">
                                                                    <td align="center">
                                                                        <asp:Literal ID="NoLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td>
                                                                        <table class="action_table" cellpadding="0" cellspacing="0" width="0" border="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ImageButton runat="server" ID="ViewImageButton" />
                                                                                </td>
                                                                                <td style="padding-left: 4px">
                                                                                    <asp:ImageButton runat="server" ID="EditImageButton" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="TypeLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <tr class="bgcolor_gray">
                                                            <td style="width: 1px" colspan="25">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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

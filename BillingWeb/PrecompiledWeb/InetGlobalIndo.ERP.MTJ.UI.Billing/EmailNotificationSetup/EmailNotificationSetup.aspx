<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.EmailNotificationSetup.EmailNotificationSetup, App_Web_ogdejpxk" %>

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
                                            <asp:DropDownList ID="TypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TypeDropDownList_SelectedIndexChanged">
                                                <asp:ListItem Value="99">ALL</asp:ListItem>
                                                <asp:ListItem Value="0">Internal</asp:ListItem>
                                                <asp:ListItem Value="1">External</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td>
                                            <asp:UpdatePanel runat="server" ID="SubTypeUpdatePanel">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="SubTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SubTypeDropDownList_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>--%>
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
                                                            <td style="width: 60px" class="tahoma_11_white" align="center">
                                                                <b>Type</b>
                                                            </td>
                                                            <td style="width: 130px" class="tahoma_11_white" align="center">
                                                                <b>Sub Type</b>
                                                            </td>
                                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                                <b>Email From</b>
                                                            </td>
                                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                                <b>Email To</b>
                                                            </td>
                                                            <td style="width: 280px" class="tahoma_11_white" align="center">
                                                                <b>Subject</b>
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
                                                                    <td>
                                                                        <asp:Literal ID="TypeLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="SubTypeLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="EmailFromLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="EmailToLiteral" runat="server"></asp:Literal>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Literal ID="SubjectLiteral" runat="server"></asp:Literal>
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
        <asp:UpdatePanel runat="server" ID="TriggerUpdatePanel">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TypeDropDownList" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

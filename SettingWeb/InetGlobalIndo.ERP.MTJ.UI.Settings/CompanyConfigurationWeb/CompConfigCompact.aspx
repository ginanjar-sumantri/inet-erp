<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CompConfigCompact.aspx.cs" Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.CompConfig.CompConfigCompact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function UpdateValue(var _prmValue) 
        {
            _prmValue.value = _prmValue.value;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <%--<form id="MenuForm" runat="server">--%>
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel runat="server" ID="WarningLabelUpdatePanel">
                    <ContentTemplate>
                        <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label><br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td width="200px">
                Group Setting
            </td>
            <td width="10px">
                :
            </td>
            <td>
                <asp:UpdatePanel runat="server" ID="DDLUpdatePanel">
                    <ContentTemplate>
                        <asp:DropDownList ID="GroupSettingDropDownList" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="GroupSettingDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GroupSettingDropDownList" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel runat="server" ID="DataRepeaterUpdatePanel">
                    <ContentTemplate>
                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                            <tr class="bgcolor_gray">
                                <td style="width: 150px" class="tahoma_11_white" align="center">
                                    <b>Config Code</b>
                                </td>
                                <td style="width: 150px" class="tahoma_11_white" align="center">
                                    <b>Value</b>
                                </td>
                                <td style="width: 400px" class="tahoma_11_white" align="center">
                                    <b>Remark</b>
                                </td>
                                <td style="width: 80px" class="tahoma_11_white" align="center">
                                    <b>Action</b>
                                </td>
                            </tr>
                            <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound"
                                OnItemCommand="ListRepeater_ItemCommand">
                                <ItemTemplate>
                                    <tr id="RepeaterItemTemplate" runat="server">
                                        <td align="left">
                                            <asp:Literal runat="server" ID="ConfigCodeLiteral"></asp:Literal>
                                        </td>
                                        <td align="left">
                                            <%--<asp:Literal runat="server" ID="ValueLiteral"></asp:Literal>--%>
                                            <asp:TextBox ID="ValueTextBox" runat="server"></asp:TextBox>
                                            <asp:DropDownList ID="ValueDropDownList" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:Literal runat="server" ID="DescriptionLiteral"></asp:Literal>
                                        </td>
                                        <td align="center">
                                            <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="UpdateButton" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>

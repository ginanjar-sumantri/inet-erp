<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="Religion.aspx.cs" Inherits="VTSWeb.UI.Religion" %>

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
            <td>
                <table cellpadding="3" cellspacing="0" border="0" style="width: 102%">
                    <tr>
                        <td align="left">
                            <%--</td>
                                <td>--%>
                            <asp:ImageButton ID="AddButton" runat="server" CausesValidation="False" OnClick="AddButton_Click" />&nbsp
                            <asp:ImageButton ID="DeleteButton" runat="server" CausesValidation="False" OnClick="DeleteButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
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
                            <b>Religion Code</b>
                        </td>
                        <td style="width: 200px" class="tahoma_11_white" align="center">
                            <b>Religion Name</b>
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
                                    <asp:Literal runat="server" ID="ReligionCodeLiteral"></asp:Literal>
                                </td>
                                <td align="left">
                                    <asp:Literal runat="server" ID="ReligionNameLiteral"></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr class="bgcolor_gray">
                        <td style="width: 1px" colspan="5">
                        </td>
                    </tr>
                </table>
                <table cellpadding="3" cellspacing="0" border="0" style="width: 100%">
                    <tr>
                        <td>
                            <%--</td>
                                <td>--%>
                            <asp:ImageButton runat="server" ID="AddButton2" CausesValidation="False" OnClick="AddButton_Click" />&nbsp
                            <asp:ImageButton runat="server" ID="DeleteButton2" CausesValidation="False" OnClick="DeleteButton_Click" />
                        </td>
                    </tr>
                </table>
                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

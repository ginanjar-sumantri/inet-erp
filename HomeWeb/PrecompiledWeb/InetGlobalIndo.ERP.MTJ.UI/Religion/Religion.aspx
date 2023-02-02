<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Home.Religion.Religion, App_Web_xxtilgok" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="AddButton">
        <table cellpadding="3" cellspacing="0" border="0" width="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="AddButton" runat="server" OnClick="AddButton_Click" />
                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                            </td>
                            <%--<td>
                                <asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                            </td>--%>
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
                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                <asp:CheckBox ID="AllCheckBox" runat="server" />
                            </td>
                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                <b>No.</b>
                            </td>
                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                <b>Action</b>
                            </td>
                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                <b>Religion Code</b>
                            </td>
                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                <b>Religion Name</b>
                            </td>
                        </tr>
                        <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <td align="center">
                                        <asp:CheckBox ID="ListCheckBox" runat="server" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal ID="NoLiteral" runat="server"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                </td>
                                                <td style="padding-left: 4px">
                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="center">
                                        <%# DataBinder.Eval(Container.DataItem,"ReligionCode") %>
                                    </td>
                                    <td align="left">
                                        <%# DataBinder.Eval(Container.DataItem,"ReligionName") %>
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
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:ImageButton runat="server" ID="AddButton2" OnClick="AddButton_Click" />
                                &nbsp;<asp:ImageButton runat="server" ID="DeleteButton2" OnClick="DeleteButton_Click" />
                            </td>
                            <%--<td>
                                <asp:ImageButton runat="server" ID="DeleteButton2" OnClick="DeleteButton_Click" />
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

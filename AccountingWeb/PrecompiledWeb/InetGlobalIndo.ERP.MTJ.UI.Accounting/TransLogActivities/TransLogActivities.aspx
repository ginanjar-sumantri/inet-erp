<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.TransLogActivities.TransLogActivities, App_Web_l0zsloj9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="ViewButton">
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
                    <table>
                        <tr>
                            <td>
                                Transaction Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="TransTypeDropDownList" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Date From
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FromTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<asp:HiddenField ID="FromHidden" runat="server" />--%>
                            </td>
                            <td>
                                <asp:Literal ID="FromLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                                To
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ToTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<asp:HiddenField ID="ToHidden" runat="server" />--%>
                            </td>
                            <td>
                                <asp:Literal ID="ToLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Activities Mode
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ActivitiesModeDropdownList" runat="server">
                                    <asp:ListItem Text="Approve" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Posting" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Unposting" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Deleted" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                User Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="ViewButton" runat="server" OnClick="ViewButton_Click" />
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
                                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                    <tr>
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
                                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--<asp:HiddenField ID="CheckHidden" runat="server" />--%>
                                <asp:HiddenField ID="TempHidden" runat="server" />
                                <table cellpadding="3" cellspacing="1" width="0" border="0">
                                    <tr class="bgcolor_gray">
                                        <%--<td style="width: 5px">
                                            <asp:CheckBox runat="server" ID="AllCheckBox" />
                                        </td>--%>
                                        <td style="width: 180px" class="tahoma_11_white" align="center">
                                            <b>File Number</b>
                                        </td>
                                        <td style="width: 180px" class="tahoma_11_white" align="center">
                                            <b>Trans Number</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Activity Date</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Activity Mode</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>By User Name</b>
                                        </td>
                                        <td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Reason</b>
                                        </td>
                                        <%--<td style="width: 100px" class="tahoma_11_white" align="center">
                                            <b>Description</b>
                                        </td>
                                        <td style="width: 80px" class="tahoma_11_white" align="center">
                                            <b>Status</b>
                                        </td>--%>
                                    </tr>
                                    <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                        <ItemTemplate>
                                            <tr id="RepeaterItemTemplate" runat="server">
                                                <%--<td align="center">
                                                    <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
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
                                                </td>--%>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="FileNumberLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="TransNumberLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="ActivityDateLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="ActivityModeLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="ByUserNameLiteral"></asp:Literal>
                                                </td>
                                                <td align="center">
                                                    <asp:Literal runat="server" ID="ReasonLiteral"></asp:Literal>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr class="bgcolor_gray">
                                        <td style="width: 1px" colspan="6">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                    <tr>
                                        <%--<td>
                                            <asp:ImageButton ID="AddButton2" runat="server" OnClick="AddButton_Click" />
                                            </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton_Click" />
                                        </td>--%>
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
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

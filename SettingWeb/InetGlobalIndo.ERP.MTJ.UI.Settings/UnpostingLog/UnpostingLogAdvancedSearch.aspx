<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="UnpostingLogAdvancedSearch.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.UnpostingLog.UnpostingLogAdvancedSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="GoImageButton">
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
                    <fieldset>
                        <legend>Search Criteria</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="Label" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Trans Type
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TransTypeTextBox" Width="150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Trans No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TransNmbrTextBox" Width="150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Unposting Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="DateFromTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                    <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateFromTextBox,'yyyy-mm-dd',this)"
                                        value="..." />--%>
                                    <asp:Literal ID="DateFromLiteral" runat="server"></asp:Literal>
                                    To :
                                    <asp:TextBox runat="server" ID="DateToTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                    <%--<input id="button2" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateToTextBox,'yyyy-mm-dd',this)"
                                        value="..." />--%>
                                    <asp:Literal ID="DateToLiteral" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Unposting By
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="UnpostingByTextBox" Width="150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Result</legend>
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
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Trans Type</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Trans No.</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Unposting Date</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Unposting By</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="TransTypeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="TransNmbrLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ActDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="UserNameLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
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
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

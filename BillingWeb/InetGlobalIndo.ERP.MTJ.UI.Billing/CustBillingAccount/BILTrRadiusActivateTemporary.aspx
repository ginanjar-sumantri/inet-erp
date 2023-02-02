<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="BILTrRadiusActivateTemporary.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase.BILTrRadiusActivateTemp"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="GoImageButton" runat="server">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td class="tahoma_14_black">
                                <b>
                                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                                </b>
                            </td>
                            <td align="right">
                                <asp:Panel ID="Panel2" DefaultButton="GoImageButton" runat="server">
                                    <table cellpadding="0" cellspacing="2" border="0">
                                        <tr>
                                            <td>
                                                <b>Quick Search :</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                    <asp:ListItem Value="TransNo" Text="Trans No."></asp:ListItem>
                                                    <asp:ListItem Value="FileNo" Text="File No."></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click" />
                                            </td>
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
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="Addbutton" runat="server" OnClick="AddButton_click" />
                            </td>
                            <%--<td>
                                <asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_click" />
                            </td>--%>
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
                    <asp:HiddenField ID="CheckHidden" runat="server" />
                    <asp:HiddenField ID="TempHidden" runat="server" />
                    <table cellpadding="3" cellspacing="1" border="0">
                        <tr class="bgcolor_gray">
                            <%--<td style="width: 5px">
                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                            </td>--%>
                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                <b>No.</b>
                            </td>
                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                <b>Action</b>
                            </td>
                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                <b>Trans No.</b>
                            </td>
                            <td style="width: 130px" class="tahoma_11_white" align="center">
                                <b>File No.</b>
                            </td>
                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                <b>Trans Date</b>
                            </td>
                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                <b>Status</b>
                            </td>
                            <td style="width: 130px" class="tahoma_11_white" align="center">
                                <b>Customer Name</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Year</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Periode</b>
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound"
                            OnItemCommand="ListRepeater_OnItemCommand">
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <%--<td align="center">
                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                    </td>--%>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ViewButton" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="EditButton" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="TransNoLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="FileNotLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="TransdateLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="StatusLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="CustomerNameLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="YearLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="PeriodeLiteral"></asp:Literal>
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
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="AddButton2" runat="server" OnClick="AddButton2_Click" />
                            </td>
                            <%--<td>
                                &nbsp;<asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton2_Click" />
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
    </asp:Panel>
</asp:Content>

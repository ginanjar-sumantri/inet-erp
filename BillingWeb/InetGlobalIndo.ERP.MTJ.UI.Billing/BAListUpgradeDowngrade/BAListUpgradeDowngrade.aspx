<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="BAListUpgradeDowngrade.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.BAListUpgradeDowngrade.BAListUpgradeDowngrade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SearchButton" runat="server">
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
                                                    <asp:ListItem Value="BANo" Text="BA No."></asp:ListItem>
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
                    <table>
                        <tr>
                            <td>
                                Year
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="YearTextBox" runat="server" Width="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Period
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PeriodTextBox" runat="server" Width="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:ImageButton ID="SearchButton" runat="server" OnClick="SearchButton_Click" />
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
                                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                            </td>
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
                                            <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
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
                    <table cellpadding="3" cellspacing="1" border="0">
                        <tr class="bgcolor_gray">
                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                <b>No.</b>
                            </td>
                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                <b>Action</b>
                            </td>
                            <td style="width: 160px" class="tahoma_11_white" align="center">
                                <b>BA No.</b>
                            </td>
                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                <b>Trans Date</b>
                            </td>
                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                <b>Check Billing</b>
                            </td>
                            <td style="width: 160px" class="tahoma_11_white" align="center">
                                <b>Reference No.</b>
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound"
                            OnItemCommand="ListRepeater_ItemCommand">
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <td>
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:ImageButton ID="AddLineButton" runat="server" />
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="BANoLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="TransDateLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:CheckBox runat="server" ID="CheckBillingCheckBox"></asp:CheckBox>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="ReferenceNoTextBox" Width="160" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr class="bgcolor_gray">
                            <td style="width: 1px" colspan="7">
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="boughtItems" runat="server" />
                    <asp:HiddenField ID="itemCount" Value="0" runat="server" />
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
    </asp:Panel>
</asp:Content>

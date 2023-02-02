<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.EmployeeAuthor.EmployeeAuthorView, App_Web_nhn2uf_h" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SetDetailButton" runat="server">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <table cellpadding="3" cellspacing="0" width="0" border="0">
                                    <tr>
                                        <td>
                                            Employee
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="EmployeeTextBox" runat="server" Width="420" ReadOnly="true" BackColor="#CCCCCC">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Transaction Type
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="TransTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TransTypeDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
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
                                                    <td>
                                                    </td>
                                                    <td>
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
                                                <tr style="height: 20px" class="bgcolor_gray">
                                                    <td style="width: 5px" class="tahoma_11_white" align="center">
                                                        <b>No.</b>
                                                    </td>
                                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                                        <b>Transaction Type</b>
                                                    </td>
                                                    <td style="width: 150px" class="tahoma_11_white" align="center">
                                                        <b>Transaction Type Name</b>
                                                    </td>
                                                    <td style="width: 100px" class="tahoma_11_white" align="center">
                                                        <b>Account</b>
                                                    </td>
                                                    <td style="width: 150px" class="tahoma_11_white" align="center">
                                                        <b>Account Name</b>
                                                    </td>
                                                </tr>
                                                <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr style="height: 20px" id="RepeaterListTemplate" runat="server">
                                                            <td align="center">
                                                                <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Literal runat="server" ID="TransTypeLiteral"></asp:Literal>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Literal runat="server" ID="TransTypeNameLiteral"></asp:Literal>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Literal runat="server" ID="AccountLiteral"></asp:Literal>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Literal runat="server" ID="AccountNameLiteral"></asp:Literal>
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
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
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
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="SetDetailButton" runat="server" OnClick="SetDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

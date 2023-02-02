<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FASalesDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FASales.FASalesDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
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
                        <legend>Detail</legend>
                        <table border="0" cellpadding="3" cellspacing="0" width="100%">
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
                                                <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerRepeater_ItemCommand"
                                                    OnItemDataBound="DataPagerRepeater_ItemDataBound">
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
                            <tr>
                                <td align="left">
                                    <asp:Panel ID="Panel3" DefaultButton="GoImageButton" runat="server">
                                        <table cellpadding="0" cellspacing="2" border="0">
                                            <tr>
                                                <td>
                                                    <b>Quick Search :</b>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                        <asp:ListItem Value="Code" Text="Fixed Asset Code"></asp:ListItem>
                                                        <asp:ListItem Value="Name" Text="Fixed Asset Name"></asp:ListItem>
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
                            <tr>
                                <td>
                                    <asp:Label ID="WarningLabel" runat="server" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <asp:HiddenField ID="AllHidden" runat="server" />
                                    <table border="0" cellpadding="3" cellspacing="1" width="0">
                                        <tr class="bgcolor_gray">
                                            <td rowspan="2" style="width: 5px">
                                                <asp:CheckBox ID="AllCheckBox" runat="server" />
                                            </td>
                                            <td align="center" class="tahoma_11_white" rowspan="2" style="width: 5px">
                                                <b>No.</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" rowspan="2" style="width: 120px">
                                                <b>Fixed Asset Code</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" rowspan="2" style="width: 150px">
                                                <b>Fixed Asset Name</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" rowspan="2" style="width: 150px">
                                                <b>Fixed Asset Status</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" rowspan="2" style="width: 150px">
                                                <b>Fixed Asset Owner</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" rowspan="2" style="width: 150px">
                                                <b>Fixed Asset Sub Group</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" rowspan="2" style="width: 150px">
                                                <b>Buy Date</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" colspan="3" style="width: 200px">
                                                <b>Amount</b>
                                            </td>
                                        </tr>
                                        <tr class="bgcolor_gray">
                                            <td align="center" class="tahoma_11_white" style="width: 100px">
                                                <b>Sales</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" style="width: 100px">
                                                <b>Sales (Home)</b>
                                            </td>
                                            <td align="center" class="tahoma_11_white" style="width: 100px">
                                                <b>Fixed Asset (Home)</b>
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
                                                    <td align="center">
                                                        <asp:Literal ID="FACodeLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="FANameLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="FAStatusLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal ID="FAOwnerLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="FASubGroupLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="BuyDateLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal ID="SalesAmountLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal ID="SalesIDRLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal ID="FixedAssetIDRLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="GrapAllCheckBox" runat="server" Text="Grab data from all pages." />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="3" cellspacing="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
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

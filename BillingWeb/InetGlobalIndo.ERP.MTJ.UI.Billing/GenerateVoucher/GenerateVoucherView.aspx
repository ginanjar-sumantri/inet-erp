<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="GenerateVoucherView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.GenerateVoucher.GenerateVoucherView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="BackButton" runat="server" />
                    <asp:ImageButton ID="CreateXMLButton" runat="server" 
                        onclick="CreateXMLButton_Click" />
                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                        <tr class="bgcolor_gray">
                            <th width="5" class="tahoma_11_white">
                                No.
                            </th>
                            <th class="tahoma_11_white">
                                Product Code
                            </th>
                            <th class="tahoma_11_white">
                                Serial Number
                            </th>
                            <th class="tahoma_11_white">
                                PIN
                            </th>
                            <th class="tahoma_11_white">
                                PIN Authentication
                            </th>
                            <th class="tahoma_11_white">
                                Expire Date
                            </th>
                            <th class="tahoma_11_white">
                                Country
                            </th>
                        </tr>
                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                            <ItemTemplate>
                                <tr id="RepeaterItemTemplate" runat="server">
                                    <td align="center">
                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="SNLiteral"></asp:Literal>
                                    </td>
                                    <td align="left">
                                        <asp:Literal runat="server" ID="PINLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="PINAuthenticationLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="ExpireDateLiteral"></asp:Literal>
                                    </td>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="CountryLiteral"></asp:Literal>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr class="bgcolor_gray">
                            <td style="width: 1px" colspan="7">
                            </td>
                        </tr>
                    </table>
                    <asp:ImageButton ID="BackButton2" runat="server" />
                    <asp:ImageButton ID="CreateXMLButton2" runat="server" 
                        onclick="CreateXMLButton_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

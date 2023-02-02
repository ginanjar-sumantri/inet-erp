<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="TermView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Home.Term.TermView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="CancelButton">
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
                        <legend>Header</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Term Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TermCodeTextBox" Width="70" MaxLength="10" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Term Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TermNameTextBox" Width="350" MaxLength="50" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Periode
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PeriodTextBox" runat="server" ReadOnly="true" Width="50" MaxLength="5"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Range
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="RangeTextBox" runat="server" ReadOnly="true" Width="50" MaxLength="5"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                    &nbsp;
                                    <asp:DropDownList ID="TypeRangeDropDownList" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <%--<td>
                                            <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                        </td>--%>
                                            <td>
                                                <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="False" />
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
                        <legend>Detail</legend>
                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr height="20px" class="bgcolor_gray">
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Period</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Type Range</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Range</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Percent Base</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Percent PPn</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterListTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="EditButton" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="PeriodLiteral"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="TypeRangeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="RangeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="PercentBaseLiteral"></asp:Literal>
                                                        %
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="PercentPPnLiteral"></asp:Literal>
                                                        %
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
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

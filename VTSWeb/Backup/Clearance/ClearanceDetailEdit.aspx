<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="ClearanceDetailEdit.aspx.cs" Inherits="VTSWeb.UI.ClearanceDetailEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table>
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" /></b>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                
                    <asp:Label ID="WarningLabel" runat="server" CssClass="warning" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" width="0" border="0">
                    <tr>
                        <td valign="top">
                            Area
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="AreaTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="150"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Purpose
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="PurposeTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="150"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Time In
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DateInTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox><%--<input
                        type="button" id="button" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateInTextBox,'yyyy-mm-dd',this)" />--%>&nbsp;
                            <asp:TextBox ID="HHInTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="30">
                            </asp:TextBox>&nbsp;:
                            <asp:TextBox ID="MMInTextBox" ReadOnly="true" BackColor="#CCCCCC" runat="server"
                                Width="30">
                            </asp:TextBox>
                            &nbsp;WIB
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Time Out
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="DateOutTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox><input
                                type="button" id="button3" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateOutTextBox,'yyyy-mm-dd',this)" />
                            Time (hh-mm)&nbsp;<asp:DropDownList ID="HHOutDropDownList" runat="server">
                            </asp:DropDownList>
                            <asp:DropDownList ID="MMOutDropDownList" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <table cellpadding="3" cellspacing="0" border="0" width="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                        </td>
                        <td>
                            <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

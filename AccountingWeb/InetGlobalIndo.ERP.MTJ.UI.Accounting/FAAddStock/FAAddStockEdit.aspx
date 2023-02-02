<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FAAddStockEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FAAddStock.FAAddStockEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
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
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TransNoTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="150">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                File No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FileNmbrTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="150">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TransDateTextBox" runat="server" Width="100" BackColor="#CCCCCC">
                                </asp:TextBox>
                                <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_TransDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                    <asp:Literal ID="TransDateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                WIS No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="WISNoTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC" MaxLength="20"
                                    runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Received By
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ReceivedByTextBox" runat="server" Width="350" MaxLength="50">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RemarkTextBox" runat="server" TextMode="MultiLine" Width="300" Height="80">
                                </asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

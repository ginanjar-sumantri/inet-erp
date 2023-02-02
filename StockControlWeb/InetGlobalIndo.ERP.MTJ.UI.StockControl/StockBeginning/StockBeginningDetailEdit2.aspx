<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockBeginningDetailEdit2.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning.StockBeginningDetailEdit2" %>

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
                                Serial Number
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SerialNumberTextBox" runat="server" Width="250" ReadOnly="true"
                                    BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PIN
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PINTextBox" runat="server" Width="250">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Manufacture ID
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ManufactureIDTextBox" runat="server" Width="250">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Expiration Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" id="Img1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <asp:ScriptManager ID="scriptMgr" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Currency
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CurrDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CurrDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Forex Rate
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ForexRateTextBox" runat="server" MaxLength="23"></asp:TextBox>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockTransRequestAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockTransRequest.StockTransRequestAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="NextButton" runat="server">
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
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                    <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Source
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WrhsSrcDropDownList" AutoPostBack="true" OnSelectedIndexChanged="WrhsSrcDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="WrhsCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    Text="*" ControlToValidate="WrhsSrcDropDownList" ErrorMessage="Warehouse must be filled"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Destination
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WrhsDestDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="WrhsDestCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    Text="*" ControlToValidate="WrhsDestDropDownList" ErrorMessage="Warehouse Destination must be filled"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Request By
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RequestByTextBox" Width="280" MaxLength="40"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequestByRequiredFieldValidator" runat="server" ErrorMessage="Request By Must Be Filled"
                                    Text="*" ControlToValidate="RequestByTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
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
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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

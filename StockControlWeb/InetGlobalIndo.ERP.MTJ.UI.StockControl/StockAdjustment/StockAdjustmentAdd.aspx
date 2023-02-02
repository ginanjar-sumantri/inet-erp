<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockAdjustmentAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockAdjustment.StockAdjustmentAdd" %>

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
                                    <asp:Literal ID="DateTextLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Opname No.
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="OpnameNoDropDownList" AutoPostBack="true" OnSelectedIndexChanged="OpnameNoDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Warehouse
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="WarehouseDropDownList" AutoPostBack="true" OnSelectedIndexChanged="WarehouseDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Warehouse Must Be Filled"
                                            Text="*" ControlToValidate="WarehouseDropDownList"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Warehouse Subled
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="SubledDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Stock Type
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="StockTypeDropDownList">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Stock Type Must Be Choosed"
                                            Text="*" ControlToValidate="StockTypeDropDownList" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Currency
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="CurrencyDropDownList" AutoPostBack="True" OnSelectedIndexChanged="CurrencyDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ErrorMessage="Currency Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="CurrencyDropDownList"></asp:CustomValidator>
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
                                        <asp:TextBox runat="server" ID="ForexRateTextBox" Width="150" MaxLength="18"></asp:TextBox>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Operator
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="OperatorTextBox" MaxLength="40" Width="280">
                                </asp:TextBox>
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

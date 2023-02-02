<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockReturReceivingAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReturReceiving.StockReturReceivingAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript">
    function CheckUncheck(_prmFgLocationCheckBox, _prmLocationDDL) {
        if (_prmFgLocationCheckBox.checked == true) {
            _prmLocationDDL.disabled = false;
        }
        else if (_prmFgLocationCheckBox.checked == false) {
            _prmLocationDDL.disabled = true;
            _prmLocationDDL.value = "null";
        }
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="NextButton" runat="server">
        <table border="0" cellpadding="3" cellspacing="0" width="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="3" cellspacing="0" width="0">
                        <tr>
                            <td>
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TransDateTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input id="button1" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_TransDateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />--%>
                                <asp:Literal ID="TransDateLiteral" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="CustNameDropDownList" AutoPostBack="true" OnSelectedIndexChanged="CustNameDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Req Retur No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ReqReturNoDropDownList">
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
                                Warehouse Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgLocationCheckBox" runat="server" Text="Single Location" OnCheckedChanged="FgLocationCheckBox_CheckedChanged"
                                    AutoPostBack="true" />
                                <asp:DropDownList ID="WrhsLocationDropDownList" runat="server">
                                </asp:DropDownList>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
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
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="SaveButton_Click" />
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

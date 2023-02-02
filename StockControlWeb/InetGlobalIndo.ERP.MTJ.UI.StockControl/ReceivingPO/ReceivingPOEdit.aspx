<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ReceivingPOEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO.ReceivingPO_ReceivingPOEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript">
        function CheckUncheck(_prmFgLocationCheckBox, _prmLocationDDL) {
            if (_prmFgLocationCheckBox.checked == true) {
                _prmLocationDDL.disabled = false; 
            } 
            else if (_prmFgLocationCheckBox.checked == false){
                _prmLocationDDL.disabled = true;
                _prmLocationDDL.value = "null";
            }
        }
    </script>

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
                                <asp:TextBox ID="TransNoTextBox" runat="server" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                File No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FileNoTextBox" runat="server" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" id="Button1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                            </td>
                            <td>
                                Car No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CarNoTextBox" MaxLength="30" Width="210"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SupplierTextBox" runat="server" Width="300" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Driver
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DriverTextBox" MaxLength="40" Width="280"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PO No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PONoTextBox" Width="160" BackColor="#CCCCCC" ReadOnly="true">
                                </asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                Warehouse
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WarehouseTextBox" ReadOnly="true" Width="280" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                SJ Supplier No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="SJSuppNoTextBox" MaxLength="30" Width="210"></asp:TextBox>
                            </td>
                            <td>
                            </td>
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
                                SJ Supplier Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="SJSuppDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                &nbsp;<%--<input type="button" value="..." id="Img1" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_SJSuppDateTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="SJSuppDateLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                            </td>
                            <td>
                                Warehouse Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgLocationCheckBox" runat="server" Text="Single Location" />
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
                            <td colspan="5">
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

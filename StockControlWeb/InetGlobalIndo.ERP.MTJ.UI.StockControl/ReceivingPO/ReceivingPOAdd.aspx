<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ReceivingPOAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO.ReceivingPOAdd" %>

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

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
    <asp:Literal ID="javascriptReceiver2" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
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
                    <asp:ScriptManager ID="scriptMgr" runat="server" EnablePartialRendering="true" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
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
                                        <%--<input type="button" id="Img1" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)" />--%>
                                        <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                                    </td>
                                    <td width="10px">
                                    </td>
                                    <td>
                                        Car No.
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="CarNoTextBox" Width="210px" MaxLength="210"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        Supplier
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <%--<asp:DropDownList ID="SupplierDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SupplierDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="SupplierCustomValidator" runat="server" ErrorMessage="Supplier Must Be Choosed"
                                            Text="*" ControlToValidate="SupplierDropDownList" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>--%>
                                        <asp:TextBox ID="SupplierTextBox" runat="server" AutoPostBack="true" OnTextChanged="SupplierTextBox_TextChanged"></asp:TextBox>
                                        <asp:Button ID="btnSearchSupplier" runat="server" Text="..." /><br />
                                        &nbsp;
                                        <asp:Label ID="SupplierLabel" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Supplier Must Be Filled"
                                            Text="*" ControlToValidate="SupplierTextBox" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
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
                                        <asp:TextBox ID="FileNoTextBox" runat="server" MaxLength="30" Width="160"></asp:TextBox>
                                        <asp:HiddenField ID="PONoHiddenField" runat="server"></asp:HiddenField>
                                        <%--<asp:DropDownList runat="server" ID="PONoDropDownList">
                                </asp:DropDownList>--%>
                                        <asp:Button ID="btnSearchSJNo" runat="server" Text="..." />
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
                                        <asp:DropDownList runat="server" ID="WarehouseDropDownList" AutoPostBack="true" OnSelectedIndexChanged="WarehouseDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Warehouse Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="WarehouseDropDownList"></asp:CustomValidator>
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
                                        <%--<input type="button" value="..." id="Img2" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_SJSuppDateTextBox,'yyyy-mm-dd',this)" />--%>
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
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="SupplierTextBox" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="WarehouseDropDownList" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
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

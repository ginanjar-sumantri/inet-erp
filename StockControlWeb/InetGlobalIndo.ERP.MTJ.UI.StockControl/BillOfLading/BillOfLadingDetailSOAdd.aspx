<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="BillOfLadingDetailSOAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading.BillOfLadingDetailAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <%--<script type="text/javascript">
        function CheckUncheck(_prmFgLocationCheckBox, _prmLocationDDL) {
            if (_prmFgLocationCheckBox.checked == true) {
                _prmLocationDDL.disabled = false;
            }
            else if (_prmFgLocationCheckBox.checked == false) {
                _prmLocationDDL.disabled = true;
                _prmLocationDDL.value = "null";
            }
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Trans Number
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CustNameTextBox" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="200px">
                                </asp:TextBox><asp:HiddenField ID="CustCodeHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                SO No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="SONoDropDownList">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="SONoDropDownListCustomValidator" runat="server" ErrorMessage="SO No Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="SONoDropDownList"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="WarehouseCodeDropDownList" AutoPostBack="true"
                                            OnSelectedIndexChanged="WarehouseCodeDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="WarehouseCustomValidator" runat="server" ErrorMessage="Warehouse Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="WarehouseCodeDropDownList"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="WrhsSubledDropDownList">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="WrhsSubledCustomValidator" runat="server" ErrorMessage="Warehouse Subled Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="WrhsSubledDropDownList"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <%--<asp:CheckBox ID="FgLocationCheckBox" runat="server" Text="Single Location" />--%>
                                        <asp:DropDownList ID="WrhsLocationDropDownList" runat="server">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="WrhsLocationCustomValidator" runat="server" ErrorMessage="Warehouse Location Subled Must Be Filled"
                                            Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="WrhsLocationDropDownList"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <%--<tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ProductNameDropDownList" AutoPostBack="true"
                                    OnSelectedIndexChanged="ProductNameDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Product Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ProductNameDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="LocationNameDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Location Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="LocationNameDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Unit
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="UnitCodeTextBox" runat="server" Width="210" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyTextBox" runat="server" MaxLength="18" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" ErrorMessage="Qty Must Be Filled"
                                    ControlToValidate="QtyTextBox">*</asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Height="80" Width="300" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
                            </td>
                        </tr>--%>
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

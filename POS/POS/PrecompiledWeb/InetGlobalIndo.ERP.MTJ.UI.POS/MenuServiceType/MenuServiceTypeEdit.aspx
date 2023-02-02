<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.MenuServiceType.MenuServiceTypeEdit, App_Web_kmj3qmyl" %>

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
                                Menu Service Type Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="MenuServiceTypeCodeTextbox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Menu Service Type Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="MenuServiceTypeNameTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:DropDownList runat="server" ID="WarehouseDropDownList" AutoPostBack="true" 
                                    onselectedindexchanged="WarehouseDropDownList_SelectedIndexChanged" >
                                </asp:DropDownList>
                                <asp:CustomValidator ID="WarehouseCustomValidator" runat="server" ErrorMessage="Warehouse Must Be Filled"
                                    Text="*" ControlToValidate="WarehouseDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
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
                                <asp:DropDownList runat="server" ID="WarehouseLocationDropDownList" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="WarehouseLocationCustomValidator" runat="server" ErrorMessage="Warehouse Location Must Be Filled"
                                    Text="*" ControlToValidate="WarehouseLocationDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Deposit in
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WarehouseDepositinDropDownList" AutoPostBack="true">
                                </asp:DropDownList>
                                <%--<asp:CustomValidator ID="WarehouseDepositinCustomValidator" runat="server" ErrorMessage="Warehouse Deposit in Must Be Filled"
                                    Text="*" ControlToValidate="WarehouseDepositinDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Deposit Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="DepositLocationDropDownList" AutoPostBack="true">
                                </asp:DropDownList>
                                <%--<asp:CustomValidator ID="DepositLocationCustomValidator" runat="server" ErrorMessage="Deposit Location Must Be Filled"
                                    Text="*" ControlToValidate="DepositLocationDropDownList" Display="Dynamic" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>--%>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

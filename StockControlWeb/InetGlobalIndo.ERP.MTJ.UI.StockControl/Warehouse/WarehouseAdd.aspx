<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="WarehouseAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Warehouse.WarehouseAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                                Warehouse Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WarehouseCodeTextBox" Width="100" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="WarehouseRequiredFieldValidator" runat="server" ErrorMessage="Warehouse Code Must Be Filled"
                                    Text="*" ControlToValidate="WarehouseCodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WarehouseNameTextBox" Width="280" MaxLength="40"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="WarehouseNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Warehouse Name Must Be Filled" Text="*" ControlToValidate="WarehouseNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WarehouseGroupNameDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="WarehouseGroupCustomValidator" ControlToValidate="WarehouseGroupNameDropDownList"
                                    ErrorMessage="Warehouse Group Must be specified" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Area
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WarehouseAreaDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="WarehouseAreaCustomValidator" ControlToValidate="WarehouseAreaDropDownList"
                                    ErrorMessage="Warehouse Area Must be specified" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="WarehouseTypeDropDownList" AutoPostBack="true"
                                    OnSelectedIndexChanged="WarehouseTypeDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Text="Owner" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Deposit In" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Deposit Out" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="CustomValidator1" ControlToValidate="WarehouseTypeDropDownList"
                                    ErrorMessage="Warehouse Type Must be specified" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Sub Ledger
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="SubledRadioButtonList" runat="server" Enabled="False">
                                    <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Supplier" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Without Subled" Value="N" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="IsActiveCheckBox" runat="server" />
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

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="WarehouseEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.Warehouse.WarehouseEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                <asp:TextBox runat="server" ID="WarehouseCodeTextBox" Width="100" MaxLength="10" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="WarehouseNameTextBox" Width="210" MaxLength="30"></asp:TextBox>
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
                                <asp:DropDownList runat="server" ID="WarehouseTypeDropDownList" AutoPostBack="True"
                                    OnSelectedIndexChanged="WarehouseTypeDropDownList_SelectedIndexChanged">
                                    <asp:ListItem Text="Owner" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Deposit In" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Deposit Out" Value="2"></asp:ListItem>
                                </asp:DropDownList>
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
                                <asp:RadioButtonList ID="SubledRadioButtonList" runat="server">
                                    <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="Supplier" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Without Subled" Value="N"></asp:ListItem>
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

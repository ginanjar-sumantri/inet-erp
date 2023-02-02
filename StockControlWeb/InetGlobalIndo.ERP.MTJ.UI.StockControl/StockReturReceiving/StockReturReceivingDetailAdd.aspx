<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockReturReceivingDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockReturReceiving.StockReturReceivingDetailAdd" %>

<%@ Register src="../ProductPicker.ascx" tagname="ProductPicker" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
        <table width="0" cellpadding="3" cellspacing="0" border="0">
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
                    <table width="0" cellpadding="3" cellspacing="0" border="0">
                        <tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <%--<asp:DropDownList runat="server" ID="ProductNameDropDownList" AutoPostBack="true"
                                    OnSelectedIndexChanged="ProductNameDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="ProductCustomValidator" runat="server" ErrorMessage="Product Must Be Filled"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ProductNameDropDownList"></asp:CustomValidator>--%>
                                <uc1:ProductPicker ID="ProductPicker1" runat="server" />
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
                                <asp:CustomValidator ID="LocationCustomValidator" runat="server" ErrorMessage="Location Must Be Filled"
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
                                <asp:RequiredFieldValidator ID="UnitRequiredFieldValidator" runat="server" ErrorMessage="Unit Code Must Be Filled"
                                    ControlToValidate="UnitCodeTextBox" Text="*"></asp:RequiredFieldValidator>
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
                                <asp:TextBox ID="QtyTextBox" runat="server" Width="150" MaxLength="18"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" ErrorMessage="Qty Must Be Filled"
                                    ControlToValidate="QtyTextBox" Text="*"></asp:RequiredFieldValidator>
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

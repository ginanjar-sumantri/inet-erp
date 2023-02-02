<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockChangeGoodDetailAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockChangeGood.StockChangeGoodDetailAdd" %>

<%@ Register src="../ProductPicker.ascx" tagname="ProductPicker" tagprefix="uc1" %>
<%@ Register src="../ProductPicker2.ascx" tagname="ProductPicker2" tagprefix="uc2" %>

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
                                Product Source
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ProductSrcDropDownList" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ProductSrcDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="ProdSrcCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="ProductSrcDropDownList" Text="*" ErrorMessage="Product Source Must Be Filled"></asp:CustomValidator>--%>
                                <uc1:ProductPicker ID="ProductPicker1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location Source
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="LocationSrcDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="LocSrcCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="LocationSrcDropDownList" Text="*" ErrorMessage="Location Source Must Be Choosed"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product Destination
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ProductDestDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="ProdDestCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="ProductDestDropDownList" Text="*" ErrorMessage="Product Destination Must Be Filled"></asp:CustomValidator>--%>
                                <uc2:ProductPicker2 ID="ProductPicker21" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location Destination
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="LocationDestDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator runat="server" ID="LocDestCustomValidator" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="LocationDestDropDownList" Text="*" ErrorMessage="Location Destination Must Be Choosed"></asp:CustomValidator>
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
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Qty Must Be Filled" ControlToValidate="QtyTextBox"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="UnitTextBox" BackColor="#cccccc" Width="210"></asp:TextBox>
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

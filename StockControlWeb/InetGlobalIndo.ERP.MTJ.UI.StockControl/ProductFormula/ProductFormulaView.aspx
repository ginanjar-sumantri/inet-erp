<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductFormulaView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductFormula.ProductFormulaView" %>

<%@ Register Src="../ProductPicker.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<%@ Register Src="../ProductPicker2.ascx" TagName="ProductPicker2" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel0" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductCodeTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox ID="ProductNameTextBox" runat="server" Width="300" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Product Formula</legend>
                        <table width="0" cellpadding="3" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="AddButton" runat="server" OnClick="AddButton_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="0" border="1" style="border: 1px solid Tan; background-color: LightGoldenrodYellow;">
                                        <tr style="background-color: Tan;">
                                            <td width="10px" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td width="400px" align="center">
                                                <b>Product</b>
                                            </td>
                                            <td width="110px" align="center">
                                                <b>Qty</b>
                                            </td>
                                            <td width="110px" align="center">
                                                <b>Unit</b>
                                            </td>
                                            <td width="110px" align="center">
                                                <b>Main Product</b>
                                            </td>
                                            <td width="60px" align="center">
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="ListRepeater" runat="server" OnItemDataBound="ListRepeater_ItemDataBound"
                                            OnItemCommand="ListRepeater_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="ProductLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal ID="QtyLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal ID="UnitLiteral" runat="server"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:CheckBox ID="MainProductCheckBox" Enabled="false" runat="server" />
                                                    </td>
                                                    <td width="60px" align="center">
                                                        <asp:LinkButton ID="lnkUpdataDatabase" Width="60px" runat="server" CommandName="UpdateDatabase">Delete</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="BackButton" runat="server" CausesValidation="false" OnClick="BackButton_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

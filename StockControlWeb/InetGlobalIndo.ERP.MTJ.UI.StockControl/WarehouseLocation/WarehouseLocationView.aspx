<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="WarehouseLocationView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.WarehouseLocation.WarehouseLocationView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="EditButton" runat="server">
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Warehouse Location Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WLocationCodeTextBox" Width="100" MaxLength="10"
                                    ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Location Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WLocationNameTextBox" Width="350" MaxLength="50"
                                    ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            <%--</td>
                            <td>--%>
                                &nbsp;<asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

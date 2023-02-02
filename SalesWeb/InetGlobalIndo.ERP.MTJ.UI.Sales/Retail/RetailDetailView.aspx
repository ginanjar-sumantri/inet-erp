<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RetailDetailView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Retail.RetailDetailView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                            <td width="80px">
                                Product
                            </td>
                            <td width="10px">
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductTextBox" runat="server" Width="500" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="PhoneTypeTR" runat="server">
                            <td>
                                Phone Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PhoneTypeTextBox" runat="server" Width="500" BackColor="#CCCCCC"
                                    ReadOnly="True">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr id="SerialNmbrTR" runat="server">
                            <td>
                                Serial Number
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SerialNumberTextBox" runat="server" Width="500" BackColor="#CCCCCC"
                                    ReadOnly="True">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                IMEI
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="IMEITextBox" runat="server" Width="200" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox ID="QtyTextBox" runat="server" Width="100" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Price
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PriceTextBox" runat="server" Width="230" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Discount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="DiscTextBox" runat="server" MaxLength="23" Width="230" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountTextBox" runat="server" Width="230" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"
                                    BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

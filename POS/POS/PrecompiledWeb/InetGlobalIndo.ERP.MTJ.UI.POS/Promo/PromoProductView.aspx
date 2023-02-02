<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.Promo.PromoProductView, App_Web_bxcwfygp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="CancelButton">
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
                                Promo Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PromoCodeTextBox" Width="100" MaxLength="50" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ProductCodeTextBox" Width="100" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                                <asp:TextBox runat="server" ID="ProductNameTextBox" Width="200" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td valign="middle" class="width">
                                Minimum Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="FgMinTypeRBL" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                    <asp:ListItem Value="Q" Text="Qty"></asp:ListItem>
                                    <asp:ListItem Value="T" Text="Total"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Minimum Value
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="MinValueTextBox" Width="100" MaxLength="15" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                Maximal Qty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="MaxQtyTextBox" Width="100" MaxLength="15" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" class="width">
                                Promo Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="PromoTypeRBL" runat="server" 
                                    RepeatDirection="Horizontal" Enabled="false">
                                    <asp:ListItem Value="P" Text="Product"></asp:ListItem>
                                    <asp:ListItem Value="D" Text="Discount"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" class="width">
                                Amount Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="AmountTypeRBL" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                    <asp:ListItem Value="A" Text="Amount"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Percentage"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AmountTextBox" Width="100" MaxLength="15" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Free Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FreeProductCodeTextBox" Width="100" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                                <asp:TextBox runat="server" ID="FreeProductNameTextBox" Width="200" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Free Qty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FreeQtyTextBox" Width="100" MaxLength="15" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                FgMultiple
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgMultipleCheckBox" runat="server" Width="20px" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="width">
                                FgActive
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" Enabled="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" MaxLength="500"
                                    TextMode="MultiLine" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
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

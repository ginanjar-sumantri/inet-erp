<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="PromoProductAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.POS.Promo.PromoProductAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
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
                                <asp:TextBox runat="server" ID="ProductCodeTextBox" Width="100" 
                                    ontextchanged="ProductCodeTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ProductCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Product Must Be Filled" Text="*" ControlToValidate="ProductCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" ID="ProductNameTextBox" Width="200"></asp:TextBox>
                                <asp:Button ID="btnSearchProduct" runat="server" CausesValidation="false" Text="..." />
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
                                <asp:RadioButtonList ID="FgMinTypeRBL" runat="server" AutoPostBack="true" 
                                    RepeatDirection="Horizontal" 
                                    onselectedindexchanged="FgMinTypeRBL_SelectedIndexChanged">
                                    <asp:ListItem Value="Q" Text="Qty"></asp:ListItem>
                                    <asp:ListItem Value="T" Text="Total"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <asp:Literal ID="MinimumValueLiteral" runat="server"> </asp:Literal>
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="MinValueTextBox" Width="100" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="MinValueRequiredFieldValidator" runat="server" ErrorMessage="Minimum Value Must Be Filled"
                                    Text="*" ControlToValidate="MinValueTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="MaxQtyTextBox" Width="100" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="MaxQtyRequiredFieldValidator" runat="server" ErrorMessage="Maximum Qty Must Be Filled"
                                    Text="*" ControlToValidate="MaxQtyTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                    RepeatDirection="Horizontal" 
                                    onselectedindexchanged="PromoTypeRBL_SelectedIndexChanged"
                                    AutoPostBack = "true">
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
                                <asp:RadioButtonList ID="AmountTypeRBL" runat="server" RepeatDirection="Horizontal">
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
                                <asp:TextBox runat="server" ID="AmountTextBox" Width="100" MaxLength="15"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="FreeProductCodeTextBox" Width="100" 
                                    ontextchanged="FreeProductCodeTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:TextBox runat="server" ID="FreeProductNameTextBox" Width="200"></asp:TextBox>
                                <asp:Button ID="btnSearchProduct2" runat="server" CausesValidation="false" Text="..." />
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
                                <asp:TextBox runat="server" ID="FreeQtyTextBox" Width="100" MaxLength="15"></asp:TextBox>
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
                                <asp:CheckBox ID="FgMultipleCheckBox" runat="server" Width="20px" />
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
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
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
                                    TextMode="MultiLine"></asp:TextBox>
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

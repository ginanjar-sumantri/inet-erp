<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.ShippingZone.ShippingZonePriceView, App_Web_zhjwwbm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <style type="text/css">
        .width
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
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
                    <fieldset>
                        <legend>Shipping Zone Price</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td class="tahoma_14_black">
                                    <b>
                                        <asp:Literal ID="Literal1" runat="server" />
                                    </b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                Zone
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="CodeTextBox" Width="120" MaxLength="12" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="NameTextBox" Width="250" MaxLength="60" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" class="width">
                                                Product Shape
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ProductShapeTextBox" Width="200" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Currency
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                            <asp:TextBox runat="server" ID="CurrencyTextBox" Width="200" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Weight
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="WeightTextBox" Width="120" MaxLength="13" BackColor="#CCCCCC"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="UnitTextBox" Width="200" BackColor="#CCCCCC"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="PriceTextBox" Width="120" BackColor="#CCCCCC"></asp:TextBox>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td>
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
                                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" TextMode="MultiLine" BackColor="#CCCCCC"></asp:TextBox>
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
                    </fieldset>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

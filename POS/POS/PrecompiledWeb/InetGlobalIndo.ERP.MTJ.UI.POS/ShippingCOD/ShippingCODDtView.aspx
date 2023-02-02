<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.ShippingCOD.ShippingCODDtView, App_Web_fsg-jrvv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <style type="text/css">
        .width
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" > <%--DefaultButton="EditButton"--%>
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
                        <legend>Shipping Cash On Delivery Detail</legend>
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
                                                TransNmbr
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TransNmbrTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Airway Bill
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="AirwayBillTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Vendor
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="VendorTextBox" Width="300" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ShippingTypeTextBox" Width="300" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Product Shape
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="ProductShapeTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="WeightTextBox" Width="60" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="UnitTextBox" Width="240" BackColor="#CCCCCC"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="Price1TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="Price2TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                AmountForex
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="AmountForexTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Disc
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="DiscPercentageTextBox" Width="60" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="DiscForexTextBox" Width="240" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Tax
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TaxTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Package
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="PackageAmountTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Insurance
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="InsuranceAmountTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Transpotation RS
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TRSCodeTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="TRSTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Transpotation RS 2
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TRSCode2TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="TRS2TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Transpotation RS 3
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TRSCode3TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="TRS3TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Other Surcharge
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="OtherSurchargeCodeTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="OtherSurchargeTextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Other Surcharge 2
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="OtherSurchargeCode2TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="OtherSurcharge2TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Other Surcharge 3
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="OtherSurchargeCode3TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="OtherSurcharge3TextBox" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Discovery FS
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="DFSValueTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Other Fee
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="OtherFeeTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="LineTotalForexTextBox" Width="180" BackColor="#CCCCCC"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80px" TextMode="MultiLine"
                                                    BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <%--<td>
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                            </td>--%>
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

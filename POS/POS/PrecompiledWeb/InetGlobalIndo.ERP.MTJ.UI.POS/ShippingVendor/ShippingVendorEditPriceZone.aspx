<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.POS.ShippingVendor.ShippingVendorEditPriceZone, App_Web_v542utgd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <style type="text/css">
        .width
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1">
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
                        <legend>Shipping Vendor</legend>
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
                                                Vendor
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="CodeTextBox" MaxLength="12" Width="180px" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                            <td>
                                                -
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="NameTextBox" Width="300" MaxLength="60" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                            <td>
                                                FgZone
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="FgZoneCheckBox" runat="server" Enabled="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table cellpadding="3" cellspacing="0" border="0" width="0">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="MultiPriceButton" runat="server" OnClick="MultiPriceButton_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="SinglePriceButton" runat="server" OnClick="SinglePriceButton_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <asp:Panel ID="PriceSinglePanel" runat="server">
                <tr>
                    <td>
                        <fieldset>
                            <legend>Shipping Vendor Price Detail</legend>
                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                        <table border="0" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="DataPagerButton" runat="server" OnClick="DataPagerButton_Click" />
                                                                </td>
                                                                <td valign="middle">
                                                                    <b>Page :</b>
                                                                </td>
                                                                <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                                    OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <td>
                                                                            <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                                            <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                                        </td>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="CheckHidden" runat="server" />
                                        <asp:HiddenField ID="TempHidden" runat="server" />
                                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                                            <tr class="bgcolor_gray">
                                                <td style="width: 5px" class="tahoma_11_white" align="center">
                                                    <b>No.</b>
                                                </td>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Action</b>
                                                </td>
                                                <td style="width: 250px" class="tahoma_11_white" align="center">
                                                    <b>Product Shape</b>
                                                </td>
                                                <td style="width: 200px" class="tahoma_11_white" align="center">
                                                    <b>Weight</b>
                                                </td>
                                                <asp:Panel ID="Field1Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field1Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field2Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field2Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field3Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field3Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field4Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field4Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field5Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field5Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field6Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field6Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field7Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field7Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field8Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field8Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field9Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field9Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field10Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field10Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field11Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field11Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field12Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field12Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field13Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field13Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field14Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field14Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field15Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field15Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field16Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field16Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field17Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field17Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field18Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field18Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field19Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field19Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="Field20Panel" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="Field20Literal" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                            </tr>
                                            <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound"
                                                OnItemCommand="ListRepeater_ItemCommand">
                                                <ItemTemplate>
                                                    <tr id="RepeaterItemTemplate" runat="server">
                                                        <td align="center">
                                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton runat="server" ID="EditButton" />
                                                                    </td>
                                                                    <td style="padding-left: 4px">
                                                                        <asp:ImageButton runat="server" ID="SaveButton" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="ProductShapeLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="WeightLiteral"></asp:Literal>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value1TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value2TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value3TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value4TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value5TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value6TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value7TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value8TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value9TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value10TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value11TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value12TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value13TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value14TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value15TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value16TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value17TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value18TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value19TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value20TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="bgcolor_gray">
                                                <td style="width: 1px" colspan="25">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="MultiplePricePanel" runat="server">
                <tr>
                    <td>
                        <fieldset>
                            <legend>Shipping Vendor Multiple Price Detail</legend>
                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:Panel DefaultButton="DataPager2Button" ID="Panel2" runat="server">
                                                        <table border="0" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="DataPager2Button" runat="server" OnClick="DataPager2Button_Click" />
                                                                </td>
                                                                <td valign="middle">
                                                                    <b>Page :</b>
                                                                </td>
                                                                <asp:Repeater EnableViewState="true" ID="DataPagerTop2Repeater" runat="server" OnItemCommand="DataPagerTop2Repeater_ItemCommand"
                                                                    OnItemDataBound="DataPagerTop2Repeater_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <td>
                                                                            <asp:LinkButton ID="PageNumberLink2Button" runat="server"></asp:LinkButton>
                                                                            <asp:TextBox Visible="false" ID="PageNumber2TextBox" runat="server" Width="30px"></asp:TextBox>
                                                                        </td>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="WarningLabel2" CssClass="warning"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="CheckHidden2" runat="server" />
                                        <asp:HiddenField ID="TempHidden2" runat="server" />
                                        <table cellpadding="3" cellspacing="1" width="0" border="0">
                                            <tr class="bgcolor_gray">
                                                <td style="width: 5px" class="tahoma_11_white" align="center">
                                                    <b>No.</b>
                                                </td>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Action</b>
                                                </td>
                                                <td style="width: 250px" class="tahoma_11_white" align="center">
                                                    <b>Product Shape</b>
                                                </td>
                                                <td style="width: 200px" class="tahoma_11_white" align="center">
                                                    <b>Weight 1</b>
                                                </td>
                                                <td style="width: 200px" class="tahoma_11_white" align="center">
                                                    <b>Weight 2</b>
                                                </td>
                                                <asp:Panel ID="FieldPanel1" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral1" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel2" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral2" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel3" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral3" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel4" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral4" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel5" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral5" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel6" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral6" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel7" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral7" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel8" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral8" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel9" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral9" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel10" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral10" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel11" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral11" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel12" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral12" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel13" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral13" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel14" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral14" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel15" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral15" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel16" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral16" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel17" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral17" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel18" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral18" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel19" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral19" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                                <asp:Panel ID="FieldPanel20" runat="server">
                                                    <td style="width: 60px" class="tahoma_11_white" align="center">
                                                        <b>
                                                            <asp:Literal ID="FieldLiteral20" runat="server"></asp:Literal>
                                                        </b>
                                                    </td>
                                                </asp:Panel>
                                            </tr>
                                            <asp:Repeater runat="server" ID="MultipleListRepeater" OnItemCommand="MultipleListRepeater_ItemCommand"
                                                OnItemDataBound="MultipleListRepeater_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr id="RepeaterItemTemplate" runat="server">
                                                        <td align="center">
                                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton runat="server" ID="EditButton" />
                                                                    </td>
                                                                    <td style="padding-left: 4px">
                                                                        <asp:ImageButton runat="server" ID="SaveButton" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="ProductShapeLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="Weight1Literal"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="Weight2Literal"></asp:Literal>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value1TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value2TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value3TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value4TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value5TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value6TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value7TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value8TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value9TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value10TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value11TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value12TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value13TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value14TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value15TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value16TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value17TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value18TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value19TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox runat="server" ID="Value20TextBox" Width="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr class="bgcolor_gray">
                                                <td style="width: 1px" colspan="6">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </asp:Panel>
        </table>
    </asp:Panel>
</asp:Content>

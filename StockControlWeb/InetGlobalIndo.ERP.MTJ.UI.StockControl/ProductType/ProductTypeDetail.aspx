<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductTypeDetail.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductTypeDetail" %>

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
                    <fieldset>
                        <legend>Header</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    Product Type Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ProductTypeCodeTextBox" Width="100" MaxLength="10"
                                        ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Product Type Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ProductTypeNameTextBox" Width="350" MaxLength="50"
                                        ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Product Category
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CategoryName" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Using Price Group
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsUsingPGCheckBox" Enabled="false" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Using Unique ID
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsUsingUniqueIDCheckBox" Enabled="false" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Stock
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="StockLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="105px">
                                    Send To Kitchen
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="SendToKitchenLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    With Tax
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="WithTaxLabel"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div id="TaxDiv" runat="server" visible="false">
                                        <fieldset>
                                            <legend>Tax</legend>
                                            <table>
                                                <tr>
                                                    <td width="95px">
                                                        Tax Type
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TaxTypeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Tax Percentage
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TaxPercentageTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                        %
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <table>
                                            <tr>
                                                <td width="105px">
                                                    Service Charge
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="ServiceChargerTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                    %
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="105px">
                                                    Service Charge Calculate
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="ServiceChargesCalculateLabel" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
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
                                    <asp:CheckBox ID="FgActiveCheckBox" runat="server" Enabled="false" />
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
                                        TextMode="MultiLine" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                                <%--</td>
                                        <td>--%>
                                                &nbsp;<asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" OnClick="BackButton_Click" />
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
            <tr>
                <td>
                    <fieldset>
                        <legend>Detail</legend>
                        <table width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                                <%--</td>
                                        <td>--%>
                                                &nbsp;<asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                                            </td>
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
                                    <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Warehouse Type</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Account Invent</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Account COGS</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Account Sales</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Account WIP</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Account Transit SJ</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Account Transit Wrhs</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Account Transit Reject</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemDataBound="ListRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="WrhsTypeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="AccInventLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="AccCOGSLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="AccSalesLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="AccWIPLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="AccTransitSJLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="AccTransitWrhsLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="AccTransitRejectLiteral"></asp:Literal>
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
            <%-- <tr>
                <td>
                    <asp:Panel ID="panelPG" runat="server">
                        <fieldset>
                            <legend>Price Group</legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="AddButton2" OnClick="AddButton2_Click" />
                                                    &nbsp;<asp:ImageButton runat="server" ID="DeleteButton2" OnClick="DeleteButton2_Click" />
                                                </td>
                                                <td align="right">
                                                    <asp:Panel DefaultButton="DataPagerButton2" ID="DataPagerPanel2" runat="server">
                                                        <table border="0" cellpadding="2" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="DataPagerButton2" runat="server" OnClick="DataPagerButton2_Click" />
                                                                </td>
                                                                <td valign="middle">
                                                                    <b>Page :</b>
                                                                </td>
                                                                <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater2" runat="server" OnItemCommand="DataPagerTopRepeater2_ItemCommand"
                                                                    OnItemDataBound="DataPagerTopRepeater2_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <td>
                                                                            <asp:LinkButton ID="PageNumberLinkButton2" runat="server"></asp:LinkButton>
                                                                            <asp:TextBox Visible="false" ID="PageNumberTextBox2" runat="server" Width="30px"></asp:TextBox>
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
                                        <asp:Label runat="server" ID="Label2" CssClass="warning"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="CheckHidden2" runat="server" />
                                        <asp:HiddenField ID="TempHidden2" runat="server" />
                                        <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                            <tr class="bgcolor_gray">
                                                <td style="width: 5px">
                                                    <asp:CheckBox runat="server" ID="AllCheckBox2" />
                                                </td>
                                                <td style="width: 5px" class="tahoma_11_white" align="center">
                                                    <b>No.</b>
                                                </td>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Price Group Code</b>
                                                </td>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Year</b>
                                                </td>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Currency</b>
                                                </td>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Amount Forex</b>
                                                </td>
                                                <td style="width: 100px" class="tahoma_11_white" align="center">
                                                    <b>Amount Home</b>
                                                </td>
                                            </tr>
                                            <asp:Repeater runat="server" ID="ListRepeater2" OnItemDataBound="ListRepeater2_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr id="RepeaterItemTemplate2" runat="server">
                                                        <td align="center">
                                                            <asp:CheckBox runat="server" ID="ListCheckBox2" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="PGCodeLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="YearLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Literal runat="server" ID="CurrencyLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Literal runat="server" ID="AmountForexLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Literal runat="server" ID="AmountLiteralLiteral"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </fieldset></asp:Panel>
                </td>
            </tr>--%>
        </table>
    </asp:Panel>
</asp:Content>

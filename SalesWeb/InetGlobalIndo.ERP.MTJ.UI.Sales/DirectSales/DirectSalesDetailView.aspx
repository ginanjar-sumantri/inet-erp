<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DirectSalesDetailView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales.DirectSalesDetailView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function BlurAmount(_prmQty, _prmPrice, _prmAmount, _prmDecimalPlace) {
            if (_prmQty.value == 0 || _prmPrice.value == 0) {
                _prmAmount.value = 0;
            }
            else {
                _prmAmount.value = parseFloat(GetCurrency(_prmQty.value)) * parseFloat(GetCurrency2(_prmPrice.value, _prmDecimalPlace.value));
                _prmAmount.value = (_prmAmount.value == 0) ? "0" : FormatCurrency2(_prmAmount.value, _prmDecimalPlace.value);
            }
            _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency2(_prmPrice.value, _prmDecimalPlace.value);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="Panel1">
        <table width="100%">
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
                        <legend>Detail</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table cellpadding="3" cellspacing="0" width="0" border="0">
                                                <tr>
                                                    <td>
                                                        Product
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="ProductCodeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                                            Width="200"></asp:TextBox>&nbsp;
                                                        <asp:TextBox ID="ProductNameTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                                            Width="400"></asp:TextBox>
                                                        <asp:HiddenField ID="TransNmbrHiddenField" runat="server"></asp:HiddenField>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Qty Order
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="QtyOrderTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Unit Order
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="UnitOrderTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                                        <asp:TextBox ID="PriceTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                        <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
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
                                                        <asp:TextBox ID="AmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Warehouse
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="WarehouseCodeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                                            Width="400"></asp:TextBox>
                                                        <%-- <asp:DropDownList runat="server" ID="WarehouseCodeDropDownList" BackColor="#CCCCCC" ReadOnly ="true">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="FgSubLedHiddenField" runat="server"></asp:HiddenField>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Warehouse Subled
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="WarehouseSubledTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                                            Width="400"></asp:TextBox>
                                                        <%--<asp:DropDownList runat="server" ID="WrhsSubledDropDownList" BackColor="#CCCCCC" ReadOnly ="true">
                                        </asp:DropDownList>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Warehouse Location
                                                    </td>
                                                    <td>
                                                        :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="WarehouseLocationTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                                            Width="400"></asp:TextBox>
                                                        <%--<asp:DropDownList runat="server" ID="LocationNameDropDownList" BackColor="#CCCCCC" ReadOnly ="true">
                                        </asp:DropDownList>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                                <asp:ImageButton ID="BackButton" runat="server" CausesValidation="False" OnClick="BackButton_Click" />
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
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel2">
        <table width="100%">
            <tr>
                <td>
                    <fieldset>
                        <legend>Serial Number</legend>
                        <table width="0" cellpadding="3" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                                &nbsp;<asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                                                &nbsp;<asp:Literal runat="server" ID="GetFormatExcelLiteral" />
                                                &nbsp;<asp:ImageButton runat="server" ID="ImportButton" OnClick="ImportButton_Click" />
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
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Serial Number</b>
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
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="SerialNumberLiteral"></asp:Literal>
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
        </table>
    </asp:Panel>
</asp:Content>

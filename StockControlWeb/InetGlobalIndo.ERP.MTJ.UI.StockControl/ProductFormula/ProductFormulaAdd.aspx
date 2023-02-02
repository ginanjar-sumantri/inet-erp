<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="ProductFormulaAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.ProductFormula.ProductFormulaAdd" %>

<%@ Register Src="../ProductPickerForFormula.ascx" TagName="ProductPickerForFormula"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function numericInput(x) {
            if (isNaN(x.value))
                x.value = "0";
        }
    </script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
    <asp:Literal ID="StyleSheetLiteral" runat="server" />

    <script language="javascript" type="text/javascript">
        function deleteItem(x, y, z) {
            _boughtItems = document.getElementById(x).value.split("^");
            _newBoughtItems = "";
            _ctrItems = 1;
            for (i = 0; i < _boughtItems.length; i++) {
                _boughtItem = _boughtItems[i].split('|');
                if (_boughtItem[0] != y) {
                    if (_newBoughtItems == "") {
                        _newBoughtItems = _ctrItems + "|" + _boughtItem[1] + "|" + _boughtItem[2] + "|";
                    } else {
                        _newBoughtItems += "^" + _ctrItems + "|" + _boughtItem[1] + "|" + _boughtItem[2] + "|";
                    }
                    _newBoughtItems += _boughtItem[3];
                    _ctrItems++;
                }
            }
            document.getElementById(x).value = _newBoughtItems;
            document.getElementById(z).value = document.getElementById(z).value - 1;
            document.forms[0].submit();
        }        
    </script>

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
                        <asp:Panel runat="server" ID="ProductPickerPanel">
                            <tr>
                                <td>
                                    Product
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <uc2:ProductPickerForFormula ID="ProductPicker2" runat="server" />
                                    <asp:HiddenField ID="tempProductCode" runat="server" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="ProductChoosePanel">
                            <tr>
                                <td>
                                    Product
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ProductCodeHdTextBox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="ProductNameHdTextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
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
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table style="border: 1px solid Tan; background-color: LightGoldenrodYellow;">
                                        <tr style="background-color: Tan;">
                                            <td width="10px" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td width="500px" align="center">
                                                <b>Product</b>
                                            </td>
                                            <td width="80px" align="center">
                                                <b>Qty</b>
                                            </td>
                                            <td width="110px" align="center">
                                                <b>Unit</b>
                                            </td>
                                            <td width="80px" align="center">
                                                <b>Main Product</b>
                                            </td>
                                            <td width="60px" align="center">
                                                <b>Action</b>
                                            </td>
                                        </tr>
                                        <tbody class="posTBodyScroll2">
                                            <asp:Repeater runat="server" ID="ListRepeater" OnItemCommand="ListRepeater_ItemCommand"
                                                OnItemDataBound="ListRepeater_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr id="RepeaterListTemplate" runat="server">
                                                        <td align="center">
                                                            <asp:Literal runat="server" ID="NoLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="ProductLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Literal runat="server" ID="QtyLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Literal runat="server" ID="UnitLiteral"></asp:Literal>
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton runat="server" ID="CheckedImageButton" />
                                                            <asp:ImageButton runat="server" ID="UnCheckImageButton" />
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="DeleteImageButton" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="CodeHiddenField" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Literal ID="perulanganDataDibeli" runat="server"></asp:Literal>
                                            <asp:Panel ID="panelAddRow" runat="server">
                                                <tr>
                                                    <td width="10px" align="center">
                                                    </td>
                                                    <td width="400px">
                                                        <%--<asp:Literal ID="ProductLiteral" runat="server"></asp:Literal><br />--%>
                                                        <%--<uc2:ProductPicker2 ID="ProductPicker2" runat="server" />--%>
                                                        <asp:TextBox ID="ProductCodeTextBox" Width="150" runat="server" OnTextChanged="ProductCodeTextBox_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                        <asp:Button ID="SearchProductCodeButton" runat="server" Text="..." />
                                                        <asp:HiddenField ID="ProductNameHiddenField" runat="server"></asp:HiddenField>
                                                        <asp:TextBox ID="ProductNameTextBox" ReadOnly="true" Width="300" runat="server"></asp:TextBox>
                                                        <%--<asp:HiddenField ID="tempProductCode2" runat="server" />--%>
                                                    </td>
                                                    <td width="80px" align="right">
                                                        <%-- <asp:Literal ID="QtyLiteral" runat="server"></asp:Literal><br />--%>
                                                        <asp:TextBox ID="QtyTextBox" runat="server" Width="100"></asp:TextBox>
                                                    </td>
                                                    <td width="110px">
                                                        <asp:HiddenField ID="UnitHiddenField" runat="server"></asp:HiddenField>
                                                        <asp:TextBox ID="UnitTextBox" runat="server" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                    </td>
                                                    <td width="80px" align="center">
                                                        <%-- <asp:Literal ID="QtyLiteral" runat="server"></asp:Literal><br />--%>
                                                        <asp:CheckBox ID="FgMainProductCheckBox" runat="server" Width="100" />
                                                        <%--<input type=checkbox o   --%>
                                                    </td>
                                                    <td width="60px" align="center">
                                                        <asp:Button ID="AddLineButton" runat="server" Text="SAVE" OnClick="AddLineButton_Click" />
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                        </tbody>
                                    </table>
                                    <asp:HiddenField ID="boughtItems" runat="server" />
                                    <asp:HiddenField ID="itemCount" Value="0" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />--%>
                    <asp:ImageButton ID="BackButton" runat="server" CausesValidation="false" OnClick="BackButton_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="StockBeginningDetailEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.StockBeginning.StockBeginningDetailEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    function Adjust(_prmQty, _prmPrice, _prmTotal)
    {
//        if (_prmQty.value > 0)
//        {
//            _prmAdjust.innerHTML = '+';
//            _prmHidden.value = '+';
//        }
//        else
//        {
//            _prmAdjust.innerHTML = '-';
//            _prmHidden.value = '-';
//        }
        
         var _total = parseFloat(GetCurrency(_prmQty.value)) * parseFloat(GetCurrency(_prmPrice.value));
        _prmTotal.value = (_total == 0 ) ? "0" : FormatCurrency(_total);
        
        _prmQty.value = (_prmQty.value == 0) ? "0" : GetCurrency(_prmQty.value);
        
        _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency(_prmPrice.value);
    }
    
    function Calculate(_prmQty, _prmPrice, _prmTotal)
    {
        var _total = parseFloat(GetCurrency(_prmQty.value)) * parseFloat(GetCurrency(_prmPrice.value));
        _prmTotal.value = (_total == 0) ? "0" : FormatCurrency(_total);
        
        _prmQty.value = (_prmQty.value == 0) ? "0" : GetCurrency(_prmQty.value);
        
        _prmPrice.value = (_prmPrice.value == 0) ? "0" : FormatCurrency(_prmPrice.value);
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductTextBox" runat="server" Width="420" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LocationTextBox" runat="server" Width="350" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
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
                                <asp:TextBox ID="QtyTextBox" runat="server" MaxLength="18" Width="150"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QtyRequiredFieldValidator" runat="server" Text="*"
                                    ErrorMessage="Qty Must Be Filled" ControlToValidate="QtyTextBox"></asp:RequiredFieldValidator>
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
                                <asp:TextBox runat="server" ID="UnitTextBox" Width="210" BackColor="#cccccc" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <%-- <tr>
                            <td>
                                Adjust
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:Label ID="FgAdjustLabel" runat="server"></asp:Label>
                                <asp:HiddenField ID="AdjustHidden" runat="server" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                Price Cost
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PriceTextBox" runat="server" Width="150" MaxLength="18"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Cost
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalTextBox" runat="server" Width="150" MaxLength="18" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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

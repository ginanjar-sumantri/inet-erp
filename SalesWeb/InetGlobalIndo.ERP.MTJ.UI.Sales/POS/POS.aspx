<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POS.aspx.cs" Inherits="POS_POS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POINT OF SALES</title>
    <asp:Literal ID="StyleSheetLiteral" runat="server" />
    
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function numericInput(x) {
            if (isNaN(x.value))
                x.value = "0";
        }
    </script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>

    <script language="javascript" type="text/javascript">
        function deleteItem(x,y,z) {
            _boughtItems = document.getElementById(x).value.split ( "^" );
            _newBoughtItems = "" ;
            _ctrItems = 1;
            for (i = 0; i < _boughtItems.length; i++) {
                _boughtItem = _boughtItems[i].split('|');
                if (_boughtItem[0] != y) {
                    if (_newBoughtItems == "") {
                        _newBoughtItems = _ctrItems + "|" + _boughtItem[1] + "|" + _boughtItem[2] + "|";
                    } else {
                        _newBoughtItems += "^" + _ctrItems + "|" + _boughtItem[1] + "|" + _boughtItem[2] + "|";
                    }
                    _newBoughtItems += _boughtItem[3] + "|" + _boughtItem[4] + "|" + _boughtItem[5] + "|" + _boughtItem[6];
                    _ctrItems++;
                }
            }
            document.getElementById(x).value = _newBoughtItems;
            document.getElementById(z).value = document.getElementById(z).value - 1;
            document.forms[0].submit();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            TRANS NO
                                        </td>
                                        <td>
                                            <asp:Literal ID="transNo" runat="server"></asp:Literal>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td>
                                            DATE
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="transDate" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                            <%--<input type="button" id="birth_date_start" value="..." onclick="displayCalendar(transDate,'yyyy-mm-dd',this)" />--%>
                                            <asp:Literal ID="TransDateLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CURRENCY
                                        </td>
                                        <td>                                            
                                            <%--<asp:TextBox ID="currency" runat="server"></asp:TextBox><br />--%>
                                            <asp:DropDownList ID="currencyX" runat="server" AutoPostBack="true" 
                                                onselectedindexchanged="currencyX_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:TextBox ID="forexRateTextBox" Text="1" runat="server" Width="75"></asp:TextBox>
                                            <asp:HiddenField ID="forexRateHiddenField" Value="1" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            FILE NO
                                        </td>
                                        <td>
                                            <asp:Literal ID="fileNo" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CUSTOMER
                                        </td>
                                        <td>
                                            <asp:TextBox ID="customer" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                            <asp:HiddenField ID="custCode" runat="server"></asp:HiddenField>
                                            <asp:Button ID="btnSearchCustomer" runat="server" Text="..." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SALES
                                        </td>
                                        <td>
                                            <asp:TextBox ID="namaSales" BackColor="#CCCCCC" runat="server"></asp:TextBox>
                                            <asp:HiddenField ID="empNumb" runat="server"></asp:HiddenField>
                                            <asp:Button ID="btnSrcEmployee" runat="server" Text="..." />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td align="center">
                                            POINT OF SALES LOGO
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 350px; text-align: right; background-color: Black; color: White;
                                            font-family: 'Arial'; font-size: 60px; font-weight: bold">
                                            <asp:Label ID="totalPriceBesar" Text="0" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table cellspacing="0" border="1">
                        <tr>
                            <th width="20">
                                NO
                            </th>
                            <th width="155">
                                PRODUCT CODE
                            </th>
                            <th width="230">
                                PRODUCT NAME
                            </th>
                            <th width="30">
                                QTY
                            </th>
                            <th width="30">
                                UOM
                            </th>
                            <th width="100">
                                PRICE
                            </th>
                            <th width="100">
                                LINE TOTAL
                            </th>
                            <th width="50">
                                SAVE / DELETE
                            </th>
                            <th width="15">
                            </th>
                        </tr>
                        <tbody class="posTBodyScroll">
                            <asp:Literal ID="perulanganDataDibeli" runat="server"></asp:Literal>
                            <asp:Panel ID="panelAddRow" runat="server">
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox ID="productCode" Width="115" runat="server" 
                                        ontextchanged="productCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <asp:Button ID="btnSearchProductCode" runat="server" Text="..." />
                                </td>
                                <td>
                                    <asp:HiddenField ID="productName" runat="server"></asp:HiddenField>
                                    <asp:TextBox ID="txtProductName" ReadOnly="true" Width="230" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="qty" Width="35" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:HiddenField ID="uom" runat="server"></asp:HiddenField>
                                    <asp:TextBox ID="txtUom" Width="35" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:HiddenField ID="price" Value="0" runat="server"></asp:HiddenField>
                                    <asp:TextBox ID="txtPrice" Width="100" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:HiddenField ID="lineTotal" Value="0" runat="server"></asp:HiddenField>
                                    <asp:TextBox ID="txtLineTotal" Width="100" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnAddLine" runat="server" Text="SAVE" OnClick="btnAddLine_Click" />
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr runat="server" visible="false" id="bottomSpanNota"><td colspan="8"></td></tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="boughtItems" runat="server" />
                    <asp:HiddenField ID="itemCount" Value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            PAYMENT TYPE
                                        </td>
                                        <td>
                                            <%--<asp:TextBox ID="paymentType" runat="server"></asp:TextBox>--%>
                                            <asp:DropDownList ID="paymentDDL" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CREDIT/DEBIT CARD #
                                        </td>
                                        <td>
                                            <asp:TextBox ID="creditCardNo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            BANK NAME
                                        </td>
                                        <td>
                                            <asp:TextBox ID="bankName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            MONEY CASH AMOUNT
                                        </td>
                                        <td>
                                            <asp:TextBox ID="cashAmount" Text="0" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            RETURN CASH
                                        </td>
                                        <td>
                                            <asp:TextBox ID="returnCash" ReadOnly="true" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr><td colspan="2"><asp:Label ID="CashWarningLabel" runat="server"></asp:Label></td></tr>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td valign="top">
                                            NOTE
                                        </td>
                                        <td>
                                            <asp:TextBox ID="note" runat="server" Width="270" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPosting" runat="server" Width="90px" Height="45px" Text="POSTING (F4)"
                                                Style="background-color: Red; font-size: 12px" onclick="btnPosting_Click"></asp:Button>
                                            <asp:Button ID="btnPrint" runat="server" Width="90px" Height="45px" Text="PRINT (F5)"
                                                Style="background-color: Red; font-size: 12px" onclick="btnPrint_Click"></asp:Button>
                                            <asp:Button id="btnNewTrans" runat="server" Width="90px" Height="45px" Text="NEW TRANS"
                                                Style="background-color: Red; font-size: 12px" onclick="btnNewTrans_Click"></asp:Button>
                                            <asp:HiddenField ID="postingState" runat="server" Value="0"></asp:HiddenField>
                                            <asp:HiddenField ID="hiddenTransNumber" runat="server" Value="0"></asp:HiddenField>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            SUB TOTAL
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="subTotal" Value="0" runat="server"></asp:HiddenField>
                                            <asp:TextBox ID="txtSubTotal" Text="0" ReadOnly="true" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            DISCOUNT
                                        </td>
                                        <td>
                                            <asp:TextBox ID="discount" Width="30px" Text="0" runat="server"></asp:TextBox>
                                            %
                                            <asp:TextBox ID="discountValue" Width="90" Text="0" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TAX
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tax" Width="30px" Text="0" runat="server"></asp:TextBox>
                                            %
                                            <asp:TextBox ID="taxValue" Width="90px" ReadOnly="true" Text="0" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Additional Fee
                                        </td>
                                        <td>
                                            <asp:TextBox ID="additionalFee" Text="0" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TOTAL VALUE
                                        </td>
                                        <td>
                                            <asp:TextBox ID="totalValue" ReadOnly="true" Text="0" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

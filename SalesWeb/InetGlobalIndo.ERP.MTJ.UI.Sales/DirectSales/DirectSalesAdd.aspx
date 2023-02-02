<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DirectSalesAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales.DirectSalesAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script src="../JQuery/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function numericInput(x) {
            if (isNaN(x.value))
                x.value = "0";
        }
    </script>

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
                    _newBoughtItems += _boughtItem[3] + "|" + _boughtItem[4] + "|" + _boughtItem[5] + "|" + _boughtItem[6] + '|' + _boughtItem[7] + '|' + _boughtItem[8];
                    _ctrItems++;
                }
            }
            document.getElementById(x).value = _newBoughtItems;
            document.getElementById(z).value = document.getElementById(z).value - 1;
            document.forms[0].submit();
        }

        function editBoughtItem(x) {
            $("#ctl00_DefaultBodyContentPlaceHolder_EditRowHiddenField").attr('value', x);
            document.forms[0].submit();
        }

    </script>

    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
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
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td width="310px">
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <%--<input type="button" id="birth_date_start" value="..." onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)" />--%>
                                <asp:Literal ID="DateLiteral" runat="server"></asp:Literal>
                            </td>
                            <td>
                            </td>
                            <td>
                                Payment Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="PayTypeDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="PayTypeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Payment Type Must Be Filled" Text="*" ControlToValidate="PayTypeDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustNmbrTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Button ID="btnSearchCustomer" runat="server" Text="..." CausesValidation="False" />
                            </td>
                            <td>
                            </td>
                            <td>
                                Base Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="BaseForexTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="CurrDropDownList" AutoPostBack="true" OnSelectedIndexChanged="CurrDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrDropDownList">
                                        </asp:CustomValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="CurrDropDownList" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                            </td>
                            <td>
                                Discount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="discount" Width="30px" Text="0" runat="server"></asp:TextBox>
                                %
                                <asp:TextBox ID="discountValue" Width="90" Text="0" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Forex Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="ForexRateTextBox" runat="server" ></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                            </td>
                            <td>
                                PPN
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="tax" Width="30px" Text="0" runat="server"></asp:TextBox>
                                %
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td rowspan="4">
                                <asp:TextBox ID="Remark" runat="server" Width="270" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                PPN Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PPNAmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td colspan="4">
                            </td>
                            <td>
                                Stamp Fee
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="StampFeeTextBox" runat="server" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td colspan="4">
                            </td>
                            <td>
                                Other Fee
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="OtherFeeTextBox" runat="server" Text="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                            <td>
                                Total Amount
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TotalAmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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
                            <th width="200">
                                PRODUCT NAME
                            </th>
                            <th width="30">
                                QTY
                            </th>
                            <th width="30">
                                Unit
                            </th>
                            <th>
                                PRICE
                            </th>
                            <th>
                                TOTAL AMOUNT
                            </th>
                            <th width="80">
                                WAREHOUSE
                            </th>
                            <th>
                                WAREHOUSE SUBLED
                            </th>
                            <th>
                                WAREHOUSE LOCATION
                            </th>
                            <th width="50">
                                SAVE / DELETE
                            </th>
                        </tr>
                        <tbody class="posTBodyScroll">
                            <asp:Literal ID="perulanganDataDibeli" runat="server"></asp:Literal>
                            <asp:Panel ID="panelAddRow" runat="server">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="productCode" Width="65" runat="server" OnTextChanged="productCode_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                        <asp:Button ID="btnSearchProductCode" runat="server" Text="..." Width="20px" />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="productName" runat="server"></asp:HiddenField>
                                        <asp:TextBox ID="txtProductName" ReadOnly="true" Width="159" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="qty" Width="35" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="UnitDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UnitDropDownList_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="PriceTextBox" runat="server" Width="85"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="LineTotalHiddenField" runat="server"></asp:HiddenField>
                                        <asp:TextBox ID="LineTotalTextBox" runat="server" Width="100"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="WarehouseCodeDropDownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="WarehouseCodeDropDownList_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="WarehouseCodeDropDownList" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="WrhsSubledDropDownList">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="LocationNameDropDownList">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddLine" runat="server" Text="SAVE" OnClick="btnAddLine_Click" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr runat="server" visible="false" id="bottomSpanNota">
                                <td colspan="11">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="UnitHiddenField" runat="server" />
                    <asp:HiddenField runat="server" ID="EditRowHiddenField" />
                    <asp:HiddenField runat="server" ID="EditPostBackHiddenField" />
                    <asp:HiddenField runat="server" ID="SaveRowHiddenfield" />
                    <asp:HiddenField ID="boughtItems" runat="server" />
                    <asp:HiddenField ID="itemCount" Value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="postingState" runat="server" Value="0"></asp:HiddenField>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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

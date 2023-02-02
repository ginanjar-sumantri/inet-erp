<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.DirectPurchase.DirectPurchaseEdit, App_Web_-xa4oz-a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function numericInput(x) {
            if (isNaN(x.value))
                x.value = "0";
        }

    </script>

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
                            <td colspan="3">
                                <asp:Label runat="server" ID="Label" CssClass="warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TransNoTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="160"></asp:TextBox>
                                <asp:HiddenField ID="StatusHiddenField" runat="server" />
                            </td>
                            <td width="10px">
                            </td>
                            <td>
                                File No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FileNmbrTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                    Width="160"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                            </td>
                            <td>
                                Trans Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="TransTypeDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TransTypeDDL_SelectedIndexChanged">
                                    <asp:ListItem Text="Direct Payment" Value="CS"></asp:ListItem>
                                    <asp:ListItem Text="Account Payable" Value="AP"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
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
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Supplier
                            </td>
                            <td>
                                :
                            </td>
                            <td rowspan="2">
                                <asp:TextBox ID="SuppNmbrTextBox" runat="server" Width="150" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:Button ID="btnSearchSupplier" runat="server" Text="..." CausesValidation="False" />
                                <br />
                                <asp:TextBox ID="SupplierNameTextBox" runat="server" Width="300" BackColor="#CCCCCC"></asp:TextBox>
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
                            <td colspan="4">
                            </td>
                            <td>
                                Disc Amount
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
                                Currency
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="CurrTextBox" BackColor="#CCCCCC" ReadOnly="true">
                                </asp:TextBox>
                                <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
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
                                <asp:TextBox ID="PPNTextBox" Width="30px" Text="0" runat="server"></asp:TextBox>
                                %
                                <asp:TextBox ID="PPNAmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="ForexRateTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                PPH
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PPHTextBox" runat="server" Text="0" Width="30px"></asp:TextBox>
                                %&nbsp;<asp:TextBox ID="PPHAmountTextBox" runat="server" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox ID="Remark" runat="server" Width="270" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                            <td>
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
                            <td>
                                <asp:ImageButton ID="ViewDetailButton" runat="server" CausesValidation="False" OnClick="ViewDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

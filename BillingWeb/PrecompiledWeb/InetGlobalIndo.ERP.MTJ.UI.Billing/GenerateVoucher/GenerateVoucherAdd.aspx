<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.GenerateVoucher.GenerateVoucherAdd, App_Web_rphxpejo" %>

<%@ Register Src="../ProductPicker.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
        <table cellpadding="3" cellspacing="0" width="0" border="0">
            <tr>
                <td class="tahoma_14_black" colspan="3">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Series
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="SeriesTextBox" Width="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Product
                </td>
                <td>
                    :
                </td>
                <td>
                    <uc1:ProductPicker ID="ProductPicker1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Trans Date
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="TransDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                    <input id="TransDateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_TransDateTextBox,'yyyy-mm-dd',this)"
                        value="..." />
                </td>
            </tr>
            <tr>
                <td>
                    Expire Date
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ExpireDate" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                    <input id="ExpireDateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_ExpireDate,'yyyy-mm-dd',this)"
                        value="..." />
                </td>
            </tr>
            <tr>
                <td>
                    Card Value
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="CardValueTextBox" Width="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Produce Quantity
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="ProduceQtyTextBox" Width="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Generate Pin Authentication
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="GeneratePINAuth" Text="Generate Authentication PIN" Width="100"></asp:CheckBox>
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
                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                    <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                    characters left
                </td>
            </tr>
            <tr>
                <td colspan="3">
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

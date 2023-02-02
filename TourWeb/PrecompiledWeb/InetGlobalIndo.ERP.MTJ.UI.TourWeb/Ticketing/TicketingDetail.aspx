<%@ page title="" language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.Ticketing.TicketingDetail, App_Web_dab5jygm" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <asp:Panel ID="ReasonPanel" runat="server" Visible="false">
                <tr>
                    <td>
                        <fieldset>
                            <legend>Reason</legend>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Insert Reason UnPosting
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <asp:TextBox ID="ReasonTextBox" runat="server" MaxLength="500" Width="400px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReasonRequiredFieldValidator" runat="server" Text="*"
                                            ErrorMessage="Reason Text Box Must Be Filled" ControlToValidate="ReasonTextBox"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="YesButton" runat="server" Text="Yes" OnClick="YesButton_OnClick" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="NoButton" runat="server" Text="No" OnClick="NoButton_OnClick" CausesValidation="False" />
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
            <tr>
                <td>
                    <fieldset>
                        <legend>Header</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td colspan="7">
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Trans No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="TransNoTextBox" Width="160" ReadOnly="true" BackColor="#CCCCCC"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    File No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="FileNmbrTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                        Width="160">
                                    </asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td width="15px">
                                    &nbsp;
                                </td>
                                <td>
                                    Payment Type
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PaymentTypeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Branch
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="BranchTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Payment
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PaymentTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CustCodeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Sales
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="SalesTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CustNameTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Remark
                                </td>
                                <td>
                                    :
                                </td>
                                <td rowspan="2">
                                    <asp:TextBox ID="RemarkTextBox" runat="server" TextMode="MultiLine" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Member Barcode
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="MemberBarcodeTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="CashierCodeRequiredFieldValidator" runat="server"
                                                ErrorMessage="Cashier Code Must Be Filled" Text="*" ControlToValidate="CashierCodeTextBox"
                                                Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Customer Phone
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="CustPhoneTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Currency / Rate
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox runat="server" ID="CurrCodeTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="80"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="CurrRateTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="5">
                                    <table width="0">
                                        <tr class="bgcolor_gray" height="20">
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>PPN %</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Date</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Rate</b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PPN
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <table>
                                        <tr>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNNoTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNDateTextBox" runat="server" Width="70" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNRateTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="5">
                                    <table width="0">
                                        <tr class="bgcolor_gray" height="20">
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Discount %</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Discount Forex</b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Discount
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="5">
                                    <table>
                                        <tr>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="DiscPercentTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="DiscForexTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="5">
                                    <table>
                                        <tr class="bgcolor_gray" height="20">
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Currency</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Base Forex</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>PPN Forex</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Other Forex</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Total Forex</b>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Amount
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <table>
                                        <tr>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="AmountBaseTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="OtherForexTextBox" runat="server" Width="100px" ReadOnly="true"
                                                    BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100px" ReadOnly="true"
                                                    BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                    <asp:HiddenField ID="StatusHiddenField" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="CreateJurnalImageButton" runat="server" OnClick="CreateJurnalImageButton_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <asp:DropDownList ID="CreateJurnalDDL" runat="server">
                                        <asp:ListItem Value="1" Text="1. Journal Entry Print Preview"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2. Journal Entry Print Preview Home Curr"></asp:ListItem>
                                    </asp:DropDownList>
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
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="AddButton" runat="server" OnClick="AddButton_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                            </td>
                                            <td>
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
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 160px" class="tahoma_11_white" align="center">
                                                <b>Booking Code</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Ticket Type</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>AirLines</b>
                                            </td>
                                            <td style="width: 160px" class="tahoma_11_white" align="center">
                                                <b>Ticket Date</b>
                                            </td>
                                            <td style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Selling Price</b>
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
                                                    <td align="left" style="width: 110px">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <%--<td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>--%>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton" />
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                    <asp:ImageButton runat="server" ID="RevisedButton" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="BookingCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="TicketTypeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="AirLinesLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="TicketDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="SellingPriceLiteral"></asp:Literal>
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
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel2">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" runat="server">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel3">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer2" Width="100%" runat="server">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel4">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer3" Width="100%" runat="server">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

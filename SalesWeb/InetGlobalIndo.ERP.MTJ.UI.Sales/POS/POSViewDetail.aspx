<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="POSViewDetail.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.POS.POSViewDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
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
                    <fieldset>
                        <legend>Header</legend>
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
                                <td>
                                    Trans Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Employee No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="EmployeeNoTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="CustNameTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="400px"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="PaymentTypeTextBox" Width="100" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Bank Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="BankNameTextBox" runat="server" Width="150" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Payment Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PaymentCodeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Currency Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CurrCodeTextBox" ReadOnly="true" BackColor="#CCCCCC"
                                        Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Forex Rate
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Base Forex
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="BaseForexTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="TotalAmountTextBox" Width="100" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Disc. Percent
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="DiscPercentTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Disc. Amount
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="DiscAmountTextBox" Width="100" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PPN Percent
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="PPNPercentTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="PPNAmountTextBox" Width="100" BackColor="#CCCCCC"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Additional Fee
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td valign="top">
                                    <asp:TextBox ID="AdditionalFeeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td valign="top">
                                    Remark
                                </td>
                                <td valign="top">
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="RemarkTextBox" TextMode="MultiLine" BackColor="#CCCCCC"
                                        Width="300" Height="100" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <%--<td>
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                            </td>--%>
                                            <td>
                                                <asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                            </td>
                                            <%--<td>
                                                <asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                                            </td>--%>
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
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <%--<asp:ImageButton ID="AddButton" runat="server" OnClick="AddButton_Click" />--%>
                                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                                <%--&nbsp;<asp:Literal runat="server" ID="GetFormatExcelLiteral" />--%>
                                                <%--&nbsp;<asp:ImageButton runat="server" ID="ImportButton" OnClick="ImportButton_Click" />--%>
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
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <asp:HiddenField ID="DescriptionHiddenField" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Product Code</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Qty</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Price</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Discount Code</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Discount Amount</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Total</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Remark</b>
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
                                                        <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="QtyLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="PriceLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="DiscountCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="DiscountAmountLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="TotalLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
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

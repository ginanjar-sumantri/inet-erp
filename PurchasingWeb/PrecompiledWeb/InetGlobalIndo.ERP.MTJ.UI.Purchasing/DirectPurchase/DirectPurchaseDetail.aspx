<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.DirectPurchase.DirectPurchaseDetail, App_Web_-xa4oz-a" %>

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
                    <asp:Label ID="WarningLabel3" runat="server" ForeColor="Red"></asp:Label>
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
                                        <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Insert Reason UnPosting
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                    <asp:TextBox ID="TransTypeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox ID="PayTypeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                        Width="300">
                                    </asp:TextBox>
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
                                    <asp:TextBox ID="SuppNmbrTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    <%--<asp:Button ID="btnSearchSupplier" runat="server" Text="..." CausesValidation="False" />--%>
                                    <br />
                                    <asp:TextBox ID="SupplierNameTextBox" runat="server" Width="300" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox ID="BaseForexTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td colspan="4">
                                </td>
                                <td>
                                    Disc Amount
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="discount" Width="30px" Text="0" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    %
                                    <asp:TextBox ID="discountValue" Width="90" Text="0" runat="server" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox ID="PPNTextBox" Width="30px" Text="0" runat="server" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                    %
                                    <asp:TextBox ID="PPNAmountTextBox" Width="90" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox ID="ForexRateTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox ID="PPHTextBox" runat="server" Text="0" Width="30px" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
                                    %&nbsp;<asp:TextBox ID="PPHAmountTextBox" Width="90" runat="server" ReadOnly="true"
                                        BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox ID="Remark" runat="server" Width="270" ReadOnly="true" BackColor="#CCCCCC"
                                        TextMode="MultiLine" Rows="3"></asp:TextBox>
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
                                    <asp:TextBox ID="TotalAmountTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <table border="0" cellpadding="3" cellspacing="0" width="0">
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
                                               <asp:ImageButton ID="CreateJurnalImageButton" runat="server" OnClick="CreateJurnalImageButton_Click" />
                                            </td>
                                            <%--<td>
                                                <asp:ImageButton ID="ReviseButton" runat="server" OnClick="ReviseButton_Click" />
                                            </td>--%>
                                            <%--<td>
                                                <asp:ImageButton ID="DeleteHeaderButton" runat="server" OnClick="DeleteHeaderButton_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                                            </td>--%>
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
                                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                            </td>
                                            <%--<td>
                                                <asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                            </td>--%>
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
                                            <td style="width: 60px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Product Code</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Product Name</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Warehouse</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Warehouse SubLed</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Warehouse Location</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Qty</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Price</b>
                                            </td>
                                            <td style="width: 80px" class="tahoma_11_white" align="center">
                                                <b>Amount</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
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
                                                    <td align="left">
                                                        <table cellpadding="0" cellspacing="0" width="0" border="0">
                                                            <tr>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton2" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="WareHouseLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="WareHouseSubLedLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="WareHouseLocationLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="QtyLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="UnitLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="PriceLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="AmountLiteral"></asp:Literal>
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

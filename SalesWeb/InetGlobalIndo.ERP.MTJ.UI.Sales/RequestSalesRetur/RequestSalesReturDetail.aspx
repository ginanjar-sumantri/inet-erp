<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RequestSalesReturDetail.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.RequestSalesRetur.RequestSalesReturDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="EditButton" runat="server">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td class="tahoma_14_black">
                    <b>
                        <asp:Literal ID="PageTitleLiteral" runat="server" />
                    </b>
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
                                <td>
                                    <asp:TextBox ID="TransNoTextBox" runat="server" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    File No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="FileNoTextBox" runat="server" Width="160" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                    <asp:HiddenField ID="StatusHiddenField" runat="server" />
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
                                    <asp:TextBox runat="server" ID="CustTextBox" Width="420" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Attn
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="AttnTextBox" runat="server" Width="280" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Use Reference
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="UseReferenceTextBox" runat="server" ReadOnly="true" BackColor ="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table id="ReferenceTable" runat="server">
                                        <%--<tr>
                                            <td width="84px">
                                                Trans Type
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TransTypeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                Reference No.
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="SJNoTextBox" runat="server" Width="150" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Delivery Back
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="DeliveryBackTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                    Product Scrap
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ProductScrapTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    Currency / Rate
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="50" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                    <asp:TextBox runat="server" ID="CurrRateTextBox" Width="150" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total Forex
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TotalForexTextBox" runat="server" Width="150" BackColor="#CCCCCC"
                                        ReadOnly="true">
                                    </asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" MaxLength="500" Height="80"
                                        TextMode="MultiLine" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                                <asp:ImageButton ID="AddButton" runat="server" OnClick="AddButton_Click" />
                                                <%--</td>
                                            <td>--%>
                                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
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
                                    <asp:HiddenField runat="server" ID="DescriptionHiddenField" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Product Code</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Product Name</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Qty</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Unit</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Price</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Amount</b>
                                            </td>
                                            <%--<td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Done Closing</b>
                                            </td>--%>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater" OnItemCommand="ListRepeater_ItemCommand"
                                            OnItemDataBound="ListRepeater_ItemDataBound">
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
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton2" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="CloseButton" />
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
                                                    <%--<td align="center">
                                                        <asp:Literal runat="server" ID="DoneClosingLiteral"></asp:Literal>
                                                    </td>--%>
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
</asp:Content>

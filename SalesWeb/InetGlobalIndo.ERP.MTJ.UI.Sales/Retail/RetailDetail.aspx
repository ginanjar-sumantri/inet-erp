<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="RetailDetail.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Retail.RetailDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
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
                <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="TransNoTextBox" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    File No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="FileNmbrTextBox" Width="200" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <td width="10px">
                                </td>
                                <td width="100px">
                                    Payment Type
                                </td>
                                <td width="10px">
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="PayTypeDropDownList" runat="server" Enabled="false">
                                        <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                        <asp:ListItem Text="Debit" Value="Debit"></asp:ListItem>
                                        <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                    </asp:DropDownList>
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
                                    <asp:TextBox ID="CustNameTextBox" runat="server" MaxLength="50" Width="300px" BackColor="#CCCCCC"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                                <td width="10px">
                                </td>
                                <td colspan="3" id="NoTD" runat="server">
                                    <table>
                                        <tr>
                                            <td width="100px">
                                                Card No.
                                            </td>
                                            <td width="10px">
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="CardNoTextBox" Width="300px" MaxLength="30" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="middle">
                                <td>
                                    Sales Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="EmpNumbTextBox" runat="server" Width="300px" MaxLength="30" BackColor="#CCCCCC"
                                        ReadOnly="true">
                                    </asp:TextBox>
                                </td>
                                <td width="10px">
                                </td>
                                <td colspan="3" id="NameTD" runat="server">
                                    <table>
                                        <tr>
                                            <td width="100px">
                                                Card Name
                                            </td>
                                            <td width="10px">
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="CardNameTextBox" Width="300px" MaxLength="50" BackColor="#CCCCCC"
                                                    ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Currency / Rate
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox runat="server" ID="CurrCodeTextBox" Width="100px" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                    <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                    <asp:HiddenField ID="DecimalPlaceHiddenField" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray" style="height: 8px">
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Currency</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Base Forex</b>
                                            </td>
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
                                    Amount
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="1" width="0" border="0">
                                        <tr>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="CurrTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="BaseForexTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="DiscTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
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
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray" style="height: 8px">
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>PPN %</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>PPN Forex</b>
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
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="1" width="0" border="0">
                                        <tr>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray" style="height: 8px">
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Additional Fee</b>
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
                                    Additional Fee
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="1" width="0" border="0">
                                        <tr>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="AddFeeTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="TotalForexTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    Description
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="RemarkTextBox" runat="server" Height="80" TextMode="MultiLine" Width="300"
                                        ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                    <asp:HiddenField ID="StatusHiddenField" runat="server"></asp:HiddenField>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
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
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel3" DefaultButton="AddButton">
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
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Product Name</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Phone Type</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Serial Number</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>IMEI</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Qty</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Price</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Discount</b>
                                            </td>
                                            <td style="width: 70px" class="tahoma_11_white" align="center">
                                                <b>Total</b>
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
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="ViewButton" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton2" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="PhoneTypeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="SerialNumberLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="IMEILiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="QtyLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="PriceLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="DiscLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="TotalLiteral"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="Panel2">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="1000px" runat="server">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

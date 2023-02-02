<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FASalesDetail.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FASales.FASalesDetail" %>

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
                                    <asp:TextBox ID="TransNoTextBox" Width="150" runat="server" BackColor="#CCCCCC" ReadOnly="true">
                                    </asp:TextBox>
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
                                    <asp:TextBox ID="FileNmbrTextBox" Width="150" runat="server" BackColor="#CCCCCC"
                                        ReadOnly="true">
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
                                    <asp:TextBox ID="TransDateTextBox" Width="100" runat="server" BackColor="#CCCCCC"
                                        ReadOnly="true">
                                    </asp:TextBox>
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
                                    <asp:TextBox ID="CustTextBox" Width="420" runat="server" ReadOnly="true" BackColor="#CCCCCC">
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
                                    <asp:TextBox ID="AttnTextBox" runat="server" Width="280" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
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
                                    <asp:TextBox ID="CurrencyTextBox" Width="50" runat="server" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                    <asp:TextBox ID="ForexRateTextBox" Width="100" runat="server" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Term
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="TermTextBox" Width="350" runat="server" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
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
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNPercentTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNNoTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#cccccc">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNDateTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNRateTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#cccccc">
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
                                <td>
                                    <table>
                                        <tr class="bgcolor_gray" height="20">
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Currency</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Base Forex</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>Discount Forex</b>
                                            </td>
                                            <td style="width: 110px" class="tahoma_11_white" align="center">
                                                <b>PPN Forex</b>
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
                                <td>
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
                                                <asp:TextBox ID="DiscForexTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                                </asp:TextBox>
                                            </td>
                                            <td style="width: 110px" align="center">
                                                <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC">
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
                            <tr valign="top">
                                <td>
                                    Remark
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="RemarkTextBox" runat="server" Width="300" TextMode="MultiLine" Height="80"
                                        ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
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
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                                <%--</td>
                                                <td>--%>
                                                &nbsp;<asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                                                &nbsp;<asp:ImageButton ID="CreateJurnalImageButton" runat="server" OnClick="CreateJurnalImageButton_Click" />
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
                    <fieldset>
                        <legend>Detail</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="AddButton" runat="server" OnClick="AddButton_Click" />
                                                <%--</td>
                                        <td>--%>
                                                &nbsp;<asp:ImageButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" />
                                                &nbsp;<asp:ImageButton ID="DeleteAllButton" runat="server" OnClick="DeleteAllButton_Click" />
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
                                <td align="left">
                                    <asp:Panel ID="Panel3" DefaultButton="GoImageButton" runat="server">
                                        <table cellpadding="0" cellspacing="2" border="0">
                                            <tr>
                                                <td>
                                                    <b>Quick Search :</b>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CategoryDropDownList" runat="server">
                                                        <asp:ListItem Value="Code" Text="Fixed Asset Code"></asp:ListItem>
                                                        <asp:ListItem Value="Name" Text="Fixed Asset Name"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="KeywordTextBox" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="GoImageButton" runat="server" OnClick="GoImageButton_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
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
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px" rowspan="2">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td rowspan="2" style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td rowspan="2" style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td rowspan="2" style="width: 120px" class="tahoma_11_white" align="center">
                                                <b>Fixed Asset Code</b>
                                            </td>
                                            <td rowspan="2" style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Fixed Asset Name</b>
                                            </td>
                                            <td colspan="3" style="width: 300px" class="tahoma_11_white" align="center">
                                                <b>Amount</b>
                                            </td>
                                        </tr>
                                        <tr class="bgcolor_gray">
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Sales</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Sales (IDR)</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Fixed Asset (IDR)</b>
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
                                                                    <asp:ImageButton runat="server" ID="ViewButton2" />
                                                                </td>
                                                                <td style="padding-left: 4px">
                                                                    <asp:ImageButton runat="server" ID="EditButton2" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="FACodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="FANameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="SalesAmountLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="SalesIDRLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="FixedAssetIDRLiteral"></asp:Literal>
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
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="1500px">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel4">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer2" Width="100%" runat="server">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel5">
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

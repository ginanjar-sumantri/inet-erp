<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustBeginningView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerBeginning.CustBeginningView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel DefaultButton="EditButton" ID="Panel1" runat="server">
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
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Invoice No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="InvoiceNoTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                        </tr>
                        <tr>
                            <td>
                                Customer PO No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustPONoTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CustTextBox" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bill To
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BillToTextBox" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Term / DueDate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TermTextBox" runat="server" Width="50" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox ID="DueDateTextBox" runat="server" Width="100" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
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
                                <asp:TextBox runat="server" ID="CurrRateTextBox" Width="100" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="1">
                                    <tr class="bgcolor_gray" height="10px">
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>PPN %</b>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>No</b>
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
                                            <asp:TextBox ID="PPNTextBox" runat="server" Width="100px" ReadOnly="true" BackColor="#CCCCCC">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <asp:TextBox runat="server" Width="100" ID="PPNNoTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <asp:TextBox runat="server" Width="70" ID="PPNDateTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <asp:TextBox runat="server" Width="100" ID="PPNRateTextBox" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <table cellpadding="0" cellspacing="1">
                                    <tr class="bgcolor_gray" height="10px">
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>Currency</b>
                                        </td>
                                        <td style="width: 110px" class="tahoma_11_white" align="center">
                                            <b>BaseForex</b>
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
                                            <asp:TextBox ID="BaseForexTextBox" runat="server" Width="100px" ReadOnly="true" BackColor="#CCCCCC">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 110px" align="center">
                                            <asp:TextBox ID="PPNForexTextBox" runat="server" Width="100px" ReadOnly="true" BackColor="#CCCCCC">
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
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" MaxLength="500" Height="80"
                                    TextMode="MultiLine" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
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

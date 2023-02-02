<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="GiroPaymentChangeDetail.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPaymentChange.GiroPaymentChangeDetail" %>

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
                                <td>
                                    <asp:TextBox runat="server" ID="TransNoTextBox" Width="150" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                    <asp:TextBox ID="FileNmbrTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"
                                        Width="150">
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
                                    <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Payment To
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CodeTextBox" Width="80" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                                    <asp:TextBox runat="server" ID="NameTextBox" Width="200" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                        TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                                    <asp:Label runat="server" ID="StatusLabel"></asp:Label>
                                    <asp:HiddenField runat="server" ID="StatusHiddenField" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
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
                <asp:Panel runat="server" ID="Panel2" DefaultButton="AddButton">
                    <fieldset>
                        <legend>Old Giro</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                                &nbsp;
                                                <asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
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
                                    <table cellpadding="3" cellspacing="1" width="0" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Old Giro</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Payment Date</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Due Date</b>
                                            </td>
                                            <td style="width: 300px" class="tahoma_11_white" align="center">
                                                <b>Bank Payment</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Currency</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Rate</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Amount</b>
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
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="OldGiroLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="PaymentDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="DueDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="BankPaymentLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="CurrencyLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="RateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="AmountLiteral"></asp:Literal>
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
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel3" DefaultButton="AddButton2">
                    <fieldset>
                        <legend>Payment With</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td colspan="3">
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="AddButton2" runat="server" OnClick="AddButton2_Click" />
                                                &nbsp;
                                                <asp:ImageButton ID="DeleteButton2" runat="server" OnClick="DeleteButton2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="WarningLabel2" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:HiddenField ID="CheckHidden2" runat="server" />
                                    <asp:HiddenField ID="TempHidden2" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
                                        <tr class="bgcolor_gray">
                                            <td style="width: 5px">
                                                <asp:CheckBox runat="server" ID="AllCheckBox2" />
                                            </td>
                                            <td style="width: 5px" class="tahoma_11_white" align="center">
                                                <b>No.</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Action</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Payment Type</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Document No.</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Currency</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Rate</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Amount</b>
                                            </td>
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Remark</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Bank Payment</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Due Date</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
                                                <b>Bank Charges</b>
                                            </td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="ListRepeater2" OnItemDataBound="ListRepeater2_ItemDataBound">
                                            <ItemTemplate>
                                                <tr id="RepeaterItemTemplate" runat="server">
                                                    <td align="center">
                                                        <asp:CheckBox runat="server" ID="ListCheckBox2" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="NoLiteral2"></asp:Literal>
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
                                                        <asp:Literal runat="server" ID="PaymentTypeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="DocumentNoLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="CurrencyLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="RateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="AmountLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="RemarkLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="BankPaymentLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="DueDateLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Literal runat="server" ID="BankChargesLiteral"></asp:Literal>
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
        <tr>
            <td>
                <asp:Panel runat="server" ID="Panel4">
                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td>
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="1000px">
                                </rsweb:ReportViewer>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

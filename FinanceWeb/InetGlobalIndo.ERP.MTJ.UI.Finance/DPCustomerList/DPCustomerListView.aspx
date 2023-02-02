<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="DPCustomerListView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerList.DPCustomerListView" %>

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
            <asp:Panel ID="ReasonPanel" runat="server" Visible="false">
                <tr>
                    <td colspan="3">
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
                    <asp:TextBox runat="server" ID="DPCustListCodeTextBox" MaxLength="20" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
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
                    Date
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="DPCustListDateTextBox" MaxLength="30" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
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
                    <%--<asp:DropDownList runat="server" ID="CustCodeDropDownList" AutoPostBack="true" OnSelectedIndexChanged="CustCodeDropDownList_SelectedIndexChanged">
                </asp:DropDownList>--%><asp:TextBox runat="server" ID="CustTextBox" ReadOnly="true"
                    BackColor="#CCCCCC"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="AttnTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    SO No
                </td>
                <td>
                    :
                </td>
                <td>
                    <%--<asp:DropDownList runat="server" ID="SONoDropDownList">
                </asp:DropDownList>--%><asp:TextBox runat="server" ID="SONoTextBox" BackColor="#CCCCCC"
                    ReadOnly="True"></asp:TextBox>
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
                    <%--<asp:DropDownList runat="server" ID="CurrCodeDropDownList" AutoPostBack="true" OnSelectedIndexChanged="CurrCodeDropDownList_SelectedIndexChanged">
                </asp:DropDownList>--%><asp:TextBox runat="server" ID="CurrCodeTextBox" Width="40"
                    ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                    <asp:TextBox runat="server" ID="ForexRateTextBox" Width="100" BackColor="#CCCCCC"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    Amount
                </td>
                <td valign="top">
                    :
                </td>
                <td>
                    <table cellpadding="0" cellspacing="1">
                        <tr style="background-color: Gray">
                            <td align="center" class="tahoma_11_white">
                                <b>Base Forex</b>
                            </td>
                            <td align="center" class="tahoma_11_white">
                                <b>PPN</b>
                            </td>
                            <td align="center" class="tahoma_11_white">
                                <b>PPN Forex</b>
                            </td>
                            <td align="center" class="tahoma_11_white">
                                <b>Total Forex</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="BaseForexTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PPNTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PPNForexTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TotalForexTextBox" BackColor="#CCCCCC" ReadOnly="True"></asp:TextBox>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

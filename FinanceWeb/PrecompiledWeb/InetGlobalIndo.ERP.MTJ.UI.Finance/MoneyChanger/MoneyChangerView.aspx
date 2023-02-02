<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Finance.MoneyChanger.MoneyChangerView, App_Web_h8xb3gvb" %>

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
                    <asp:Label CssClass="warning" runat="server" ID="WarningLabel"></asp:Label>
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
                    <table>
                        <tr>
                            <td>
                                Trans No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TransNoTextBox" Width="200" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="FileNoTextBox" Width="200" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TypeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="petty_tr">
                            <td>
                                Petty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PettyTextBox" runat="server" Width="350" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="payment_tr">
                            <td>
                                Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PaymentTextBox" runat="server" Width="350" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="CurrTextBox" BackColor="#CCCCCC" ReadOnly="true"
                                    Width="50"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Rate
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="RateTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox ID="AmountTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="TypeExchangeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="petty_exchange_tr">
                            <td>
                                Petty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PettyExchangeTextBox" runat="server" Width="350" BackColor="#CCCCCC"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="payment_exchange_tr">
                            <td>
                                Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" Width="350" BackColor="#CCCCCC" ReadOnly="true" ID="PaymentExchangeTextBox">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Currency Exchange
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CurrExchangeTextBox" runat="server" BackColor="#CCCCCC" Width="50px"
                                    ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Rate Exchange
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CurrRateExchangeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Exchange
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountExchangeTextBox" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"
                                    BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                    </table>
                </td>
            </tr>
            <tr>
                <td>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

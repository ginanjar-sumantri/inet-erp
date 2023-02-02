<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="FAAddStockDetailView.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FAAddStock.FAAddStockDetailEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
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
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Product
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ProductTextBox" runat="server" Width="420" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LocationTextBox" runat="server" Width="350" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FACodeTextBox" Width="150" MaxLength="20" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FANameTextBox" runat="server" Width="420" MaxLength="80" ReadOnly="true"
                                    BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Condition
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FAStatusTextBox" Width="210" runat="server" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Owner
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FAOwnerCheckBox" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fixed Asset Sub Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="FASubGroupTextBox" runat="server" Width="350" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ReadOnly="true" BackColor="#CCCCCC" runat="server" ID="FALocationTypeTextBox"
                                    Width="150">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FALocationTextBox" Width="350" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Life Month
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="LifeMonthTextBox" runat="server" Width="50" ReadOnly="true" BackColor="#CCCCCC">
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
                                <asp:TextBox ID="CurrTextBox" Width="100" runat="server" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Amount Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="AmountForexTextBox" runat="server" Width="110" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Status Process
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="StatusProcessCheckBox" Enabled="false" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Specification
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SpecificationTextBox" runat="server" Width="300px" Height="80px"
                                    TextMode="MultiLine" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

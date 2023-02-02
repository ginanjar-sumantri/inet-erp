<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Accounting.FAPurchase.FAPurchaseDetailView, App_Web_0mu-vdrj" %>

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
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                Fixed Asset Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FACodeTextBox" Width="150" MaxLength="20" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox ID="FANameTextBox" runat="server" Width="200" MaxLength="80" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox ID="FAStatusTextBox" runat="server" Width="200" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:CheckBox runat="server" ID="FAOwnerCheckBox" Enabled="False" />
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
                                <asp:TextBox ID="FASubGroupTextBox" runat="server" Width="200" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox ID="FALocationTypeTextBox" runat="server" Width="200" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox ID="FALocationTextBox" runat="server" Width="200" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:TextBox ID="LifeMonthTextBox" runat="server" Width="50" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Price Forex
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="PriceForexTextBox" runat="server" Width="110" ReadOnly="true" BackColor="#CCCCCC">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Qty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="QtyTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="True">
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
                                <asp:TextBox ID="AmountForexTextBox" runat="server" Width="110" BackColor="#CCCCCC"
                                    ReadOnly="True"></asp:TextBox>
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
                                <asp:CheckBox runat="server" ID="StatusProcessCheckBox" Enabled="False" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Specification
                            </td>
                            <td valign="top">
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
                                <asp:ImageButton ID="EditButton" runat="server" CausesValidation="False" OnClick="EditButton_Click" />
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

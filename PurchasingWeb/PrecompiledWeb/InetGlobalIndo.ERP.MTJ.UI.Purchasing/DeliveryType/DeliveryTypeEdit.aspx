<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.DeliveryType.DeliveryEdit, App_Web_dnakz54p" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
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
                                Delivery Site Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DeliveryCodeTextBox" Width="100" MaxLength="10" ReadOnly="true"
                                    BackColor="#CCCCCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Delivery Site Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DeliveryNameTextBox" Width="200" MaxLength="60"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DeliveryNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Delivery Name Must Be Filled" Text="*" ControlToValidate="DeliveryNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Address 1
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Address1TextBox" Width="200" MaxLength="60" Height="80px"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Address 2
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Address2TextBox" Width="200" MaxLength="60" Height="80px"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Zip Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ZipCodeTextBox" Width="150" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Phone
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PhoneTextBox" Width="150" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fax
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="FaxTextBox" Width="150" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ContactNameTextBox" Width="150" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact Title
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ContactTitleTextBox" Width="150" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contact HP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ContactHPTextBox" Width="150" MaxLength="30"></asp:TextBox>
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
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="False" OnClick="CancelButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" CausesValidation="False" OnClick="ResetButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

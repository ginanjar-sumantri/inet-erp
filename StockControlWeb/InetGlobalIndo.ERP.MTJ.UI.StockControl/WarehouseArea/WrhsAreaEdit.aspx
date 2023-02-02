<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="WrhsAreaEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.StockControl.WarehouseArea.WrhsAreaEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Warehouse Area Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WrhsAreaCodeTextBox" Width="100" MaxLength="10" BackColor="#cccccc"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Warehouse Area Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="WrhsAreaNameTextBox" Width="350" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="WrhsAreaNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Warehouse Area Name Must Be Filled" Text="*" ControlToValidate="WrhsAreaNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Address 1
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Address1TextBox" TextMode="MultiLine" Width="200"
                                    Height="50" MaxLength="60">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Address 2
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="Address2TextBox" TextMode="MultiLine" Width="200"
                                    Height="50" MaxLength="60">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="ZipCodeTextBox" MaxLength="10" Width="70">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="PhoneTextBox" MaxLength="30" Width="210">
                                </asp:TextBox>
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
                                <asp:TextBox runat="server" ID="FaxTextBox" MaxLength="30" Width="210">
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
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Contact Detail</legend>
                        <table width="0" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>
                                    Contact Person
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ContactPersonTextBox" MaxLength="40" Width="280">
                                    </asp:TextBox>
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
                                    <asp:TextBox runat="server" ID="ContactTitleTextBox" MaxLength="30" Width="210">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact Phone
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ContactPhoneTextBox" MaxLength="30" Width="210">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Contact Email
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="ContactEmailTextBox" MaxLength="40" Width="280">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="ContactEmailTextBox"
                                        runat="server" ErrorMessage="Contact Email is not Valid" Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
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

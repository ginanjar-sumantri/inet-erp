<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Tour.AirLine.AirLineAdd, App_Web_vpbxqdcy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <asp:Literal ID="javascriptReceiver2" runat="server"></asp:Literal>
    <asp:Literal ID="javascriptReceiver" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel2" DefaultButton="SaveButton" runat="server">
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
                                Air Line Code
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AirLineCodeTextBox" Width="250"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AirLineCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Air Line Code Must Be Filled" Text="*" ControlToValidate="AirLineCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Air Line Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AirLineNameTextBox" Width="250"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AirLineNameRequiredFieldValidator" runat="server"
                                    ErrorMessage="Air Line Name Must Be Filled" Text="*" ControlToValidate="AirLineNameTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                Supplier
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="SupplierTextBox" runat="server" BackColor="#CCCCCC" AutoPostBack="true"
                                    OnTextChanged="SupplierTextBox_TextChanged"></asp:TextBox>
                                <asp:Button ID="btnSearchSupplier" runat="server" Text="..." /><br />
                                <asp:Label ID="SupplierLabel" runat="server"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Supplier Must Be Filled"
                                    Text="*" ControlToValidate="SupplierTextBox" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Product 
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:HiddenField runat="server" ID="ProductCodeHiddenField"/>
                                <asp:TextBox runat="server" ID="ProductCodeTextBox"></asp:TextBox>
                                <asp:Button ID="btnSearchProduct" runat="server" CausesValidation="false" Text="..." />
                                <asp:RequiredFieldValidator ID="CashierCodeRequiredFieldValidator" runat="server"
                                    ErrorMessage="Cashier Code Must Be Filled" Text="*" ControlToValidate="ProductCodeTextBox"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <asp:DropDownList runat="server" ID="CurrCodeDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CurrCodeCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrCodeDropDownList">
                                </asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <fieldset>
                                    <legend>Air Line Info</legend>
                                    <table>
                                        <tr>
                                            <td valign="top">
                                                Address 1
                                            </td>
                                            <td valign="top">
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="Address1TextBox" Width="250" TextMode="MultiLine"
                                                    Height="100"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="Address2TextBox" Width="250" TextMode="MultiLine"
                                                    Height="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                City
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="CityDDL" Width="250">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                Postcode
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="PostCodeTextBox" Width="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Telephone
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="TelephoneTextBox" Width="250"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp
                                            </td>
                                            <td>
                                                Fax
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="FaxTextBox" Width="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="EmailTextBox" Width="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Active
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" Checked="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Remark
                                            </td>
                                            <td valign="top">
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="250" TextMode="MultiLine" Height="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Limit
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="LimitTextBox" Width="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <fieldset>
                                    <legend>Contact Info</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                Contact Name
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ContactPersonTextBox" runat="server" Width="250"></asp:TextBox>
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
                                                <asp:TextBox ID="ContactTitleTextBox" runat="server" Width="250"></asp:TextBox>
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
                                                <asp:TextBox ID="ContactPhoneTextBox" runat="server" Width="250"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
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

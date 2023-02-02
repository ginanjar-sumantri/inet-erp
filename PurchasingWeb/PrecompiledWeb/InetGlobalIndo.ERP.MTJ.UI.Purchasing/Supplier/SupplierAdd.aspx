<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier.SupplierAdd, App_Web_kcrbpj1_" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function CheckUncheckPPN(_prmPPNCheckBox, _prmNPWPTextBox, _prmNPPKPTextBox) {
            if (_prmPPNCheckBox.checked == true) {
                _prmNPWPTextBox.readOnly = false;
                _prmNPWPTextBox.style.background = '#FFFFFF';
                _prmNPPKPTextBox.readOnly = false;
                _prmNPPKPTextBox.style.background = '#FFFFFF';
            }
            else {
                _prmNPWPTextBox.readOnly = true;
                _prmNPWPTextBox.style.background = '#CCCCCC';
                _prmNPWPTextBox.value = "";
                _prmNPPKPTextBox.readOnly = true;
                _prmNPPKPTextBox.style.background = '#CCCCCC';
                _prmNPPKPTextBox.value = "";
            }
        }
    </script>

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
                                Supplier Code
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="SuppCodeTextBox" Width="100px" MaxLength="12"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SuppCodeRequiredFieldValidator" runat="server" ErrorMessage="Supplier Code Must Be Filled"
                                    Text="*" ControlToValidate="SuppCodeTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" ID="SuppNameTextBox" Width="200px" MaxLength="60"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SuppNameRequiredFieldValidator" runat="server" ErrorMessage="Supplier Name Must Be Filled"
                                    Text="*" ControlToValidate="SuppNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="SupplierTypeDropDownList" Width="150">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="SupplierTypeCustomValidator" runat="server" ControlToValidate="SupplierTypeDropDownList"
                                    ErrorMessage="Supplier Type Must Be Choosed" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                PPN
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FgPPNCheckBox" Checked="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="SupplierGroupDropDownList" Width="150">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="SupplierGroupCustomValidator" runat="server" ControlToValidate="SupplierGroupDropDownList"
                                    ErrorMessage="Supplier Group Must be Choosed" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                NPWP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NPWPTextBox" Width="150px" MaxLength="25"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier Address
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AddressTextBox" Width="150px" MaxLength="500"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                NPPKP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="NPPKPTextBox" Width="150px" MaxLength="25"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Supplier Address 2
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="AddressTextBox2" Width="150px" MaxLength="500"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Contact Person
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ContactPersonTextBox" Width="150px" MaxLength="40"></asp:TextBox>
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
                                <asp:DropDownList runat="server" ID="CityDropDownList" Width="150">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="CityDropDownList"
                                    ErrorMessage="City must be choosed" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Contact Title
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ContactTitleTextBox" Width="150px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ZipCode
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ZipCodeTextBox" Width="150" MaxLength="10"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Contact HP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ContactHPTextBox" Width="150px" MaxLength="30"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="TelephoneTextBox" Width="150" MaxLength="30"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                Remark
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                FgActive
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="FgActiveCheckBox" Checked="True" />
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
                                <asp:TextBox runat="server" ID="EmailTextBox" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                    ErrorMessage="Email is not valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
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
                                <asp:DropDownList runat="server" ID="CurrencyDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="CurrencyDropDownList"
                                    ErrorMessage="Currency must be choosed" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
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
                                <asp:DropDownList runat="server" ID="TermDropDownList" Width="150">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="TermDropDownList"
                                    ErrorMessage="Term must be choosed" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="BankDropDownList" Width="150">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="BankDropDownList"
                                    ErrorMessage="Bank must be choosed" Text="*" ClientValidationFunction="DropDownValidation"></asp:CustomValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="left">
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
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

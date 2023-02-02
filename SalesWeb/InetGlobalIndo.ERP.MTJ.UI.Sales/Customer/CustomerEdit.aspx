<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="CustomerEdit.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Sales.Customer.CustomerEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="SaveButton">
        <table width="100%" border="0" cellpadding="3" cellspacing="0">
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
                    <table width="0" border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td>
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="CustCodeTextBox" runat="server" ReadOnly="true" BackColor="#CCCCCC"></asp:TextBox>
                                <asp:TextBox ID="CustNameTextBox" runat="server" Width="250" MaxLength="60"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CustNameRequiredFieldValidator" runat="server" ErrorMessage="Customer Name Must Be Filled"
                                    Text="*" ControlToValidate="CustNameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Group
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="CustGroupDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CustGroupDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustGroupDDLCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Customer Group Must Be Filled" Text="*" ControlToValidate="CustGroupDropDownList"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Type
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="CustTypeDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustTypeDDLCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Customer Type Must Be Filled" Text="*" ControlToValidate="CustTypeDropDownList"></asp:CustomValidator>
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
                        <legend>Customer Info</legend>
                        <table width="0" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>
                                    Address #1
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="Addr1TextBox" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address #2
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="Addr2TextBox" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
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
                                    <asp:DropDownList ID="CityDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CityCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                        ErrorMessage="City Must Be Filled" Text="*" ControlToValidate="CityDropDownList"></asp:CustomValidator>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Postal
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ZipCodeTextBox" runat="server"></asp:TextBox>
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
                                    <asp:TextBox ID="PhoneTextBox" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Fax
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="FaxTextBox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Email
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="EmailTextBox" runat="server" Width="250"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                        Text="*" ErrorMessage="Email is not Valid" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Currency
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:DropDownList ID="CurrDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CurrencyCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                        ErrorMessage="Currency Must Be Filled" Text="*" ControlToValidate="CurrDropDownList"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Term
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:DropDownList ID="TermDropDownList" runat="server" />
                                    <asp:CustomValidator ID="TermDDLCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                        ErrorMessage="Term Must Be Filled" Text="*" ControlToValidate="TermDropDownList"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    NPWP
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="NPWPTextBox" runat="server" Width="150"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NPWPRequiredFieldValidator" runat="server" ErrorMessage="NPWP Must Be Filled"
                                        Text="*" ControlToValidate="NPWPTextBox" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    NPWP Address
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="NPWPAddressTextBox" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NPWPAddressRequiredFieldValidator" runat="server"
                                        ErrorMessage="NPWP Must Be Filled" Text="*" ControlToValidate="NPWPAddressTextBox"
                                        Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    NPPKP
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="NPPKPTextBox" runat="server" Width="150"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NPPKPRequiredFieldValidator" runat="server" ErrorMessage="NPPKP Must Be Filled"
                                        Text="*" ControlToValidate="NPPKPTextBox" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Active
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sales Person
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:DropDownList ID="SalesPersonDropDownList" runat="server">
                                    </asp:DropDownList>
                                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" Text="*" ClientValidationFunction="DropDownValidation"
                                        ControlToValidate="SalesPersonDropDownList" ErrorMessage="Sales Person Must Be Choosed"></asp:CustomValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Due Date Cycle
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="DueDateCycleTextBox" runat="server" Width="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    PPN
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="FgPPNCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="FgPPNCheckBox_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Faktur Pajak
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="PrintFPCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="PrintFPCheckBox_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Stamp
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:CheckBox ID="StampCheckBox" runat="server" />
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
                                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="300" Height="80" TextMode="MultiLine"></asp:TextBox>
                                    <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                    characters left
                                </td>
                            </tr>
                        </table>
                    </fieldset>
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
                        <legend>Contact Info</legend>
                        <table width="0" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>
                                    Contact Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ContactNameTextBox" runat="server" Width="300" MaxLength="40"></asp:TextBox>
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
                                    <asp:TextBox ID="ContactTitleTextBox" runat="server" Width="300" MaxLength="100"></asp:TextBox>
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
                                    <asp:TextBox ID="ContactHPTextBox" runat="server" Width="300" MaxLength="30"></asp:TextBox>
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
                                    <asp:TextBox ID="ContactEmailTextBox" runat="server" Width="300" MaxLength="40"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
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
                        <legend>Customer Limit</legend>
                        <table width="0" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>
                                    Customer Limit
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CustLimitTextBox" runat="server" Width="150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Use Limit
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="UseLimitTextBox" runat="server" Width="150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Grace Period
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="GracePeriodTextBox" runat="server" Width="50"></asp:TextBox>
                                    days
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
                                    <asp:CheckBox ID="FgLimitCheckBox" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
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
                        <legend>Bill To</legend>
                        <table width="0" border="0" cellpadding="3" cellspacing="0">
                            <tr>
                                <td>
                                    Customer
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:DropDownList ID="BillToDropDownList" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                        <tr>
                            <td>
                                <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ResetButton" runat="server" OnClick="ResetButton_Click" CausesValidation="false" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ViewDetailButton" CausesValidation="false" runat="server" OnClick="ViewDetailButton_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="SaveAndViewDetailButton" runat="server" OnClick="SaveAndViewDetailButton_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

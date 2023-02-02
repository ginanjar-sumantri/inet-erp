<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.SalesConfirmation.SalesConfirmationDetail, App_Web_rjuuzlax" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <table cellpadding="3" cellspacing="0" width="0" border="0">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <asp:Panel runat="server" ID="Panel1" DefaultButton="EditButton">
            <tr>
                <td>
                    <fieldset>
                        <legend>Header</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td colspan="7">
                                    <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="180px">
                                    Trans No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="TransNoTextBox" Width="160" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td width="10px">
                                </td>
                                <td>
                                    File No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="FileNmbrTextBox" Width="160" MaxLength="20" BackColor="#CCCCCC"
                                        ReadOnly="true"></asp:TextBox>
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
                                <td width="10px">
                                </td>
                                <td>
                                    Form ID
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="FormIDTextBox" Width="160" MaxLength="20" BackColor="#CCCCCC"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sales
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="SalesTextBox" Width="200" BackColor="#CCCCCC" ReadOnly="true">
                                    </asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Bank Payment Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="BankPaymentCodeTextBox" runat="server" Width="200" BackColor="#CCCCCC"
                                        ReadOnly="true">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    SLA
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="SLATextBox" runat="server" Width="30" MaxLength="5" BackColor="#CCCCCC"
                                        ReadOnly="true"></asp:TextBox>
                                    %
                                </td>
                                <td>
                                </td>
                                <td>
                                    Contract Minimum
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="ContractMinTextBox" runat="server" Width="50" MaxLength="5" BackColor="#CCCCCC"
                                        ReadOnly="true"></asp:TextBox>
                                    month(s)
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Registration
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="RegTextBox" runat="server" Width="400" BackColor="#CCCCCC" ReadOnly="true">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    Free Trial Day(s)
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="FreeTrialDaysTextBox" runat="server" Width="50" MaxLength="5" BackColor="#CCCCCC"
                                        ReadOnly="true"></asp:TextBox>
                                    day(s)
                                </td>
                                <td>
                                </td>
                                <td>
                                    Remark
                                </td>
                                <td>
                                    :
                                </td>
                                <td rowspan="2">
                                    <asp:TextBox runat="server" ID="RemarkTextBox" Width="200" Height="60" TextMode="MultiLine"
                                        BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    Customer Approved by
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="ApprovedByTextBox" runat="server" Width="150" MaxLength="50" BackColor="#CCCCCC"
                                        ReadOnly="true"></asp:TextBox>
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
                                    <asp:TextBox ID="CustTypeTextBox" runat="server" Width="200" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Customer Group
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="CustGroupTextBox" runat="server" Width="200" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
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
                                    <asp:TextBox ID="TermTextBox" runat="server" Width="200" ReadOnly="true" BackColor="#CCCCCC">
                                    </asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Generate Customer Billing Account
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:CheckBox ID="GenerateCheckBox" runat="server" Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <fieldset>
                                        <legend>Company / Customer Information</legend>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr valign="top">
                                                <td width="168px">
                                                    Code
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:CheckBox ID="FgNewCustCheckBox" runat="server" Text="New" Enabled="false" />
                                                    <asp:TextBox runat="server" ID="CustomerTextBox" Width="300" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                    <%--<asp:CustomValidator ID="CustomerCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                                    ErrorMessage="Customer Must Be Filled" Text="*" ControlToValidate="CustomerDropDownList">
                                                </asp:CustomValidator>--%>
                                                </td>
                                            </tr>
                                            <tr id="tr2" style="visibility: hidden;" runat="server">
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="CustomerCodeTextBox" runat="server" Width="100" MaxLength="12" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                    <br />
                                                    <div id="span1" class="tooltip" style="visibility: hidden;">
                                                        &quot;Company Code&quot; will be automaticaly generated, if you leave it empty.</div>
                                                </td>
                                            </tr>
                                            <tr id="tr1" style="visibility: hidden;" runat="server">
                                                <td>
                                                    Name
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="CompNameTextBox" runat="server" Width="300" MaxLength="60" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="tr3" style="visibility: hidden;" runat="server">
                                                <td>
                                                    Customer Billing Account
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:CheckBox runat="server" ID="CustBillAccCheckBox" Text="New" Enabled="false">
                                                    </asp:CheckBox>
                                                    <asp:TextBox runat="server" ID="CustBillAccTextBox" Width="200" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    Address
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="CompAddrTextBox" runat="server" Width="300" Height="80" MaxLength="100"
                                                        TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="CompZipCodeTextBox" runat="server" Width="100" MaxLength="10" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td width="10px">
                                                </td>
                                                <td>
                                                    Country
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CompCountryTextBox" runat="server" Width="150" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
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
                                                    <asp:TextBox ID="CompCityTextBox" Width="150" runat="server" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    Telephone
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CompTelpTextBox" runat="server" MaxLength="30" Width="150" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Facsimile
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CompFaxTextBox" runat="server" MaxLength="30" Width="150" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    Cellular
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CompCellularTextBox" runat="server" MaxLength="30" Width="150" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    NPWP
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CompNPWPTextBox" runat="server" MaxLength="30" Width="200" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    Business Type
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="BusinessTypeTextBox" runat="server" MaxLength="50" Width="200" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Website
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="CompWebsiteTextBox" runat="server" MaxLength="50" Width="400" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <fieldset>
                                        <legend>Contact Person</legend>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td width="168px">
                                                    Name
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="NameTextBox" runat="server" Width="300" MaxLength="60" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Date of Birth
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="BirthDateTextBox" Width="100" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td width="10px">
                                                </td>
                                                <td>
                                                    Gender
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="GenderRadioButtonList" runat="server" RepeatDirection="Horizontal"
                                                        Enabled="false">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    ID Card No.
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="IDCardNoTextBox" runat="server" Width="240" MaxLength="30" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    Address
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td rowspan="3">
                                                    <asp:TextBox ID="AddressTextBox" runat="server" Width="300" Height="70" TextMode="MultiLine"
                                                        BackColor="#CCCCCC" ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                                <td width="10px">
                                                </td>
                                                <td>
                                                    Zip Code
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="PostalAddrTextBox" runat="server" Width="100" MaxLength="10" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                </td>
                                                <td>
                                                    Country
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CountryTextBox" runat="server" Width="150" BackColor="#CCCCCC" ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                </td>
                                                <td>
                                                    City
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CityTextBox" Width="150" runat="server" BackColor="#CCCCCC" ReadOnly="true">
                                                    </asp:TextBox>
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
                                                    <asp:TextBox ID="EmailTextBox" runat="server" Width="300" MaxLength="50" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
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
                                                    <asp:TextBox ID="TelpTextBox" runat="server" Width="150" MaxLength="30" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    Facsimile
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="FaxTextBox" runat="server" Width="150" MaxLength="30" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Cellular
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="CellularTextBox" runat="server" Width="150" MaxLength="30" BackColor="#CCCCCC"
                                                        ReadOnly="true">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <fieldset>
                                        <legend>Installation</legend>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td width="168px">
                                                    Target Instalation Day(s)
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TargetInstalationDayTextBox" runat="server" Width="50" MaxLength="5"
                                                        BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                    day(s)
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    Installation Address
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="InstallationAddrTextBox" runat="server" Width="300" Height="80"
                                                        MaxLength="200" TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Installation Device Status
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="InstallationDeviceStatusTextBox" runat="server" MaxLength="50" Width="300"
                                                        BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td>
                                                    Installation Device Description
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="InstallationDeviceDescTextBox" runat="server" Width="300" Height="80"
                                                        MaxLength="200" TextMode="MultiLine" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <fieldset>
                                        <legend>Technical Contact</legend>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td width="168px">
                                                    Technical Name
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="TechNameTextBox" runat="server" Width="300" MaxLength="50" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Technical PIC
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="TechPICTextBox" runat="server" Width="300" MaxLength="50" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TechPhoneTextBox" runat="server" Width="150" MaxLength="30" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td width="10px">
                                                </td>
                                                <td>
                                                    Cellular
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TechCellularTextBox" runat="server" Width="150" MaxLength="30" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Facsimile
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="TechFaxTextBox" runat="server" Width="150" MaxLength="30" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
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
                                                    <asp:TextBox ID="TechEmailTextBox" runat="server" Width="300" MaxLength="50" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Email 2
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td colspan="5">
                                                    <asp:TextBox ID="TechEmail2TextBox" runat="server" Width="300" MaxLength="50" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <fieldset>
                                        <legend>PPN</legend>
                                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                                            <tr>
                                                <td width="168px">
                                                    PPN
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="FgPPNCheckBox" runat="server" Enabled="false" />
                                                    <asp:TextBox ID="PPNPercentageTextBox" runat="server" Width="30" MaxLength="3" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>%&nbsp;
                                                    <asp:TextBox ID="PPNForexTextBox" runat="server" Width="200" MaxLength="23" BackColor="#CCCCCC"
                                                        ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="5">
                                    <asp:Label ID="StatusLabel" runat="server"></asp:Label>
                                    <asp:HiddenField ID="StatusHiddenField" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <table cellpadding="3" cellspacing="0" border="0" width="0">
                                        <tr>
                                            <td valign="top">
                                                <asp:ImageButton ID="EditButton" runat="server" OnClick="EditButton_Click" />
                                                &nbsp;<asp:ImageButton ID="BackButton" runat="server" OnClick="BackButton_Click" />
                                                &nbsp;<asp:ImageButton ID="ActionButton" runat="server" OnClick="ActionButton_Click" />
                                                &nbsp;<asp:ImageButton ID="PreviewButton" runat="server" OnClick="PreviewButton_Click" />
                                                &nbsp;<br />
                                                <asp:DropDownList ID="PreviewDropDownList" runat="server">
                                                </asp:DropDownList>
                                                <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                                                </asp:ScriptManager>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="PreviewHiddenField" runat="server" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="PreviewDropDownList" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>Detail</legend>
                        <table cellpadding="3" cellspacing="0" width="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="3" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:ImageButton runat="server" ID="AddButton" OnClick="AddButton_Click" />
                                                &nbsp;<asp:ImageButton runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />
                                            </td>
                                            <td align="right">
                                                <asp:Panel DefaultButton="DataPagerButton" ID="DataPagerPanel" runat="server">
                                                    <table border="0" cellpadding="2" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="DataPagerButton" runat="server" OnClick="DataPagerButton_Click" />
                                                            </td>
                                                            <td valign="middle">
                                                                <b>Page :</b>
                                                            </td>
                                                            <asp:Repeater EnableViewState="true" ID="DataPagerTopRepeater" runat="server" OnItemCommand="DataPagerTopRepeater_ItemCommand"
                                                                OnItemDataBound="DataPagerTopRepeater_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <td>
                                                                        <asp:LinkButton ID="PageNumberLinkButton" runat="server"></asp:LinkButton>
                                                                        <asp:TextBox Visible="false" ID="PageNumberTextBox" runat="server" Width="30px"></asp:TextBox>
                                                                    </td>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Label1" CssClass="warning"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="CheckHidden" runat="server" />
                                    <asp:HiddenField ID="TempHidden" runat="server" />
                                    <table cellpadding="3" cellspacing="1" width="100%" border="0">
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
                                            <td style="width: 150px" class="tahoma_11_white" align="center">
                                                <b>Product Code</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Product Name</b>
                                            </td>
                                            <td style="width: 200px" class="tahoma_11_white" align="center">
                                                <b>Product Specification</b>
                                            </td>
                                            <td style="width: 50px" class="tahoma_11_white" align="center">
                                                <b>Currency</b>
                                            </td>
                                            <td style="width: 100px" class="tahoma_11_white" align="center">
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
                                                                    <asp:ImageButton ID="EditButton" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="ProductCodeLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ProductNameLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Literal runat="server" ID="ProductSpecLiteral"></asp:Literal>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Literal runat="server" ID="CurrLiteral"></asp:Literal>
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
                </td>
            </tr>
        </asp:Panel>
    </table>
    <asp:Panel runat="server" ID="Panel2">
        <table cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <asp:Label ID="TermAndConditionLabel" runat="server"></asp:Label>
                    <div id="TermAndConditionDiv" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

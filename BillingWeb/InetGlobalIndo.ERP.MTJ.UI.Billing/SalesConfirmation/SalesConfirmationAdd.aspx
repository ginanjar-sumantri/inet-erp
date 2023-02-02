<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeFile="SalesConfirmationAdd.aspx.cs"
    Inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.SalesConfirmation.SalesConfirmationAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script type="text/javascript">
        function CheckUncheck(_prmFgNewCustCheckBox, _prmCustDDL, _prmCustTextBox, _prmCustBillAccCheckBox, _prmCustBillAccDDL) {
            if (_prmFgNewCustCheckBox.checked == true) {
                _prmCustDDL.style.visibility = "hidden";
                _prmCustDDL.value = "null";
                _prmCustTextBox.style.visibility = "visible";
                _prmCustTextBox.value = "";
                //_prmSpan.style.visibility = "visible";
                document.getElementById("span1").style.visibility = "visible";
                document.getElementById("tr1").style.visibility = "visible";
                document.getElementById("tr2").style.visibility = "hidden";
                _prmCustBillAccCheckBox.style.visibility = "hidden";
                _prmCustBillAccCheckBox.text = "";
                _prmCustBillAccDDL.style.visibility = "hidden";
                _prmCustBillAccDDL.value = "null";
            }
            else if (_prmFgNewCustCheckBox.checked == false) {
                _prmCustDDL.style.visibility = "visible";
                _prmCustDDL.value = "null";
                _prmCustTextBox.style.visibility = "hidden";
                _prmCustTextBox.value = "";
                //_prmSpan.style.visibility = "hidden";
                document.getElementById("span1").style.visibility = "hidden";
                document.getElementById("tr1").style.visibility = "hidden";
                document.getElementById("tr2").style.visibility = "visible";
                _prmCustBillAccCheckBox.style.visibility = "visible";
                _prmCustBillAccCheckBox.text = "";
                _prmCustBillAccDDL.style.visibility = "visible";
                _prmCustBillAccDDL.value = "null";
            }
        }

        function CheckUncheckCustBillAcc(_prmCustBillAccCheckBox, _prmCustBillAccDDL) {
            if (_prmCustBillAccCheckBox.checked == true) {
                _prmCustBillAccDDL.style.visibility = "hidden";
                _prmCustBillAccDDL.value = "null";
            }
            else if (_prmCustBillAccCheckBox.checked == false) {
                _prmCustBillAccDDL.style.visibility = "visible";
                _prmCustBillAccDDL.value = "null";
            }
        }

        function CheckUncheckPPN(_prmFgPPNCheckBox, _prmPPNPercentage, _prmPPNForex) {
            if (_prmFgPPNCheckBox.checked == true) {
                _prmPPNPercentage.readOnly = false;
                _prmPPNPercentage.style.background = '#FFFFFF';
                _prmPPNForex.readOnly = true;
                _prmPPNForex.style.background = '#CCCCCC';
            }
            else if (_prmFgPPNCheckBox.checked == false) {
                _prmPPNPercentage.readOnly = true;
                _prmPPNPercentage.style.background = '#CCCCCC';
                _prmPPNPercentage.value = "";
                _prmPPNForex.readOnly = true;
                _prmPPNForex.style.background = '#CCCCCC';
                _prmPPNForex.value = "";
            }
        }

        function DisablePPNForex(_prmPPNPercentage, _prmPPNForex) {
            if (_prmPPNPercentage.value != "") {
                _prmPPNForex.readOnly = true;
                _prmPPNForex.style.background = '#CCCCCC';
                _prmPPNForex.value = "";
            }
            else {
                _prmPPNForex.readOnly = false;
                _prmPPNForex.style.background = '#FFFFFF';
                _prmPPNForex.value = "";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel runat="server" ID="Panel1" DefaultButton="NextButton">
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
                            <td width="180px">
                                Trans Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <input id="DateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_DateTextBox,'yyyy-mm-dd',this)"
                                    value="..." />
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
                                <asp:TextBox runat="server" ID="FormIDTextBox" Width="160" MaxLength="20"></asp:TextBox>
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
                                <asp:DropDownList runat="server" ID="SalesDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="SalesCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="SalesDropDownList" Text="*" ErrorMessage="Sales Must be Chosen"></asp:CustomValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                                Bank Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="BankPaymentCodeDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomerCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Bank Payment Must Be Chosen" Text="*" ControlToValidate="BankPaymentCodeDropDownList">
                                </asp:CustomValidator>
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
                                <asp:TextBox ID="SLATextBox" runat="server" Width="30" MaxLength="5"></asp:TextBox>
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
                                <asp:TextBox ID="ContractMinTextBox" runat="server" Width="50" MaxLength="5"></asp:TextBox>
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
                                <asp:DropDownList ID="RegDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Registration Must Be Chosen" Text="*" ControlToValidate="RegDropDownList">
                                </asp:CustomValidator>
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
                                <asp:TextBox ID="FreeTrialDaysTextBox" runat="server" Width="50" MaxLength="5"></asp:TextBox>
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
                                <asp:TextBox runat="server" ID="RemarkTextBox" Width="200" Height="60" TextMode="MultiLine"></asp:TextBox>
                                <asp:TextBox ID="CounterTextBox" runat="server" Width="50" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                                characters left
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Customer Approved by
                            </td>
                            <td>
                                :
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="ApprovedByTextBox" runat="server" Width="150" MaxLength="50"></asp:TextBox>
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
                                <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Customer Type Must Be Chosen" Text="*" ControlToValidate="CustTypeDropDownList">
                                </asp:CustomValidator>
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
                                <asp:DropDownList ID="CustGroupDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Customer Group Must Be Chosen" Text="*" ControlToValidate="CustGroupDropDownList">
                                </asp:CustomValidator>
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
                                <asp:DropDownList ID="TermDropDownLilst" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="DropDownValidation"
                                    ErrorMessage="Term Must Be Chosen" Text="*" ControlToValidate="TermDropDownLilst">
                                </asp:CustomValidator>
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
                                <asp:CheckBox ID="GenerateCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <fieldset>
                                    <legend>Company / Customer Information</legend>
                                    <table cellpadding="3" cellspacing="0" width="0" border="0">
                                        <tr>
                                            <td width="168px">
                                                Code
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td colspan="5">
                                                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                                                </asp:ScriptManager>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="FgNewCustCheckBox" runat="server" Text="New" />
                                                        <asp:DropDownList runat="server" ID="CustomerDropDownList" OnSelectedIndexChanged="CustomerDropDownList_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <%--<asp:CustomValidator ID="CustomerCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                                    ErrorMessage="Customer Must Be Filled" Text="*" ControlToValidate="CustomerDropDownList">
                                                </asp:CustomValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td colspan="5">
                                                <asp:TextBox ID="CustomerCodeTextBox" runat="server" Width="100" MaxLength="12"></asp:TextBox>
                                                <br />
                                                <div id="span1" class="tooltip" style="visibility: hidden;">
                                                    &quot;Company Code&quot; will be automaticaly generated, if you leave it empty.</div>
                                            </td>
                                        </tr>
                                        <tr id="tr1" style="visibility: hidden;">
                                            <td>
                                                Name
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td colspan="5">
                                                <asp:TextBox ID="CompNameTextBox" runat="server" Width="300" MaxLength="60"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr2" style="visibility: hidden;">
                                            <td>
                                                Customer Billing Account
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td colspan="5">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox runat="server" ID="CustBillAccCheckBox" Text="New"></asp:CheckBox>
                                                        <asp:DropDownList runat="server" ID="CustBillAccDropDownList">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="CustomerDropDownList" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="CompAddrTextBox" runat="server" Width="300" Height="80" MaxLength="100"
                                                            TextMode="MultiLine"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="CustomerDropDownList" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="CompZipCodeTextBox" runat="server" Width="100" MaxLength="10"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="CustomerDropDownList" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
                                                <asp:DropDownList ID="CompCountryDropDownList" runat="server">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="CustomValidator6" runat="server" ClientValidationFunction="DropDownValidation"
                                                    ErrorMessage="Country Must Be Chosen" Text="*" ControlToValidate="CompCountryDropDownList">
                                                </asp:CustomValidator>
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
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="CompCityDropDownList" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="DropDownValidation"
                                                            ErrorMessage="City Must Be Chosen" Text="*" ControlToValidate="CompCityDropDownList">
                                                        </asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="CustomerDropDownList" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="CompTelpTextBox" runat="server" MaxLength="30" Width="150"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="CustomerDropDownList" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="CompFaxTextBox" runat="server" MaxLength="30" Width="150"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="CustomerDropDownList" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
                                                <asp:TextBox ID="CompCellularTextBox" runat="server" MaxLength="30" Width="150"></asp:TextBox>
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
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="CompNPWPTextBox" runat="server" MaxLength="30" Width="200"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="CustomerDropDownList" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
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
                                                <asp:TextBox ID="BusinessTypeTextBox" runat="server" MaxLength="50" Width="200"></asp:TextBox>
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
                                                <asp:TextBox ID="CompWebsiteTextBox" runat="server" MaxLength="50" Width="400"></asp:TextBox>
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
                                                <asp:TextBox ID="NameTextBox" runat="server" Width="300" MaxLength="60"></asp:TextBox>
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
                                                <asp:TextBox runat="server" ID="BirthDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                                <input id="BirthDateButton" type="button" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_BirthDateTextBox,'yyyy-mm-dd',this)"
                                                    value="..." />
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
                                                <asp:RadioButtonList ID="GenderRadioButtonList" runat="server" RepeatDirection="Horizontal">
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
                                                <asp:TextBox ID="IDCardNoTextBox" runat="server" Width="240" MaxLength="30">
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
                                                <asp:TextBox ID="AddressTextBox" runat="server" Width="300" Height="70" TextMode="MultiLine">
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
                                                <asp:TextBox ID="PostalAddrTextBox" runat="server" Width="100" MaxLength="10">
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
                                                <asp:DropDownList ID="CountryDropDownList" runat="server">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DropDownValidation"
                                                    ErrorMessage="Country Must Be Chosen" Text="*" ControlToValidate="CountryDropDownList">
                                                </asp:CustomValidator>
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
                                                <asp:DropDownList ID="CityDropDownList" runat="server">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DropDownValidation"
                                                    ErrorMessage="City Must Be Chosen" Text="*" ControlToValidate="CityDropDownList">
                                                </asp:CustomValidator>
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
                                                <asp:TextBox ID="EmailTextBox" runat="server" Width="300" MaxLength="50">
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server"
                                                    ErrorMessage="Email is not valid" Text="*" ControlToValidate="EmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
                                                <asp:TextBox ID="TelpTextBox" runat="server" Width="150" MaxLength="30">
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
                                                <asp:TextBox ID="FaxTextBox" runat="server" Width="150" MaxLength="30">
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
                                                <asp:TextBox ID="CellularTextBox" runat="server" Width="150" MaxLength="30">
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
                                                <asp:TextBox ID="TargetInstalationDayTextBox" runat="server" Width="50" MaxLength="5"></asp:TextBox>
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
                                                    MaxLength="200" TextMode="MultiLine"></asp:TextBox>
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
                                                <asp:TextBox ID="InstallationDeviceStatusTextBox" runat="server" MaxLength="50" Width="300"></asp:TextBox>
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
                                                    MaxLength="200" TextMode="MultiLine"></asp:TextBox>
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
                                                <asp:TextBox ID="TechNameTextBox" runat="server" Width="300" MaxLength="50"></asp:TextBox>
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
                                                <asp:TextBox ID="TechPICTextBox" runat="server" Width="300" MaxLength="50"></asp:TextBox>
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
                                                <asp:TextBox ID="TechPhoneTextBox" runat="server" Width="150" MaxLength="30"></asp:TextBox>
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
                                                <asp:TextBox ID="TechCellularTextBox" runat="server" Width="150" MaxLength="30"></asp:TextBox>
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
                                                <asp:TextBox ID="TechFaxTextBox" runat="server" Width="150" MaxLength="30"></asp:TextBox>
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
                                                <asp:TextBox ID="TechEmailTextBox" runat="server" Width="300" MaxLength="50"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email is not valid"
                                                    Text="*" ControlToValidate="TechEmailTextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
                                                <asp:TextBox ID="TechEmail2TextBox" runat="server" Width="300" MaxLength="50"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Email 2 is not valid"
                                                    Text="*" ControlToValidate="TechEmail2TextBox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
                                                <asp:CheckBox ID="FgPPNCheckBox" runat="server" />
                                                <asp:TextBox ID="PPNPercentageTextBox" runat="server" Width="30" MaxLength="3" BackColor="#CCCCCC"></asp:TextBox>%&nbsp;
                                                <asp:TextBox ID="PPNForexTextBox" runat="server" Width="200" MaxLength="23" BackColor="#CCCCCC"></asp:TextBox>
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
                                <asp:ImageButton ID="NextButton" runat="server" OnClick="NextButton_Click" />
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

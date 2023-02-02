<%@ page language="C#" masterpagefile="~/default.master" autoeventwireup="true" inherits="InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase.CustBillingAccountAdd, App_Web_cajrmfjr" %>

<%@ Register Src="../ProductPickerIgnoreStock.ascx" TagName="ProductPicker" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">
    <link href="../calendar/calendar.css" rel="stylesheet" type="text/css" media="all" />

    <script type="text/javascript" src="../calendar/calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function Activate(_prmActiveCheckBox, _prmActivateDate) {
            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();

            if (_prmActiveCheckBox.checked == true) {
                _prmActivateDate.value = (year + "-" + month + "-" + day);
            } else {
                _prmActivateDate.value = "";
            }
        }

        function Selected(_prmDDL, _prmTextBox) {
            if (_prmDDL.value != "null") {
                _prmTextBox.value = _prmDDL.value;
            }
            else {
                _prmTextBox.value = "";
            }
        }

        function Blur(_prmDDL, _prmTextBox) {
            _prmDDL.value = _prmTextBox.value;

            if (_prmDDL.value == '') {
                _prmTextBox.value = "";
                _prmDDL.value = "null";
            }
        }

        function ValidatePeriod(_prmPeriod) {
            var _tempPeriod = _prmPeriod.value;
            if (parseInt(_tempPeriod) < 1 || parseInt(_tempPeriod) > 12) {
                _prmPeriod.value = "";
            }
        }

        function ValidateYear(_prmYear) {
            var _tempYear = _prmYear.value;
            if (parseInt(_tempYear) < 1 || parseInt(_tempYear) > 9999) {
                _prmYear.value = "";
            }
        }

        function ValidateInterval(_prmInterval) {
            var _tempInterval = _prmInterval.value;
            if (parseInt(_tempInterval) < 1) {
                _prmInterval.value = "";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <asp:Panel ID="Panel1" DefaultButton="SaveButton" runat="server">
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
                                Customer
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="CustDropDownList" runat="server">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Customer Must Be Choosed"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="CustDropDownList"></asp:CustomValidator>
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
                                <%--<asp:DropDownList ID="ProductDropDownList" runat="server" 
                                    onselectedindexchanged="ProductDropDownList_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="ProductCustomValidator" runat="server" ErrorMessage="Product Must Be Choosed"
                                    Text="*" ClientValidationFunction="DropDownValidation" ControlToValidate="ProductDropDownList"></asp:CustomValidator>--%>
                                <uc1:ProductPicker ID="ProductPicker1" runat="server" />
                                <asp:HiddenField ID="tempProductCode" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="DescTextBox" Width="560" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:ScriptManager ID="scriptMgr" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <tr>
                                    <td>
                                        Currency
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CurrDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CurrDropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="CurrCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                            ControlToValidate="CurrDropDownList" ErrorMessage="Currency Must Be Choosed"
                                            Text="*"></asp:CustomValidator>
                                        <asp:HiddenField runat="server" ID="DecimalPlaceHiddenField" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Account
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="AccountTextBox" Width="100" MaxLength="12"></asp:TextBox>
                                        <asp:DropDownList runat="server" ID="AccountDropDownList">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="AccountCodeRequiredFieldValidator" runat="server"
                                            ErrorMessage="Account Must Be Filled" Text="*" ControlToValidate="AccountTextBox"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
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
                                        <asp:TextBox runat="server" ID="AmountTextBox" Width="200" MaxLength="23"></asp:TextBox>
                                    </td>
                                </tr>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td>
                                Active
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:CheckBox ID="FgActiveCheckBox" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                BandWidth INT / Ratio
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BandwidthIntTextBox" Width="50"></asp:TextBox>
                                <asp:TextBox runat="server" ID="RatioIntTextBox" Width="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                BandWidth IIX / Ratio
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BandwidthIixTextBox" Width="50"></asp:TextBox>
                                <asp:TextBox runat="server" ID="RatioIixTextBox" Width="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Bank Account
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="BankAccountDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="BankAccountCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="BankAccountDropDownList" ErrorMessage="Bank Account Must Be Choosed"
                                    Text="*"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Type Payment
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="TypePaymentDropDownList">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="TypePaymentCustomValidator" runat="server" ClientValidationFunction="DropDownValidation"
                                    ControlToValidate="TypePaymentDropDownList" ErrorMessage="Type Payment Must Be Choosed"
                                    Text="*"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Activate Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ActivateDateTextBox" Width="100" BackColor="#CCCCCC"></asp:TextBox>
                                <input id="ActivateDateButton" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_ActivateDateTextBox,'yyyy-mm-dd',this)"
                                    type="button" value="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Expired Date
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ExpiredDateTextBox" BackColor="#CCCCCC" Width="100"></asp:TextBox>
                                <input id="ExpiredDateButton" onclick="displayCalendar(ctl00_DefaultBodyContentPlaceHolder_ExpiredDateTextBox,'yyyy-mm-dd',this)"
                                    type="button" value="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Contract No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="ContractNoTextBox" Width="350" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                BA No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="BANoTextBox" Width="350" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Period
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="PeriodTextBox" Width="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Year
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="YearTextBox" Width="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Interval
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="IntervalTextBox" Width="50"></asp:TextBox>
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

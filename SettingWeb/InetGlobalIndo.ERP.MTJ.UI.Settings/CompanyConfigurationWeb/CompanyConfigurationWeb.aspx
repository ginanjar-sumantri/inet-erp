<%@ Page Title="" Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true"
    CodeFile="CompanyConfigurationWeb.aspx.cs" Inherits="InetGlobalIndo.ERP.MTJ.UI.Settings.CompanyConfigurationWeb.CompanyConfigurationWeb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultHeadContentPlaceHolder" runat="Server">

    <script language="javascript">
        function HarusAngka(x) { if (isNaN(x.value)) x.value = ""; }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DefaultBodyContentPlaceHolder" runat="Server">
    <%--<form id="MenuForm" runat="server">--%>
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table cellpadding="3" cellspacing="0" width="100%" border="0">
        <tr>
            <td class="tahoma_14_black">
                <b>
                    <asp:Literal ID="PageTitleLiteral" runat="server" />
                </b>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label runat="server" ID="WarningLabel" CssClass="warning"></asp:Label><br />
                <asp:Label runat="server" ID="WarningLabel1" CssClass="warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        <tr>
                            <td>
                                <table id="CreateTable" runat="server">
                                    <tr>
                                        <td>
                                        <asp:HiddenField id="CompanyHiddenField" runat ="server" />
                                            <asp:Panel ID="GeneralPanel" runat="server">
                                                <fieldset>
                                                    <legend>General</legend>
                                                    <table cellpadding="3" cellspacing="0" width="500px">
                                                        <tr>
                                                            <td>
                                                                Date Format
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DateFormatDDL" runat="server">
                                                                    <asp:ListItem Text="dd/MM/yyyy" Value="dd/MM/yyyy"></asp:ListItem>
                                                                    <asp:ListItem Text="MM/dd/yyyy" Value="MM/dd/yyyy"></asp:ListItem>
                                                                    <asp:ListItem Text="yyyy/MM/dd" Value="yyyy/MM/dd"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="DateFormatDDLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Logo'Position
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="PosisiLogoDDL" runat="server">
                                                                    <asp:ListItem Text="SIP' Logo placed at Header's Right Side" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="SIP' Logo placed at Header's Left Side" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="PosisiLogoDDLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Theme
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ThemeDDL" runat="server">
                                                                    <asp:ListItem Text="Plain" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="GoGreen" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="BlueMarine" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Dodge" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="ThemeDDLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="200px">
                                                                Enable User's Job Title While Generate Reports
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="ViewJobTitlePrintReportDDL" runat="server">
                                                            <asp:ListItem Text="Don't Show" Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Show" Value="1"></asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                                <asp:RadioButtonList ID="ViewJobTitlePrintReportRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Don't Show" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Show" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="ViewJobTitlePrintReportRadioButtonListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextGeneralImageButton" runat="server" OnClick="NextGeneralImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="ACCOUNTINGPanel" runat="server">
                                                <fieldset>
                                                    <legend>ACCOUNTING</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                JE Account Curr Lock
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="JEAccountCurrLockDDL" runat="server">
                                                            <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                                <asp:RadioButtonList ID="JEAccountCurrLockRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="JEAccountCurrLockRadioButtonListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Currency Format
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="CurrencyFormatDDL" runat="server">
                                                                    <asp:ListItem Text="#.##0,00" Value="#.##0,00" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="CurrencyFormatDDLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextACCOUNTINGImageButton" runat="server" OnClick="NextACCOUNTINGImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="AccountingPanel2" runat="server">
                                                <fieldset>
                                                    <legend>ACCOUNTING II</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                FA Code Auto Number
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="FACodeAutoNumberRBL" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Not Auto" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Auto" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="FACodeAutoNumberRBLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                FA Code Digit Number
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="FACodeDigitNumberDLL" runat="server">
                                                                    <asp:ListItem Text="3" Value="3" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="FACodeDigitNumberDLLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                FA Depreciation Method
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="FADepreciationMethodDDL" runat="server">
                                                                    <asp:ListItem Text="StraightLine" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="DoubleDeclining" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="FADepreciationMethodDDLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Auto Nmbr Loc For FA
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="AutoNmbrLocForFARBL" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="True" Value="T" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="False" Value="F"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="AutoNmbrLocForFARBLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Auto Nmbr Period For FA
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="AutoNmbrPeriodForFARadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="True" Value="T" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="False" Value="F"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="AutoNmbrPeriodForFARadioButtonListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextACCOUNTING2ImageButton" runat="server" OnClick="NextACCOUNTING2ImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="STOCKCONTROLPanel" runat="server">
                                                <fieldset>
                                                    <legend>STOCK CONTROL</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                BOL Reference Type
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="BOLReferenceTypeDDL" runat="server">
                                                                    <asp:ListItem Text="Sales Order" Value="SO" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Delivery Order" Value="DO"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="BOLReferenceTypeDDLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                COGS Method
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="COGSMethodDropDownList" runat="server">
                                                                    <asp:ListItem Text="FIFO" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="LIFO" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="AVG" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="COGSMethodDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextSTOCKCONTROLImageButton" runat="server" OnClick="NextSTOCKCONTROLImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="POSPanel" runat="server">
                                                <fieldset>
                                                    <legend>POS</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Transaction Wrhs Default
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="TransactionWrhsDefaultDropDownList" runat="server">
                                                                    <%--<asp:ListItem Text="Sales Order" Value="SO"></asp:ListItem>
                                                            <asp:ListItem Text="Delivery Order" Value="DO"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="TransactionWrhsDefaultDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Transaction Wrhs Deposit IN
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="TransactionWrhsDepositINDropDownList" runat="server">
                                                                    <%--<asp:ListItem Text="FIFO" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="LIFO" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="AVG" Value="2"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="TransactionWrhsDepositINDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Transaction Wrhs Location Default
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="TransactionWrhsLocationDefaultDropDownList" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="TransactionWrhsLocationDefaultDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Transaction Wrhs Location Deposit IN
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="TransactionWrhsLocationDepositINDropDownList" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="TransactionWrhsLocationDepositINDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td>
                                                                POS Internet Reminder Duration
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="POSInternetReminderDurationTextBox" runat="server" MaxLength="3"></asp:TextBox>
                                                                <asp:HiddenField ID="POSInternetReminderDurationHiddenField" runat="server" />
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextPOSImageButton" runat="server" OnClick="NextPOSImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PAYROLLPanel" runat="server">
                                                <fieldset>
                                                    <legend>PAYROLL</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Salary Encryption
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="SalaryEncryptionRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="UnEncrypt" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Encrypt" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="SalaryEncryptionRadioButtonListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Salary Encryption Key
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="SalaryEncryptionKeyTextBox" runat="server"></asp:TextBox>
                                                                <%--<asp:DropDownList ID="SalaryEncryptionKeyDropDownList" runat="server">
                                                                </asp:DropDownList>--%>
                                                                <asp:HiddenField ID="SalaryEncryptionKeyDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Salary Encryption Key Validation
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="SalaryEncryptionKeyValidationTextBox" runat="server"></asp:TextBox>
                                                                <%--<asp:DropDownList ID="SalaryEncryptionKeyValidationDropDownList" runat="server">
                                                                </asp:DropDownList>--%>
                                                                <asp:HiddenField ID="SalaryEncryptionKeyValidationDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextPAYROLLImageButton" runat="server" OnClick="NextPAYROLLImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PAYROLLPanel2" runat="server">
                                                <fieldset>
                                                    <legend>PAYROLL II</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Is Dispensation Free
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="IsDispensationFreeRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="IsDispensationFreeRadioButtonListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Klik BCA Account No
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="KlikBCAAccountNoTextBox" runat="server" MaxLength="10"></asp:TextBox>
                                                                <asp:HiddenField ID="KlikBCAAccountNoTextBoxHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Klik BCA Bank Code Setup
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="KlikBCABankCodeSetupTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="KlikBCABankCodeSetupTextBoxHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Klik BCA Company Code
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="KlikBCACompanyCodeTextBox" runat="server" MaxLength="13"></asp:TextBox>
                                                                <asp:HiddenField ID="KlikBCACompanyCodeTextBoxHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Leave Can Take Leave After Work Time
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="LeaveCanTakeLeaveAfterWorkTimeDDL" runat="server">
                                                                    <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="LeaveCanTakeLeaveAfterWorkTimeDDLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Leave Method
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="LeaveMethodDropDownList" runat="server">
                                                                    <asp:ListItem Text="Period" Value="Period" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="WorkTime" Value="WorkTime"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="LeaveMethodDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Leave Period Effective
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="LeavePeriodEffectiveDropDownList" runat="server">
                                                                    <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="LeavePeriodEffectiveDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Min Period THR Process
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="MinPeriodTHRProcessDropDownList" runat="server">
                                                                    <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="MinPeriodTHRProcessDropDownListHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Over Time
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="OverTimeTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="OverTimeTextBoxHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Payroll Authorized Sign Image
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PayrollAuthorizedSignImageTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="PayrollAuthorizedSignImageTextBoxHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Payroll Authorized Sign Name
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PayrollAuthorizedSignNameTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="PayrollAuthorizedSignNameTextBoxHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Pensiun
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PensiunTextBox" runat="server" MaxLength="2"></asp:TextBox>
                                                                <asp:HiddenField ID="PensiunTextBoxHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PKP Rounding
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="PKPRoundingRBL" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="PKPRoundingRBLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Premi Group By
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="PremiGroupByDDL" runat="server">
                                                                    <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Employee Type" Value="Employee Type"></asp:ListItem>
                                                                    <asp:ListItem Text="Work Place" Value="Work Place"></asp:ListItem>
                                                                    <asp:ListItem Text=" Employee ID" Value=" Employee ID"></asp:ListItem>
                                                                    <asp:ListItem Text="Job Title" Value="Job Title"></asp:ListItem>
                                                                    <asp:ListItem Text="Job Level" Value="Job Level"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="PremiGroupByDDLHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextPAYROLL2ImageButton" runat="server" OnClick="NextPAYROLL2ImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table id="EditTable" runat="server">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="GeneralPanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>General</legend>
                                                    <table cellpadding="3" cellspacing="0" width="500px">
                                                        <tr>
                                                            <td>
                                                                Date Format
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DateFormatDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="dd/MM/yyyy" Value="dd/MM/yyyy" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="MM/dd/yyyy" Value="MM/dd/yyyy"></asp:ListItem>
                                                                    <asp:ListItem Text="yyyy/MM/dd" Value="yyyy/MM/dd"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Logo'Position
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="PosisiLogoDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="SIP' Logo placed at Header's Right Side" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="SIP' Logo placed at Header's Left Side" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Theme
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ThemeDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="Plain" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="GoGreen" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="BlueMarine" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Dodge" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="200px">
                                                                Enable User's Job Title While Generate Reports
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="ViewJobTitlePrintReportDDL" runat="server">
                                                            <asp:ListItem Text="Don't Show" Value="0" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="Show" Value="1"></asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                                <asp:RadioButtonList ID="ViewJobTitlePrintReportRadioButtonListEdit" runat="server"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Don't Show" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Show" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextGeneralImageButtonEdit" runat="server" OnClick="NextGeneralImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="GeneralPanelEdit2" runat="server">
                                                <fieldset>
                                                    <legend>General II</legend>
                                                    <table cellpadding="3" cellspacing="0" width="500px">
                                                        <tr>
                                                            <td>
                                                                Consolidation ID
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ConsolidationIDDropDownList" runat="server">
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="ConsolidationIDHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="GeneralImageButton2" runat="server" OnClick="NextGeneralImageButton2_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="NCCPanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>NCC</legend>
                                                    <table cellpadding="3" cellspacing="0" width="500px">
                                                        <tr>
                                                            <td>
                                                                Active PG Year
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="ActivePGYearTextBox" runat="server" MaxLength="4"></asp:TextBox>
                                                                <asp:HiddenField ID="ActivePGYearHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Flyer Email
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="FlyerEmailTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="FlyerEmailHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                TAT Timer
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TATTimerTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="TATTimerHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NCCImageButton" runat="server" OnClick="NextNCCImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="BILLINGPanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>BILLING</legend>
                                                    <table cellpadding="3" cellspacing="0" width="500px">
                                                        <tr>
                                                            <td>
                                                                Billing Authorized Sign Image
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingAuthorizedSignImageTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingAuthorizedSignImageHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Authorized Sign Name
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingAuthorizedSignNameTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingAuthorizedSignNameHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Email To
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingEmailToTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingEmailToHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Fax
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingFaxTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingFaxHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Footer Image
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingFooterImageTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingFooterImageHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Footer Text
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingFooterTextTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingFooterTextHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Footer Text 2
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingFooterText2TextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingFooterText2HiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Header Image
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingHeaderImageTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingHeaderImageHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Left Image
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingLeftImageTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingLeftImageHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing SMS Center
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingSMSCenterTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingSMSCenterHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Billing Telp
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="BillingTelpTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="BillingTelpHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                RDLC Billing Invoice Send Email
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="RDLCBillingInvoiceSendEmailTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="RDLCBillingInvoiceSendEmailHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="BILLINGImageButton" runat="server" OnClick="NextBILLINGImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="HUMANRESOURCEPanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>HUMAN RESOURCE</legend>
                                                    <table cellpadding="3" cellspacing="0" width="500px">
                                                        <tr>
                                                            <td>
                                                                PPh
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PPhTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="PPhHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PPh Jabatan
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PPhJabatanTextBox" runat="server" MaxLength="3"></asp:TextBox>%
                                                                <asp:HiddenField ID="PPhJabatanHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PPh Max Anak
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PPhMaxAnakTextBox" runat="server" MaxLength="1"></asp:TextBox>
                                                                <asp:HiddenField ID="PPhMaxAnakHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PPh Max Jabatan
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PPhMaxJabatanTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="PPhMaxJabatanHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PPh Method
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="PPhMethodDropDownList" runat="server">
                                                                    <asp:ListItem Text="STANDARD" Value="STANDARD"></asp:ListItem>
                                                                    <asp:ListItem Text="ACCUMULATION" Value="ACCUMULATION"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="PPhMethodHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PPh PTKP
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PPhPTKPTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="PPhPTKPHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PPh PTKPT
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PPhPTKPTTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="PPhPTKPTHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="HUMANRESOURCEImageButton" runat="server" OnClick="NextHUMANRESOURCEImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="POSPanelEdit2" runat="server">
                                                <fieldset>
                                                    <legend>POS</legend>
                                                    <table cellpadding="3" cellspacing="0" width="500px">
                                                        <tr>
                                                            <td>
                                                                Have Product Item Duration
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="HaveProductItemDurationRadioButtonList" runat="server">
                                                                    <asp:ListItem Text="True" Value="True" Selected ="True"></asp:ListItem>
                                                                    <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="HaveProductItemDurationHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Ignore Item Discount
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="IgnoreItemDiscountRadioButtonList" runat="server">
                                                                    <asp:ListItem Text="True" Value="True" Selected ="True"></asp:ListItem>
                                                                    <asp:ListItem Text="False" Value="False"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:HiddenField ID="IgnoreItemDiscountHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                POS Booking Time Limit After
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="POSBookingTimeLimitAfterTextBox" runat="server"></asp:TextBox>Minutes
                                                                <asp:HiddenField ID="POSBookingTimeLimitAfterHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                POS Booking Time Limit Before
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="POSBookingTimeLimitBeforeTextBox" runat="server"></asp:TextBox>Minutes
                                                                <asp:HiddenField ID="POSBookingTimeLimitBeforeHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                POS Default Cust Code
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="POSDefaultCustCodeTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="POSDefaultCustCodeHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                POS Rounding
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="POSRoundingTextBox" runat="server"></asp:TextBox>
                                                                <asp:HiddenField ID="POSRoundingHiddenField" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="POSImageButton2" runat="server" OnClick="NextPOSImageButton2_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="ACCOUNTINGPanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>ACCOUNTING</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                JE Account Curr Lock
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <%--<asp:DropDownList ID="JEAccountCurrLockDDL" runat="server">
                                                            <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                                <asp:RadioButtonList ID="JEAccountCurrLockRadioButtonListEdit" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Currency Format
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="CurrencyFormatDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="#.##0,00" Value="#.##0,00" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextAccountingImageButtonEdit" runat="server" OnClick="NextACCOUNTINGImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset></asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="ACCOUNTING2PanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>ACCOUNTING II</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                FA Code Auto Number
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="FACodeAutoNumberRadioButtonListEdit" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Not Auto" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Auto" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                FA Code Digit Number
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="FACodeDigitNumberDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="3" Value="3" Selected="True"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                FA Depreciation Method
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="FADepreciationMethodDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="StraightLine" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="DoubleDeclining" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Auto Nmbr Loc For FA
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="AutoNmbrLocForFARadioButtonListEdit" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="True" Value="T" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="False" Value="F"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Auto Nmbr Period For FA
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="AutoNmbrPeriodForFARadioButtonListEdit" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="True" Value="T" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="False" Value="F"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextAccounting2ImageButtonEdit" runat="server" OnClick="NextACCOUNTING2ImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="STOCKCONTROLPanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>STOCK CONTROL</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                BOL Reference Type
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="BOLReferenceTypeDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="Sales Order" Value="SO" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Delivery Order" Value="DO"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                COGS Method
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="COGSMethodDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="FIFO" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="LIFO" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="AVG" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextSTOCKCONTROLImageButtonEdit" runat="server" OnClick="NextSTOCKCONTROLImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="POSPanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>POS</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Transaction Wrhs Default
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="TransactionWrhsDefaultDropDownListEdit" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Transaction Wrhs Deposit IN
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="TransactionWrhsDepositINDropDownListEdit" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Transaction Wrhs Location Default
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="TransactionWrhsLocationDefaultDropDownListEdit" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Transaction Wrhs Location Deposit IN
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="TransactionWrhsLocationDepositINDropDownListEdit" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td>
                                                                POS Internet Reminder Duration
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="POSInternetReminderDurationTextBoxEdit" runat="server" MaxLength="3"></asp:TextBox>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextPOSImageButtonEdit" runat="server" OnClick="NextPOSImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PAYROLLPanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>PAYROLL</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Salary Encryption
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="SalaryEncryptionRadioButtonListEdit" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="UnEncrypt" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Encrypt" Value="1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Salary Encryption Key
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="SalaryEncryptionKeyTextBoxEdit" runat="server"></asp:TextBox>
                                                                <%--<asp:DropDownList ID="SalaryEncryptionKeyDropDownListEdit" runat="server">
                                                                </asp:DropDownList>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Salary Encryption Key Validation
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="SalaryEncryptionKeyValidationTextBoxEdit" runat="server"></asp:TextBox>
                                                                <%-- <asp:DropDownList ID="SalaryEncryptionKeyValidationDropDownListEdit" runat="server">
                                                                </asp:DropDownList>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextPAYROLLImageButtonEdit" runat="server" OnClick="NextPAYROLLImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PAYROLL2PanelEdit" runat="server">
                                                <fieldset>
                                                    <legend>PAYROLL II</legend>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Is Dispensation Free
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="IsDispensationFreeRadioButtonListEdit" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Klik BCA Account No
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="KlikBCAAccountNoTextBoxEdit" runat="server" MaxLength="10"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Klik BCA Bank Code Setup
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="KlikBCABankCodeSetupTextBoxEdit" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Klik BCA Company Code
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="KlikBCACompanyCodeTextBoxEdit" runat="server" MaxLength="13"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Leave Can Take Leave After Work Time
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="LeaveCanTakeLeaveAfterWorkTimeDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Leave Method
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="LeaveMethodDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="Period" Value="Period" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="WorkTime" Value="WorkTime"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Leave Period Effective
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="LeavePeriodEffectiveDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Min Period THR Process
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="MinPeriodTHRProcessDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Over Time
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="OverTimeTextBoxEdit" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Payroll Authorized Sign Image
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PayrollAuthorizedSignImageTextBoxEdit" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Payroll Authorized Sign Name
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PayrollAuthorizedSignNameTextBoxEdit" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Pensiun
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="PensiunTextBoxEdit" runat="server" MaxLength="2"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                PKP Rounding
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="PKPRoundingRadioButtonListEdit" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Premi Group By
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="PremiGroupByDropDownListEdit" runat="server">
                                                                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                                    <asp:ListItem Text="Employee Type" Value="Employee Type"></asp:ListItem>
                                                                    <asp:ListItem Text="Work Place" Value="Work Place"></asp:ListItem>
                                                                    <asp:ListItem Text=" Employee ID" Value=" Employee ID"></asp:ListItem>
                                                                    <asp:ListItem Text="Job Title" Value="Job Title"></asp:ListItem>
                                                                    <asp:ListItem Text="Job Level" Value="Job Level"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:ImageButton ID="NextPAYROLL2ImageButtonEdit" runat="server" OnClick="NextPAYROLL2ImageButton_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <%--    </form>--%>
</asp:Content>

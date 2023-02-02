using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.CompanyConfigurationWeb
{
    public partial class CompanyConfigurationWeb : System.Web.UI.Page
    {
        private CompanyConfig _companyConfigurationBL = new CompanyConfig();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private DatabaseBL _dataBaseBL = new DatabaseBL();
        private String[] _configCode = new String[39];
        //private String _dateFormat = "DateFormat", 
        //    _membershipDbName = "MembershipDbName",
        // _posisiLogo = "PosisiLogo", 
        // _theme = "Theme", 
        // _viewJobTitlePrintReport = "ViewJobTitlePrintReport",
        // _jeAccountCurrLock = "JEAccountCurrLock", 
        // _currencyFormat = "CurrencyFormat", 
        // _faCodeAutoNumber = , 
        // _faCodeDigitNumber = "FACodeDigitNumber", 
        // _faDepreciationMethod = "FADepreciationMethod", 
        // _autoNmbrLocForFA = "AutoNmbrLocForFA",
        // _autoNmbrPeriodForFA = "AutoNmbrPeriodForFA",
        // _bolReferenceType = "BOLReferenceType", 
        // _cogsMethod = "COGSMethod",
        // _transactionWrhsDefault = "TransactionWrhsDefault",
        // _transactionWrhsDepositIN = "TransactionWrhsDepositIN",
        // _transactionWrhsLocationDefault = "TransactionWrhsLocationDefault", 
        // _transactionWrhsLocationDepositIN = "TransactionWrhsLocationDepositIN", 
        // _salaryEncryption = "SalaryEncryption",
        // _salaryEncryptionKey = "SalaryEncryptionKey",
        // _salaryEncryptionKeyValidation = "SalaryEncryptionKeyValidation", 
        // _isDispensationFree = "IsDispensationFree",
        //  _klikBCAAccountNo = "KlikBCAAccountNo", 
        //  _klikBCABankCodeSetup = "KlikBCABankCodeSetup",
        // _klikBCACompanyCode = , 
        // _leaveCanTakeLeaveAfterWorkTime = "LeaveCanTakeLeaveAfterWorkTime",
        // _leaveMethod = "LeaveMethod", 
        // _leavePeriodEffective = "LeavePeriodEffective", 
        // _minPeriodTHRProcess = "MinPeriodTHRProcess",
        // _organizationUnitDeparmentLevel = "OrganizationUnitDeparmentLevel", 
        // _overTime = "OverTime", 
        // _payrollAuthorizedSignImage = "PayrollAuthorizedSignImage",
        // _PayrollAuthorizedSignName = "PayrollAuthorizedSignName", 
        // _pensiun = "Pensiun", 
        // _periodConfig = "PeriodConfig",
        // _pkpRounding = "PKPRounding",
        // _premiGroupBy = "PremiGroupBy";


        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetConfigCode();
            if (!this.Page.IsPostBack == true)
            {

                this.CompanyHiddenField.Value = _companyConfigurationBL.CekGroupConfig("BILLING").ToString();

                bool _cekAwal = _companyConfigurationBL.CekCompanyConfig();
                if (!_cekAwal)
                {
                    this.CreateTable.Visible = true;
                    this.EditTable.Visible = false;
                    this.WarehouseAndWarehouseLocationForDLLCreate();

                }
                else
                {
                    this.CreateTable.Visible = false;
                    this.EditTable.Visible = true;
                    this.WarehouseAndWarehouseLocationForDLLEdit();
                    this.ConsolidationIDDDL();
                    this.ShowDataEdit();
                }

                this.SetAttribut();
                this.SetAwal();
            }

        }

        private void ConsolidationIDDDL() 
        {
            this.ConsolidationIDDropDownList.DataSource = _dataBaseBL.GetListMsDbConsolidationList();
            this.ConsolidationIDDropDownList.DataValueField = "ConsolidationID";
            this.ConsolidationIDDropDownList.DataTextField = "ConsolidationID";
            this.ConsolidationIDDropDownList.DataBind();
        }

        private List<MsWarehouse> WarehouseForDDL()
        {
            List<MsWarehouse> _result = new List<MsWarehouse>();

            _result = _warehouseBL.GetListForDDL();

            return _result;
        }

        private List<MsWrhsLocation> WarehouseLocationForDDL()
        {
            List<MsWrhsLocation> _result = new List<MsWrhsLocation>();

            _result = _warehouseBL.GetListWrhsLocationForDDL();

            return _result;
        }

        private void WarehouseAndWarehouseLocationForDLLCreate()
        {
            this.TransactionWrhsDefaultDropDownList.DataSource = this.WarehouseForDDL();
            this.TransactionWrhsDefaultDropDownList.DataValueField = "WrhsCode";
            this.TransactionWrhsDefaultDropDownList.DataTextField = "WrhsName";
            this.TransactionWrhsDefaultDropDownList.DataBind();

            this.TransactionWrhsDepositINDropDownList.DataSource = this.WarehouseForDDL();
            this.TransactionWrhsDepositINDropDownList.DataValueField = "WrhsCode";
            this.TransactionWrhsDepositINDropDownList.DataTextField = "WrhsName";
            this.TransactionWrhsDepositINDropDownList.DataBind();

            this.TransactionWrhsLocationDefaultDropDownList.DataSource = this.WarehouseLocationForDDL();
            this.TransactionWrhsLocationDefaultDropDownList.DataValueField = "WLocationCode";
            this.TransactionWrhsLocationDefaultDropDownList.DataTextField = "WLocationName";
            this.TransactionWrhsLocationDefaultDropDownList.DataBind();

            this.TransactionWrhsLocationDepositINDropDownList.DataSource = this.WarehouseLocationForDDL();
            this.TransactionWrhsLocationDepositINDropDownList.DataValueField = "WLocationCode";
            this.TransactionWrhsLocationDepositINDropDownList.DataTextField = "WLocationName";
            this.TransactionWrhsLocationDepositINDropDownList.DataBind();
        }

        private void WarehouseAndWarehouseLocationForDLLEdit()
        {
            this.TransactionWrhsDefaultDropDownListEdit.DataSource = this.WarehouseForDDL();
            this.TransactionWrhsDefaultDropDownListEdit.DataValueField = "WrhsCode";
            this.TransactionWrhsDefaultDropDownListEdit.DataTextField = "WrhsName";
            this.TransactionWrhsDefaultDropDownListEdit.DataBind();

            this.TransactionWrhsDepositINDropDownListEdit.DataSource = this.WarehouseForDDL();
            this.TransactionWrhsDepositINDropDownListEdit.DataValueField = "WrhsCode";
            this.TransactionWrhsDepositINDropDownListEdit.DataTextField = "WrhsName";
            this.TransactionWrhsDepositINDropDownListEdit.DataBind();

            this.TransactionWrhsLocationDefaultDropDownListEdit.DataSource = this.WarehouseLocationForDDL();
            this.TransactionWrhsLocationDefaultDropDownListEdit.DataValueField = "WLocationCode";
            this.TransactionWrhsLocationDefaultDropDownListEdit.DataTextField = "WLocationName";
            this.TransactionWrhsLocationDefaultDropDownListEdit.DataBind();

            this.TransactionWrhsLocationDepositINDropDownListEdit.DataSource = this.WarehouseLocationForDDL();
            this.TransactionWrhsLocationDepositINDropDownListEdit.DataValueField = "WLocationCode";
            this.TransactionWrhsLocationDepositINDropDownListEdit.DataTextField = "WLocationName";
            this.TransactionWrhsLocationDepositINDropDownListEdit.DataBind();
        }

        private void ShowDataEdit()
        {
            List<CompanyConfiguration> _listCompanyConfig = _companyConfigurationBL.GetListCompanyConfig();
            //int _num = 0;
            foreach (var _row in _listCompanyConfig)
            {
                //_num += 1;

                /// insert
                if (_row.ConfigCode == "DateFormat")
                {
                    this.DateFormatDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.DateFormatDropDownListEdit.Enabled = false;
                    else
                        this.DateFormatDropDownListEdit.Enabled = true;
                }
                //else if (_num == 2)
                //"SIP_Membership";//MembershipDbName
                else if (_row.ConfigCode == "PosisiLogo")
                {
                    this.PosisiLogoDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PosisiLogoDropDownListEdit.Enabled = false;
                    else
                        this.PosisiLogoDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "Theme")
                {
                    this.ThemeDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.ThemeDropDownListEdit.Enabled = false;
                    else
                        this.ThemeDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "ViewJobTitlePrintReport")
                {
                    this.ViewJobTitlePrintReportRadioButtonListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.ViewJobTitlePrintReportRadioButtonListEdit.Enabled = false;
                    else
                        this.ViewJobTitlePrintReportRadioButtonListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "JEAccountCurrLock")
                {
                    this.JEAccountCurrLockRadioButtonListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.JEAccountCurrLockRadioButtonListEdit.Enabled = false;
                    else
                        this.JEAccountCurrLockRadioButtonListEdit.Enabled = true;
                }

                else if (_row.ConfigCode == "CurrencyFormat")
                {
                    this.CurrencyFormatDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.CurrencyFormatDropDownListEdit.Enabled = false;
                    else
                        this.CurrencyFormatDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "FACodeAutoNumber")
                {
                    this.FACodeAutoNumberRadioButtonListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.FACodeAutoNumberRadioButtonListEdit.Enabled = false;
                    else
                        this.FACodeAutoNumberRadioButtonListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "FACodeDigitNumber")
                {
                    this.FACodeDigitNumberDropDownListEdit.SelectedValue = _row.SetValue;

                    if (_row.AlowEdit != 'Y')
                        this.FACodeDigitNumberDropDownListEdit.Enabled = false;
                    else
                        this.FACodeDigitNumberDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "FADepreciationMethod")
                {
                    this.FADepreciationMethodDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.FADepreciationMethodDropDownListEdit.Enabled = false;
                    else
                        this.FADepreciationMethodDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "AutoNmbrLocForFA")
                {
                    this.AutoNmbrLocForFARadioButtonListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.AutoNmbrLocForFARadioButtonListEdit.Enabled = false;
                    else
                        this.AutoNmbrLocForFARadioButtonListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "AutoNmbrPeriodForFA")
                {
                    this.AutoNmbrPeriodForFARadioButtonListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.AutoNmbrPeriodForFARadioButtonListEdit.Enabled = false;
                    else
                        this.AutoNmbrPeriodForFARadioButtonListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "BOLReferenceType")
                {
                    this.BOLReferenceTypeDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BOLReferenceTypeDropDownListEdit.Enabled = false;
                    else
                        this.BOLReferenceTypeDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "COGSMethod")
                {
                    this.COGSMethodDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.COGSMethodDropDownListEdit.Enabled = false;
                    else
                        this.COGSMethodDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "TransactionWrhsDefault")
                {
                    this.TransactionWrhsDefaultDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.TransactionWrhsDefaultDropDownListEdit.Enabled = false;
                    else
                        this.TransactionWrhsDefaultDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "TransactionWrhsDepositIN")
                {
                    this.TransactionWrhsDepositINDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.TransactionWrhsDepositINDropDownListEdit.Enabled = false;
                    else
                        this.TransactionWrhsDepositINDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "TransactionWrhsLocationDefault")
                {
                    this.TransactionWrhsLocationDefaultDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.TransactionWrhsLocationDefaultDropDownListEdit.Enabled = false;
                    else
                        this.TransactionWrhsLocationDefaultDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "TransactionWrhsLocationDepositIN")
                {
                    this.TransactionWrhsLocationDepositINDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.TransactionWrhsLocationDepositINDropDownListEdit.Enabled = false;
                    else
                        this.TransactionWrhsLocationDepositINDropDownListEdit.Enabled = true;
                }
                //else if (_row.ConfigCode == "POSInternetReminderDuration")
                //{
                //    this.POSInternetReminderDurationTextBoxEdit.Text = _row.SetValue;
                //    if (_row.AlowEdit != 'Y')
                //        this.POSInternetReminderDurationTextBoxEdit.Enabled = false;
                //    else
                //        this.POSInternetReminderDurationTextBoxEdit.Enabled = true;
                //}
                else if (_row.ConfigCode == "SalaryEncryption")
                {
                    this.SalaryEncryptionRadioButtonListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.SalaryEncryptionRadioButtonListEdit.Enabled = false;
                    else
                        this.SalaryEncryptionRadioButtonListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "SalaryEncryptionKey")
                {
                    this.SalaryEncryptionKeyTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.SalaryEncryptionKeyTextBoxEdit.Enabled = false;
                    else
                        this.SalaryEncryptionKeyTextBoxEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "SalaryEncryptionKeyValidation")
                {
                    this.SalaryEncryptionKeyValidationTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.SalaryEncryptionKeyValidationTextBoxEdit.Enabled = false;
                    else
                        this.SalaryEncryptionKeyValidationTextBoxEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "IsDispensationFree")
                {
                    this.IsDispensationFreeRadioButtonListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.IsDispensationFreeRadioButtonListEdit.Enabled = false;
                    else
                        this.IsDispensationFreeRadioButtonListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "KlikBCAAccountNo")
                {
                    this.KlikBCAAccountNoTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.KlikBCAAccountNoTextBoxEdit.Enabled = false;
                    else
                        this.KlikBCAAccountNoTextBoxEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "KlikBCABankCodeSetup")
                {
                    this.KlikBCABankCodeSetupTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.KlikBCABankCodeSetupTextBoxEdit.Enabled = false;
                    else
                        this.KlikBCABankCodeSetupTextBoxEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "KlikBCACompanyCode")
                {
                    this.KlikBCACompanyCodeTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.KlikBCACompanyCodeTextBoxEdit.Enabled = false;
                    else
                        this.KlikBCACompanyCodeTextBoxEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "LeaveCanTakeLeaveAfterWorkTime")
                {
                    this.LeaveCanTakeLeaveAfterWorkTimeDropDownListEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.LeaveCanTakeLeaveAfterWorkTimeDropDownListEdit.Enabled = false;
                    else
                        this.LeaveCanTakeLeaveAfterWorkTimeDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "LeaveMethod")
                {
                    this.LeaveMethodDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.LeaveMethodDropDownListEdit.Enabled = false;
                    else
                        this.LeaveMethodDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "LeavePeriodEffective")
                {
                    this.LeavePeriodEffectiveDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.LeavePeriodEffectiveDropDownListEdit.Enabled = false;
                    else
                        this.LeavePeriodEffectiveDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "MinPeriodTHRProcess")
                {
                    this.MinPeriodTHRProcessDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.MinPeriodTHRProcessDropDownListEdit.Enabled = false;
                    else
                        this.MinPeriodTHRProcessDropDownListEdit.Enabled = true;
                }
                //else if (_row.ConfigCode == 30)
                //    "0";//OrganizationUnitDeparmentLevel
                else if (_row.ConfigCode == "OverTime")
                {
                    this.OverTimeTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.OverTimeTextBoxEdit.Enabled = false;
                    else
                        this.OverTimeTextBoxEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "PayrollAuthorizedSignImage")
                {
                    this.PayrollAuthorizedSignImageTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PayrollAuthorizedSignImageTextBoxEdit.Enabled = false;
                    else
                        this.PayrollAuthorizedSignImageTextBoxEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "PayrollAuthorizedSignName")
                {
                    this.PayrollAuthorizedSignNameTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PayrollAuthorizedSignNameTextBoxEdit.Enabled = false;
                    else
                        this.PayrollAuthorizedSignNameTextBoxEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "Pensiun")
                {
                    this.PensiunTextBoxEdit.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PensiunTextBoxEdit.Enabled = false;
                    else
                        this.PensiunTextBoxEdit.Enabled = true;
                }
                //else if (_row.ConfigCode == 35)
                //    "0";//PeriodConfig
                else if (_row.ConfigCode == "PKPRounding")
                {
                    this.PKPRoundingRadioButtonListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PKPRoundingRadioButtonListEdit.Enabled = false;
                    else
                        this.PKPRoundingRadioButtonListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "PremiGroupBy")
                {
                    this.PremiGroupByDropDownListEdit.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PremiGroupByDropDownListEdit.Enabled = false;
                    else
                        this.PremiGroupByDropDownListEdit.Enabled = true;
                }
                else if (_row.ConfigCode == "ConsolidationID")//////Hidden
                {
                    this.ConsolidationIDDropDownList.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.ConsolidationIDDropDownList.Enabled = false;
                    else
                        this.ConsolidationIDDropDownList.Enabled = true;
                }
                //else if (_row.ConfigCode == "JEAccountCurrLock")
                //{
                //    this.JEAccountCurrLockDropDownList.SelectedValue = _row.SetValue;
                //    if (_row.AlowEdit != 'Y')
                //        this.JEAccountCurrLockDropDownList.Enabled = false;
                //    else
                //        this.JEAccountCurrLockDropDownList.Enabled = true;
                //}
                else if (_row.ConfigCode == "ActivePGYear")
                {
                    this.ActivePGYearTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.ActivePGYearTextBox.Enabled = false;
                    else
                        this.ActivePGYearTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "FlyerEmail")
                {
                    this.FlyerEmailTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.FlyerEmailTextBox.Enabled = false;
                    else
                        this.FlyerEmailTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "TATTimer")
                {
                    this.TATTimerTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.TATTimerTextBox.Enabled = false;
                    else
                        this.TATTimerTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingAuthorizedSignImage")
                {
                    this.BillingAuthorizedSignImageTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingAuthorizedSignImageTextBox.Enabled = false;
                    else
                        this.BillingAuthorizedSignImageTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingAuthorizedSignName")
                {
                    this.BillingAuthorizedSignNameTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingAuthorizedSignNameTextBox.Enabled = false;
                    else
                        this.BillingAuthorizedSignNameTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingEmailTo")
                {
                    this.BillingEmailToTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingEmailToTextBox.Enabled = false;
                    else
                        this.BillingEmailToTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingFax")
                {
                    this.BillingFaxTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingFaxTextBox.Enabled = false;
                    else
                        this.BillingFaxTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingFooterImage")
                {
                    this.BillingFooterImageTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingFooterImageTextBox.Enabled = false;
                    else
                        this.BillingFooterImageTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingFooterText")
                {
                    this.BillingFooterTextTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingFooterTextTextBox.Enabled = false;
                    else
                        this.BillingFooterTextTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingFooterText2")
                {
                    this.BillingFooterText2TextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingFooterText2TextBox.Enabled = false;
                    else
                        this.BillingFooterText2TextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingHeaderImage")
                {
                    this.BillingHeaderImageTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingHeaderImageTextBox.Enabled = false;
                    else
                        this.BillingHeaderImageTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingLeftImage")
                {
                    this.BillingLeftImageTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingLeftImageTextBox.Enabled = false;
                    else
                        this.BillingLeftImageTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingSMSCenter")
                {
                    this.BillingSMSCenterTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingSMSCenterTextBox.Enabled = false;
                    else
                        this.BillingSMSCenterTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "BillingTelp")
                {
                    this.BillingTelpTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.BillingTelpTextBox.Enabled = false;
                    else
                        this.BillingTelpTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "RDLCBillingInvoiceSendEmail")
                {
                    this.RDLCBillingInvoiceSendEmailTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.RDLCBillingInvoiceSendEmailTextBox.Enabled = false;
                    else
                        this.RDLCBillingInvoiceSendEmailTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "PPh")
                {
                    this.PPhTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PPhTextBox.Enabled = false;
                    else
                        this.PPhTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "PPhJabatan")
                {
                    this.PPhJabatanTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PPhJabatanTextBox.Enabled = false;
                    else
                        this.PPhJabatanTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "PPhMaxAnak")
                {
                    this.PPhMaxAnakTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PPhMaxAnakTextBox.Enabled = false;
                    else
                        this.PPhMaxAnakTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "PPhMaxJabatan")
                {
                    this.PPhMaxJabatanTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PPhMaxJabatanTextBox.Enabled = false;
                    else
                        this.PPhMaxJabatanTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "PPhMethod")
                {
                    this.PPhMethodDropDownList.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PPhMethodDropDownList.Enabled = false;
                    else
                        this.PPhMethodDropDownList.Enabled = true;
                }
                else if (_row.ConfigCode == "PPhPTKP")
                {
                    this.PPhPTKPTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PPhPTKPTextBox.Enabled = false;
                    else
                        this.PPhPTKPTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "PPhPTKPT")
                {
                    this.PPhPTKPTTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.PPhPTKPTTextBox.Enabled = false;
                    else
                        this.PPhPTKPTTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "HaveProductItemDuration")
                {
                    this.HaveProductItemDurationRadioButtonList.SelectedValue = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.HaveProductItemDurationRadioButtonList.Enabled = false;
                    else
                        this.HaveProductItemDurationRadioButtonList.Enabled = true;
                }
                else if (_row.ConfigCode == "IgnoreItemDiscount")
                {
                    this.IgnoreItemDiscountRadioButtonList.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.IgnoreItemDiscountRadioButtonList.Enabled = false;
                    else
                        this.IgnoreItemDiscountRadioButtonList.Enabled = true;
                }
                else if (_row.ConfigCode == "POSBookingTimeLimitAfter")
                {
                    this.POSBookingTimeLimitAfterTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.POSBookingTimeLimitAfterTextBox.Enabled = false;
                    else
                        this.POSBookingTimeLimitAfterTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "POSBookingTimeLimitBefore")
                {
                    this.POSBookingTimeLimitBeforeTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.POSBookingTimeLimitBeforeTextBox.Enabled = false;
                    else
                        this.POSBookingTimeLimitBeforeTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "POSDefaultCustCode")
                {
                    this.POSDefaultCustCodeTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.POSDefaultCustCodeTextBox.Enabled = false;
                    else
                        this.POSDefaultCustCodeTextBox.Enabled = true;
                }
                else if (_row.ConfigCode == "POSRounding")
                {
                    this.POSRoundingTextBox.Text = _row.SetValue;
                    if (_row.AlowEdit != 'Y')
                        this.POSRoundingTextBox.Enabled = false;
                    else
                        this.POSRoundingTextBox.Enabled = true;
                }
            }

        }

        private void SetAttribut()
        {
            if (this.CreateTable.Visible == true)
            {
                this.NextGeneralImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextACCOUNTINGImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextACCOUNTING2ImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextSTOCKCONTROLImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextPOSImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextPAYROLLImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextPAYROLL2ImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";

                //this.POSInternetReminderDurationTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                //this.POSInternetReminderDurationTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.KlikBCAAccountNoTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.KlikBCAAccountNoTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.KlikBCACompanyCodeTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.KlikBCACompanyCodeTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PensiunTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PensiunTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
            }
            else
            {
                this.NextGeneralImageButtonEdit.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextAccountingImageButtonEdit.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextAccounting2ImageButtonEdit.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextSTOCKCONTROLImageButtonEdit.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextPOSImageButtonEdit.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextPAYROLLImageButtonEdit.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NextPAYROLL2ImageButtonEdit.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";

                this.GeneralImageButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.NCCImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.BILLINGImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.HUMANRESOURCEImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.POSImageButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";

                //this.POSInternetReminderDurationTextBoxEdit.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                //this.POSInternetReminderDurationTextBoxEdit.Attributes.Add("OnChange", "HarusAngka(this);");

                this.KlikBCAAccountNoTextBoxEdit.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.KlikBCAAccountNoTextBoxEdit.Attributes.Add("OnChange", "HarusAngka(this);");

                this.KlikBCACompanyCodeTextBoxEdit.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.KlikBCACompanyCodeTextBoxEdit.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PensiunTextBoxEdit.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PensiunTextBoxEdit.Attributes.Add("OnChange", "HarusAngka(this);");

                this.ActivePGYearTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.ActivePGYearTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.TATTimerTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.TATTimerTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.BillingFaxTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.BillingFaxTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.BillingSMSCenterTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.BillingSMSCenterTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.BillingTelpTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.BillingTelpTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PPhJabatanTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PPhJabatanTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PPhMaxAnakTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PPhMaxAnakTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PPhMaxJabatanTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PPhMaxJabatanTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PPhPTKPTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PPhPTKPTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PPhPTKPTTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PPhPTKPTTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.POSBookingTimeLimitAfterTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.POSBookingTimeLimitAfterTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.POSBookingTimeLimitBeforeTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.POSBookingTimeLimitBeforeTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.POSRoundingTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.POSRoundingTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
            }
        }

        private void SetConfigCode()
        {
            for (int i = 0; i <= 37; i++)
            {
                if (i == 0)
                    _configCode[i] = "";
                else if (i == 1)
                    _configCode[i] = "DateFormat";
                else if (i == 2)
                    _configCode[i] = "MembershipDbName";
                else if (i == 3)
                    _configCode[i] = "PosisiLogo";
                else if (i == 4)
                    _configCode[i] = "Theme";
                else if (i == 5)
                    _configCode[i] = "ViewJobTitlePrintReport";
                else if (i == 6)
                    _configCode[i] = "JEAccountCurrLock";
                else if (i == 7)
                    _configCode[i] = "CurrencyFormat";
                else if (i == 8)
                    _configCode[i] = "FACodeAutoNumber";
                else if (i == 9)
                    _configCode[i] = "FACodeDigitNumber";
                else if (i == 10)
                    _configCode[i] = "FADepreciationMethod";
                else if (i == 11)
                    _configCode[i] = "AutoNmbrLocForFA";
                else if (i == 12)
                    _configCode[i] = "AutoNmbrPeriodForFA";
                else if (i == 13)
                    _configCode[i] = "BOLReferenceType";
                else if (i == 14)
                    _configCode[i] = "COGSMethod";
                else if (i == 15)
                    _configCode[i] = "TransactionWrhsDefault";
                else if (i == 16)
                    _configCode[i] = "TransactionWrhsDepositIN";
                else if (i == 17)
                    _configCode[i] = "TransactionWrhsLocationDefault";
                else if (i == 18)
                    _configCode[i] = "TransactionWrhsLocationDepositIN";
                else if (i == 19)
                    _configCode[i] = "SalaryEncryption";
                else if (i == 20)
                    _configCode[i] = "SalaryEncryptionKey";
                else if (i == 21)
                    _configCode[i] = "SalaryEncryptionKeyValidation";
                else if (i == 22)
                    _configCode[i] = "IsDispensationFree";
                else if (i == 23)
                    _configCode[i] = "KlikBCAAccountNo";
                else if (i == 24)
                    _configCode[i] = "KlikBCABankCodeSetup";
                else if (i == 25)
                    _configCode[i] = "KlikBCACompanyCode";
                else if (i == 26)
                    _configCode[i] = "LeaveCanTakeLeaveAfterWorkTime";
                else if (i == 27)
                    _configCode[i] = "LeaveMethod";
                else if (i == 28)
                    _configCode[i] = "LeavePeriodEffective";
                else if (i == 29)
                    _configCode[i] = "MinPeriodTHRProcess";
                else if (i == 30)
                    _configCode[i] = "OrganizationUnitDeparmentLevel";
                else if (i == 31)
                    _configCode[i] = "OverTime";
                else if (i == 32)
                    _configCode[i] = "PayrollAuthorizedSignImage";
                else if (i == 33)
                    _configCode[i] = "PayrollAuthorizedSignName";
                else if (i == 34)
                    _configCode[i] = "Pensiun";
                else if (i == 35)
                    _configCode[i] = "PeriodConfig";
                else if (i == 36)
                    _configCode[i] = "PKPRounding";
                else if (i == 37)
                    _configCode[i] = "PremiGroupBy";
                //else if (i == 38)
                //    _configCode[i] = "POSInternetReminderDuration";
                else
                    _configCode[i] = "";

            }

        }

        private void SetAwal()
        {
            if (this.CreateTable.Visible == true)
            {
                this.GeneralPanel.Visible = true;
                this.ACCOUNTINGPanel.Visible = false;
                this.AccountingPanel2.Visible = false;
                this.STOCKCONTROLPanel.Visible = false;
                this.POSPanel.Visible = false;
                this.PAYROLLPanel.Visible = false;
                this.PAYROLLPanel2.Visible = false;
            }
            else
            {
                this.GeneralPanelEdit.Visible = true;
                this.ACCOUNTINGPanelEdit.Visible = false;
                this.ACCOUNTING2PanelEdit.Visible = false;
                this.STOCKCONTROLPanelEdit.Visible = false;
                this.POSPanelEdit.Visible = false;
                this.PAYROLLPanelEdit.Visible = false;
                this.PAYROLL2PanelEdit.Visible = false;

                this.GeneralPanelEdit2.Visible = false;
                this.NCCPanelEdit.Visible = false;
                this.BILLINGPanelEdit.Visible = false;
                this.HUMANRESOURCEPanelEdit.Visible = false;
                this.POSPanelEdit2.Visible = false;

            }
        }

        private void ClareLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void NextGeneralImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CreateTable.Visible == true)
            {
                this.GeneralPanel.Visible = false;
                this.ACCOUNTINGPanel.Visible = true;
                this.AccountingPanel2.Visible = false;
                this.STOCKCONTROLPanel.Visible = false;
                this.POSPanel.Visible = false;
                this.PAYROLLPanel.Visible = false;
                this.PAYROLLPanel2.Visible = false;

                this.DateFormatDDLHiddenField.Value = this.DateFormatDDL.SelectedValue;
                this.PosisiLogoDDLHiddenField.Value = this.PosisiLogoDDL.SelectedValue;
                this.ThemeDDLHiddenField.Value = this.ThemeDDL.SelectedValue;
                this.ViewJobTitlePrintReportRadioButtonListHiddenField.Value = this.ViewJobTitlePrintReportRadioButtonList.SelectedValue;
            }
            else
            {
                if (Convert.ToBoolean(this.CompanyHiddenField.Value))
                {
                    this.GeneralPanelEdit2.Visible = true;
                    this.GeneralPanelEdit.Visible = false;
                    this.ACCOUNTINGPanelEdit.Visible = false;
                    this.ACCOUNTING2PanelEdit.Visible = false;
                    this.STOCKCONTROLPanelEdit.Visible = false;
                    this.POSPanelEdit.Visible = false;
                    this.PAYROLLPanelEdit.Visible = false;
                    this.PAYROLL2PanelEdit.Visible = false;
                }
                else
                {
                    this.GeneralPanelEdit.Visible = false;
                    this.ACCOUNTINGPanelEdit.Visible = true;
                    this.ACCOUNTING2PanelEdit.Visible = false;
                    this.STOCKCONTROLPanelEdit.Visible = false;
                    this.POSPanelEdit.Visible = false;
                    this.PAYROLLPanelEdit.Visible = false;
                    this.PAYROLL2PanelEdit.Visible = false;
                }

                this.DateFormatDDLHiddenField.Value = this.DateFormatDropDownListEdit.SelectedValue;
                this.PosisiLogoDDLHiddenField.Value = this.PosisiLogoDropDownListEdit.SelectedValue;
                this.ThemeDDLHiddenField.Value = this.ThemeDropDownListEdit.SelectedValue;
                this.ViewJobTitlePrintReportRadioButtonListHiddenField.Value = this.ViewJobTitlePrintReportRadioButtonListEdit.SelectedValue;
            }
        }

        protected void NextACCOUNTINGImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CreateTable.Visible == true)
            {
                this.GeneralPanel.Visible = false;
                this.ACCOUNTINGPanel.Visible = false;
                this.AccountingPanel2.Visible = true;
                this.STOCKCONTROLPanel.Visible = false;
                this.POSPanel.Visible = false;
                this.PAYROLLPanel.Visible = false;
                this.PAYROLLPanel2.Visible = false;

                this.JEAccountCurrLockRadioButtonListHiddenField.Value = this.JEAccountCurrLockRadioButtonList.SelectedValue;
                this.CurrencyFormatDDLHiddenField.Value = this.CurrencyFormatDDL.SelectedValue;
            }
            else
            {
                this.GeneralPanelEdit.Visible = false;
                this.ACCOUNTINGPanelEdit.Visible = false;
                this.ACCOUNTING2PanelEdit.Visible = true;
                this.STOCKCONTROLPanelEdit.Visible = false;
                this.POSPanelEdit.Visible = false;
                this.PAYROLLPanelEdit.Visible = false;
                this.PAYROLL2PanelEdit.Visible = false;

                this.JEAccountCurrLockRadioButtonListHiddenField.Value = this.JEAccountCurrLockRadioButtonListEdit.SelectedValue;
                this.CurrencyFormatDDLHiddenField.Value = this.CurrencyFormatDropDownListEdit.SelectedValue;
            }
        }

        protected void NextACCOUNTING2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CreateTable.Visible == true)
            {
                this.GeneralPanel.Visible = false;
                this.ACCOUNTINGPanel.Visible = false;
                this.AccountingPanel2.Visible = false;
                this.STOCKCONTROLPanel.Visible = true;
                this.POSPanel.Visible = false;
                this.PAYROLLPanel.Visible = false;
                this.PAYROLLPanel2.Visible = false;

                this.FACodeAutoNumberRBLHiddenField.Value = this.FACodeAutoNumberRBL.SelectedValue;
                this.FACodeDigitNumberDLLHiddenField.Value = this.FACodeDigitNumberDLL.SelectedValue;
                this.FADepreciationMethodDDLHiddenField.Value = this.FADepreciationMethodDDL.SelectedValue;
                this.AutoNmbrLocForFARBLHiddenField.Value = this.AutoNmbrLocForFARBL.SelectedValue;
                this.AutoNmbrPeriodForFARadioButtonListHiddenField.Value = this.AutoNmbrPeriodForFARadioButtonList.SelectedValue;
            }
            else
            {
                this.GeneralPanelEdit.Visible = false;
                this.ACCOUNTINGPanelEdit.Visible = false;
                this.ACCOUNTING2PanelEdit.Visible = false;
                this.STOCKCONTROLPanelEdit.Visible = true;
                this.POSPanelEdit.Visible = false;
                this.PAYROLLPanelEdit.Visible = false;
                this.PAYROLL2PanelEdit.Visible = false;

                this.FACodeAutoNumberRBLHiddenField.Value = this.FACodeAutoNumberRadioButtonListEdit.SelectedValue;
                this.FACodeDigitNumberDLLHiddenField.Value = this.FACodeDigitNumberDropDownListEdit.SelectedValue;
                this.FADepreciationMethodDDLHiddenField.Value = this.FADepreciationMethodDropDownListEdit.SelectedValue;
                this.AutoNmbrLocForFARBLHiddenField.Value = this.AutoNmbrLocForFARadioButtonListEdit.SelectedValue;
                this.AutoNmbrPeriodForFARadioButtonListHiddenField.Value = this.AutoNmbrPeriodForFARadioButtonListEdit.SelectedValue;
            }
        }

        protected void NextSTOCKCONTROLImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CreateTable.Visible == true)
            {
                this.GeneralPanel.Visible = false;
                this.ACCOUNTINGPanel.Visible = false;
                this.AccountingPanel2.Visible = false;
                this.STOCKCONTROLPanel.Visible = false;
                this.POSPanel.Visible = true;
                this.PAYROLLPanel.Visible = false;
                this.PAYROLLPanel2.Visible = false;

                this.BOLReferenceTypeDDLHiddenField.Value = this.BOLReferenceTypeDDL.SelectedValue;
                this.COGSMethodDropDownListHiddenField.Value = this.COGSMethodDropDownList.SelectedValue;
            }
            else
            {
                this.GeneralPanelEdit.Visible = false;
                this.ACCOUNTINGPanelEdit.Visible = false;
                this.ACCOUNTING2PanelEdit.Visible = false;
                this.STOCKCONTROLPanelEdit.Visible = false;
                this.POSPanelEdit.Visible = true;
                this.PAYROLLPanelEdit.Visible = false;
                this.PAYROLL2PanelEdit.Visible = false;

                this.BOLReferenceTypeDDLHiddenField.Value = this.BOLReferenceTypeDropDownListEdit.SelectedValue;
                this.COGSMethodDropDownListHiddenField.Value = this.COGSMethodDropDownListEdit.SelectedValue;
            }
        }

        protected void NextPOSImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CreateTable.Visible == true)
            {
                this.GeneralPanel.Visible = false;
                this.ACCOUNTINGPanel.Visible = false;
                this.AccountingPanel2.Visible = false;
                this.STOCKCONTROLPanel.Visible = false;
                this.POSPanel.Visible = false;
                this.PAYROLLPanel.Visible = true;
                this.PAYROLLPanel2.Visible = false;

                this.TransactionWrhsDefaultDropDownListHiddenField.Value = this.TransactionWrhsDefaultDropDownList.SelectedValue;
                this.TransactionWrhsDepositINDropDownListHiddenField.Value = this.TransactionWrhsDepositINDropDownList.SelectedValue;
                this.TransactionWrhsLocationDefaultDropDownListHiddenField.Value = this.TransactionWrhsLocationDefaultDropDownList.SelectedValue;
                this.TransactionWrhsLocationDepositINDropDownListHiddenField.Value = this.TransactionWrhsLocationDepositINDropDownList.SelectedValue;
                //this.POSInternetReminderDurationHiddenField.Value = this.POSInternetReminderDurationTextBox.Text;

            }
            else
            {
                this.GeneralPanelEdit.Visible = false;
                this.ACCOUNTINGPanelEdit.Visible = false;
                this.ACCOUNTING2PanelEdit.Visible = false;
                this.STOCKCONTROLPanelEdit.Visible = false;
                this.POSPanelEdit.Visible = false;
                this.PAYROLLPanelEdit.Visible = true;
                this.PAYROLL2PanelEdit.Visible = false;

                this.TransactionWrhsDefaultDropDownListHiddenField.Value = this.TransactionWrhsDefaultDropDownListEdit.SelectedValue;
                this.TransactionWrhsDepositINDropDownListHiddenField.Value = this.TransactionWrhsDepositINDropDownListEdit.SelectedValue;
                this.TransactionWrhsLocationDefaultDropDownListHiddenField.Value = this.TransactionWrhsLocationDefaultDropDownListEdit.SelectedValue;
                this.TransactionWrhsLocationDepositINDropDownListHiddenField.Value = this.TransactionWrhsLocationDepositINDropDownListEdit.SelectedValue;
                //this.POSInternetReminderDurationHiddenField.Value = this.POSInternetReminderDurationTextBoxEdit.Text;
            }

        }

        protected void NextPAYROLLImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CreateTable.Visible == true)
            {
                this.GeneralPanel.Visible = false;
                this.ACCOUNTINGPanel.Visible = false;
                this.AccountingPanel2.Visible = false;
                this.STOCKCONTROLPanel.Visible = false;
                this.POSPanel.Visible = false;
                this.PAYROLLPanel.Visible = false;
                this.PAYROLLPanel2.Visible = true;

                this.SalaryEncryptionRadioButtonListHiddenField.Value = this.SalaryEncryptionRadioButtonList.SelectedValue;
                this.SalaryEncryptionKeyDropDownListHiddenField.Value = this.SalaryEncryptionKeyTextBox.Text;
                this.SalaryEncryptionKeyValidationDropDownListHiddenField.Value = this.SalaryEncryptionKeyValidationTextBox.Text;
            }
            else
            {
                this.GeneralPanelEdit.Visible = false;
                this.ACCOUNTINGPanelEdit.Visible = false;
                this.ACCOUNTING2PanelEdit.Visible = false;
                this.STOCKCONTROLPanelEdit.Visible = false;
                this.POSPanelEdit.Visible = false;
                this.PAYROLLPanelEdit.Visible = false;
                this.PAYROLL2PanelEdit.Visible = true;

                this.SalaryEncryptionRadioButtonListHiddenField.Value = this.SalaryEncryptionRadioButtonListEdit.SelectedValue;
                this.SalaryEncryptionKeyDropDownListHiddenField.Value = this.SalaryEncryptionKeyTextBoxEdit.Text;
                this.SalaryEncryptionKeyValidationDropDownListHiddenField.Value = this.SalaryEncryptionKeyValidationTextBoxEdit.Text;
            }
        }

        protected void NextPAYROLL2ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CreateTable.Visible == true)
            {
                this.IsDispensationFreeRadioButtonListHiddenField.Value = this.IsDispensationFreeRadioButtonList.SelectedValue;
                this.KlikBCAAccountNoTextBoxHiddenField.Value = this.KlikBCAAccountNoTextBox.Text;
                this.KlikBCABankCodeSetupTextBoxHiddenField.Value = this.KlikBCABankCodeSetupTextBox.Text;
                this.KlikBCACompanyCodeTextBoxHiddenField.Value = this.KlikBCACompanyCodeTextBox.Text;
                this.LeaveCanTakeLeaveAfterWorkTimeDDLHiddenField.Value = this.LeaveCanTakeLeaveAfterWorkTimeDDL.SelectedValue;
                this.LeaveMethodDropDownListHiddenField.Value = this.LeaveMethodDropDownList.SelectedValue;
                this.LeavePeriodEffectiveDropDownListHiddenField.Value = this.LeavePeriodEffectiveDropDownList.SelectedValue;
                this.MinPeriodTHRProcessDropDownListHiddenField.Value = this.MinPeriodTHRProcessDropDownList.SelectedValue;
                this.OverTimeTextBoxHiddenField.Value = this.OverTimeTextBox.Text;
                this.PayrollAuthorizedSignImageTextBoxHiddenField.Value = this.PayrollAuthorizedSignImageTextBox.Text;
                this.PayrollAuthorizedSignNameTextBoxHiddenField.Value = this.PayrollAuthorizedSignNameTextBox.Text;
                this.PensiunTextBoxHiddenField.Value = this.PensiunTextBox.Text;
                this.PKPRoundingRBLHiddenField.Value = this.PKPRoundingRBL.SelectedValue;
                this.PremiGroupByDDLHiddenField.Value = this.PremiGroupByDDL.SelectedValue;

            }
            else
            {
                this.IsDispensationFreeRadioButtonListHiddenField.Value = this.IsDispensationFreeRadioButtonListEdit.SelectedValue;
                this.KlikBCAAccountNoTextBoxHiddenField.Value = this.KlikBCAAccountNoTextBoxEdit.Text;
                this.KlikBCABankCodeSetupTextBoxHiddenField.Value = this.KlikBCABankCodeSetupTextBoxEdit.Text;
                this.KlikBCACompanyCodeTextBoxHiddenField.Value = this.KlikBCACompanyCodeTextBoxEdit.Text;
                this.LeaveCanTakeLeaveAfterWorkTimeDDLHiddenField.Value = this.LeaveCanTakeLeaveAfterWorkTimeDropDownListEdit.SelectedValue;
                this.LeaveMethodDropDownListHiddenField.Value = this.LeaveMethodDropDownListEdit.SelectedValue;
                this.LeavePeriodEffectiveDropDownListHiddenField.Value = this.LeavePeriodEffectiveDropDownListEdit.SelectedValue;
                this.MinPeriodTHRProcessDropDownListHiddenField.Value = this.MinPeriodTHRProcessDropDownListEdit.SelectedValue;
                this.OverTimeTextBoxHiddenField.Value = this.OverTimeTextBoxEdit.Text;
                this.PayrollAuthorizedSignImageTextBoxHiddenField.Value = this.PayrollAuthorizedSignImageTextBoxEdit.Text;
                this.PayrollAuthorizedSignNameTextBoxHiddenField.Value = this.PayrollAuthorizedSignNameTextBoxEdit.Text;
                this.PensiunTextBoxHiddenField.Value = this.PensiunTextBoxEdit.Text;
                this.PKPRoundingRBLHiddenField.Value = this.PKPRoundingRadioButtonListEdit.SelectedValue;
                this.PremiGroupByDDLHiddenField.Value = this.PremiGroupByDropDownListEdit.SelectedValue;
            }
            List<CompanyConfiguration> _newListComFig = new List<CompanyConfiguration>();
            List<CompanyConfiguration> _listComFig = _companyConfigurationBL.GetListCompanyConfig();
            List<CompanyConfiguration> _newListComFigEdit = new List<CompanyConfiguration>();

            CompanyConfiguration _getCompanyConfiguration = null;

            int _count = 0;
            int _countHide = 0;
            foreach (var _row in _listComFig)/////EDIT
            {
                int _length = 37;
                for (int i = 1; i <= _length; i++)
                {
                    if (_row.ConfigCode == _configCode[i])
                    {
                        _getCompanyConfiguration = _companyConfigurationBL.GetSingleCompanyConfiguration(_row.ConfigCode);
                        String[] _value = this.InsertSetValue(i).Split('^');
                        _getCompanyConfiguration.SetValue = _value[0];
                        _getCompanyConfiguration.ConfigDescription = _value[1];
                        _getCompanyConfiguration.GroupConfig = _value[2];
                        _getCompanyConfiguration.AlowEdit = this.SetAlowEdit(i);
                        _getCompanyConfiguration.CreateBy = HttpContext.Current.User.Identity.Name;
                        _count += 1;
                    }
                }

                if (Convert.ToBoolean(this.CompanyHiddenField.Value))
                {
                    int _countHidden = 30;
                    for (int i = 1; i < _countHidden; i++)
                    {
                        String _hidden = InsertHiddenValue(i);
                        String[] _rowHidden = _hidden.Split('^');
                        if (_row.ConfigCode == _rowHidden[0])
                        {
                            _getCompanyConfiguration = _companyConfigurationBL.GetSingleCompanyConfiguration(_row.ConfigCode);
                            _getCompanyConfiguration.SetValue = _rowHidden[1];
                            _getCompanyConfiguration.ConfigDescription = _rowHidden[2];
                            _getCompanyConfiguration.GroupConfig = _rowHidden[3];
                            _getCompanyConfiguration.CreateBy = HttpContext.Current.User.Identity.Name;
                            _countHide += 1;
                        }
                    }
                }

                _newListComFigEdit.Add(_getCompanyConfiguration);
            }

            if (Convert.ToBoolean(this.CompanyHiddenField.Value))
            {
                if (_countHide != 30)
                {
                    int _lengthHide = 30;
                    for (int i = 1; i <= _lengthHide; i++)
                    {
                        String _hidden = InsertHiddenValue(i);
                        String[] _rowHidden = _hidden.Split('^');
                        _getCompanyConfiguration = _companyConfigurationBL.GetSingleCompanyConfiguration(_rowHidden[0]);
                        if (_getCompanyConfiguration.ConfigCode == null)
                        {
                            CompanyConfiguration _comFig = new CompanyConfiguration();
                            _comFig.ConfigCode = _rowHidden[0];
                            _comFig.SetValue = _rowHidden[1];
                            _comFig.ConfigDescription = _rowHidden[2];
                            _comFig.GroupConfig = _rowHidden[3];
                            _comFig.CreateBy = HttpContext.Current.User.Identity.Name;
                            _newListComFig.Add(_comFig);
                        }
                    }
                }
            }

            if (_count != 37)///////CREATE NEW
            {
                int _length = 37;
                for (int i = 1; i <= _length; i++)
                {
                    _getCompanyConfiguration = _companyConfigurationBL.GetSingleCompanyConfiguration(_configCode[i]);
                    if (_getCompanyConfiguration.ConfigCode == null)
                    {
                        CompanyConfiguration _comFig = new CompanyConfiguration();
                        _comFig.ConfigCode = _configCode[i];
                        String[] _value = this.InsertSetValue(i).Split('^');
                        _comFig.SetValue = _value[0];
                        _comFig.ConfigDescription = _value[1];
                        _comFig.GroupConfig = _value[2];
                        _comFig.AlowEdit = this.SetAlowEdit(i);
                        _comFig.CreateBy = HttpContext.Current.User.Identity.Name;
                        _newListComFig.Add(_comFig);
                    }
                }
            }

            bool _upDateOrInsert = _companyConfigurationBL.ToInsertAllCompanyConfiguration(_newListComFig, _newListComFigEdit);

            if (_upDateOrInsert)
                this.WarningLabel.Text = "Insert Success";
            else
                this.WarningLabel.Text = "Insert failed";

        }

        protected String InsertHiddenValue(int _prmI)
        {
            String _result = "";
            switch (_prmI)
            {
                case 1:
                    _result = "ConsolidationID^" + this.ConsolidationIDHiddenField.Value + "^Masukan Kode Konsolidasi sesuai dengan settingan di table ConsolidationDbList di Membership^GENERAL";
                    break;
                case 2:
                    _result = "ActivePGYear^" + this.ActivePGYearHiddenField.Value + "^Digunakan untuk setting Tahun yang berlaku pada Price Group^NCC";
                    break;
                case 3:
                    _result = "CustomerCode^" + "NCCRX2" + "^Setting untuk kode upload file xml ke server. dimana kode ini harus identika dengan customer code di db pusat/utama di MsCustomer^NCC";
                    break;
                case 4:
                    _result = "FlyerEmail^" + this.FlyerEmailHiddenField.Value + "^Setting untuk pengiriman email Status Flyer^NCC";
                    break;
                case 5:
                    _result = "TATTimer^" + this.TATTimerHiddenField.Value + "^timer untuk report TAT^NCC";
                    break;
                case 6:
                    _result = "BillingAuthorizedSignImage^" + this.BillingAuthorizedSignImageHiddenField.Value + "^Image tanda tangan untuk Billing Invoice^BILLING";
                    break;
                case 7:
                    _result = "BillingAuthorizedSignName^" + this.BillingAuthorizedSignNameHiddenField.Value + "^Nama yang menandatangani Billing Invoice^BILLING";
                    break;
                case 8:
                    _result = "BillingEmailTo^" + this.BillingEmailToHiddenField.Value + "^Alamat Email Billing Invoice^BILLING";
                    break;
                case 9:
                    _result = "BillingFax^" + this.BillingFaxHiddenField.Value + "^Fax Billing Invoice^BILLING";
                    break;
                case 10:
                    _result = "BillingFooterImage^" + this.BillingFooterImageHiddenField.Value + "^Tulisan di Footer Print Preview Billing^BILLING";
                    break;
                case 11:
                    _result = "BillingFooterText^" + this.BillingFooterTextHiddenField.Value + "^Footer text di bawah informasi layanan Billing^BILLING";
                    break;
                case 12:
                    _result = "BillingFooterText2^" + this.BillingFooterText2HiddenField.Value + "^Footer text di bawah informasi layanan Billing^BILLING";
                    break;
                case 13:
                    _result = "BillingHeaderImage^" + this.BillingHeaderImageHiddenField.Value + "^Image yang digunakan untuk Header Billing Invoice Print Preview^BILLING";
                    break;
                case 14:
                    _result = "BillingLeftImage^" + this.BillingLeftImageHiddenField.Value + "^Image yang digunakan untuk Left Border Billing Invoice Print Preview^BILLING";
                    break;
                case 15:
                    _result = "BillingSMSCenter^" + this.BillingSMSCenterHiddenField.Value + "^No HP untuk SMS Center^BILLING";
                    break;
                case 16:
                    _result = "BillingTelp^" + this.BillingTelpHiddenField.Value + "^Telp Billing Invoice^BILLING";
                    break;
                case 17:
                    _result = "RDLCBillingInvoiceSendEmail^" + this.RDLCBillingInvoiceSendEmailHiddenField.Value + "^RDLC Default Billing Invoice untuk Send Email ke Customer^BILLING";
                    break;
                case 18:
                    _result = "PPh^" + this.PPhHiddenField.Value + "^Item Payroll PPh 21^HUMANRESOURCE";
                    break;
                case 19:
                    _result = "PPhJabatan^" + this.PPhJabatanHiddenField.Value + "^% BIAYA JABATAN^HUMANRESOURCE";
                    break;
                case 20:
                    _result = "PPhMaxAnak^" + this.PPhMaxAnakHiddenField.Value + "^MAXIMUM ANAK PTKP^HUMANRESOURCE";
                    break;
                case 21:
                    _result = "PPhMaxJabatan^" + this.PPhMaxJabatanHiddenField.Value + "^MAX BIAYA JABATAN BULANAN^HUMANRESOURCE";
                    break;
                case 22:
                    _result = "PPhMethod^" + this.PPhMethodHiddenField.Value + "^Method Process PPh 21 (ACCUMULATION/STANDARD)^HUMANRESOURCE";
                    break;
                case 23:
                    _result = "PPhPTKP^" + this.PPhPTKPHiddenField.Value + "^PENDAPATAN TIDAK KENA PAJAK TAHUNAN^HUMANRESOURCE";
                    break;
                case 24:
                    _result = "PPhPTKPT^" + this.PPhPTKPTHiddenField.Value + "^PENDAPATAN TIDAK KENA PAJAK TANGUNGAN TAHUNAN^HUMANRESOURCE";
                    break;
                case 25:
                    _result = "HaveProductItemDuration^" + this.HaveProductItemDurationHiddenField.Value + "^0 : False (tidak punya product item duration), 1: True ( Muncul )^POS";
                    break;
                case 26:
                    _result = "IgnoreItemDiscount^" + this.IgnoreItemDiscountHiddenField.Value + "^0 : False (Item tidak di discount), 1: True (Item tetap di discount)^POS";
                    break;
                case 27:
                    _result = "POSBookingTimeLimitAfter^" + this.POSBookingTimeLimitAfterHiddenField.Value + "^in minutes^POS";
                    break;
                case 28:
                    _result = "POSBookingTimeLimitBefore^" + this.POSBookingTimeLimitBeforeHiddenField.Value + "^in minutes^POS";
                    break;
                case 29:
                    _result = "POSDefaultCustCode^" + this.POSDefaultCustCodeHiddenField.Value + "^isi dengan kode customer default untuk setiap proses penjualan di POS^POS";
                    break;
                case 30:
                    _result = "POSRounding^" + this.POSRoundingHiddenField.Value + "^Rounding ke bawah per satuan value disebut^POS";
                    break;
            }

            return _result;
        }

        protected String InsertSetValue(int _prmI)
        {
            String _result = "";
            if (_prmI == 1)
                _result = this.DateFormatDDLHiddenField.Value + "^Setting untuk format tampilan tanggal di laporan atau di form transaksi^GENERAL";
            else if (_prmI == 2)
                _result = "SIP_Membership" + "^isi nama database membership^GENERAL";//MembershipDbName
            else if (_prmI == 3)
                _result = this.PosisiLogoDDLHiddenField.Value + "^0 : default (posisi logo SiP sebelah kiri, posisi company kanan), 1 : posisi SiP kanan, company kiri^GENERAL";
            else if (_prmI == 4)
                _result = this.ThemeDDLHiddenField.Value + "^kode theme yang dipakai sesuai yang mau di pilih di membership master.theme ThemeCode^GENERAL";
            else if (_prmI == 5)
                _result = this.ViewJobTitlePrintReportRadioButtonListHiddenField.Value + "^0 : False ( tidak ditampilkan ), 1: True ( Muncul )^GENERAL";
            else if (_prmI == 6)
                _result = this.JEAccountCurrLockRadioButtonListHiddenField.Value + "^Setting untuk memastikan ketika proses input satu account currencynya harus sama dengan default di msaccount^ACCOUNTING";
            else if (_prmI == 7)
                _result = this.CurrencyFormatDDLHiddenField.Value + "^Setting untuk format tampilan Nilai/Harga di laporan atau di form transaksi^ACCOUNTING";
            else if (_prmI == 8)
                _result = this.FACodeAutoNumberRBLHiddenField.Value + "^0 : Tidak Auto, 1 : Auto Generate berdasarkan Sub Group^ACCOUNTING";
            else if (_prmI == 9)
                _result = this.FACodeDigitNumberDLLHiddenField.Value + "^jumlah digit counter^ACCOUNTING";
            else if (_prmI == 10)
                _result = this.FADepreciationMethodDDLHiddenField.Value + "^0: StraightLine ,  1:DoubleDeclining^ACCOUNTING";
            else if (_prmI == 11)
                _result = this.AutoNmbrLocForFARBLHiddenField.Value + "^T : True dimana pada saat generate kode fixed asset muncul LOCATION_CODE/ sebagai lanjutan dari kode counter yang diseting pada FAGroupSub^ACCOUNTING";
            else if (_prmI == 12)
                _result = this.AutoNmbrPeriodForFARadioButtonListHiddenField.Value + "^T : True dimana pada saat generate kode fixed asset muncul MMMYY/ sebagai lanjutan dari kode counter yang diseting pada FAGroupSub^ACCOUNTING";
            else if (_prmI == 13)
                _result = this.BOLReferenceTypeDDLHiddenField.Value + "^SO : jika referensi berdasarkan transaksi Sales Order, DO : jika referensi berdasarkan transaksi Delivery Order/ SP Keluar Barang^STOCK CONTROL";
            else if (_prmI == 14)
                _result = this.COGSMethodDropDownListHiddenField.Value + "^0 : FIFO, 1:LIFO, 2:AVG^STOCK CONTROL";
            else if (_prmI == 15)
                _result = this.TransactionWrhsDefaultDropDownListHiddenField.Value + "^Default Warehouse untuk Transaksi POS / NCP Sales yang tidak menampilkan pilihan informasi warehouse^POS";
            else if (_prmI == 16)
                _result = this.TransactionWrhsDepositINDropDownListHiddenField.Value + "^Default Warehouse untuk Transaksi POS / NCP Sales yang tidak menampilkan pilihan informasi warehouse^POS";
            else if (_prmI == 17)
                _result = this.TransactionWrhsLocationDefaultDropDownListHiddenField.Value + "^Default Warehouse untuk Transaksi POS / NCP Sales yang tidak menampilkan pilihan informasi warehouse Location^POS";
            else if (_prmI == 18)
                _result = this.TransactionWrhsLocationDepositINDropDownListHiddenField.Value + "^Default Warehouse untuk Transaksi POS / NCP Sales yang tidak menampilkan pilihan informasi warehouse Location^POS";
            else if (_prmI == 19)
                _result = this.SalaryEncryptionRadioButtonListHiddenField.Value + "^0 : False ( tidak diencrypt ), 1: True ( diencrypt )^PAYROLL";
            else if (_prmI == 20)
                _result = this.SalaryEncryptionKeyDropDownListHiddenField.Value + "^Diisi ketika akan membuka Encryption Salary^PAYROLL";
            else if (_prmI == 21)
                _result = this.SalaryEncryptionKeyValidationDropDownListHiddenField.Value + "^Diisi melalui Salary Entry PIN Setup. Digunakan u/ membuka encryption salary. Berisi hasil encryption dari SalaryEncryptionKey^PAYROLL";
            else if (_prmI == 22)
                _result = this.IsDispensationFreeRadioButtonListHiddenField.Value + "^Y:tidak potong cuti; N: tergantung punya hak cuti atau tidak^PAYROLL";
            else if (_prmI == 23)
                _result = this.KlikBCAAccountNoTextBoxHiddenField.Value + "^10 digit rekening perusahaan^PAYROLL";
            else if (_prmI == 24)
                _result = this.KlikBCABankCodeSetupTextBoxHiddenField.Value + "^Kode Bank BCA di Master Bank^PAYROLL";
            else if (_prmI == 25)
                _result = this.KlikBCACompanyCodeTextBoxHiddenField.Value + "^13 digit company code klikbca^PAYROLL";
            else if (_prmI == 26)
                _result = this.LeaveCanTakeLeaveAfterWorkTimeDDLHiddenField.Value + "^karyawan dapat mengambil cuti setelah bulan ke berapa^PAYROLL";
            else if (_prmI == 27)
                _result = this.LeaveMethodDropDownListHiddenField.Value + "^Period : Jika cuti direset pada period tertentu misal akhir tahun; WorkTime : Jika cuti direset pada masa bekerja^PAYROLL";
            else if (_prmI == 28)
                _result = this.LeavePeriodEffectiveDropDownListHiddenField.Value + "^Jangka waktu lama periode efektif cuti, harus lebih >= 12 Month^PAYROLL";
            else if (_prmI == 29)
                _result = this.MinPeriodTHRProcessDropDownListHiddenField.Value + "^Jumlah minimum validasi banyak bulan setelah THR terakhir^PAYROLL";
            else if (_prmI == 30)
                _result = "0" + "^mengikuti struktur organisasi unit^PAYROLL";//OrganizationUnitDeparmentLevel
            else if (_prmI == 31)
                _result = this.OverTimeTextBoxHiddenField.Value + "^Item Payroll Overtime^PAYROLL";
            else if (_prmI == 32)
                _result = this.PayrollAuthorizedSignImageTextBoxHiddenField.Value + "^Image tanda tangan untuk Slip Gaji^PAYROLL";
            else if (_prmI == 33)
                _result = this.PayrollAuthorizedSignNameTextBoxHiddenField.Value + "^Nama yang menandatangani Slip Gaji^PAYROLL";
            else if (_prmI == 34)
                _result = this.PensiunTextBoxHiddenField.Value + "^Umur Pensiun^PAYROLL";
            else if (_prmI == 35)
                _result = "0" + "^Settingan Period ditentukan sendiri, baik period maupun week sesuai rentang waktu yang diperlukan^PAYROLL";//PeriodConfig
            else if (_prmI == 36)
                _result = this.PKPRoundingRBLHiddenField.Value + "^PKP jika ingin rounding dalam ribuan, if 'Y', jika tidak berdasarkan nilai original perhitungan isi  'N'^PAYROLL";
            else if (_prmI == 37)
                _result = this.PremiGroupByDDLHiddenField.Value + "^Job Level, Job Title, Employee ID, Work Place, Employee Type, All^PAYROLL";
            //else if (_prmI == 38)
            //    _result = this.POSInternetReminderDurationHiddenField.Value+"^";
            return _result;
        }

        private Char SetAlowEdit(int _prmI)
        {
            Char _result = ' ';
            if (_prmI == 1)
                _result = 'Y';
            else if (_prmI == 2)
                _result = 'N';//MembershipDbName
            else if (_prmI == 3)
                _result = 'Y';
            else if (_prmI == 4)
                _result = 'N';
            else if (_prmI == 5)
                _result = 'Y';
            else if (_prmI == 6)
                _result = 'Y';
            else if (_prmI == 7)
                _result = 'Y';
            else if (_prmI == 8)
                _result = 'Y';
            else if (_prmI == 9)
                _result = 'Y';
            else if (_prmI == 10)
                _result = 'Y';
            else if (_prmI == 11)
                _result = 'Y';
            else if (_prmI == 12)
                _result = 'Y';
            else if (_prmI == 13)
                _result = 'Y';
            else if (_prmI == 14)
                _result = 'N';
            else if (_prmI == 15)
                _result = 'Y';
            else if (_prmI == 16)
                _result = 'Y';
            else if (_prmI == 17)
                _result = 'Y';
            else if (_prmI == 18)
                _result = 'Y';
            else if (_prmI == 19)
                _result = 'Y';
            else if (_prmI == 20)
                _result = 'Y';
            else if (_prmI == 21)
                _result = 'Y';
            else if (_prmI == 22)
                _result = 'Y';
            else if (_prmI == 23)
                _result = 'Y';
            else if (_prmI == 24)
                _result = 'Y';
            else if (_prmI == 25)
                _result = 'Y';
            else if (_prmI == 26)
                _result = 'Y';
            else if (_prmI == 27)
                _result = 'Y';
            else if (_prmI == 28)
                _result = 'Y';
            else if (_prmI == 29)
                _result = 'Y';
            else if (_prmI == 30)
                _result = 'N';//OrganizationUnitDeparmentLevel
            else if (_prmI == 31)
                _result = 'Y';
            else if (_prmI == 32)
                _result = 'Y';
            else if (_prmI == 33)
                _result = 'Y';
            else if (_prmI == 34)
                _result = 'Y';
            else if (_prmI == 35)
                _result = 'N';//PeriodConfig
            else if (_prmI == 36)
                _result = 'Y';
            else if (_prmI == 37)
                _result = 'Y';
            //else if (_prmI == 38)
            //    _result = 'Y';

            return _result;
        }

        protected String UpDateSetValue(int _prmI)
        {
            String _result = "";
            if (_prmI == 1)
                _result = this.DateFormatDDLHiddenField.Value;
            else if (_prmI == 2)
                _result = "SIP_Membership";//MembershipDbName
            else if (_prmI == 3)
                _result = this.PosisiLogoDDLHiddenField.Value;
            else if (_prmI == 4)
                _result = this.ThemeDDLHiddenField.Value;
            else if (_prmI == 5)
                _result = this.ViewJobTitlePrintReportRadioButtonListHiddenField.Value;
            else if (_prmI == 6)
                _result = this.JEAccountCurrLockRadioButtonListHiddenField.Value;
            else if (_prmI == 7)
                _result = this.CurrencyFormatDDLHiddenField.Value;
            else if (_prmI == 8)
                _result = this.FACodeAutoNumberRBLHiddenField.Value;
            else if (_prmI == 9)
                _result = this.FACodeDigitNumberDLLHiddenField.Value;
            else if (_prmI == 10)
                _result = this.FADepreciationMethodDDLHiddenField.Value;
            else if (_prmI == 11)
                _result = this.AutoNmbrLocForFARBLHiddenField.Value;
            else if (_prmI == 12)
                _result = this.AutoNmbrPeriodForFARadioButtonListHiddenField.Value;
            else if (_prmI == 13)
                _result = this.BOLReferenceTypeDDLHiddenField.Value;
            else if (_prmI == 14)
                _result = this.COGSMethodDropDownListHiddenField.Value;
            else if (_prmI == 15)
                _result = this.TransactionWrhsDefaultDropDownListHiddenField.Value;
            else if (_prmI == 16)
                _result = this.TransactionWrhsDepositINDropDownListHiddenField.Value;
            else if (_prmI == 17)
                _result = this.TransactionWrhsLocationDefaultDropDownListHiddenField.Value;
            else if (_prmI == 18)
                _result = this.TransactionWrhsLocationDepositINDropDownListHiddenField.Value;
            else if (_prmI == 19)
                _result = this.SalaryEncryptionRadioButtonListHiddenField.Value;
            else if (_prmI == 20)
                _result = this.SalaryEncryptionKeyDropDownListHiddenField.Value;
            else if (_prmI == 21)
                _result = this.SalaryEncryptionKeyValidationDropDownListHiddenField.Value;
            else if (_prmI == 22)
                _result = this.IsDispensationFreeRadioButtonListHiddenField.Value;
            else if (_prmI == 23)
                _result = this.KlikBCAAccountNoTextBoxHiddenField.Value;
            else if (_prmI == 24)
                _result = this.KlikBCABankCodeSetupTextBoxHiddenField.Value;
            else if (_prmI == 25)
                _result = this.KlikBCACompanyCodeTextBoxHiddenField.Value;
            else if (_prmI == 26)
                _result = this.LeaveCanTakeLeaveAfterWorkTimeDDLHiddenField.Value;
            else if (_prmI == 27)
                _result = this.LeaveMethodDropDownListHiddenField.Value;
            else if (_prmI == 28)
                _result = this.LeavePeriodEffectiveDropDownListHiddenField.Value;
            else if (_prmI == 29)
                _result = this.MinPeriodTHRProcessDropDownListHiddenField.Value;
            else if (_prmI == 30)
                _result = "0";//OrganizationUnitDeparmentLevel
            else if (_prmI == 31)
                _result = this.OverTimeTextBoxHiddenField.Value;
            else if (_prmI == 32)
                _result = this.PayrollAuthorizedSignImageTextBoxHiddenField.Value;
            else if (_prmI == 33)
                _result = this.PayrollAuthorizedSignNameTextBoxHiddenField.Value;
            else if (_prmI == 34)
                _result = this.PensiunTextBoxHiddenField.Value;
            else if (_prmI == 35)
                _result = "0";//PeriodConfig
            else if (_prmI == 36)
                _result = this.PKPRoundingRBLHiddenField.Value;
            else if (_prmI == 37)
                _result = this.PremiGroupByDDLHiddenField.Value;
            //else if (_prmI == 38)
            //    _result = this.POSInternetReminderDurationHiddenField.Value;
            return _result;
        }

        protected void NextGeneralImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.GeneralPanelEdit2.Visible = false;
            this.NCCPanelEdit.Visible = true;

            this.ConsolidationIDHiddenField.Value = this.ConsolidationIDDropDownList.SelectedValue;
        }

        protected void NextNCCImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.NCCPanelEdit.Visible = false;
            this.BILLINGPanelEdit.Visible = true;


            this.ActivePGYearHiddenField.Value = this.ActivePGYearTextBox.Text;
            this.FlyerEmailHiddenField.Value = this.FlyerEmailTextBox.Text;
            this.TATTimerHiddenField.Value = this.TATTimerTextBox.Text;
        }

        protected void NextBILLINGImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.BILLINGPanelEdit.Visible = false;
            this.HUMANRESOURCEPanelEdit.Visible = true;

            this.BillingAuthorizedSignImageHiddenField.Value = this.BillingAuthorizedSignImageTextBox.Text;
            this.BillingAuthorizedSignNameHiddenField.Value = this.BillingAuthorizedSignNameTextBox.Text;
            this.BillingEmailToHiddenField.Value = this.BillingEmailToTextBox.Text;
            this.BillingFaxHiddenField.Value = this.BillingFaxTextBox.Text;
            this.BillingFooterImageHiddenField.Value = this.BillingFooterImageTextBox.Text;
            this.BillingFooterTextHiddenField.Value = this.BillingFooterTextTextBox.Text;
            this.BillingFooterText2HiddenField.Value = this.BillingFooterText2TextBox.Text;
            this.BillingHeaderImageHiddenField.Value = this.BillingHeaderImageTextBox.Text;
            this.BillingLeftImageHiddenField.Value = this.BillingLeftImageTextBox.Text;
            this.BillingSMSCenterHiddenField.Value = this.BillingSMSCenterTextBox.Text;
            this.BillingTelpHiddenField.Value = this.BillingTelpTextBox.Text;
            this.RDLCBillingInvoiceSendEmailHiddenField.Value = this.RDLCBillingInvoiceSendEmailTextBox.Text;

        }

        protected void NextHUMANRESOURCEImageButton_Click(object sender, ImageClickEventArgs e)
        {
            this.HUMANRESOURCEPanelEdit.Visible = false;
            this.POSPanelEdit2.Visible = true;

            this.PPhHiddenField.Value = this.PPhTextBox.Text;
            this.PPhJabatanHiddenField.Value = this.PPhJabatanTextBox.Text;
            this.PPhMaxAnakHiddenField.Value = this.PPhMaxAnakTextBox.Text;
            this.PPhMaxJabatanHiddenField.Value = this.PPhMaxJabatanTextBox.Text;
            this.PPhMethodHiddenField.Value = this.PPhMethodDropDownList.SelectedValue;
            this.PPhPTKPHiddenField.Value = this.PPhPTKPTextBox.Text;
            this.PPhPTKPTHiddenField.Value = this.PPhPTKPTextBox.Text;

        }

        protected void NextPOSImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.POSPanelEdit2.Visible = false;
            this.ACCOUNTINGPanelEdit.Visible = true;

            this.HaveProductItemDurationHiddenField.Value = this.HaveProductItemDurationRadioButtonList.SelectedValue;
            this.IgnoreItemDiscountHiddenField.Value = this.IgnoreItemDiscountRadioButtonList.SelectedValue;
            this.POSBookingTimeLimitAfterHiddenField.Value = this.POSBookingTimeLimitAfterTextBox.Text;
            this.POSBookingTimeLimitBeforeHiddenField.Value = this.POSBookingTimeLimitBeforeTextBox.Text;
            this.POSDefaultCustCodeHiddenField.Value = this.POSDefaultCustCodeTextBox.Text;
            this.POSRoundingHiddenField.Value = this.POSRoundingTextBox.Text;
        }

    }
}

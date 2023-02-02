using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.SalesConfirmation
{
    public partial class SalesConfirmationEdit : SalesConfirmationBase
    {
        private SalesConfirmationBL _salesConfirmationBL = new SalesConfirmationBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private CityBL _cityBL = new CityBL();
        private CountryBL _countryBL = new CountryBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private GenderBL _genderBL = new GenderBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private TermBL _termBL = new TermBL();
        private CustBillAccountBL _custBillAccBL = new CustBillAccountBL();
        private RegistrationConfigBL _regConfigBL = new RegistrationConfigBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute();

                this.ShowCustomer();
                this.ShowCountry();
                this.ShowCity();
                this.ShowCompCity();
                this.ShowCompCountry();
                this.ShowSales();
                this.ShowGender();
                this.ShowPaymentType();
                this.ShowCustType();
                this.ShowCustGroup();
                this.ShowTerm();
                this.ShowCustBillAcc();
                this.ShowRegistration();

                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.SLATextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.ContractMinTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.ContractMinTextBox.ClientID + ");");
            this.FreeTrialDaysTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.FreeTrialDaysTextBox.ClientID + ");");
            this.TargetInstalationDayTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.TargetInstalationDayTextBox.ClientID + ");");

            this.PPNPercentageTextBox.Attributes.Add("OnChange", "return DisablePPNForex(" + this.PPNPercentageTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + ");");
            this.PPNPercentageTextBox.Attributes.Add("OnKeyUp", "formatangka(" + this.PPNPercentageTextBox.ClientID + ");");
            this.PPNForexTextBox.Attributes.Add("OnBlur", "return ChangeFormat2(" + this.PPNForexTextBox.ClientID + ", 2);");
            this.FgPPNCheckBox.Attributes.Add("OnClick", "CheckUncheckPPN(" + this.FgPPNCheckBox.ClientID + "," + this.PPNPercentageTextBox.ClientID + "," + this.PPNForexTextBox.ClientID + ");");
            this.FgNewCustCheckBox.Attributes.Add("OnClick", "return CheckUncheck(" + this.FgNewCustCheckBox.ClientID + "," + this.CustomerDropDownList.ClientID + "," + this.CustomerCodeTextBox.ClientID + "," + this.CustBillAccCheckBox.ClientID + "," + this.CustBillAccDropDownList.ClientID + ");");
            this.CustBillAccCheckBox.Attributes.Add("OnClick", "return CheckUncheckCustBillAcc(" + this.CustBillAccCheckBox.ClientID + "," + this.CustBillAccDropDownList.ClientID + ");");
        }

        protected void ShowCustomer()
        {
            this.CustomerDropDownList.Items.Clear();
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataSource = this._customerBL.GetListCustForDDLForReport();
            this.CustomerDropDownList.DataBind();
            this.CustomerDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowRegistration()
        {
            this.RegDropDownList.Items.Clear();
            this.RegDropDownList.DataTextField = "RegName";
            this.RegDropDownList.DataValueField = "RegCode";
            this.RegDropDownList.DataSource = this._regConfigBL.GetListRegistrationConfigForDDL();
            this.RegDropDownList.DataBind();
            this.RegDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowPaymentType()
        {
            this.BankPaymentCodeDropDownList.Items.Clear();
            this.BankPaymentCodeDropDownList.DataTextField = "PayName";
            this.BankPaymentCodeDropDownList.DataValueField = "PayCode";
            this.BankPaymentCodeDropDownList.DataSource = this._paymentBL.GetListDDLPayment();
            this.BankPaymentCodeDropDownList.DataBind();
            this.BankPaymentCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCity()
        {
            this.CityDropDownList.Items.Clear();
            this.CityDropDownList.DataTextField = "CityName";
            this.CityDropDownList.DataValueField = "CityCode";
            this.CityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CityDropDownList.DataBind();
            this.CityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCompCity()
        {
            this.CompCityDropDownList.Items.Clear();
            this.CompCityDropDownList.DataTextField = "CityName";
            this.CompCityDropDownList.DataValueField = "CityCode";
            this.CompCityDropDownList.DataSource = this._cityBL.GetListCityForDDL();
            this.CompCityDropDownList.DataBind();
            this.CompCityDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCountry()
        {
            this.CountryDropDownList.Items.Clear();
            this.CountryDropDownList.DataTextField = "CountryName";
            this.CountryDropDownList.DataValueField = "CountryCode";
            this.CountryDropDownList.DataSource = this._countryBL.GetList();
            this.CountryDropDownList.DataBind();
            this.CountryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCompCountry()
        {
            this.CompCountryDropDownList.Items.Clear();
            this.CompCountryDropDownList.DataTextField = "CountryName";
            this.CompCountryDropDownList.DataValueField = "CountryCode";
            this.CompCountryDropDownList.DataSource = this._countryBL.GetList();
            this.CompCountryDropDownList.DataBind();
            this.CompCountryDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowSales()
        {
            this.SalesDropDownList.Items.Clear();
            this.SalesDropDownList.DataTextField = "EmpName";
            this.SalesDropDownList.DataValueField = "EmpNumb";
            this.SalesDropDownList.DataSource = this._employeeBL.GetListEmpSalesForDDL();
            this.SalesDropDownList.DataBind();
            this.SalesDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowGender()
        {
            this.GenderRadioButtonList.DataSource = this._genderBL.GetListGenderForDDL();
            this.GenderRadioButtonList.DataValueField = "GenderCode";
            this.GenderRadioButtonList.DataTextField = "GenderName";
            this.GenderRadioButtonList.DataBind();
        }

        protected void ShowCustType()
        {
            this.CustTypeDropDownList.Items.Clear();
            this.CustTypeDropDownList.DataTextField = "CustTypeName";
            this.CustTypeDropDownList.DataValueField = "CustTypeCode";
            this.CustTypeDropDownList.DataSource = this._customerBL.GetListCustTypeForDDL();
            this.CustTypeDropDownList.DataBind();
            this.CustTypeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustGroup()
        {
            this.CustGroupDropDownList.Items.Clear();
            this.CustGroupDropDownList.DataTextField = "CustGroupName";
            this.CustGroupDropDownList.DataValueField = "CustGroupCode";
            this.CustGroupDropDownList.DataSource = this._customerBL.GetListCustGroupForDDL();
            this.CustGroupDropDownList.DataBind();
            this.CustGroupDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowTerm()
        {
            this.TermDropDownLilst.Items.Clear();
            this.TermDropDownLilst.DataTextField = "TermName";
            this.TermDropDownLilst.DataValueField = "TermCode";
            this.TermDropDownLilst.DataSource = this._termBL.GetListTermForDDL();
            this.TermDropDownLilst.DataBind();
            this.TermDropDownLilst.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowCustBillAcc()
        {
            this.CustBillAccDropDownList.Items.Clear();
            this.CustBillAccDropDownList.DataTextField = "CustBillAccount";
            this.CustBillAccDropDownList.DataValueField = "CustBillAccount";
            this.CustBillAccDropDownList.DataSource = this._custBillAccBL.GetListDDLCustBillAccount(this.CustomerDropDownList.SelectedValue);
            this.CustBillAccDropDownList.DataBind();
            this.CustBillAccDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void ShowData()
        {
            this.ClearLabel();

            BILTrSalesConfirmation _salesConfirmationHd = this._salesConfirmationBL.GetSingleSalesConfirmation(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.DateTextBox.Text = DateFormMapper.GetValue(_salesConfirmationHd.TransDate);
            this.TransNoTextBox.Text = _salesConfirmationHd.TransNmbr;
            this.FileNmbrTextBox.Text = _salesConfirmationHd.FileNmbr;
            this.AddressTextBox.Text = _salesConfirmationHd.ResponsibleAddress;
            this.BankPaymentCodeDropDownList.SelectedValue = _salesConfirmationHd.BankPaymentCode;
            this.BirthDateTextBox.Text = DateFormMapper.GetValue(_salesConfirmationHd.ResponsibleBirthDate);
            this.CellularTextBox.Text = _salesConfirmationHd.ResponsibleMobilePhone;
            this.CityDropDownList.SelectedValue = _salesConfirmationHd.ResponsibleAddrCity;
            this.CustGroupDropDownList.SelectedValue = _salesConfirmationHd.CustGroup;
            this.CustTypeDropDownList.SelectedValue = _salesConfirmationHd.CustType;
            this.TermDropDownLilst.SelectedValue = _salesConfirmationHd.Term;
            this.BusinessTypeTextBox.Text = _salesConfirmationHd.CompanyBusinessType;
            this.GenerateCheckBox.Checked = Convert.ToBoolean(_salesConfirmationHd.FgGenerateBillAccount);

            this.CompAddrTextBox.Text = _salesConfirmationHd.CompanyAddress;
            this.CompCellularTextBox.Text = _salesConfirmationHd.CompanyMobilePhone;
            this.CompCityDropDownList.SelectedValue = _salesConfirmationHd.CompanyAddrCity;
            this.CompCountryDropDownList.SelectedValue = _salesConfirmationHd.CompanyAddrCountry;
            this.CompFaxTextBox.Text = _salesConfirmationHd.CompanyFax;
            this.CompNameTextBox.Text = _salesConfirmationHd.CompanyName;
            this.CompNPWPTextBox.Text = _salesConfirmationHd.CompanyNPWP;
            this.CompTelpTextBox.Text = _salesConfirmationHd.CompanyPhone;
            this.CompWebsiteTextBox.Text = _salesConfirmationHd.CompanyWebAddress;
            this.CompZipCodeTextBox.Text = _salesConfirmationHd.CompanyAddrPostal;
            this.ContractMinTextBox.Text = _salesConfirmationHd.ContractMinimum.ToString();
            this.CountryDropDownList.SelectedValue = _salesConfirmationHd.ResponsibleAddrCountry;
            if (_salesConfirmationHd.fgNewCustomer == true)
            {
                this.CustomerCodeTextBox.Text = _salesConfirmationHd.CustCode;
                this.CompNameTextBox.Text = _salesConfirmationHd.CompanyName;
                this.CustomerCodeTextBox.Attributes.Add("Style", "visibility:visible;");
                //this.CompNameTextBox.Attributes.Add("Style", "visibility:visible;");
                //this.tr3.Attributes.Add("Style", "visibility:visible;");
                this.CustomerDropDownList.Attributes.Add("Style", "visibility:hidden;");
                this.CustomerDropDownList.SelectedValue = "null";
                this.CustBillAccCheckBox.Attributes.Add("Style", "visibility:hidden;");
                this.CustBillAccCheckBox.Checked = false;
                this.CustBillAccDropDownList.Attributes.Add("Style", "visibility:hidden;");
                this.CustBillAccDropDownList.SelectedValue = "null";
            }
            else
            {
                this.CustomerDropDownList.SelectedValue = _salesConfirmationHd.CustCode;
                this.CompNameTextBox.Text = "";
                //this.CompNameTextBox.Attributes.Add("Style", "visibility:hidden;");
                //this.tr3.Attributes.Add("Style", "visibility:hidden;");
                this.CustomerCodeTextBox.Attributes.Add("Style", "visibility:hidden;");
                this.CustomerCodeTextBox.Text = "";

                if (_salesConfirmationHd.CustBillAccount != "null")
                {
                    this.CustBillAccCheckBox.Attributes.Add("Style", "visibility:visible;");
                    this.CustBillAccDropDownList.Attributes.Add("Style", "visibility:visible;");
                    this.ShowCustBillAcc();
                    this.CustBillAccCheckBox.Checked = false;
                    this.CustBillAccCheckBox.Text = "New";
                    this.CustBillAccDropDownList.SelectedValue = _salesConfirmationHd.CustBillAccount;
                }
                else
                {
                    this.CustBillAccCheckBox.Checked = true;
                    this.CustBillAccCheckBox.Attributes.Add("Style", "visibility:visible;");
                    this.CustBillAccDropDownList.Attributes.Add("Style", "visibility:hidden;");
                    this.CustBillAccDropDownList.SelectedValue = "null";
                }
            }
            this.EmailTextBox.Text = _salesConfirmationHd.ResponsibleEmailAddress;
            this.FaxTextBox.Text = _salesConfirmationHd.ResponsibleFax;
            this.FgNewCustCheckBox.Checked = Convert.ToBoolean(_salesConfirmationHd.fgNewCustomer);

            this.FormIDTextBox.Text = _salesConfirmationHd.FormulirID;
            this.FreeTrialDaysTextBox.Text = _salesConfirmationHd.FreeTrialDays.ToString();
            this.GenderRadioButtonList.SelectedValue = _salesConfirmationHd.ResponsibleGender;
            this.IDCardNoTextBox.Text = _salesConfirmationHd.ResponsibleIDCard;
            this.InstallationAddrTextBox.Text = _salesConfirmationHd.InstallationAddress;
            this.InstallationDeviceDescTextBox.Text = _salesConfirmationHd.InstallationDeviceDesc;
            this.InstallationDeviceStatusTextBox.Text = _salesConfirmationHd.InstallationDeviceStatus;
            this.PostalAddrTextBox.Text = _salesConfirmationHd.ResponsibleAddrPostal;
            this.FgPPNCheckBox.Checked = Convert.ToBoolean(_salesConfirmationHd.fgPPN);
            if (_salesConfirmationHd.fgPPN == true)
            {
                this.PPNForexTextBox.Text = Convert.ToDecimal((_salesConfirmationHd.PPNForex == null) ? 0 : _salesConfirmationHd.PPNForex).ToString("#,##0.00");
                this.PPNPercentageTextBox.Text = Convert.ToDecimal((_salesConfirmationHd.PPNPercentage == null) ? 0 : _salesConfirmationHd.PPNPercentage).ToString("#,##0.##");
                this.PPNPercentageTextBox.Attributes.Remove("ReadOnly");
                this.PPNPercentageTextBox.Attributes.Add("Style", "background-color: #FFFFFF");
                this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNForexTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
            }
            else
            {
                this.PPNForexTextBox.Text = "";
                this.PPNPercentageTextBox.Text = "";
                this.PPNPercentageTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNPercentageTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
                this.PPNForexTextBox.Attributes.Add("ReadOnly", "True");
                this.PPNForexTextBox.Attributes.Add("Style", "background-color: #CCCCCC");
            }
            this.RemarkTextBox.Text = _salesConfirmationHd.Remark;
            this.SalesDropDownList.SelectedValue = _salesConfirmationHd.SalesID;
            this.SLATextBox.Text = Convert.ToDecimal((_salesConfirmationHd.SLA == null) ? 0 : _salesConfirmationHd.SLA).ToString("#,##0.##");
            this.TargetInstalationDayTextBox.Text = _salesConfirmationHd.TargetInstallationDay.ToString();
            this.TechCellularTextBox.Text = _salesConfirmationHd.TechnicalMobilePhone;
            this.TechEmail2TextBox.Text = _salesConfirmationHd.TechnicalEmail2;
            this.TechEmailTextBox.Text = _salesConfirmationHd.TechnicalEmail;
            this.TechFaxTextBox.Text = _salesConfirmationHd.TechnicalFax;
            this.TechNameTextBox.Text = _salesConfirmationHd.TechnicalName;
            this.TechPhoneTextBox.Text = _salesConfirmationHd.TechnicalPhone;
            this.TechPICTextBox.Text = _salesConfirmationHd.TechnicalPIC;
            this.TelpTextBox.Text = _salesConfirmationHd.ResponsiblePhone;
            this.ApprovedByTextBox.Text = _salesConfirmationHd.ApprovedByCustomerName;
            this.NameTextBox.Text = _salesConfirmationHd.ResponsibleCustName;
            this.RegDropDownList.SelectedValue = _salesConfirmationHd.RegCode;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILTrSalesConfirmation _salesConfirmationHd = this._salesConfirmationBL.GetSingleSalesConfirmation(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _salesConfirmationHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _salesConfirmationHd.fgNewCustomer = this.FgNewCustCheckBox.Checked;

            _salesConfirmationHd.FormulirID = this.FormIDTextBox.Text;
            _salesConfirmationHd.Remark = this.RemarkTextBox.Text;
            _salesConfirmationHd.SalesID = this.SalesDropDownList.SelectedValue;
            _salesConfirmationHd.ContractMinimum = Convert.ToInt32(this.ContractMinTextBox.Text);
            _salesConfirmationHd.SLA = Convert.ToDecimal(this.SLATextBox.Text);
            _salesConfirmationHd.BankPaymentCode = this.BankPaymentCodeDropDownList.SelectedValue;
            _salesConfirmationHd.FreeTrialDays = Convert.ToInt32(this.FreeTrialDaysTextBox.Text);
            _salesConfirmationHd.CustGroup = this.CustGroupDropDownList.SelectedValue;
            _salesConfirmationHd.CustType = this.CustTypeDropDownList.SelectedValue;
            _salesConfirmationHd.Term = this.TermDropDownLilst.SelectedValue;
            _salesConfirmationHd.FgGenerateBillAccount = this.GenerateCheckBox.Checked;

            _salesConfirmationHd.ResponsibleCustName = this.NameTextBox.Text;
            _salesConfirmationHd.ResponsibleBirthDate = DateFormMapper.GetValue(this.BirthDateTextBox.Text);
            _salesConfirmationHd.ResponsibleGender = this.GenderRadioButtonList.SelectedValue;
            _salesConfirmationHd.ResponsibleIDCard = this.IDCardNoTextBox.Text;
            _salesConfirmationHd.ResponsibleAddress = this.AddressTextBox.Text;
            _salesConfirmationHd.ResponsibleAddrPostal = this.PostalAddrTextBox.Text;
            _salesConfirmationHd.ResponsibleAddrCity = this.CityDropDownList.SelectedValue;
            _salesConfirmationHd.ResponsibleAddrCountry = this.CountryDropDownList.SelectedValue;
            _salesConfirmationHd.ResponsibleEmailAddress = this.EmailTextBox.Text;
            _salesConfirmationHd.ResponsiblePhone = this.TelpTextBox.Text;
            _salesConfirmationHd.ResponsibleMobilePhone = this.CellularTextBox.Text;
            _salesConfirmationHd.ResponsibleFax = this.FaxTextBox.Text;

            _salesConfirmationHd.CompanyAddrCity = this.CompCityDropDownList.SelectedValue;
            _salesConfirmationHd.CompanyAddrCountry = this.CompCountryDropDownList.SelectedValue;
            _salesConfirmationHd.CompanyAddress = this.CompAddrTextBox.Text;
            _salesConfirmationHd.CompanyAddrPostal = this.CompZipCodeTextBox.Text;
            _salesConfirmationHd.CompanyFax = this.CompFaxTextBox.Text;
            _salesConfirmationHd.CompanyMobilePhone = this.CompCellularTextBox.Text;
            if (this.FgNewCustCheckBox.Checked == true)
            {
                _salesConfirmationHd.CustCode = this.CustomerCodeTextBox.Text;
                _salesConfirmationHd.CompanyName = this.CompNameTextBox.Text;
            }
            else
            {
                _salesConfirmationHd.CustCode = this.CustomerDropDownList.SelectedValue;
                _salesConfirmationHd.CompanyName = _customerBL.GetNameByCode(this.CustomerDropDownList.SelectedValue);
                _salesConfirmationHd.CustBillAccount = this.CustBillAccDropDownList.SelectedValue;

                if (this.CustBillAccDropDownList.SelectedValue != "null")
                {
                    Guid _custBillAccount = this._custBillAccBL.GetCustBillCode(this.CustBillAccDropDownList.SelectedValue);

                    Master_CustBillAccount _custBillAcc = this._custBillAccBL.GetSingleCustBillAccount(_custBillAccount);

                    _salesConfirmationHd.CustBillAccountAmount = _custBillAcc.AmountForex;
                    _salesConfirmationHd.CustBillAccountDesc = _custBillAcc.CustBillDescription;
                    _salesConfirmationHd.CustBillAccountCurrCode = _custBillAcc.CurrCode;
                }
            }
            _salesConfirmationHd.CompanyNPWP = this.CompNPWPTextBox.Text;
            _salesConfirmationHd.CompanyPhone = this.CompTelpTextBox.Text;
            _salesConfirmationHd.CompanyWebAddress = this.CompWebsiteTextBox.Text;
            _salesConfirmationHd.CompanyBusinessType = this.BusinessTypeTextBox.Text;

            _salesConfirmationHd.TargetInstallationDay = Convert.ToInt32(this.TargetInstalationDayTextBox.Text);
            _salesConfirmationHd.InstallationAddress = this.InstallationAddrTextBox.Text;
            _salesConfirmationHd.InstallationDeviceDesc = this.InstallationDeviceDescTextBox.Text;
            _salesConfirmationHd.InstallationDeviceStatus = this.InstallationDeviceStatusTextBox.Text;

            _salesConfirmationHd.TechnicalEmail = this.TechEmailTextBox.Text;
            _salesConfirmationHd.TechnicalEmail2 = this.TechEmail2TextBox.Text;
            _salesConfirmationHd.TechnicalFax = this.TechFaxTextBox.Text;
            _salesConfirmationHd.TechnicalMobilePhone = this.TechCellularTextBox.Text;
            _salesConfirmationHd.TechnicalName = this.TechNameTextBox.Text;
            _salesConfirmationHd.TechnicalPhone = this.TechPhoneTextBox.Text;
            _salesConfirmationHd.TechnicalPIC = this.TechPICTextBox.Text;

            _salesConfirmationHd.fgPPN = this.FgPPNCheckBox.Checked;
            _salesConfirmationHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
            _salesConfirmationHd.PPNPercentage = (this.PPNPercentageTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentageTextBox.Text);

            _salesConfirmationHd.RegCode = this.RegDropDownList.SelectedValue;
            _salesConfirmationHd.EditBy = HttpContext.Current.User.Identity.Name;
            _salesConfirmationHd.EditDate = DateTime.Now;

            _salesConfirmationHd.ApprovedByCustomerName = this.ApprovedByTextBox.Text;

            Boolean _result = this._salesConfirmationBL.EditSalesConfirmation(_salesConfirmationHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Page.IsValid == true)
            {
                BILTrSalesConfirmation _salesConfirmationHd = this._salesConfirmationBL.GetSingleSalesConfirmation(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _salesConfirmationHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                _salesConfirmationHd.fgNewCustomer = this.FgNewCustCheckBox.Checked;

                _salesConfirmationHd.FormulirID = this.FormIDTextBox.Text;
                _salesConfirmationHd.Remark = this.RemarkTextBox.Text;
                _salesConfirmationHd.SalesID = this.SalesDropDownList.SelectedValue;
                _salesConfirmationHd.ContractMinimum = Convert.ToInt32(this.ContractMinTextBox.Text);
                _salesConfirmationHd.SLA = Convert.ToDecimal(this.SLATextBox.Text);
                _salesConfirmationHd.BankPaymentCode = this.BankPaymentCodeDropDownList.SelectedValue;
                _salesConfirmationHd.FreeTrialDays = Convert.ToInt32(this.FreeTrialDaysTextBox.Text);
                _salesConfirmationHd.FgGenerateBillAccount = this.GenerateCheckBox.Checked;

                _salesConfirmationHd.ResponsibleCustName = this.NameTextBox.Text;
                _salesConfirmationHd.ResponsibleBirthDate = DateFormMapper.GetValue(this.BirthDateTextBox.Text);
                _salesConfirmationHd.ResponsibleGender = this.GenderRadioButtonList.SelectedValue;
                _salesConfirmationHd.ResponsibleIDCard = this.IDCardNoTextBox.Text;
                _salesConfirmationHd.ResponsibleAddress = this.AddressTextBox.Text;
                _salesConfirmationHd.ResponsibleAddrPostal = this.PostalAddrTextBox.Text;
                _salesConfirmationHd.ResponsibleAddrCity = this.CityDropDownList.SelectedValue;
                _salesConfirmationHd.ResponsibleAddrCountry = this.CountryDropDownList.SelectedValue;
                _salesConfirmationHd.ResponsibleEmailAddress = this.EmailTextBox.Text;
                _salesConfirmationHd.ResponsiblePhone = this.TelpTextBox.Text;
                _salesConfirmationHd.ResponsibleMobilePhone = this.CellularTextBox.Text;
                _salesConfirmationHd.ResponsibleFax = this.FaxTextBox.Text;

                _salesConfirmationHd.CompanyAddrCity = this.CompCityDropDownList.SelectedValue;
                _salesConfirmationHd.CompanyAddrCountry = this.CompCountryDropDownList.SelectedValue;
                _salesConfirmationHd.CompanyAddress = this.CompAddrTextBox.Text;
                _salesConfirmationHd.CompanyAddrPostal = this.CompZipCodeTextBox.Text;
                _salesConfirmationHd.CompanyFax = this.CompFaxTextBox.Text;
                _salesConfirmationHd.CompanyMobilePhone = this.CompCellularTextBox.Text;
                if (this.FgNewCustCheckBox.Checked == true)
                {
                    _salesConfirmationHd.CustCode = this.CustomerCodeTextBox.Text;
                    _salesConfirmationHd.CompanyName = this.CompNameTextBox.Text;
                }
                else
                {
                    _salesConfirmationHd.CustCode = this.CustomerDropDownList.SelectedValue;
                    _salesConfirmationHd.CompanyName = _customerBL.GetNameByCode(this.CustomerDropDownList.SelectedValue);
                    _salesConfirmationHd.CustBillAccount = this.CustBillAccDropDownList.SelectedValue;

                    if (this.CustBillAccDropDownList.SelectedValue != "null")
                    {
                        Guid _custBillAccount = this._custBillAccBL.GetCustBillCode(this.CustBillAccDropDownList.SelectedValue);

                        Master_CustBillAccount _custBillAcc = this._custBillAccBL.GetSingleCustBillAccount(_custBillAccount);

                        _salesConfirmationHd.CustBillAccountAmount = _custBillAcc.AmountForex;
                        _salesConfirmationHd.CustBillAccountDesc = _custBillAcc.CustBillDescription;
                        _salesConfirmationHd.CustBillAccountCurrCode = _custBillAcc.CurrCode;
                    }
                }
                _salesConfirmationHd.CompanyNPWP = this.CompNPWPTextBox.Text;
                _salesConfirmationHd.CompanyPhone = this.CompTelpTextBox.Text;
                _salesConfirmationHd.CompanyWebAddress = this.CompWebsiteTextBox.Text;

                _salesConfirmationHd.TargetInstallationDay = Convert.ToInt32(this.TargetInstalationDayTextBox.Text);
                _salesConfirmationHd.InstallationAddress = this.InstallationAddrTextBox.Text;
                _salesConfirmationHd.InstallationDeviceDesc = this.InstallationDeviceDescTextBox.Text;
                _salesConfirmationHd.InstallationDeviceStatus = this.InstallationDeviceStatusTextBox.Text;

                _salesConfirmationHd.TechnicalEmail = this.TechEmailTextBox.Text;
                _salesConfirmationHd.TechnicalEmail2 = this.TechEmail2TextBox.Text;
                _salesConfirmationHd.TechnicalFax = this.TechFaxTextBox.Text;
                _salesConfirmationHd.TechnicalMobilePhone = this.TechCellularTextBox.Text;
                _salesConfirmationHd.TechnicalName = this.TechNameTextBox.Text;
                _salesConfirmationHd.TechnicalPhone = this.TechPhoneTextBox.Text;
                _salesConfirmationHd.TechnicalPIC = this.TechPICTextBox.Text;

                _salesConfirmationHd.fgPPN = this.FgPPNCheckBox.Checked;
                _salesConfirmationHd.PPNForex = (this.PPNForexTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNForexTextBox.Text);
                _salesConfirmationHd.PPNPercentage = (this.PPNPercentageTextBox.Text == "") ? 0 : Convert.ToDecimal(this.PPNPercentageTextBox.Text);

                _salesConfirmationHd.RegCode = this.RegDropDownList.SelectedValue;

                _salesConfirmationHd.EditBy = HttpContext.Current.User.Identity.Name;
                _salesConfirmationHd.EditDate = DateTime.Now;

                _salesConfirmationHd.ApprovedByCustomerName = this.ApprovedByTextBox.Text;

                Boolean _result = this._salesConfirmationBL.EditSalesConfirmation(_salesConfirmationHd);

                if (_result == true)
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed Edit Data";
                }
            }
        }

        protected void CustomerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustomerDropDownList.SelectedValue != "null")
            {
                MsCustomer _msCust = this._customerBL.GetSingleCust(this.CustomerDropDownList.SelectedValue);

                this.ShowCustBillAcc();

                this.CompAddrTextBox.Text = _msCust.Address1;
                this.CompFaxTextBox.Text = _msCust.Fax;
                this.CompCityDropDownList.SelectedValue = _msCust.City;
                this.CompZipCodeTextBox.Text = _msCust.ZipCode;
                this.CompTelpTextBox.Text = _msCust.Phone;
                this.CompNPWPTextBox.Text = _msCust.NPWP;
            }
        }
    }
}
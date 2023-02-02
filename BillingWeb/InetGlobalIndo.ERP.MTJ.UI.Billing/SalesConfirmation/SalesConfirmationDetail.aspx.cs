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
using System.Linq;
using System.Web.UI.HtmlControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using Microsoft.Reporting.WebForms;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.SalesConfirmation
{
    public partial class SalesConfirmationDetail : SalesConfirmationBase
    {
        private SalesConfirmationBL _salesConfirmationBL = new SalesConfirmationBL();
        private CustomerBL _customerBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private CityBL _cityBL = new CityBL();
        private CountryBL _countryBL = new CountryBL();
        private EmployeeBL _employeeBL = new EmployeeBL();
        private GenderBL _genderBL = new GenderBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private CurrencyBL _currencyBl = new CurrencyBL();
        private ReportBillingBL _reportBL = new ReportBillingBL();
        private TermBL _termBL = new TermBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();
        private ReportListBL _reportListBL = new ReportListBL();
        private UserBL _userBL = new UserBL();
        private RegistrationConfigBL _regConfigBL = new RegistrationConfigBL();
        
        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;
        //private Boolean _isCheckedAll = true;

        private string _currPageKey = "CurrentPage";

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        //private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";

        //private string _reportPath1 = "SalesConfirmation/SalesConfirmationPrintPreview.rdlc";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.ShowGender();

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData(0);
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
            this.Label1.Text = "";
        }

        private void ShowGender()
        {
            this.GenderRadioButtonList.DataSource = this._genderBL.GetListGenderForDDL();
            this.GenderRadioButtonList.DataValueField = "GenderCode";
            this.GenderRadioButtonList.DataTextField = "GenderName";
            this.GenderRadioButtonList.DataBind();
        }

        private Boolean IsCheckedAll()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._salesConfirmationBL.RowsCountSalesConfirmationDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        private void ShowPage(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount();

            if (_prmCurrentPage - _maxlength > 0)
            {
                min = _prmCurrentPage - _maxlength;
            }
            else
            {
                min = 0;
            }

            if (_prmCurrentPage + _maxlength < q)
            {
                max = _prmCurrentPage + _maxlength + 1;
            }
            else
            {
                max = Convert.ToDecimal(q);
            }

            if (_prmCurrentPage > 0)
                _addElement += 2;

            if (_prmCurrentPage < q - 1)
                _addElement += 2;

            i = Convert.ToInt32(min);
            _pageNumber = new Int32[Convert.ToInt32(max - min) + _addElement];
            if (_pageNumber.Length != 0)
            {
                //NB: Prev Or First
                if (_prmCurrentPage > 0)
                {
                    this._navMark[0] = 0;

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[0]);
                    _pageNumberElement++;

                    this._navMark[1] = Convert.ToInt32(Math.Abs(_prmCurrentPage - 1));

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[1]);
                    _pageNumberElement++;
                }

                for (; i < max; i++)
                {
                    _pageNumber[_pageNumberElement] = i;
                    _pageNumberElement++;
                }

                if (_prmCurrentPage < q - 1)
                {
                    this._navMark[2] = Convert.ToInt32(_prmCurrentPage + 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[2]);
                    _pageNumberElement++;

                    if (_pageNumber[_pageNumberElement - 2] > _pageNumber[_pageNumberElement - 1])
                    {
                        this._flag = true;
                    }

                    this._navMark[3] = Convert.ToInt32(q - 1);

                    _pageNumber[_pageNumberElement] = Convert.ToInt32(this._navMark[3]);
                    _pageNumberElement++;
                }

                int?[] _navMarkBackup = new int?[4];
                this._navMark.CopyTo(_navMarkBackup, 0);
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();

                _flag = true;
                _nextFlag = false;
                _lastFlag = false;
                _navMark = _navMarkBackup;

                //this.DataPagerBottomRepeater.DataSource = from _query in _pageNumber
                //                                          select _query;
                //this.DataPagerBottomRepeater.DataBind();
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = null;
                this.DataPagerTopRepeater.DataBind();
            }
        }

        private Boolean IsChecked(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden.Value.Split(',');

            for (int i = 0; i < _value.Length; i++)
            {
                if (_prmValue == _value[i])
                {
                    _result = true;
                    break;
                }
            }

            return _result;
        }

        protected void ShowData(Int32 _prmCurrentPage)
        {
            _salesConfirmationBL = new SalesConfirmationBL();

            BILTrSalesConfirmation _salesConfirmationHd = this._salesConfirmationBL.GetSingleSalesConfirmation(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.DateTextBox.Text = DateFormMapper.GetValue(_salesConfirmationHd.TransDate);
            this.TransNoTextBox.Text = _salesConfirmationHd.TransNmbr;
            this.FileNmbrTextBox.Text = _salesConfirmationHd.FileNmbr;
            this.AddressTextBox.Text = _salesConfirmationHd.ResponsibleAddress;
            this.BankPaymentCodeTextBox.Text = _paymentBL.GetPaymentName(_salesConfirmationHd.BankPaymentCode);
            this.BirthDateTextBox.Text = DateFormMapper.GetValue(_salesConfirmationHd.ResponsibleBirthDate);
            this.CellularTextBox.Text = _salesConfirmationHd.ResponsibleMobilePhone;
            this.CityTextBox.Text = _cityBL.GetCityNameByCode(_salesConfirmationHd.ResponsibleAddrCity);
            this.CustGroupTextBox.Text = _customerBL.GetCustGroupNameByCode(_salesConfirmationHd.CustGroup);
            this.CustTypeTextBox.Text = _customerBL.GetCustTypeNameByCode(_salesConfirmationHd.CustType);
            this.TermTextBox.Text = _termBL.GetTermNameByCode(_salesConfirmationHd.Term);
            this.BusinessTypeTextBox.Text = _salesConfirmationHd.CompanyBusinessType;
            this.GenerateCheckBox.Checked = Convert.ToBoolean(_salesConfirmationHd.FgGenerateBillAccount);

            this.CompAddrTextBox.Text = _salesConfirmationHd.CompanyAddress;
            this.CompCellularTextBox.Text = _salesConfirmationHd.CompanyMobilePhone;
            this.CompCityTextBox.Text = _cityBL.GetCityNameByCode(_salesConfirmationHd.CompanyAddrCity);
            this.CompCountryTextBox.Text = _countryBL.GetCountryNameByCode(_salesConfirmationHd.CompanyAddrCountry);
            this.CompFaxTextBox.Text = _salesConfirmationHd.CompanyFax;
            this.CompNameTextBox.Text = _salesConfirmationHd.CompanyName;
            this.CompNPWPTextBox.Text = _salesConfirmationHd.CompanyNPWP;
            this.CompTelpTextBox.Text = _salesConfirmationHd.CompanyPhone;
            this.CompWebsiteTextBox.Text = _salesConfirmationHd.CompanyWebAddress;
            this.CompZipCodeTextBox.Text = _salesConfirmationHd.CompanyAddrPostal;
            this.ContractMinTextBox.Text = _salesConfirmationHd.ContractMinimum.ToString();
            this.CountryTextBox.Text = _countryBL.GetCountryNameByCode(_salesConfirmationHd.ResponsibleAddrCountry);
            if (_salesConfirmationHd.fgNewCustomer == true)
            {
                this.CustomerCodeTextBox.Text = _salesConfirmationHd.CustCode;
                this.CompNameTextBox.Text = _salesConfirmationHd.CompanyName;
                this.CustomerCodeTextBox.Attributes.Add("Style", "visibility:visible;");
                this.tr1.Attributes.Add("Style", "visibility:visible;");
                this.tr2.Attributes.Add("Style", "visibility:visible;");
                this.tr3.Attributes.Add("Style", "visibility:hidden;");
                this.CustomerTextBox.Attributes.Add("Style", "visibility:hidden;");
            }
            else
            {
                this.CustomerCodeTextBox.Text = _salesConfirmationHd.CustCode;
                this.CustomerTextBox.Text = _customerBL.GetNameByCode(_salesConfirmationHd.CustCode);
                this.CustomerCodeTextBox.Attributes.Add("Style", "visibility:hidden;");
                this.tr1.Attributes.Add("Style", "visibility:hidden;");
                this.tr2.Attributes.Add("Style", "visibility:hidden;");
                this.tr3.Attributes.Add("Style", "visibility:visible;");
                if (_salesConfirmationHd.CustBillAccount != "null")
                {
                    this.CustBillAccCheckBox.Checked = false;
                    this.CustBillAccTextBox.Text = _salesConfirmationHd.CustBillAccount;
                }
                else
                {
                    this.CustBillAccCheckBox.Checked = true;
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
            }
            else
            {
                this.PPNForexTextBox.Text = "";
                this.PPNPercentageTextBox.Text = "";
            }
            this.RegTextBox.Text = _regConfigBL.GetRegNameByCode(_salesConfirmationHd.RegCode);
            this.RemarkTextBox.Text = _salesConfirmationHd.Remark;
            this.SalesTextBox.Text = _employeeBL.GetEmpNameByCode(_salesConfirmationHd.SalesID);
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
            this.StatusLabel.Text = SalesConfirmationDataMapper.GetStatusText(_salesConfirmationHd.Status);
            this.StatusHiddenField.Value = _salesConfirmationHd.Status.ToString();

            if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.EditButton.Visible = true;
            }

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;

            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._salesConfirmationBL.GetListSalesConfirmationDt(_prmCurrentPage, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.ShowActionButton();
            this.ShowPreviewButton();

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        public void ShowPreviewDDL()
        {
            this.PreviewDropDownList.DataSource = this._reportListBL.GetListPrintPreviewSelection(AppModule.GetValue(TransactionType.SalesConfirmation), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));
            this.PreviewDropDownList.DataValueField = "SortNo";
            this.PreviewDropDownList.DataTextField = "ReportName";
            this.PreviewDropDownList.DataBind();
            //this.PreviewHiddenField.Value = this.PreviewDropDownList.SelectedIndex.ToString();
        }

        public void ShowPreviewButton()
        {
            this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;

            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (Convert.ToByte(this.StatusHiddenField.Value) >= SalesConfirmationDataMapper.GetStatusByte(TransStatus.Approved))
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewDropDownList.Visible = true;
                    this.ShowPreviewDDL();
                    //this.PreviewHiddenField.Value = this.PreviewDropDownList.SelectedIndex.ToString();
                }
                else
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewDropDownList.Visible = false;
                }
            }
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                //this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                //this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                //if (this._permUnposting == PermissionLevel.NoAccess)
                //{
                this.ActionButton.Visible = false;
                //}
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._salesConfirmationBL.GetApproval(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._salesConfirmationBL.Approve(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._salesConfirmationBL.Posting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                string _result = this._salesConfirmationBL.UnPosting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }

            this.ShowData(0);
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = new ReportDataSource();

            String _select = (this.PreviewDropDownList.SelectedIndex).ToString();
            if (_select == "0")
            {
                _reportDataSource1 = this._reportBL.SalesConfirmationPrintPreview(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            else if (_select == "1")
            {
                _reportDataSource1 = this._reportBL.SalesConfirmationPrintPreview(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            else if (_select == "2")
            {
                _reportDataSource1 = this._reportBL.BillingInvoicePrintPreview(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            else if (_select == "3")
            {
                _reportDataSource1 = this._reportBL.CustomerInvoicePrintPreview(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            //Guid _compId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);
            //String _companyTag = new UserBL().GetCompanyTag(_compId);

            String _jobTitleStatus = this._companyConfigBL.GetSingle(CompanyConfigure.ViewJobTitlePrintReport).SetValue;
            //String _termAndCondition = this._termAndConditionBL.GetSingle(TermAndConditionDataMapper.GetType(TermAndConditionType.SalesConfirmation)).Body;

            //System.Web.UI.HtmlControls.HtmlGenericControl htmlDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("ReportViewer1");
            //htmlDiv.InnerHtml = _termAndCondition;
            //String _plainText = htmlDiv.InnerText;

            //this.TermAndConditionLabel.Text = _plainText;

            string _path = _reportListBL.GetPathPrintPreviewSC(AppModule.GetValue(TransactionType.SalesConfirmation), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name), this.PreviewDropDownList.SelectedIndex + 1);

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[4];
            _reportParam[0] = new ReportParameter("TransNmbr", Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), true);
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), true);
            _reportParam[2] = new ReportParameter("JobTitleStatus", _jobTitleStatus, false);
            _reportParam[3] = new ReportParameter("TermAndCondition", "", false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._salesConfirmationBL.DeleteMultiSalesConfirmationDt(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BILTrSalesConfirmationDt _temp = (BILTrSalesConfirmationDt)e.Item.DataItem;

                string _code = _temp.ProductCode;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _cb = (CheckBox)e.Item.FindControl("ListCheckBox");
                _cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + "," + _cb.ClientID + ",'" + _code + "', '" + _awal + "' , '" + _akhir + "', '" + _cbox + "' )");
                _cb.Checked = this.IsChecked(_code);

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                if (this.StatusHiddenField.Value.Trim().ToLower() == SalesConfirmationDataMapper.GetStatusByte(TransStatus.Posted).ToString().Trim().ToLower())
                {
                    _editButton.Visible = false;
                }
                else
                {
                    _editButton.PostBackUrl = this._editDetailPage + "?" + this._detailKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
                    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }

                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }

                HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
                _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
                if (e.Item.ItemType == ListItemType.Item)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
                }
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
                }

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductName);

                byte _decimalPlace = this._currencyBl.GetDecimalPlace(_temp.CurrCode);
                Literal _amountForexLiteral = (Literal)e.Item.FindControl("AmountLiteral");
                _amountForexLiteral.Text = (_temp.AmountForex == 0) ? "0" : Convert.ToDecimal(_temp.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                Literal _productSpecLiteral = (Literal)e.Item.FindControl("ProductSpecLiteral");
                _productSpecLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductSpecification);

                Literal _currCodeLiteral = (Literal)e.Item.FindControl("CurrLiteral");
                _currCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.CurrCode);
            }
        }

        //protected void PreviewDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.PreviewHiddenField.Value = this.PreviewDropDownList.SelectedIndex.ToString();
        //}

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton");
                    _pageNumberLinkButton.CommandName = "DataPager";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark[0] = null;
                    }
                    else if (_pageNumber == this._navMark[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark[1] = null;
                    }
                    else if (_pageNumber == this._navMark[2] && this._flag == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark[2] = null;
                        this._nextFlag = true;
                        this._flag = true;
                    }
                    else if (_pageNumber == this._navMark[3] && this._flag == true && this._nextFlag == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark[3] = null;
                        this._lastFlag = true;
                    }
                    else
                    {
                        if (this._lastFlag == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark[2] && this._flag == true)
                            this._flag = false;
                    }
                }
            }
        }

        protected void DataPagerButton_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount()) - 1;
                        break;
                    }
                    else if (_reqPage < 0)
                    {
                        ((TextBox)_item.Controls[3]).Text = "1";
                        _reqPage = 0;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            this.ViewState[this._currPageKey] = _reqPage;

            this.ShowData(_reqPage);
        }
    }
}
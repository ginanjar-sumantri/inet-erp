using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustomerInvoice
{
    public partial class CustomerInvoiceDetail : CustomerInvoiceBase
    {
        private CustomerInvoiceBL _customerInvoiceBL = new CustomerInvoiceBL();
        private CustomerBL _customerBL = new CustomerBL();
        private TermBL _termBL = new TermBL();
        private GLPeriodBL _glPeriodBL = new GLPeriodBL();
        private ReportBillingBL _reportBillingBL = new ReportBillingBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PermissionBL _permBL = new PermissionBL();
        private CompanyConfig _compConfig = new CompanyConfig();
        private ReportListBL _reportListBL = new ReportListBL();
        private UserBL _userBL = new UserBL();
        private CompanyConfig _compantConfigBL = new CompanyConfig();

        private string _reportPath = "";
        private string _reportPath2 = "CustomerInvoice/TaxCustomerInvoicePrintPreview.rdlc";
        private string _reportPath3 = "CustomerInvoice/TaxCustomerInvoiceForexCurrencyPrintPreview.rdlc";
        private string _reportPath4 = "CustomerInvoice/CDCCustomerInvoicePrintPreview.rdlc";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        //private string _cdcCompanyCode = "d8be7ec6-9dbb-4372-9165-7d9f380ce6ab"; // NB : temporary solution

        private string _currPageKey = "CurrentPage";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;
        //private Boolean _isCheckedAll = true;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";
        private string _imgTaxPreview = "tax_preview.jpg";

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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.DataPagerButton.Attributes.Add("Style", "visibility: hidden;");

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.ViewState[this._currPageKey] = 0;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.ClearLabel();
                this.ShowData();
                this.ShowDetail(0);

                this.SetButtonPermission();
            }
        }

        private void SetButtonPermission()
        {
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

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowPreviewDDL()
        {
            this.PrintPreviewDropDownList.DataSource = this._reportListBL.GetListPrintPreviewSelection(AppModule.GetValue(TransactionType.CustomerInvoice), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));
            this.PrintPreviewDropDownList.DataValueField = "ReportPath";
            this.PrintPreviewDropDownList.DataTextField = "ReportName";
            this.PrintPreviewDropDownList.DataBind();
        }

        protected void ClearLabel()
        {
            this.Label.Text = "";
            this.WarningLabel.Text = "";
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        public void ShowPreviewButton()
        {
            this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;

            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PrintPreviewDropDownList.Visible = false;
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower() || this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower() || this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PrintPreviewDropDownList.Visible = true;
                    this.PreviewButton.Visible = true;
                    this.ShowPreviewDDL();
                }
                else
                {
                    this.PrintPreviewDropDownList.Visible = false;
                    this.PreviewButton.Visible = false;
                }
            }
        }

        public void ShowTaxPreviewButton()
        {
            string _tempCustCode = this._customerInvoiceBL.GetCustCodeFromCustomerInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
            bool _tempPrintFP = this._customerBL.GetPrintFP(_tempCustCode);

            this.TaxPreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgTaxPreview;

            this.TaxPreviewButton.Visible = false;

            this._permTaxPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.TaxPreview);

            if (this._permTaxPreview == PermissionLevel.NoAccess)
            {
                this.TaxPreviewButton.Visible = false;
            }
            else
            {

                if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower() && _tempPrintFP == true)
                {
                    this.TaxPreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower() && _tempPrintFP == true)
                {
                    this.TaxPreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower() && _tempPrintFP == true)
                {
                    this.TaxPreviewButton.Visible = true;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower() && _tempPrintFP == true)
                {
                    this.TaxPreviewButton.Visible = true;
                }
            }
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

        public void ShowData()
        {
            this._customerInvoiceBL = new CustomerInvoiceBL();
            Billing_CustomerInvoiceHd _customerInvoiceHd = this._customerInvoiceBL.GetSingleCustomerInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_customerInvoiceHd.CurrCode);

            this.TransactionNoTextBox.Text = _customerInvoiceHd.TransNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_customerInvoiceHd.TransDate);
            this.PeriodTextBox.Text = this._glPeriodBL.GetDescByPeriod(_customerInvoiceHd.Period);
            this.YearTextBox.Text = _customerInvoiceHd.Year.ToString();
            this.CustomerTextBox.Text = this._customerBL.GetNameByCode(_customerInvoiceHd.CustCode) + " - " + _customerInvoiceHd.CustCode;
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_customerInvoiceHd.DueDate);
            this.AttnTextBox.Text = _customerInvoiceHd.Attn;
            this.TermTextBox.Text = this._termBL.GetTermNameByCode(_customerInvoiceHd.Term);
            this.TypeTextbox.Text = CustomerInvoiceDataMapper.GetTypeText(CustomerInvoiceDataMapper.GetType(_customerInvoiceHd.Type));
            this.CurrCodeTextBox.Text = _customerInvoiceHd.CurrCode;
            this.ForexRateTextBox.Text = (_customerInvoiceHd.ForexRate == 0) ? "0" : _customerInvoiceHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempPPNPercent = Convert.ToDecimal((_customerInvoiceHd.PPN == null) ? 0 : _customerInvoiceHd.PPN);
            this.PPNPercentTextBox.Text = (_tempPPNPercent == 0) ? "0" : _tempPPNPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_tempPPNPercent > 0)
            {
                this.PPNNoTextBox.Text = _customerInvoiceHd.PPNNo;
                this.PPNDateTextBox.Text = DateFormMapper.GetValue(_customerInvoiceHd.PPNDate);
            }
            else
            {
                this.PPNNoTextBox.Text = "";
                this.PPNDateTextBox.Text = "";
            }

            decimal _tempPPNRate = Convert.ToDecimal((_customerInvoiceHd.PPNRate == null) ? 0 : _customerInvoiceHd.PPNRate);
            this.PPNRateTextBox.Text = (_tempPPNRate == 0) ? "0" : _tempPPNRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.CurrTextBox.Text = _customerInvoiceHd.CurrCode;

            decimal _tempBaseForex = Convert.ToDecimal((_customerInvoiceHd.BaseForex == null) ? 0 : _customerInvoiceHd.BaseForex);
            this.AmountBaseTextBox.Text = (_tempBaseForex == 0) ? "0" : _tempBaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempDiscPercent = Convert.ToDecimal((_customerInvoiceHd.DiscPercent == null) ? 0 : _customerInvoiceHd.DiscPercent);
            this.DiscPercentTextBox.Text = (_tempDiscPercent == 0) ? "0" : _tempDiscPercent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempDiscForex = Convert.ToDecimal((_customerInvoiceHd.DiscForex == null) ? 0 : _customerInvoiceHd.DiscForex);
            this.DiscForexTextBox.Text = (_tempDiscForex == 0) ? "0" : _tempDiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempPPNForex = Convert.ToDecimal((_customerInvoiceHd.PPNForex == null) ? 0 : _customerInvoiceHd.PPNForex);
            this.PPNForexTextBox.Text = (_tempPPNForex == 0) ? "0" : _tempPPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempOtherFee = Convert.ToDecimal((_customerInvoiceHd.OtherFee == null) ? 0 : _customerInvoiceHd.OtherFee);
            this.OtherFeeTextBox.Text = (_tempOtherFee == 0) ? "0" : _tempOtherFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempStampFee = Convert.ToDecimal((_customerInvoiceHd.StampFee == null) ? 0 : _customerInvoiceHd.StampFee);
            this.StampFeeTextBox.Text = (_tempStampFee == 0) ? "0" : _tempStampFee.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempCommissionExpense = Convert.ToDecimal((_customerInvoiceHd.CommissionExpense == null) ? 0 : _customerInvoiceHd.CommissionExpense);
            this.CommissionExpenseTextBox.Text = (_tempCommissionExpense == 0) ? "0" : _tempCommissionExpense.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _tempTotalForex = Convert.ToDecimal((_customerInvoiceHd.TotalForex == null) ? 0 : _customerInvoiceHd.TotalForex);
            this.TotalForexTextBox.Text = (_tempTotalForex == 0) ? "0" : _tempTotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.RemarkTextBox.Text = _customerInvoiceHd.Remark;

            this.StatusLabel.Text = CustomerInvoiceDataMapper.GetStatusText(_customerInvoiceHd.Status);
            this.StatusHiddenField.Value = _customerInvoiceHd.Status.ToString();

            this.ShowActionButton();
            this.ShowPreviewButton();
            this.ShowTaxPreviewButton();

            if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
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

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._customerInvoiceBL.RowsCountCustomerInvoiceDt(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
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
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();
            }
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDetail(Convert.ToInt32(e.CommandArgument));
            }
        }

        //Heri 20080912
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

            this.ShowDetail(_reqPage);
        }

        protected void ShowDetail(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._customerInvoiceBL.GetListCustomerInvoiceDt(_prmCurrentPage, _maxrow, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
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

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrCodeTextBox.Text);

                Billing_CustomerInvoiceDt _temp = (Billing_CustomerInvoiceDt)e.Item.DataItem;
                string _code = _temp.CustomerInvoiceDtCode.ToString();

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

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                if (this.StatusHiddenField.Value.Trim().ToLower() == BillingInvoiceDataMapper.GetStatus(TransStatus.Posted).ToString().Trim().ToLower())
                {
                    _editButton.Visible = false;
                }
                else
                {
                    _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey));
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

                Literal _custInvoiceDescLiteral = (Literal)e.Item.FindControl("CustInvoiceDescLiteral");
                _custInvoiceDescLiteral.Text = HttpUtility.HtmlEncode(_temp.CustInvoiceDescription);

                Literal _amountForexLiteral = (Literal)e.Item.FindControl("AmountForexLiteral");
                _amountForexLiteral.Text = (_temp.AmountForex == 0) ? "0" : _temp.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                Literal _remarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
                _remarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._customerInvoiceBL.GetApproval(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                if (_result == "")
                {
                    this.Label.Text = "Get Approval Success";
                }
                else
                {
                    this.Label.Text = _result;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._customerInvoiceBL.Approve(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                if (_result == "")
                {
                    this.Label.Text = "Approve Success";
                }
                else
                {
                    this.Label.Text = _result;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._customerInvoiceBL.Posting(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                if (_result == "")
                {
                    this.Label.Text = "Posting Success";
                }
                else
                {
                    this.Label.Text = _result;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerInvoiceDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                string _result = this._customerInvoiceBL.UnPosting(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), HttpContext.Current.User.Identity.Name);

                if (_result == "")
                {
                    this.Label.Text = "You Success UnPosting";
                }
                else
                {
                    this.Label.Text = _result;
                }
            }

            this.ShowData();
            this.ShowDetail(0);
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportBillingBL.CustomerInvoicePrintPreview(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            Guid _compId = new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name);
            String _companyTag = new UserBL().GetCompanyTag(_compId);

            //if (new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name) == new Guid(_cdcCompanyCode))
            //{
            //    this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath4;
            //}
            //else
            //{
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + PrintPreviewDropDownList.SelectedValue;
            //}
            this.ReportViewer1.DataBind();

            String _image = _compConfig.GetSingle(CompanyConfigure.BillingAuthorizedSignImage).SetValue;

            ReportParameter[] _reportParam = new ReportParameter[6];
            _reportParam[0] = new ReportParameter("InvoiceNo", Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), true);
            _reportParam[1] = new ReportParameter("CompanyTag", _companyTag, true);
            _reportParam[2] = new ReportParameter("Image", ApplicationConfig.HomeWebAppURL + "images/" + _image, false);
            _reportParam[3] = new ReportParameter("HeaderImage", ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name), false);
            _reportParam[4] = new ReportParameter("CompanyAddress", new UserBL().CompanyAddress(HttpContext.Current.User.Identity.Name), false);
            _reportParam[5] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

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

            bool _result = this._customerInvoiceBL.DeleteMultiCustomerInvoiceDt(_tempSplit, new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

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

        protected void TaxPreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            Billing_CustomerInvoiceHd _customerInvoiceHd = this._customerInvoiceBL.GetSingleCustomerInvoiceHd(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            if (_customerInvoiceHd.CurrCode == _currencyBL.GetCurrDefault())
            {
                this._reportPath = this._reportPath2;
            }
            else
            {
                this._reportPath = this._reportPath3;
            }

            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportBillingBL.BillingTaxCustomerInvoicePrintPreview(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;
            this.ReportViewer1.DataBind();

            CompanyConfiguration _companyConfig = this._compantConfigBL.GetSingleCompanyConfiguration("CompanyNPWP");

            ReportParameter[] _reportParam = new ReportParameter[4];
            _reportParam[0] = new ReportParameter("CustomerInvoiceNo", Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), true);
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name));
            _reportParam[2] = new ReportParameter("CompanyAddress", new UserBL().CompanyAddress(HttpContext.Current.User.Identity.Name));
            _reportParam[3] = new ReportParameter("NPWP", _companyConfig.SetValue, true);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
}
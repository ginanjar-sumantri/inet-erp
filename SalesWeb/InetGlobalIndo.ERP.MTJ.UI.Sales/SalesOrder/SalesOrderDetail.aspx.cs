using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using Microsoft.Reporting.WebForms;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.SalesOrder
{
    public partial class SalesOrderDetail : SalesOrderBase
    {
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CustomerBL _custBL = new CustomerBL();
        private UnitBL _unitBL = new UnitBL();
        private TermBL _termBL = new TermBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private ReportSalesBL _reportSalesBL = new ReportSalesBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();
        private ReportListBL _reportListBL = new ReportListBL();
        private UserBL _userBL = new UserBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();
        private ProductBL _productBL = new ProductBL();
        //private string _reportPath1 = "SalesOrder/SalesOrderPrintPreview.rdlc";

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private string _currPageKey = "CurrentPage";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _no2 = 0;
        private int _nomor = 0;
        private int _nomor2 = 0;
        //private Boolean _isCheckedAll = true;

        private int _prmRevisi = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";

        private byte _decimalPlace = 0;

        private string _confirmTitle = "Description Required";

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

                MKTSOHd _salesOrderHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), 0);
                if (_salesOrderHd.Status == SalesOrderDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.AddButton.Visible = false;
                    this.DeleteButton.Visible = false;
                    this.EditButton.Visible = false;
                    this.ImportButton.Visible = false;
                    this.GetFormatExcelLiteral.Text = "";
                }
                else
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                    //this.DeleteHeaderButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                    this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                    this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                    this.ImportButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/import_excel.jpg";
                    this.GetFormatExcelLiteral.Text = "<a href='" + ApplicationConfig.SalesFormatVirDirPath + "Sales Order.xls'>" + "<img src='" + ApplicationConfig.HomeWebAppURL + "images/download_excel.jpg" + "' border='0'>" + "</a>";
                }
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.SetButtonPermission();

                //this.ShowRevisiDDL();
                this._prmRevisi = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey));

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData(_prmRevisi, 0);
                this.ShowData2(_prmRevisi, 0);
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
                //this.DeleteHeaderButton.Visible = false;
            }
        }

        private void SetAttribute()
        {
            //this.DeleteHeaderButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._salesOrderBL.RowsCountMKTSODt(this.TransNoTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
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

        //public void ShowRevisiDDL()
        //{
        //    this.RevisiDropDownList.Items.Clear();
        //    this.RevisiDropDownList.DataTextField = "Revisi";
        //    this.RevisiDropDownList.DataValueField = "Revisi";
        //    this.RevisiDropDownList.DataSource = this._salesOrderBL.GetListRevisiForDDL(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //    this.RevisiDropDownList.DataBind();
        //}

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
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
            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
            }
        }

        //public void ShowReviseButton()
        //{
        //    this._permRevisi = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Revisi);

        //    if (this._permRevisi == PermissionLevel.NoAccess)
        //    {
        //        this.ReviseButton.Visible = false;
        //    }
        //    else
        //    {
        //        this.ReviseButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/revision.jpg";

        //        if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
        //        {
        //            this.ReviseButton.Visible = true;
        //            this.DeleteHeaderButton.Visible = false;
        //        }
        //        else
        //        {
        //            this.ReviseButton.Visible = false;
        //            this.DeleteHeaderButton.Visible = true;
        //        }
        //    }
        //}

        public void ClearLabel()
        {
            this.Label.Text = "";
            this.WarningLabel.Text = "";
        }

        public void ShowData(int _prmRevisiNo, Int32 _prmCurrentPage)
        {
            this._salesOrderBL = new SalesOrderBL();
            MKTSOHd _salesOrderHd = this._salesOrderBL.GetSingleMKTSOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _prmRevisiNo);

            _decimalPlace = _currencyBL.GetDecimalPlace(_salesOrderHd.CurrCode);

            this.TransNoTextBox.Text = _salesOrderHd.TransNmbr;
            this.FileNmbrTextBox.Text = _salesOrderHd.FileNmbr;
            //this.RevisiDropDownList.SelectedValue = _salesOrderHd.Revisi.ToString();
            this.DateTextBox.Text = DateFormMapper.GetValue(_salesOrderHd.TransDate);
            this.StatusHiddenField.Value = _salesOrderHd.Status.ToString();
            this.StatusLabel.Text = SalesOrderDataMapper.GetStatusText(_salesOrderHd.Status);
            this.CustTextBox.Text = _custBL.GetNameByCode(_salesOrderHd.CustCode) + " - " + _salesOrderHd.CustCode;
            this.AttnTextBox.Text = _salesOrderHd.Attn;
            this.BillToTextBox.Text = _custBL.GetNameByCode(_salesOrderHd.BillTo) + " - " + _salesOrderHd.BillTo;
            this.TermTextBox.Text = _termBL.GetTermNameByCode(_salesOrderHd.Term);
            this.DueDateTextBox.Text = DateFormMapper.GetValue(_salesOrderHd.DueDate);
            this.CustPONoTextBox.Text = _salesOrderHd.CustPONo;
            this.CustPODateTextBox.Text = DateFormMapper.GetValue(_salesOrderHd.CustPODate);
            this.DeliveryTextBox.Text = _custBL.GetCustAddressNameByCode(_salesOrderHd.CustCode, _salesOrderHd.DeliveryTo);
            this.DeliveryDateTextBox.Text = DateFormMapper.GetValue(_salesOrderHd.DeliveryDate);
            this.CurrCodeTextBox.Text = _salesOrderHd.CurrCode;
            this.CurrTextBox.Text = _salesOrderHd.CurrCode;
            this.ForexRateTextBox.Text = (_salesOrderHd.ForexRate == 0) ? "0" : _salesOrderHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_salesOrderHd.TotalForex == 0) ? "0" : _salesOrderHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.FgDPCheckBox.Checked = TransactionDataMapper.GetStatusFlag(_salesOrderHd.FgDP);
            decimal _dpValue = Convert.ToDecimal((_salesOrderHd.DP == null) ? 0 : _salesOrderHd.DP);
            this.DPPercentTextBox.Text = (_dpValue == 0) ? "0" : _dpValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            decimal _dpForexValue = Convert.ToDecimal((_salesOrderHd.DPForex == null) ? 0 : _salesOrderHd.DPForex);
            this.DPForexTextBox.Text = (_dpForexValue == 0) ? "0" : _dpForexValue.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _salesOrderHd.Remark;
            this.BaseForexTextBox.Text = (_salesOrderHd.BaseForex == 0) ? "0" : _salesOrderHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscTextBox.Text = (_salesOrderHd.Disc == 0) ? "0" : _salesOrderHd.Disc.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscForexTextBox.Text = (_salesOrderHd.DiscForex == 0) ? "0" : _salesOrderHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNPercentTextBox.Text = (_salesOrderHd.PPN == 0) ? "0" : _salesOrderHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_salesOrderHd.PPNForex == 0) ? "0" : _salesOrderHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.ShowActionButton();
            this.ShowPreviewButton();
            //this.ShowReviseButton();

            if (this.StatusHiddenField.Value == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.EditButton.Visible = false;
                this.ImportButton.Visible = false;
            }
            else
            {
                this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.EditButton.Visible = true;
                this.ImportButton.Visible = true;
            }

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._salesOrderBL.GetListMKTSODt(_prmCurrentPage, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _prmRevisiNo);
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            //if (this.ListRepeater.Items.Count > 0)
            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
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

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportSalesBL.SalesOrderPrintPreview(this.TransNoTextBox.Text, 0);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            string _path = _reportListBL.GetPathPrintPreview(AppModule.GetValue(TransactionType.SalesOrder), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path;

            this.ReportViewer1.DataBind();

            String _jobTitleStatus = this._companyConfigBL.GetSingle(CompanyConfigure.ViewJobTitlePrintReport).SetValue;

            ReportParameter[] _reportParam = new ReportParameter[4];
            _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
            _reportParam[1] = new ReportParameter("ViewJobTitlePrintReport", _jobTitleStatus, false);
            _reportParam[2] = new ReportParameter("Revisi", "0", true);
            _reportParam[3] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ImportButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._importPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisiKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            //string[] _date = this.DateTextBox.Text.Split('-');
            //int _year = Convert.ToInt32(_date[0]);
            //int _period = Convert.ToInt32(_date[1]);
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._salesOrderBL.GetAppr(this.TransNoTextBox.Text, 0, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._salesOrderBL.Approve(this.TransNoTextBox.Text, 0, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._salesOrderBL.Posting(this.TransNoTextBox.Text, 0, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._salesOrderBL.Unposting(this.TransNoTextBox.Text, 0, _year, _period, HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result;
            }

            this._prmRevisi = 0;
            this.ShowData(_prmRevisi, 0);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            bool _result = this._salesOrderBL.DeleteMultiMKTSODt(_tempSplit, this.TransNoTextBox.Text, 0);

            this.ClearLabel();

            if (_result == true)
            {
                this.WarningLabel.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this.ViewState[this._currPageKey] = 0;

            this._prmRevisi = 0;

            this.ShowData(_prmRevisi, 0);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MKTSODt _temp = (MKTSODt)e.Item.DataItem;

                string _code = _temp.ProductCode.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                //if (this._isCheckedAll == true && _listCheckbox.Checked == false)
                //{
                //    this._isCheckedAll = false;
                //}

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton2.Visible = false;
                }

                if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    _editButton2.Visible = false;
                }
                else
                {
                    _editButton2.PostBackUrl = this._editDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey));
                    _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }

                //ImageButton _closeButton = (ImageButton)e.Item.FindControl("CloseButton");
                //this._permClose = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Close);

                //if (this._permClose == PermissionLevel.NoAccess)
                //{
                //    _closeButton.Visible = false;
                //}

                //if ((this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower()) && (_temp.DoneClosing.ToString().Trim().ToLower() == SalesOrderDataMapper.GetStatusDetail(SalesOrderStatusDt.Open).ToString().Trim().ToLower()) && _temp.Qty > (((_temp.QtyDO == null) ? 0 : _temp.QtyDO) + ((_temp.QtyClose == null) ? 0 : _temp.QtyClose)))
                //{
                //    _closeButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/close.jpg";
                //    _closeButton.CommandArgument = _code;
                //    _closeButton.CommandName = "CloseButton";
                //    _closeButton.Attributes.Add("OnClick", "return ConfirmFillDescription('" + this.DescriptionHiddenField.ClientID + "', '" + _confirmTitle + "');");
                //}
                //else
                //{
                //    _closeButton.Visible = false;
                //}

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

                Literal _qtyOrderLiteral = (Literal)e.Item.FindControl("QtyOrderLiteral");
                _qtyOrderLiteral.Text = HttpUtility.HtmlEncode((_temp.QtyOrder == 0) ? "0" : _temp.QtyOrder.ToString("#,##0.##"));

                Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                _priceLiteral.Text = HttpUtility.HtmlEncode((_temp.Price == 0) ? "0" : Convert.ToDecimal(_temp.Price).ToString("#,##0.##"));

                Literal _amountLiteral = (Literal)e.Item.FindControl("AmountLiteral");
                _amountLiteral.Text = HttpUtility.HtmlEncode((_temp.Amount == 0) ? "0" : Convert.ToDecimal(_temp.Amount).ToString("#,##0.##"));

                Literal _unitOrderLiteral = (Literal)e.Item.FindControl("UnitOrderLiteral");
                _unitOrderLiteral.Text = HttpUtility.HtmlEncode(_unitBL.GetUnitNameByCode(_temp.UnitOrder));

                decimal _qtyValue = Convert.ToDecimal((_temp.Qty == null) ? 0 : _temp.Qty);
                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode((_qtyValue == 0) ? "0" : _qtyValue.ToString("#,###.##"));

                Literal _unitLiteral = (Literal)e.Item.FindControl("UnitLiteral");
                _unitLiteral.Text = HttpUtility.HtmlEncode(_unitBL.GetUnitNameByCode(_temp.Unit));

                Literal _doneCloseLiteral = (Literal)e.Item.FindControl("DoneClosingLiteral");
                _doneCloseLiteral.Text = SalesOrderDataMapper.GetStatusTextDetail(Convert.ToChar(_temp.DoneClosing));

                decimal _qtyCloseValue = Convert.ToDecimal((_temp.QtyClose == null) ? 0 : _temp.QtyClose);
                Literal _qtyCloseLiteral = (Literal)e.Item.FindControl("QtyCloseLiteral");
                _qtyCloseLiteral.Text = HttpUtility.HtmlEncode((_qtyCloseValue == 0) ? "0" : _qtyCloseValue.ToString("#,###.##"));
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == "CloseButton")
            //{
            //    this.ClearLabel();

            //    string _result = this._salesOrderBL.Closing(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)), e.CommandArgument.ToString(), this.DescriptionHiddenField.Value, HttpContext.Current.User.Identity.Name);

            //    if (_result == "")
            //    {
            //        this.WarningLabel.Text = "Closing Success";
            //    }
            //    else
            //    {
            //        this.WarningLabel.Text = _result;
            //    }

            //    this._prmRevisi = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey));
            //    this.ShowData(_prmRevisi, 0);
            //    this.DescriptionHiddenField.Value = "";
            //}
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this._prmRevisi = 0;
                this.ShowData(_prmRevisi, Convert.ToInt32(e.CommandArgument));
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

            this._prmRevisi = 0;
            this.ShowData(_prmRevisi, _reqPage);
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._salesOrderBL.Unposting(this.TransNoTextBox.Text, 0, _year, _period, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.SalesOrder), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                    this.WarningLabel.Text = _result;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.WarningLabel.Text = _result;
            }
            this._prmRevisi = 0;
            this.ShowData(_prmRevisi, 0);
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }

        //protected void RevisiDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.RevisiDropDownList.SelectedValue, ApplicationConfig.EncryptionKey)));
        //    this._prmRevisi = Convert.ToInt32(this.RevisiDropDownList.SelectedValue);
        //    this.ShowData(_prmRevisi, 0);
        //}

        //protected void ReviseButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.ClearLabel();

        //    string _result = this._salesOrderBL.Revisi(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

        //    if (_result == "")
        //    {
        //        this.Label.Text = "Revisi Successfully created";

        //        _prmRevisi = _salesOrderBL.GetNewRevisiByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

        //        this.ShowRevisiDDL();
        //        this.ShowData(_prmRevisi, 0);
        //    }
        //    else
        //    {
        //        this.Label.Text = _result;

        //        this._prmRevisi = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey));
        //        this.ShowData(_prmRevisi, 0);
        //    }
        //}

        //protected void DeleteHeaderButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
        //    string[] _tempSplit = new string[1];
        //    string _page = "0";
        //    string _error = "";

        //    this.ClearLabel();

        //    if (this.RevisiDropDownList.Items.Count > 1)
        //    {
        //        _tempSplit[0] = _transNo + "-" + this.RevisiDropDownList.SelectedValue;

        //        bool _result = this._salesOrderBL.DeleteMultiMKTSOHd(_tempSplit);

        //        if (_result == true)
        //        {
        //            this.Label.Text = "Delete Success";
        //        }
        //        else
        //        {
        //            this.Label.Text = "Delete Failed";
        //        }

        //        _prmRevisi = _salesOrderBL.GetNewRevisiByCode(_transNo);
        //        this.ShowRevisiDDL();
        //        this.ShowData(_prmRevisi, 0);
        //    }
        //    else
        //    {
        //        _tempSplit[0] = _transNo + "-" + this.RevisiDropDownList.SelectedValue;

        //        bool _result = this._salesOrderBL.DeleteMultiMKTSOHd(_tempSplit);

        //        if (_result == true)
        //        {
        //            _error = "Delete Success";
        //            Response.Redirect(this._homePage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        //        }
        //        else
        //        {
        //            this.Label.Text = "Delete Failed";
        //            _prmRevisi = _salesOrderBL.GetNewRevisiByCode(_transNo);
        //            this.ShowRevisiDDL();
        //            this.ShowData(_prmRevisi, 0);
        //        }
        //    }
        //}

        #region Detail 2

        private double RowCount2()
        {
            double _result = 0;

            _result = this._salesOrderBL.RowsCountMKTSOProduct(this.TransNoTextBox.Text);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
        }

        public void ShowData2(int _prmRevisiNo, Int32 _prmCurrentPage)
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.TempHidden.Value = "";

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater2.DataSource = null;
            }
            else
            {
                if (this.StatusHiddenField.Value == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString())
                {
                    this.ListRepeater2.DataSource = this._salesOrderBL.GetListMKTSOProduct(_prmCurrentPage, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _prmRevisiNo);
                }
            }
            this.ListRepeater2.DataBind();

            //this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            ////if (this.ListRepeater.Items.Count > 0)
            //this.AllCheckBox.Checked = this.IsCheckedAll();

            //this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage2(_prmCurrentPage);
            }

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
        }

        private void ShowPage2(Int32 _prmCurrentPage)
        {
            Int32[] _pageNumber;
            byte _addElement = 0;
            Int32 _pageNumberElement = 0;

            int i = 0;
            decimal min = 0, max = 0;
            double q = this.RowCount2();

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
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater2.DataBind();

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
                this.DataPagerTopRepeater2.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater2.DataBind();
            }
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MKTSOProduct _temp = (MKTSOProduct)e.Item.DataItem;

                string _code = _temp.ProductCode.ToString();
                Int32 _code2 = _temp.ItemID;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code;
                }
                else
                {
                    this.TempHidden.Value += "," + _code;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no2 += 1;
                _noLiteral.Text = _no.ToString();                
                               
                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewProductDetailPage + "?" + this._itemID + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2.ToString(), ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton2");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton2.Visible = false;
                }

                if (this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    _editButton2.Visible = false;
                }
                else
                {
                    _editButton2.PostBackUrl = this._editDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisiKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt("0", ApplicationConfig.EncryptionKey));
                    _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }

                ImageButton _closeButton = (ImageButton)e.Item.FindControl("CloseButton");
                this._permClose = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Close);

                if (this._permClose == PermissionLevel.NoAccess)
                {
                    _closeButton.Visible = false;
                }

                if ((this.StatusHiddenField.Value.Trim().ToLower() == SalesOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower()) && (_temp.DoneClosing.ToString().Trim().ToLower() == SalesOrderDataMapper.GetStatusDetail(SalesOrderStatusDt.Open).ToString().Trim().ToLower()) && _temp.Qty > (((_temp.QtyDO == null) ? 0 : _temp.QtyDO) + ((_temp.QtyClose == null) ? 0 : _temp.QtyClose)))
                {
                    _closeButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/close.jpg";
                    _closeButton.CommandArgument = _code2.ToString();
                    _closeButton.CommandName = "CloseButton";
                    _closeButton.Attributes.Add("OnClick", "return ConfirmFillDescription('" + this.DescriptionHiddenField.ClientID + "', '" + _confirmTitle + "');");
                }
                else
                {
                    _closeButton.Visible = false;
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

                Literal _itemIDLiteral = (Literal)e.Item.FindControl("ItemIDLiteral");
                _itemIDLiteral.Text = HttpUtility.HtmlEncode(_temp.ItemID.ToString());

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(this._productBL.GetProductNameByCode(_temp.ProductCode));

                Literal _productRefLiteral = (Literal)e.Item.FindControl("ProductReferenceLiteral");
                _productRefLiteral.Text = HttpUtility.HtmlEncode(this._productBL.GetProductNameByCode(_temp.ProductCodeReff));

                Literal _qtyOrderLiteral = (Literal)e.Item.FindControl("QtyCloseLiteral");
                _qtyOrderLiteral.Text = HttpUtility.HtmlEncode((_temp.QtyClose == 0) ? "0" : Convert.ToDecimal(_temp.QtyClose).ToString("#,##0.##"));

                decimal _qtyValue = Convert.ToDecimal((_temp.Qty == null) ? 0 : _temp.Qty);
                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode((_qtyValue == 0) ? "0" : _qtyValue.ToString("#,###.##"));

                Literal _unitLiteral = (Literal)e.Item.FindControl("UnitLiteral");
                _unitLiteral.Text = HttpUtility.HtmlEncode(_unitBL.GetUnitNameByCode(_temp.Unit));

                Literal _doneCloseLiteral = (Literal)e.Item.FindControl("DoneClosingLiteral");
                _doneCloseLiteral.Text = SalesOrderDataMapper.GetStatusTextDetail(Convert.ToChar(_temp.DoneClosing));
            }
        }       

        protected void ListRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CloseButton")
            {
                this.ClearLabel();

                string _result = this._salesOrderBL.Closing(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(e.CommandArgument.ToString()), this.DescriptionHiddenField.Value, HttpContext.Current.User.Identity.Name);

                if (_result == "")
                {
                    this.WarningLabel.Text = "Closing Success";
                }
                else
                {
                    this.WarningLabel.Text = _result;
                }

                this._prmRevisi = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey));
                this.ShowData(_prmRevisi, 0);
                this.DescriptionHiddenField.Value = "";
            }
        }

        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this._prmRevisi = 0;
                this.ShowData2(_prmRevisi, Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void DataPagerButton2_Click(object sender, EventArgs e)
        {
            Int32 _reqPage = 0;

            foreach (Control _item in this.DataPagerTopRepeater2.Controls)
            {
                if (((TextBox)_item.Controls[3]).Text != "")
                {
                    _reqPage = Convert.ToInt32(((TextBox)_item.Controls[3]).Text) - 1;

                    if (_reqPage > this.RowCount2())
                    {
                        ((TextBox)_item.Controls[3]).Text = this.RowCount2().ToString();
                        _reqPage = Convert.ToInt32(this.RowCount2()) - 1;
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

            this._prmRevisi = 0;
            this.ShowData2(_prmRevisi, _reqPage);
        }

        #endregion
    }
}
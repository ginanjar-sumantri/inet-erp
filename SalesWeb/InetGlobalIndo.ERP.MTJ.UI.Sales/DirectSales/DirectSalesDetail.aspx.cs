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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales.DirectSales
{
    public partial class DirectSalesDetail : DirectSalesBase
    {
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CustomerBL _custBL = new CustomerBL();
        private UnitBL _unitBL = new UnitBL();
        private TermBL _termBL = new TermBL();
        private DirectSalesBL _directSalesBL = new DirectSalesBL();
        private ReportSalesBL _reportSalesBL = new ReportSalesBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();
        private ReportListBL _reportListBL = new ReportListBL();
        private UserBL _userBL = new UserBL();
        private PaymentBL _paymentBL = new PaymentBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();
        private ReportTourBL _reportTourBL = new ReportTourBL();

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
        private int _nomor = 0;
        //private Boolean _isCheckedAll = true;

        private int _prmRevisi = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _reportPath1 = "DirectSales/JournalEntryPrintPreview.rdlc";
        private string _reportPath2 = "DirectSales/JournalEntryPrintPreviewHomeCurr.rdlc";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";
        private string _imgCreateJurnal = "create_journal.jpg";

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

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                //this.DeleteHeaderButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";


                this.SetButtonPermission();
                this.ClearLabel();
                this.SetAttribute();
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

            _result = this._directSalesBL.RowsCountDirectSalesDt(this.TransNoTextBox.Text);
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

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;
                int _count = _directSalesBL.CekInSALTrDirectSales_SerialNumber(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_count != 0)
                    this.ActionButton.Attributes.Add("OnClick", "javascript:window.open('" + ApplicationConfig.SalesWebAppURL + "DirectSales/PIN.aspx?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "', 'Serial Number', 'width=320,height=300, scrollbars=no, toolbar=no')");

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        //public void ShowPreviewButton()
        //{
        //    this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

        //    if (this._permPrintPreview == PermissionLevel.NoAccess)
        //    {
        //        this.PreviewButton.Visible = false;
        //    }
        //    else
        //    {
        //        if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
        //        {
        //            this.PreviewButton.Visible = false;
        //            this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
        //        }
        //        else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
        //        {
        //            this.PreviewButton.Visible = false;
        //            this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
        //        }
        //        else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
        //        {
        //            this.PreviewButton.Visible = true;
        //            this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
        //        }
        //        else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Posting).ToString().ToLower())
        //        {
        //            this.PreviewButton.Visible = true;
        //            this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
        //        }
        //    }
        //}

        public void ClearLabel()
        {
            this.Label.Text = "";
            this.WarningLabel.Text = "";
        }

        public void ShowData(Int32 _prmCurrentPage)
        {
            this._directSalesBL = new DirectSalesBL();
            SALTrDirectSalesHd _DirectSalesHd = this._directSalesBL.GetSingleDirectSalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _decimalPlace = _currencyBL.GetDecimalPlace(_DirectSalesHd.CurrCode);

            this.TransNoTextBox.Text = _DirectSalesHd.TransNmbr;
            this.FileNmbrTextBox.Text = _DirectSalesHd.FileNo;
            //this.RevisiDropDownList.SelectedValue = _salesOrderHd.Revisi.ToString();
            this.DateTextBox.Text = DateFormMapper.GetValue(_DirectSalesHd.Date);
            //this.StatusHiddenField.Value = _DirectSalesHd.Status.ToString();
            this.StatusLabel.Text = DirectSalesDataMapper.GetStatusText(_DirectSalesHd.Status);
            this.CustTextBox.Text = _custBL.GetNameByCode(_DirectSalesHd.CustCode) + " - " + _DirectSalesHd.CustCode;
            this.RemarkTextBox.Text = _DirectSalesHd.Remark;
            this.CurrCodeTextBox.Text = _DirectSalesHd.CurrCode;
            this.PayTypeTextBox.Text = _paymentBL.GetPaymentName(_DirectSalesHd.PayCode);
            this.ForexRateTextBox.Text = _DirectSalesHd.ForexRate.ToString("#,##0.00");
            this.BaseForexTextBox.Text = _DirectSalesHd.BaseForex.ToString("#,##0.00");
            this.DiscAmountTextBox.Text = _DirectSalesHd.DiscAmount.ToString("#,##0.00");
            this.PPNAmountTextBox.Text = _DirectSalesHd.PPNAmount.ToString("#,##0.00");
            this.DiscPercentTextBox.Text = Convert.ToDecimal(_DirectSalesHd.DiscPercent).ToString("#,##0.00");
            this.PPNPercentTextBox.Text = Convert.ToDecimal(_DirectSalesHd.PPNPercent).ToString("#,##0.00");
            this.StampFeeTextBox.Text = Convert.ToDecimal(_DirectSalesHd.StampFee).ToString("#,##0.00");
            this.OtherFeeTextBox.Text = Convert.ToDecimal(_DirectSalesHd.OtherFee).ToString("#,##0.00");
            this.TotalAmountTextBox.Text = _DirectSalesHd.TotalAmount.ToString("#,##0.00");
            this.StatusHiddenField.Value = _DirectSalesHd.Status.ToString();

            this.ShowActionButton();
            this.ShowPreviewButton();
            //this.ShowReviseButton();

            if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
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
                this.ListRepeater.DataSource = this._directSalesBL.GetListDirectSalesDt(_prmCurrentPage, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
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

            this.ShowCreateJurnalButton();
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
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

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
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

            if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._directSalesBL.GetAppr(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._directSalesBL.Approve(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._directSalesBL.Posting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._directSalesBL.Unposting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result;
            }

            //this._prmRevisi = Convert.ToInt32(0);
            this.ShowData(0);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeCurrKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.CurrCodeTextBox.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            bool _result = this._directSalesBL.DeleteMultiDirectSalesDt(_tempSplit, this.TransNoTextBox.Text);

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

            this.ShowData(0);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SALTrDirectSalesDt _temp = (SALTrDirectSalesDt)e.Item.DataItem;

                string _transNmbr = _temp.TransNmbr;
                string _code = _temp.ProductCode.ToString();
                string _wrhsCode = _temp.WrhsCode;
                string _wrhsLocation = _temp.WLocationCode;
                string _all = _transNmbr + "|" + _code + "|" + _wrhsCode + "|" + _wrhsLocation;

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
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_code);

                //if (this._isCheckedAll == true && _listCheckbox.Checked == false)
                //{
                //    this._isCheckedAll = false;
                //}

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._Warehouse + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_wrhsCode, ApplicationConfig.EncryptionKey)) + "&" + this._Location + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_wrhsLocation, ApplicationConfig.EncryptionKey));
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

                if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
                {
                    _editButton2.Visible = false;
                }
                else
                {
                    _editButton2.PostBackUrl = this._editDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._Warehouse + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_wrhsCode, ApplicationConfig.EncryptionKey)) + "&" + this._Location + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_wrhsLocation, ApplicationConfig.EncryptionKey));
                    _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }

                ImageButton _closeButton = (ImageButton)e.Item.FindControl("CloseButton");
                this._permClose = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Close);

                if (this._permClose == PermissionLevel.NoAccess)
                {
                    _closeButton.Visible = false;
                }

                if ((this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower()))
                {
                    _closeButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/close.jpg";
                    _closeButton.CommandArgument = _code;
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

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductName);

                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode(_temp.Qty.ToString("#,##0"));

                Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                _priceLiteral.Text = HttpUtility.HtmlEncode(_temp.Price.ToString("#,##0.00"));

                //decimal _qtyValue = Convert.ToDecimal((_temp.Qty == null) ? 0 : _temp.Qty);
                Literal _amountLiteral = (Literal)e.Item.FindControl("AmountLiteral");
                _amountLiteral.Text = HttpUtility.HtmlEncode(_temp.Amount.ToString("#,##0.00"));

                Literal _wareHouseLiteral = (Literal)e.Item.FindControl("WareHouseLiteral");
                _wareHouseLiteral.Text = HttpUtility.HtmlEncode(_temp.WrhsName);

                Literal _wareHouseSubLedLiteral = (Literal)e.Item.FindControl("WareHouseSubLedLiteral");
                _wareHouseSubLedLiteral.Text = HttpUtility.HtmlEncode(_temp.WrhsSubledName);

                Literal _WareHouseLocationLiteral = (Literal)e.Item.FindControl("WareHouseLocationLiteral");
                _WareHouseLocationLiteral.Text = HttpUtility.HtmlEncode(_temp.WLocationName);
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == "CloseButton")
            //{
            //    this.ClearLabel();

            //    string _result = this._directSalesBL.Closing(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisiKey), ApplicationConfig.EncryptionKey)), e.CommandArgument.ToString(), this.DescriptionHiddenField.Value, HttpContext.Current.User.Identity.Name);

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

        public void ShowPreviewButton()
        {
            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == DirectSalesDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
            }
        }

        public void ShowCreateJurnalButton()
        {
            this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

            if (this._permPosting == PermissionLevel.NoAccess)
            {
                this.CreateJurnalImageButton.Visible = false;
            }
            else
            {
                this.CreateJurnalImageButton.Visible = false;
                this.CreateJurnalImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgCreateJurnal;

                if (this.StatusHiddenField.Value.Trim().ToLower() == POSTicketingDataMapper.GetStatusByte(TransStatus.Posted).ToString().ToLower())
                {
                    this.CreateJurnalImageButton.Visible = true;
                }
            }
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            String _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportSalesBL.DirectSalesPrintPreview(this.TransNoTextBox.Text);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            string _path = _reportListBL.GetPathPrintPreview(AppModule.GetValue(TransactionType.DirectSales), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path;

            this.ReportViewer1.DataBind();

            String _jobTitleStatus = this._companyConfigBL.GetSingle(CompanyConfigure.ViewJobTitlePrintReport).SetValue;

            string _address = this._directSalesBL.GetValueForReport("DirectSalesPrintPreview", "Header_Address1");
            string _body1 = this._directSalesBL.GetValueForReport("DirectSalesPrintPreview", "Body_Note1");
            string _body2 = this._directSalesBL.GetValueForReport("DirectSalesPrintPreview", "Body_Note2");
            string _body3 = this._directSalesBL.GetValueForReport("DirectSalesPrintPreview", "Body_Note3");
            string _footer = this._directSalesBL.GetValueForReport("DirectSalesPrintPreview", "Footer_Note1");

            SALTrDirectSalesHd _sALTrDirectSalesHd = this._directSalesBL.GetSingleDirectSalesHd(this.TransNoTextBox.Text);
            String _fileNo = _sALTrDirectSalesHd.FileNo;
            DateTime _date = _sALTrDirectSalesHd.Date;
            String _currCode = _sALTrDirectSalesHd.CurrCode;
            Decimal _forexRate = _sALTrDirectSalesHd.ForexRate;
            Decimal _baseForex = _sALTrDirectSalesHd.BaseForex;
            Decimal _discAmount = _sALTrDirectSalesHd.DiscAmount;
            Decimal _pPNAmount = _sALTrDirectSalesHd.PPNAmount;
            Decimal? _stampFee = _sALTrDirectSalesHd.StampFee;
            Decimal? _otherFee = _sALTrDirectSalesHd.OtherFee;
            Decimal _totalAmount = _sALTrDirectSalesHd.TotalAmount;
            String _editBy = _sALTrDirectSalesHd.EditBy;
            String _custCode = _sALTrDirectSalesHd.CustCode;

            MsCustomer _msCustomer = this._custBL.GetSingleCust(_custCode);
            String _custName = _msCustomer.CustName;
            String _address1 = _msCustomer.Address1;

            if (_path == "DirectSales/DirectSalesPrintPreviewCompanyAddressStandard.rdlc" | _path == "DirectSales/DirectSalesPrintPreviewCompanyAddressStandardHalfPage.rdlc")
            {
                ReportParameter[] _reportParam = new ReportParameter[20];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                _reportParam[2] = new ReportParameter("Header_Address1", _address, true);
                _reportParam[3] = new ReportParameter("Body_Note1", _body1, true);
                _reportParam[4] = new ReportParameter("Body_Note2", _body2, true);
                _reportParam[5] = new ReportParameter("Body_Note3", _body3, true);
                _reportParam[6] = new ReportParameter("Footer_Note1", _footer, true);
                _reportParam[7] = new ReportParameter("CustName", _custName, true);
                _reportParam[8] = new ReportParameter("Address1", _address1, true);
                _reportParam[9] = new ReportParameter("FileNo", _fileNo, true);
                _reportParam[10] = new ReportParameter("Date", _date.ToString(), true);
                _reportParam[11] = new ReportParameter("CurrCode", _currCode, true);
                _reportParam[12] = new ReportParameter("ForexRate", _forexRate.ToString(), true);
                _reportParam[13] = new ReportParameter("BaseForex", _baseForex.ToString(), true);
                _reportParam[14] = new ReportParameter("DiscAmount", _discAmount.ToString(), true);
                _reportParam[15] = new ReportParameter("PPNAmount", _pPNAmount.ToString(), true);
                _reportParam[16] = new ReportParameter("StampFee", _stampFee.ToString(), true);
                _reportParam[17] = new ReportParameter("OtherFee", _otherFee.ToString(), true);
                _reportParam[18] = new ReportParameter("TotalAmount", _totalAmount.ToString(), true);
                _reportParam[19] = new ReportParameter("EditBy", _editBy, true);
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            }
            else if (_path == "DirectSales/DirectSalesPrintPreviewCompanyAddressStocker.rdlc" | _path == "DirectSales/DirectSalesPrintPreviewCompanyAddressStockerHalfPage.rdlc")
            {
                ReportParameter[] _reportParam = new ReportParameter[17];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                _reportParam[2] = new ReportParameter("Header_Address1", _address, true);
                _reportParam[3] = new ReportParameter("Footer_Note1", _footer, true);
                _reportParam[4] = new ReportParameter("CustName", _custName, true);
                _reportParam[5] = new ReportParameter("Address1", _address1, true);
                _reportParam[6] = new ReportParameter("FileNo", _fileNo, true);
                _reportParam[7] = new ReportParameter("Date", _date.ToString(), true);
                _reportParam[8] = new ReportParameter("CurrCode", _currCode, true);
                _reportParam[9] = new ReportParameter("ForexRate", _forexRate.ToString(), true);
                _reportParam[10] = new ReportParameter("BaseForex", _baseForex.ToString(), true);
                _reportParam[11] = new ReportParameter("DiscAmount", _discAmount.ToString(), true);
                _reportParam[12] = new ReportParameter("PPNAmount", _pPNAmount.ToString(), true);
                _reportParam[13] = new ReportParameter("StampFee", _stampFee.ToString(), true);
                _reportParam[14] = new ReportParameter("OtherFee", _otherFee.ToString(), true);
                _reportParam[15] = new ReportParameter("TotalAmount", _totalAmount.ToString(), true);
                _reportParam[16] = new ReportParameter("EditBy", _editBy, true);
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            }
            else if (_path == "DirectSales/DirectSalesPrintPreviewNoCompanyAddressStandard.rdlc" | _path == "DirectSales/DirectSalesPrintPreviewNoCompanyAddressStandardHalfPage.rdlc")
            {
                ReportParameter[] _reportParam = new ReportParameter[19];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                _reportParam[2] = new ReportParameter("Body_Note1", _body1, true);
                _reportParam[3] = new ReportParameter("Body_Note2", _body2, true);
                _reportParam[4] = new ReportParameter("Body_Note3", _body3, true);
                _reportParam[5] = new ReportParameter("Footer_Note1", _footer, true);
                _reportParam[6] = new ReportParameter("CustName", _custName, true);
                _reportParam[7] = new ReportParameter("Address1", _address1, true);
                _reportParam[8] = new ReportParameter("FileNo", _fileNo, true);
                _reportParam[9] = new ReportParameter("Date", _date.ToString(), true);
                _reportParam[10] = new ReportParameter("CurrCode", _currCode, true);
                _reportParam[11] = new ReportParameter("ForexRate", _forexRate.ToString(), true);
                _reportParam[12] = new ReportParameter("BaseForex", _baseForex.ToString(), true);
                _reportParam[13] = new ReportParameter("DiscAmount", _discAmount.ToString(), true);
                _reportParam[14] = new ReportParameter("PPNAmount", _pPNAmount.ToString(), true);
                _reportParam[15] = new ReportParameter("StampFee", _stampFee.ToString(), true);
                _reportParam[16] = new ReportParameter("OtherFee", _otherFee.ToString(), true);
                _reportParam[17] = new ReportParameter("TotalAmount", _totalAmount.ToString(), true);
                _reportParam[18] = new ReportParameter("EditBy", _editBy, true);
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            } 
            else if (_path == "DirectSales/DirectSalesPrintPreviewNoCompanyAddressStocker.rdlc" | _path == "DirectSales/DirectSalesPrintPreviewNoCompanyAddressStockerHalfPage.rdlc")
            {
                ReportParameter[] _reportParam = new ReportParameter[16];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                _reportParam[2] = new ReportParameter("Footer_Note1", _footer, true);
                _reportParam[3] = new ReportParameter("CustName", _custName, true);
                _reportParam[4] = new ReportParameter("Address1", _address1, true);
                _reportParam[5] = new ReportParameter("FileNo", _fileNo, true);
                _reportParam[6] = new ReportParameter("Date", _date.ToString(), true);
                _reportParam[7] = new ReportParameter("CurrCode", _currCode, true);
                _reportParam[8] = new ReportParameter("ForexRate", _forexRate.ToString(), true);
                _reportParam[9] = new ReportParameter("BaseForex", _baseForex.ToString(), true);
                _reportParam[10] = new ReportParameter("DiscAmount", _discAmount.ToString(), true);
                _reportParam[11] = new ReportParameter("PPNAmount", _pPNAmount.ToString(), true);
                _reportParam[12] = new ReportParameter("StampFee", _stampFee.ToString(), true);
                _reportParam[13] = new ReportParameter("OtherFee", _otherFee.ToString(), true);
                _reportParam[14] = new ReportParameter("TotalAmount", _totalAmount.ToString(), true);
                _reportParam[15] = new ReportParameter("EditBy", _editBy, true);
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            }
        }

        protected void CreateJurnalImageButton_Click(object sender, ImageClickEventArgs e)
        {

            if (this.CreateJurnalDDL.SelectedValue == "1")
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.Panel4.Visible = false;
                this.Panel3.Visible = true;

                this.ReportViewer2.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource1 = this._reportTourBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer2.LocalReport.DataSources.Clear();
                this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                this.ReportViewer2.DataBind();
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                this.ReportViewer2.LocalReport.Refresh();
            }
            else if (this.CreateJurnalDDL.SelectedValue == "2")
            {
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.Panel4.Visible = true;
                this.Panel3.Visible = false;

                this.ReportViewer3.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource2 = this._reportTourBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer3.LocalReport.DataSources.Clear();
                this.ReportViewer3.LocalReport.DataSources.Add(_reportDataSource2);
                this.ReportViewer3.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                this.ReportViewer3.DataBind();
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                this.ReportViewer3.LocalReport.SetParameters(_reportParam);
                this.ReportViewer3.LocalReport.Refresh();
            }
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._directSalesBL.Unposting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.DirectSales), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                    this.Label.Text = _result;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.Label.Text = _result;
            }

            this.ShowData(0);
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}
using System;
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
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Linq;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Tour;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerNote
{
    public partial class CustomerNoteDetail : CustomerNoteBase
    {
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private CustomerNoteBL _customerNoteBL = new CustomerNoteBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private ReportFinanceBL _reportFinanceBL = new ReportFinanceBL();
        private SalesOrderBL _salesorderBL = new SalesOrderBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();
        private ReportTourBL _reportTourBL = new ReportTourBL();

        private int?[] _navMark = { null, null, null, null };
        private bool _flag = true;
        private bool _nextFlag = false;
        private bool _lastFlag = false;

        private int?[] _navMark2 = { null, null, null, null };
        private bool _flag2 = true;
        private bool _nextFlag2 = false;
        private bool _lastFlag2 = false;

        private string _currPageKey = "CurrentPage";
        private string _currPageKey2 = "CurrentPage2";

        private int _page;
        private int _maxrow = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no = 0;
        private int _nomor = 0;

        private int _page2;
        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no2 = 0;
        private int _nomor2 = 0;
        private string _reportPath1 = "CustomerNote/CustomerInvoice.rdlc";
        private string _reportPath2 = "CustomerNote/JournalEntryPrintPreview.rdlc";
        private string _reportPath3 = "CustomerNote/JournalEntryPrintPreviewHomeCurr.rdlc";
        

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgPreview = "preview.jpg";
        private string _imgCreateJurnal = "view_journal.jpg";

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

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.GenerateDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/generate.jpg";

                this.ClearLabel();
                this.ShowData();
            
                this.SetButtonPermission();
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
                this.AddButton2.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.Label2.Text = "";
            this.WarningLabel.Text = "";
            this.Label1.Text = "";
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        public void ShowData()
        {
            _customerNoteBL = new CustomerNoteBL();
            FINCustInvHd _finCustInvHd = this._customerNoteBL.GetSingleFINCustInvHdView(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finCustInvHd.CurrCode);

            this.TransNoTextBox.Text = _finCustInvHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finCustInvHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finCustInvHd.TransDate);
            this.StatusLabel.Text = CustomerNoteDataMapper.GetStatusText(_finCustInvHd.Status);
            this.CustTextBox.Text = _finCustInvHd.CustName;
            this.CurrCodeTextBox.Text = _finCustInvHd.CurrCode;
            this.CurrRateTextBox.Text = (_finCustInvHd.ForexRate == 0) ? "0" : _finCustInvHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TermTextBox.Text = _finCustInvHd.TermName;
            this.CurrTextBox.Text = _finCustInvHd.CurrCode;
            this.StatusLabel.Text = CustomerNoteDataMapper.GetStatusText(_finCustInvHd.Status);
            this.StatusHiddenField.Value = _finCustInvHd.Status.ToString();
            this.AttnTextBox.Text = _finCustInvHd.Attn;
            this.RemarkTextBox.Text = _finCustInvHd.Remark;
            if (_finCustInvHd.PPNRate != null)
            {
                decimal _ppnRate = Convert.ToDecimal(_finCustInvHd.PPNRate);
                this.PPNRateTextBox.Text = (_ppnRate == 0) ? "0" : _ppnRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finCustInvHd.PPN != null)
            {
                decimal _ppn = Convert.ToDecimal(_finCustInvHd.PPN);
                this.PPNPercentTextBox.Text = (_ppn == 0) ? "0" : _ppn.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            this.PPNNoTextBox.Text = _finCustInvHd.PPNNo;
            if (_finCustInvHd.PPNForex != null)
            {
                decimal _ppnForex = Convert.ToDecimal(_finCustInvHd.PPNForex);
                this.PPNForexTextBox.Text = (_ppnForex == 0) ? "0" : _ppnForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finCustInvHd.PPNDate != null)
            {
                this.PPNDateTextBox.Text = DateFormMapper.GetValue((DateTime)_finCustInvHd.PPNDate);
            }
            if (_finCustInvHd.BaseForex != null)
            {
                decimal _amountBase = Convert.ToDecimal(_finCustInvHd.BaseForex);
                this.AmountBaseTextBox.Text = (_amountBase == 0) ? "0" : _amountBase.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finCustInvHd.DiscForex != null)
            {
                decimal _discForex = Convert.ToDecimal(_finCustInvHd.DiscForex);
                this.DiscForexTextBox.Text = (_discForex == 0) ? "0" : _discForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finCustInvHd.OtherForex != null)
            {
                decimal _otherForex = Convert.ToDecimal(_finCustInvHd.OtherForex);
                this.OtherForexTextBox.Text = (_otherForex == 0) ? "0" : _otherForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            if (_finCustInvHd.TotalForex != null)
            {
                decimal _totalForex = Convert.ToDecimal(_finCustInvHd.TotalForex);
                this.TotalForexTextBox.Text = (_totalForex == 0) ? "0" : _totalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }

            this.BillToTextBox.Text = _finCustInvHd.BillToName;

            this.ShowActionButton();

            if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.AddButton2.Visible = false;
                this.DeleteButton.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.AddButton2.Visible = true;
                this.DeleteButton.Visible = true;
                this.EditButton.Visible = true;
            }

            this.ShowDetail(0);
            this.ShowDetail2(0);

            this.ShowActionButton();
            this.ShowPreviewButton();

            this.ShowCreateJurnalButton();
            this.Panel1.Visible = true;
            this.Panel2.Visible = false;
            this.Panel3.Visible = true;
            this.Panel4.Visible = false;
            this.Panel5.Visible = false;

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.Label2.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }
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
            }
            else
            {
                this.DataPagerTopRepeater.DataSource = from _query in _pageNumber
                                                       select _query;
                this.DataPagerTopRepeater.DataBind();
            }
        }

        //public void ShowDataDetail(Int32 _prmCurrentPage)
        //{
        //    this.TempHidden.Value = "";

        //    NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

        //    this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

        //    if (this._permView == PermissionLevel.NoAccess)
        //    {
        //        this.ListRepeater.DataSource = null;
        //    }
        //    else
        //    {
        //        this.ListRepeater.DataSource = this._customerNoteBL.GetListFINCustInvDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //    }
        //    this.ListRepeater.DataBind();

        //    this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

        //    this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

        //    if (this._permView != PermissionLevel.NoAccess)
        //    {
        //        this.ShowPage(_prmCurrentPage);
        //    }
        //}

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
            if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._customerNoteBL.GetAppr(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._customerNoteBL.Approve(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._customerNoteBL.Posting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._customerNoteBL.Unposting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _year, _period, HttpContext.Current.User.Identity.Name);

                //this.WarningLabel.Text = _result;
            }

            this.ShowData();
            this.ShowDetail(0);
        }

        protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');
            string _error = "";
            string _page = "0";

            this.ClearLabel();

            bool _result = this._customerNoteBL.DeleteMultiFINCustInvDt(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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

            Response.Redirect(this._detailPage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FINCustInvDt _temp = (FINCustInvDt)e.Item.DataItem;

                string _code = _temp.SJNo;
                string _code2 = _temp.ProductCode;
                string _all = _code2 + "-" + _code;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _all;
                }
                else
                {
                    this.TempHidden.Value += "," + _all;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //string _sjNumber = this._codeItem + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //string _transNmbr = this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey));
                //string _productCode = this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey));
                //_viewButton.PostBackUrl = this._viewDetailPage + "?" + _sjNumber + "&" + _productCode + "&" + _transNmbr;
                //_viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                //if (this._permView == PermissionLevel.NoAccess)
                //{
                //    _viewButton.Visible = false;
                //}

                ImageButton _editButton2 = (ImageButton)e.Item.FindControl("EditButton");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton2.Visible = false;
                }

                if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    _editButton2.Visible = false;
                }
                else
                {
                    _editButton2.PostBackUrl = this._editDetailPage + "?" + this._codeItem + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey));
                    _editButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
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

                Literal _sjNoLiteral = (Literal)e.Item.FindControl("SJNoLiteral");
                string SjFileNo = this._billOfLadingBL.GetFileNmbrFromSTCSJHd(_temp.SJNo);
                _sjNoLiteral.Text = HttpUtility.HtmlEncode(SjFileNo);

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductName);

                Literal _soNoLiteral = (Literal)e.Item.FindControl("SONoLiteral");
                string SoFileNo = this._salesorderBL.GetFileNmbrMKTSOHd(_temp.SONo);
                _soNoLiteral.Text = HttpUtility.HtmlEncode(SoFileNo);

                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode((_temp.Qty == 0) ? "0" : _temp.Qty.ToString("#,###.##"));

                FINCustInvHd _finCustInvHd = this._customerNoteBL.GetSingleFINCustInvHdView(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_finCustInvHd.CurrCode);

                Literal _unitLiteral = (Literal)e.Item.FindControl("UnitLiteral");
                _unitLiteral.Text = HttpUtility.HtmlEncode(_temp.UnitName);

                Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                _priceLiteral.Text = (_temp.PriceForex == 0 ? "0" : HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.PriceForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace))));

                Literal _amountLiteral = (Literal)e.Item.FindControl("AmountLiteral");
                _amountLiteral.Text = (_temp.AmountForex == 0 ? "0" : HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace))));
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

        protected void DataPagerTopRepeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager2")
            {
                this.ViewState[this._currPageKey2] = Convert.ToInt32(e.CommandArgument);

                this.ShowDetail2(Convert.ToInt32(e.CommandArgument));
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

        protected void DataPagerTopRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                int _pageNumber = (int)e.Item.DataItem;

                NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

                if (Convert.ToInt32(this.ViewState[this._currPageKey]) == _pageNumber)
                {
                    TextBox _pageNumberTextbox = (TextBox)e.Item.FindControl("PageNumberTextBox2");

                    _pageNumberTextbox.Text = (_pageNumber + 1).ToString();
                    _pageNumberTextbox.MaxLength = 9;
                    _pageNumberTextbox.Visible = true;

                    _pageNumberTextbox.Attributes.Add("style", "text-align: center;");
                    _pageNumberTextbox.Attributes.Add("OnKeyDown", "return NumericWithEnter();");
                }
                else
                {
                    LinkButton _pageNumberLinkButton = (LinkButton)e.Item.FindControl("PageNumberLinkButton2");
                    _pageNumberLinkButton.CommandName = "DataPager2";
                    _pageNumberLinkButton.CommandArgument = _pageNumber.ToString();

                    if (_pageNumber == this._navMark2[0])
                    {
                        _pageNumberLinkButton.Text = "First";
                        this._navMark2[0] = null;
                    }
                    else if (_pageNumber == this._navMark2[1])
                    {
                        _pageNumberLinkButton.Text = "Prev";
                        this._navMark2[1] = null;
                    }
                    else if (_pageNumber == this._navMark2[2] && this._flag2 == false)
                    {
                        _pageNumberLinkButton.Text = "Next";
                        this._navMark2[2] = null;
                        this._nextFlag2 = true;
                        this._flag2 = true;
                    }
                    else if (_pageNumber == this._navMark2[3] && this._flag2 == true && this._nextFlag2 == true)
                    {
                        _pageNumberLinkButton.Text = "Last";
                        this._navMark2[3] = null;
                        this._lastFlag2 = true;
                    }
                    else
                    {
                        if (this._lastFlag2 == false)
                        {
                            _pageNumberLinkButton.Text = (_pageNumber + 1).ToString();
                        }

                        if (_pageNumber == this._navMark2[2] && this._flag2 == true)
                            this._flag2 = false;
                    }
                }
            }
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._customerNoteBL.RowsCountFINCustInvDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
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

        private Boolean IsCheckedAll2()
        {
            Boolean _result = true;

            foreach (RepeaterItem _row in this.ListRepeater2.Items)
            {
                CheckBox _chk = (CheckBox)_row.FindControl("ListCheckBox2");

                if (_chk.Checked == false)
                {
                    _result = false;
                    break;
                }
            }

            if (this.ListRepeater2.Items.Count == 0)
            {
                _result = false;
            }

            return _result;
        }

        public void ShowDetail(Int32 _prmCurrentPage)
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
                this.ListRepeater.DataSource = this._customerNoteBL.GetListFINCustInvDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "'  ,'" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        public void ShowDetail2(Int32 _prmCurrentPage2)
        {
            this.TempHidden2.Value = "";

            this._page2 = _prmCurrentPage2;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater2.DataSource = null;
            }
            else
            {
                this.ListRepeater2.DataSource = this._customerNoteBL.GetListFINCustInvSJList(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "'  ,'" + _akhir2 + "', '" + _tempHidden2 + "' );");

            this.AllCheckBox2.Checked = this.IsCheckedAll2();

            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.Label1.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
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

        private Boolean IsChecked2(String _prmValue)
        {
            Boolean _result = false;
            String[] _value = this.CheckHidden2.Value.Split(',');

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

        public void ShowPreviewButton()
        {
            this._permPrintPreview = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.PrintPreview);

            if (this._permPrintPreview == PermissionLevel.NoAccess)
            {
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                    this.PreviewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPreview;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == CustomerNoteDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
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

                if (this.StatusHiddenField.Value.Trim().ToLower() == POSTicketingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.CreateJurnalImageButton.Visible = true;
                }
            }
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._customerNoteBL.DeleteMultiFINCustInvSJList(_tempSplit);

            if (_result == true)
            {
                this.Label2.Text = "Delete Success";
            }
            else
            {
                this.Label2.Text = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;

            this.ShowData();
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                FINCustInvSJList _temp = (FINCustInvSJList)e.Item.DataItem;

                string _code1 = _temp.TransNmbr.ToString();
                string _code2 = _temp.SJNo.ToString();
                string _all = _code1 + "-" + _code2;

                if (this.TempHidden2.Value == "")
                {
                    this.TempHidden2.Value = _all;
                }
                else
                {
                    this.TempHidden2.Value += "," + _all;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral2");
                _no2 = _page2 * _maxrow2;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral.Text = _no2.ToString();
                _nomor2 += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox2");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "', 'false')");
                _listCheckbox.Checked = this.IsChecked2(_all);

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton2");
                //_viewButton.PostBackUrl = this._viewDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code1, ApplicationConfig.EncryptionKey)) + "&" + this._wrhsKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey)) + "&" + this._subledKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code3, ApplicationConfig.EncryptionKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code4, ApplicationConfig.EncryptionKey));
                //_viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                //if (this._permView == PermissionLevel.NoAccess)
                //{
                //    _viewButton.Visible = false;
                //}

                //ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton2");
                //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                //if (this._permEdit == PermissionLevel.NoAccess)
                //{
                //    _editButton.Visible = false;
                //}
                //else
                //{
                //    if (this.StatusHiddenField.Value != CustomerNoteDataMapper.GetStatus(TransStatus.Posted).ToString())
                //    {
                //        _editButton.PostBackUrl = this._editDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code1, ApplicationConfig.EncryptionKey)) + "&" + this._wrhsKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey)) + "&" + this._subledKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code3, ApplicationConfig.EncryptionKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code4, ApplicationConfig.EncryptionKey));
                //        _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                //    }
                //    else
                //    {
                //        _editButton.Visible = false;
                //    }
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

                Literal _RRNoLiteral = (Literal)e.Item.FindControl("SJNoLiteral");
                _RRNoLiteral.Text = HttpUtility.HtmlEncode(_temp.SJNoFileNmbr);

                Literal _custLiteral = (Literal)e.Item.FindControl("CustomerLiteral");
                _custLiteral.Text = HttpUtility.HtmlEncode(_temp.CustName);

                Literal _poNoLiteral = (Literal)e.Item.FindControl("SONoLiteral");
                _poNoLiteral.Text = HttpUtility.HtmlEncode(_temp.SONo);

                Literal _remarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
                _remarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);
            }
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportFinanceBL.CustomerNotePrintPreview(this.TransNoTextBox.Text);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
            this.ReportViewer1.DataBind();

            ReportParameter[] _reportParam = new ReportParameter[2];
            _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
            _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void GenerateDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            bool _result = this._customerNoteBL.GenerateFINCustInvDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                this.WarningLabel.Text = "Generate Success";
                this.ShowData();
                this._no = 0;
                this._nomor = 0;
                this._no2 = 0;
                this._nomor2 = 0;
                this.ShowDetail(0);
                this.ShowDetail2(0);
            }
            else
            {
                this.WarningLabel.Text = "Generate Failed";
            }
        }

        protected void CreateJurnalImageButton_Click(object sender, ImageClickEventArgs e)
        {

            if (this.CreateJurnalDDL.SelectedValue == "1")
            {
                this.Panel1.Visible = false;
                this.Panel3.Visible = false;
                this.Panel4.Visible = true;
                this.Panel5.Visible = false;

                this.ReportViewer2.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource1 = this._reportTourBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer2.LocalReport.DataSources.Clear();
                this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
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
                this.Panel3.Visible = false;
                this.Panel4.Visible = false;
                this.Panel5.Visible = true;

                this.ReportViewer3.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                ReportDataSource _reportDataSource2 = this._reportTourBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                this.ReportViewer3.LocalReport.DataSources.Clear();
                this.ReportViewer3.LocalReport.DataSources.Add(_reportDataSource2);
                this.ReportViewer3.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath3;
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

            string _result = this._customerNoteBL.Unposting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _year, _period, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.CustomerNote), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
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

            this.ShowData();
            this.ShowDetail(0);
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}
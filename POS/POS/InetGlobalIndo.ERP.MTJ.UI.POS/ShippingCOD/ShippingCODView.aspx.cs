using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using Microsoft.Reporting.WebForms;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.ShippingCOD
{
    public partial class ShippingCODView : ShippingCODBase
    {
        private VendorBL _vendorBL = new VendorBL();
        private PermissionBL _permBL = new PermissionBL();
        private CountryBL _countryBL = new CountryBL();
        private CityBL _cityBL = new CityBL();
        private BankBL _bankBL = new BankBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private TermBL _termBL = new TermBL();
        private ShippingBL _shippingBL = new ShippingBL();
        private UserBL _userBL = new UserBL();


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
        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        //private string _imgGetApproval = "get_approval.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        //private string _imgApprove = "approve.jpg";
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

            if (!this.Page.IsPostBack == true)
            {

                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                //this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                //this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/Delete.jpg";

                this.SetAttribute();
                this.SetButtonPermission();
                this.ShowData();
                this.ShowActionButton();
            }
        }

        private void SetAttribute()
        {
            this.TransNmbrTextBox.Attributes.Add("ReadOnly", "True");
            this.FileNmbrTextBox.Attributes.Add("ReadOnly", "True");
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.ReferenceNoTextBox.Attributes.Add("ReadOnly", "True");
            this.OperatorTextBox.Attributes.Add("ReadOnly", "True");
            this.CustomerTextBox.Attributes.Add("ReadOnly", "True");
            this.SenderTextBox.Attributes.Add("ReadOnly", "True");
            this.DeliverTextBox.Attributes.Add("ReadOnly", "True");
            this.SubtotalTextBox.Attributes.Add("ReadOnly", "True");
            this.DiscountTextBox.Attributes.Add("ReadOnly", "True");
            this.PPNTextBox.Attributes.Add("ReadOnly", "True");
            this.OtherForexTextBox.Attributes.Add("ReadOnly", "True");
            this.ServiceChargeTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalForexTextBox.Attributes.Add("ReadOnly", "True");
            this.FakturPajakTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("ReadOnly", "True");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetButtonPermission()
        {
            //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            //if (this._permEdit == PermissionLevel.NoAccess)
            //{
            //    this.EditButton.Visible = false;
            //}

            //this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            //if (this._permAdd == PermissionLevel.NoAccess)
            //{
            //    this.AddButton.Visible = false;
            //}

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
            }
        }

        public void ShowData()
        {
            Int32 _reqPage = 0;
            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            POSTrShippingHd _posTrShippingHd = this._shippingBL.GetSinglePOSTrShippingHd(_tempSplit[0].ToString());
            this.TransNmbrTextBox.Text = _posTrShippingHd.TransNmbr.ToString();
            this.FileNmbrTextBox.Text = (_posTrShippingHd.FileNmbr == null) ? "" : _posTrShippingHd.FileNmbr.ToString();
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_posTrShippingHd.TransDate);
            this.ReferenceNoTextBox.Text = _posTrShippingHd.ReferenceNo.ToString();
            this.OperatorTextBox.Text = _posTrShippingHd.OperatorID;
            this.CustomerTextBox.Text = (_posTrShippingHd.MemberID == "" ? "" : _posTrShippingHd.MemberID + " - ") + _posTrShippingHd.CustName + " - " + _posTrShippingHd.CustPhone;
            this.SenderTextBox.Text = _posTrShippingHd.SenderName + " - " + (_posTrShippingHd.SenderHandphone == "" ? "" : _posTrShippingHd.SenderHandphone + " - ") + (_posTrShippingHd.SenderTelephone == "" ? "" : _posTrShippingHd.SenderTelephone + " - ") + (_posTrShippingHd.SenderAddress == "" ? "" : _posTrShippingHd.SenderAddress + " - ") + this._cityBL.GetCityNameByCode(_posTrShippingHd.SenderCityCode) + " " + _posTrShippingHd.SenderPostalCode;
            this.DeliverTextBox.Text = _posTrShippingHd.DeliverName + " - " + (_posTrShippingHd.DeliverHandphone == "" ? "" : _posTrShippingHd.DeliverHandphone + " - ") + (_posTrShippingHd.DeliverTelephone == "" ? "" : _posTrShippingHd.DeliverTelephone + " - ") + (_posTrShippingHd.DeliverAddress == "" ? "" : _posTrShippingHd.DeliverAddress + " - ") + this._countryBL.GetCountryNameByCode(_posTrShippingHd.DeliveryCountryCode) + " - " + this._cityBL.GetCityNameByCode(_posTrShippingHd.DeliverCityCode) + " " + _posTrShippingHd.DeliverPostalCode;
            this.SubtotalTextBox.Text = Convert.ToDecimal(_posTrShippingHd.SubTotalForex).ToString("#,#0.##");
            this.DiscountTextBox.Text = Convert.ToDecimal(_posTrShippingHd.DiscForex).ToString("#,#0.##");
            this.PPNTextBox.Text = Convert.ToDecimal(_posTrShippingHd.PPNForex + _posTrShippingHd.PB1Forex).ToString("#,#0.##");
            this.OtherForexTextBox.Text = Convert.ToDecimal(_posTrShippingHd.OtherForex).ToString("#,#0.##");
            this.ServiceChargeTextBox.Text = Convert.ToDecimal(_posTrShippingHd.ServiceChargeAmount).ToString("#,#0.##");
            this.TotalForexTextBox.Text = Convert.ToDecimal(_posTrShippingHd.TotalForex).ToString("#,#0.##");
            this.FakturPajakTextBox.Text = _posTrShippingHd.FakturPajakNmbr + " " + DateFormMapper.GetValue(_posTrShippingHd.FakturPajakDate) + " Rate :" + Convert.ToDecimal(_posTrShippingHd.FakturPajakRate).ToString("#,#0.##");
            this.RemarkTextBox.Text = (_posTrShippingHd.Remark == null ? "" : _posTrShippingHd.Remark);

            this.StatusHiddenField.Value = _posTrShippingHd.Status.ToString();
            if (this.StatusHiddenField.Value.Trim().ToLower() == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.Posted).ToString().ToLower())
            {
                this.DeleteButton.Visible = false;
            }
            else
            {
                this.DeleteButton.Visible = true;
            }

            this.ShowDataDetail(_reqPage);
        }

        //protected void EditButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        //}

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.SendToCashier).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            String _result = "";

            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.SendToCashier).ToString().ToLower())
            {
                _result = this._shippingBL.PostingPOSTrShippingHd(this.TransNmbrTextBox.Text, HttpContext.Current.User.Identity.Name);
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.Posted).ToString().ToLower())
            {
                _result = this._shippingBL.UnpostingPOSTrShippingHd(this.TransNmbrTextBox.Text, HttpContext.Current.User.Identity.Name);
            }
            this.WarningLabel.Text = _result;
            this.ShowData();
            this.ShowActionButton();
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
                if (this.StatusHiddenField.Value.Trim().ToLower() == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.Posted).ToString().ToLower())
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

                if (this.StatusHiddenField.Value.Trim().ToLower() == POSTrShippingDataMapper.GetStatus(POSTrShippingStatus.Posted).ToString().ToLower())
                {
                    this.CreateJurnalImageButton.Visible = true;
                }
            }
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = new ReportDataSource();//this._reportTourBL.TicketingPrintPreview(this.TransNmbrTextBox.Text);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            string _path = ""; // _reportListBL.GetPathPrintPreview(AppModule.GetValue(TransactionType.Ticketing), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));

            //string _value = this._shippingBL.GetValueForReport("TicketingPrintPreviewWithHeader.rdlc", "Bank_Account");
            //string _value2 = this._shippingBL.GetValueForReport("TicketingPrintPreviewWithoutHeader.rdlc", "Bank_Account");
            //string _address1 = this._shippingBL.GetValueForReport("TicketingPrintPreviewWithHeader.rdlc", "Header_Address1");
            //string _address2 = this._shippingBL.GetValueForReport("TicketingPrintPreviewWithHeader.rdlc", "Header_Address2");

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path;
            this.ReportViewer1.DataBind();
            if (_path != "Ticketing /TicketingPrintPreviewWithoutHeader.rdlc")
            {
                ReportParameter[] _reportParam = new ReportParameter[4];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNmbrTextBox.Text, true);
                _reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                //_reportParam[2] = new ReportParameter("Bank_Account", _value, true);
                //_reportParam[3] = new ReportParameter("Header_Address1", _address1, true);
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            }
            else
            {
                ReportParameter[] _reportParam = new ReportParameter[2];
                _reportParam[0] = new ReportParameter("Nmbr", this.TransNmbrTextBox.Text, true);
                //_reportParam[1] = new ReportParameter("Bank_Account", _value2, true);
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            }
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void CreateJurnalImageButton_Click(object sender, ImageClickEventArgs e)
        {

            if (this.CreateJurnalDDL.SelectedValue == "1")
            {
                this.Panel1.Visible = false;
                //this.Panel4.Visible = false;
                //this.Panel3.Visible = true;

                //this.ReportViewer2.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                //ReportDataSource _reportDataSource1 = this._reportTourBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                //this.ReportViewer2.LocalReport.DataSources.Clear();
                //this.ReportViewer2.LocalReport.DataSources.Add(_reportDataSource1);
                //this.ReportViewer2.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath1;
                //this.ReportViewer2.DataBind();
                //ReportParameter[] _reportParam = new ReportParameter[2];
                //_reportParam[0] = new ReportParameter("Nmbr", this.TransNmbrTextBox.Text, true);
                //_reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                //this.ReportViewer2.LocalReport.SetParameters(_reportParam);
                //this.ReportViewer2.LocalReport.Refresh();
            }
            else if (this.CreateJurnalDDL.SelectedValue == "2")
            {
                //this.Panel1.Visible = false;
                //this.Panel3.Visible = false;
                //this.Panel4.Visible = true;

                //this.ReportViewer3.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

                //ReportDataSource _reportDataSource2 = this._reportTourBL.JournalTicketingPrintPreview(this.TransNoTextBox.Text);

                //this.ReportViewer3.LocalReport.DataSources.Clear();
                //this.ReportViewer3.LocalReport.DataSources.Add(_reportDataSource2);
                //this.ReportViewer3.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath2;
                //this.ReportViewer3.DataBind();
                //ReportParameter[] _reportParam = new ReportParameter[2];
                //_reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
                //_reportParam[1] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
                //this.ReportViewer3.LocalReport.SetParameters(_reportParam);
                //this.ReportViewer3.LocalReport.Refresh();
            }
        }


        #region Detail

        private double RowCount()
        {
            double _result = 0;

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            _result = this._shippingBL.RowsCountPOSTrShippingDt(_tempSplit[0]);
            _result = System.Math.Ceiling(_result / (double)_maxrow);

            return _result;
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

        public void ShowDataDetail(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            string[] _tempSplit = (Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)).Split('|');
            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._shippingBL.GetListShippingDtByTransNmbr(_tempSplit[0]);
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            POSTrShippingDt _temp = (POSTrShippingDt)e.Item.DataItem;
            string _code = _temp.TransNmbr.ToString() + "|" + _temp.ItemNo.ToString();

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

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
            _listCheckbox.Checked = this.IsChecked(_code);

            ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
            _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                _viewButton.Visible = false;
            }

            //ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
            //_editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
            //_editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";

            //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            //if (this._permEdit == PermissionLevel.NoAccess)
            //{
            //    _editButton.Visible = false;
            //}

            Literal _airwayBillLiteral = (Literal)e.Item.FindControl("AirwayBillLiteral");
            _airwayBillLiteral.Text = HttpUtility.HtmlEncode(_temp.AirwayBill);

            POSMsShippingVendor _posMsShippingVendor = this._vendorBL.GetSingle(_temp.VendorCode);

            Literal _vendorLiteral = (Literal)e.Item.FindControl("VendorLiteral");
            _vendorLiteral.Text = _posMsShippingVendor.VendorName;

            Literal _shippingTypeLiteral = (Literal)e.Item.FindControl("ShippingTypeLiteral");
            if (_posMsShippingVendor.FgZone == 'Y')
                _shippingTypeLiteral.Text = this._shippingBL.GetSinglePOSMsZone(_temp.ShippingTypeCode).ZoneName;
            else
                _shippingTypeLiteral.Text = this._shippingBL.GetSinglePOSMsShippingType(_temp.ShippingTypeCode).ShippingTypeName;

            string _productshape = "";
            if (_temp.ProductShape == "0")
                _productshape = "Document";
            else if (_temp.ProductShape == "1")
                _productshape = "Non Document";
            else
                _productshape = "International Priority";

            Literal _productShapeLiteral = (Literal)e.Item.FindControl("ProductShapeLiteral");
            _productShapeLiteral.Text = _productshape;

            Literal _weightLiteral = (Literal)e.Item.FindControl("WeightLiteral");
            _weightLiteral.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Weight).ToString("#,#0"));

            Literal _price1Literal = (Literal)e.Item.FindControl("Price1Literal");
            _price1Literal.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Price1).ToString("#,#0"));

            Literal _price2Literal = (Literal)e.Item.FindControl("Price2Literal");
            _price2Literal.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.Price2).ToString("#,#0"));

            Literal _totalForexLiteral = (Literal)e.Item.FindControl("TotalForexLiteral");
            _totalForexLiteral.Text = HttpUtility.HtmlEncode(Convert.ToDecimal(_temp.LineTotalForex).ToString("#,#0"));

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
        }

        //protected void AddButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        //}

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();
            bool _result = false;
            foreach (var _item in _tempSplit)
            {
                string[] _tempSplit2 = _item.Split('|');
                //_result = this._shippingBL.DeletePOSTrShippingDt(_tempSplit2[0], Convert.ToInt32(_tempSplit2[1]));
                _result = this._shippingBL.SetVOIDDt(_tempSplit2[0], true);
            }

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }
            this.WarningLabel.Text = _error;
            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;
            this.ShowDataDetail(0);
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDataDetail(Convert.ToInt32(e.CommandArgument));
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

            this.ShowDataDetail(_reqPage);
        }

        #endregion
    }
}
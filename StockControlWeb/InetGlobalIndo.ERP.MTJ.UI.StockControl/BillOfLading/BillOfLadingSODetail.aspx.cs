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
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public partial class BillOfLadingSODetail : BillOfLadingBase
    {
        private SupplierBL _supplierBL = new SupplierBL();
        private CustomerBL _customerBL = new CustomerBL();
        private BillOfLadingBL _BillOfLadingBL = new BillOfLadingBL();
        private DeliveryOrderBL _deliveryOrderBL = new DeliveryOrderBL();
        private UnitBL _unitBL = new UnitBL();
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();
        private ReportStockControlBL _reportStock = new ReportStockControlBL();
        private ReportListBL _reportListBL = new ReportListBL();
        private PermissionBL _permBL = new PermissionBL();
        private UserBL _userBL = new UserBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

        //private string _reportPath1 = "BillOfLading/BillOfLadingPrintPreview.rdlc";

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

        private int _page2;
        private int _maxrow2 = Convert.ToInt32(ApplicationConfig.ListPageSize);
        private int _maxlength2 = Convert.ToInt32(ApplicationConfig.DataPagerRange);
        private int _no2 = 0;
        private int _nomor2 = 0;
        //private Boolean _isCheckedAll = true;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPreview = "preview.jpg";

        private string _referenceType;

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
                _referenceType = _BillOfLadingBL.GetSingleBOLReferenceType();

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.ViewState[this._currPageKey] = 0;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                //this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.GenerateDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/generate.jpg";

                //bool _cek;
                //Int32 _count = _BillOfLadingBL.;
                //this.GenerateDetailButton.Attributes.Add("OnClick", "Click(" + _count + ", " + _cek + ");");

                if (_BillOfLadingBL.CekGenerateButton(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)))
                {
                    this.GenerateDetailButton.Attributes.Add("OnClick", "return confirm('Data yang sudah di detail akan di hapus');");
                }

                this.ClearLabel();
                this.ShowData();
                this.ShowDataDetail1();
                this.ShowDataDetail2(0);
                this.SetButtonPermission();
            }
            this.ShowActionButton();
            this.ShowPreviewButton();
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton2.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                this.DeleteButton.Visible = false;
                this.DeleteButton2.Visible = false;
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
            this.WarningLabel2.Text = "";
            this.Label1.Text = "";
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._BillOfLadingBL.RowsCountSTCSJDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
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
            this._BillOfLadingBL = new BillOfLadingBL();
            STCSJHd _stcSJHd = this._BillOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _stcSJHd.TransNmbr;
            this.FileNmbrTextBox.Text = _stcSJHd.FileNmbr;
            this.TransDateTextBox.Text = DateFormMapper.GetValue(_stcSJHd.TransDate);
            this.StatusLabel.Text = BillOfLadingDataMapper.GetStatusText(_stcSJHd.Status);
            this.CustCodeTextBox.Text = this._customerBL.GetNameByCode(_stcSJHd.CustCode) + " - " + _stcSJHd.CustCode;
            //this.SONoTextBox.Text = this._salesOrderBL.GetFileNmbrMKTSOHd(_stcSJHd.SONo);
            this.CarNoTextBox.Text = _stcSJHd.CarNo;
            this.DriverTextBox.Text = _stcSJHd.Driver;
            this.RemarkTextBox.Text = _stcSJHd.Remark;
            //this.WarehouseCodeTextBox.Text = _wrhsBL.GetWarehouseNameByCode(_stcSJHd.WrhsCode);

            //if (_stcSJHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            //{
            //    this.WrhsSubledTextBox.Text = "";
            //}
            //else
            //{
            //    if (_stcSJHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
            //    {
            //        this.WrhsSubledTextBox.Text = this._customerBL.GetNameByCode(_stcSJHd.WrhsSubLed.ToString());
            //    }
            //    else if (_stcSJHd.WrhsFgSubLed == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
            //    {
            //        this.WrhsSubledTextBox.Text = this._supplierBL.GetSuppNameByCode(_stcSJHd.WrhsSubLed.ToString());
            //    }
            //}

            if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.AddButton2.Visible = false;
                this.DeleteButton.Visible = false;
                this.DeleteButton2.Visible = false;
                this.EditButton.Visible = false;
                this.GenerateDetailButton.Visible = false;
            }
            else
            {
                this.AddButton2.Visible = true;
                this.DeleteButton.Visible = true;
                this.DeleteButton2.Visible = true;
                this.EditButton.Visible = true;
                this.GenerateDetailButton.Visible = true;
            }

            this.StatusHiddenField.Value = _stcSJHd.Status.ToString();

            this.TempHidden.Value = "";

            this.Panel1.Visible = true;
            this.Panel2.Visible = true;
            this.Panel3.Visible = false;
            this.ShowActionButton();
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

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
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
                this.PreviewButton.Visible = false;
            }
            else
            {
                if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                STCSJDt _temp = (STCSJDt)e.Item.DataItem;
                string _code = _temp.TransNmbr.ToString();
                string _code1 = _temp.DONo;
                string _code2 = _temp.ProductCode;
                string _code3 = _temp.LocationCode;
                string _code4 = _temp.WrhsCode;
                string _all = _code1 + "|" + _code2 + "|" + _code3 + "|" + _code4;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _all;
                }
                else
                {
                    this.TempHidden.Value += "," + _all;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked(_all);

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._DOKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code1, ApplicationConfig.EncryptionKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code3, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                if (this.StatusHiddenField.Value.Trim().ToLower() != BillOfLadingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    string _transNmbr = this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey));
                    string _doNumber = this._DOKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code1, ApplicationConfig.EncryptionKey));
                    string _productKey = this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code2, ApplicationConfig.EncryptionKey));
                    string _locationKey = this._locationKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code3, ApplicationConfig.EncryptionKey));
                    string _wrhsCode = this._wrhsCode + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code4, ApplicationConfig.EncryptionKey));

                    _editButton.PostBackUrl = this._editDetailSoPage + "?" + _transNmbr + "&" + _doNumber + "&" + _productKey + "&" + _locationKey + "&" + _wrhsCode;
                    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }
                else
                {
                    _editButton.Visible = false;
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

                Literal _doNo = (Literal)e.Item.FindControl("DONoLiteral");
                //_doNo.Text = this._deliveryOrderBL.GetFileNmbrMKTDOHd(_temp.DONo);
                _doNo.Text = HttpUtility.HtmlEncode(_temp.DONo);

                Literal _productCode = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCode.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _itemID = (Literal)e.Item.FindControl("ItemIDLiteral");
                _itemID.Text = HttpUtility.HtmlEncode(_temp.ItemID.ToString());

                Literal _productName = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productName.Text = HttpUtility.HtmlEncode(_temp.ProductName);

                Literal _locationCode = (Literal)e.Item.FindControl("LocationLiteral");
                _locationCode.Text = HttpUtility.HtmlEncode(_temp.LocationName);

                Literal _unit = (Literal)e.Item.FindControl("UnitLiteral");
                _unit.Text = HttpUtility.HtmlEncode(_unitBL.GetUnitNameByCode(_temp.Unit));

                Literal _qty = (Literal)e.Item.FindControl("QtyLiteral");
                _qty.Text = HttpUtility.HtmlEncode((_temp.Qty == 0) ? "0" : _temp.Qty.ToString("#,##0.##"));
            }
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = true;

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportStock.STSJPrintPreview(this.TransNoTextBox.Text);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            string _path = _reportListBL.GetPathPrintPreview(AppModule.GetValue(TransactionType.BillOfLading), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));

            String _SJRef = _BillOfLadingBL.GetSJRef();

            if (_SJRef == "DO")
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path;
            else if (_SJRef == "SO")
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + "BillOfLading/BillOfLadingPrintPreviewForSO.rdlc";

            this.ReportViewer1.DataBind();

            String _jobTitleStatus = this._companyConfigBL.GetSingle(CompanyConfigure.ViewJobTitlePrintReport).SetValue;

            ReportParameter[] _reportParam = new ReportParameter[3];
            _reportParam[0] = new ReportParameter("Nmbr", this.TransNoTextBox.Text, true);
            _reportParam[1] = new ReportParameter("ViewJobTitlePrintReport", _jobTitleStatus, false);
            _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);
            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            //string[] _date = this.TransDateTextBox.Text.Split('-');
            DateTime _transdate = DateFormMapper.GetValue(this.TransDateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._BillOfLadingBL.GetAppr(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._BillOfLadingBL.Approve(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._BillOfLadingBL.Posting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                this.WarningLabel.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._BillOfLadingBL.Unposting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

                //this.WarningLabel.Text = _result;
            }      
      
            this.ShowData();//Convert.ToInt32(e.CommandArgument)
            //this.ShowDataDetail1();
            //this.ShowDataDetail2(0);
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
            string _page = "0";

            this.ClearLabel();

            bool _result = this._BillOfLadingBL.DeleteMultiSTCSJDt(this.TransNoTextBox.Text, _tempSplit);

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

            Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowData();
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

            this.ShowDataDetail2(_reqPage);
        }

        private void ShowDataDetail1()
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater2.DataSource = null;
            }
            else
            {
                this.ListRepeater2.DataSource = this._BillOfLadingBL.GetListSTCSjDtReference(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater2.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.AllCheckBox.Checked = this.IsCheckedAll2();

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                //this.ShowPage(_prmCurrentPage);
            }
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                STCSJDtReference _temp = (STCSJDtReference)e.Item.DataItem;

                string _code = _temp.TransNmbr.ToString();
                string _code1 = _temp.ReferenceNmbr.ToString();

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _code1;
                }
                else
                {
                    this.TempHidden.Value += "," + _code1;
                }

                Literal _noLiteral2 = (Literal)e.Item.FindControl("NoLiteral2");
                _no2 = _page2 * _maxrow2;
                _no2 += 1;
                _no2 = _nomor2 + _no2;
                _noLiteral2.Text = _no2.ToString();
                _nomor2 += 1;

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox2");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox.ClientID + ", '" + _code1 + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");
                _listCheckbox.Checked = this.IsChecked2(_code1);

                //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                //_viewButton.PostBackUrl = this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //_viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                //this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                //if (this._permView == PermissionLevel.NoAccess)
                //{
                //    _viewButton.Visible = false;
                //}

                //ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                //if (_temp.Status != TransactionDataMapper.GetStatus(TransStatus.Posted))
                //{
                //    _editButton.PostBackUrl = this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                //    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                //}
                //else
                //{
                //    _editButton.Visible = false;
                //}

                //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                //if (this._permEdit == PermissionLevel.NoAccess)
                //{
                //    _editButton.Visible = false;
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

                Literal _transNmbr = (Literal)e.Item.FindControl("RequestNoLiteral");
                _transNmbr.Text = HttpUtility.HtmlEncode(_temp.ReferenceNmbr);

                Literal _fileNmbrLiteral = (Literal)e.Item.FindControl("TypeLiteral");
                _fileNmbrLiteral.Text = HttpUtility.HtmlEncode("SO");

                Literal _transDate = (Literal)e.Item.FindControl("DateLiteral");
                _transDate.Text = DateFormMapper.GetValue(_temp.TransDate);

            }
        }

        protected void GenerateDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            bool _result = this._BillOfLadingBL.GenerateSTCSJDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_result == true)
            {
                ShowDataDetail2(0);
            }
            else
            {
                this.WarningLabel.Text = "Generate Failed";
            }

            //this._prmRevisi = 0;
        }

        private void ShowDataDetail2(Int32 _prmCurrentPage)
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");
            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._BillOfLadingBL.GetListSTCSJDt(_prmCurrentPage, _maxrow, this.TransNoTextBox.Text.Trim());
            }
            this.ListRepeater.DataBind();

            this.AllCheckBox.Checked = this.IsCheckedAll();

            if (this.StatusHiddenField.Value.Trim().ToLower() == BillOfLadingDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                //this.AddButton.Visible = false;
                this.AddButton2.Visible = false;
                this.DeleteButton.Visible = false;
                this.DeleteButton2.Visible = false;
                this.EditButton.Visible = false;
                this.GenerateDetailButton.Visible = false;
            }
            else
            {
                //this.AddButton.Visible = true;
                this.AddButton2.Visible = true;
                this.DeleteButton.Visible = true;
                this.DeleteButton2.Visible = true;
                this.EditButton.Visible = true;
                this.GenerateDetailButton.Visible = true;
            }

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel2.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            if (this._permView != PermissionLevel.NoAccess)
            {
                this.ShowPage(_prmCurrentPage);
            }
        }

        protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_addDetailSoPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)));
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');
            string _page = "0";

            this.ClearLabel();

            bool _result = this._BillOfLadingBL.DeleteMultiSTCSJDtReference(this.TransNoTextBox.Text, _tempSplit);

            if (_result == true)
            {
                _error = "Delete Success";
            }
            else
            {
                _error = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;

            Response.Redirect(this._viewPageSo + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
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

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.TransDateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._BillOfLadingBL.Unposting(this.TransNoTextBox.Text, _year, _period, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.BillOfLading), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
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
            this.ShowDataDetail1();
            this.ShowDataDetail2(0);
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }

    }
}
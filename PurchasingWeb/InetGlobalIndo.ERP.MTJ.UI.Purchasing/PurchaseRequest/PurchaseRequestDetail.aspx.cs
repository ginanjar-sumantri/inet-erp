using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.IO;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest
{
    public partial class PurchaseRequestDetail : PurchaseRequestBase
    {
        private PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();
        private OrganizationStructureBL _organizationStructureBL = new OrganizationStructureBL();
        private ProductBL _productBL = new ProductBL();
        private ReportPurchaseBL _reportPurchaseBL = new ReportPurchaseBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private PettyBL _pettyBL = new PettyBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();
        private ReportListBL _reportListBL = new ReportListBL();
        private UserBL _userBL = new UserBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

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

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPreview = "preview.jpg";

        private string _confirmTitle = "Description Required";

        private decimal _totalAmount = 0;
        private string _currCode = "";
        private byte _decimalPlace = 0;

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

                PRCRequestHd _prcRequestHd = this._purchaseRequestBL.GetSinglePRCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_prcRequestHd.Status == PurchaseRequestDataMapper.GetStatus(TransStatus.Posted))
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
                    this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                    this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                    this.ImportButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/import_excel.jpg";
                    this.GetFormatExcelLiteral.Text = "<a href='" + ApplicationConfig.PurchasingFormatVirDirPath + "Purchase Request.xls'>" + "<img src='" + ApplicationConfig.HomeWebAppURL + "images/download_excel.jpg" + "' border='0'>" + "</a>";
                }
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                this.ShowData();
                this.ShowDetail(0);
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

        public void ClearLabel()
        {
            this.Label.Text = "";
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            this._purchaseRequestBL = new PurchaseRequestBL();
            PRCRequestHd _prcRequestHd = this._purchaseRequestBL.GetSinglePRCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _currCode = this._purchaseRequestBL.GetSinglePRCRequestHd(_prcRequestHd.TransNmbr).CurrCode;
            _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            this.TransNoTextBox.Text = _prcRequestHd.TransNmbr;
            this.FileNmbrTextBox.Text = _prcRequestHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_prcRequestHd.TransDate);
            this.OrgUnitTextBox.Text = this._organizationStructureBL.GetDescriptionByCode(_prcRequestHd.OrgUnit);
            this.CurrencyTextBox.Text = _prcRequestHd.CurrCode;
            this.RequestByTextBox.Text = _prcRequestHd.RequestBy;
            this.RemarkTextBox.Text = _prcRequestHd.Remark;
            this.StatusLabel.Text = PurchaseRequestDataMapper.GetStatusText(_prcRequestHd.Status);
            this.StatusHiddenField.Value = _prcRequestHd.Status.ToString();

            this.ShowActionButton();
            this.ShowPreviewButton();

            this.Panel1.Visible = true;
            this.Panel2.Visible = true;
            this.Panel3.Visible = false;
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
                else
                {
                    if (this._pettyBL.CheckLimitAuthorization(AppModule.GetValue(TransactionType.PurchaseRequest), HttpContext.Current.User.Identity.Name, Convert.ToDecimal((this.TotalAmountLabel.Text == "") ? "0" : this.TotalAmountLabel.Text)) == true)
                    {
                        this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;
                    }
                    else
                    {
                        this.ActionButton.Visible = false;
                    }
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
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
                if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
            }
        }

        private double RowCount()
        {
            double _result = 0;

            _result = this._purchaseRequestBL.RowsCountPRCRequestDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
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
        }

        public void ShowDetail(Int32 _prmCurrentPage)
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._page = _prmCurrentPage;

            this.ListRepeater.DataSource = this._purchaseRequestBL.GetListPRCRequestDt(_prmCurrentPage, _maxrow, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.ListRepeater.DataBind();

            this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "'  ,'" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.TotalAmountLabel.Text = _totalAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.ShowPage(_prmCurrentPage);
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                PRCRequestDt _temp = (PRCRequestDt)e.Item.DataItem;

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
                _no = _page * _maxrow;
                _no += 1;
                _no = _nomor + _no;
                _noLiteral.Text = _no.ToString();
                _nomor += 1;

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItem + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                _viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

                this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

                if (this._permView == PermissionLevel.NoAccess)
                {
                    _viewButton.Visible = false;
                }

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }
                else
                {
                    if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                    {
                        _editButton.Visible = false;
                    }
                    else
                    {
                        _editButton.PostBackUrl = this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItem + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_code, ApplicationConfig.EncryptionKey));
                        _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                    }
                }

                decimal _qtyPO = Convert.ToDecimal((_temp.QtyPO == null) ? 0 : _temp.QtyPO);
                decimal _tempCalculation = _qtyPO + Convert.ToDecimal((_temp.QtyClose == null) ? 0 : _temp.QtyClose);
                ImageButton _closeButton = (ImageButton)e.Item.FindControl("CloseButton");
                this._permClose = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Close);

                if (this._permClose == PermissionLevel.NoAccess)
                {
                    _closeButton.Visible = false;
                }
                else
                {
                    if ((this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower()) && (_temp.DoneClosing.ToString().Trim().ToLower() == PurchaseRequestDataMapper.GetStatusDetail(PurchaseRequestStatusDt.Open).ToString().Trim().ToLower()) && (_temp.Qty > _tempCalculation))
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
                }

                CheckBox _cb = (CheckBox)e.Item.FindControl("ListCheckBox");
                _cb.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + "," + _cb.ClientID + ",'" + _code + "', '" + _awal + "' , '" + _akhir + "', '" + _cbox + "' )");

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
                _productCodeLiteral.Text = _temp.ProductCode;

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productNameLiteral.Text = _temp.ProductName;

                Literal _specificationLiteral = (Literal)e.Item.FindControl("SpecificationLiteral");
                _specificationLiteral.Text = HttpUtility.HtmlEncode(_temp.Specification);

                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = (_temp.Qty == 0) ? "0" : _temp.Qty.ToString("#,###.##");

                Literal _EstPriceLiteral = (Literal)e.Item.FindControl("EstPrireLiteral");
                _EstPriceLiteral.Text = (_temp.EstPrice == 0) ? "0" : _temp.EstPrice.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

                Literal _requiredDateLiteral = (Literal)e.Item.FindControl("RequiredDateLiteral");
                _requiredDateLiteral.Text = DateFormMapper.GetValue(_temp.RequireDate);

                _totalAmount = _totalAmount + (_temp.Qty * _temp.EstPrice);
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CloseButton")
            {
                string _result = this._purchaseRequestBL.Close(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), e.CommandArgument.ToString(), this.DescriptionHiddenField.Value, HttpContext.Current.User.Identity.Name);

                if (_result == "")
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Success Close";
                    this.DescriptionHiddenField.Value = "";
                    this.ShowDetail(0);
                }
                else
                {
                    this.ClearLabel();
                    this.DescriptionHiddenField.Value = "";
                    this.WarningLabel.Text = _result;
                }
            }
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._purchaseRequestBL.GetApproval(this.TransNoTextBox.Text, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._purchaseRequestBL.Approve(this.TransNoTextBox.Text, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._purchaseRequestBL.Posting(this.TransNoTextBox.Text, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseRequestDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._purchaseRequestBL.UnPosting(this.TransNoTextBox.Text, HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result;
            }

            this.ShowData();
            this.ShowDetail(0);
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = true;

            String _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportPurchaseBL.PurchaseRequestPrintPreview(_transNo);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            string _path = _reportListBL.GetPathPrintPreview(AppModule.GetValue(TransactionType.PurchaseRequest), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));

            this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _path;

            this.ReportViewer1.DataBind();

            String _jobTitleStatus = this._companyConfigBL.GetSingle(CompanyConfigure.ViewJobTitlePrintReport).SetValue;

            ReportParameter[] _reportParam = new ReportParameter[3];
            _reportParam[0] = new ReportParameter("TransNmbr", _transNo, true);
            _reportParam[1] = new ReportParameter("JobTitleStatus", _jobTitleStatus, false);
            _reportParam[2] = new ReportParameter("CompanyName", new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name), false);

            this.ReportViewer1.LocalReport.SetParameters(_reportParam);
            this.ReportViewer1.LocalReport.Refresh();
        }

        protected void ImportButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._importPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _error = "";
            string _temp = this.CheckHidden.Value;
            string[] _tempsplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._purchaseRequestBL.DeleteMultiPRCRequestDt(_tempsplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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

            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
        }

        protected void DataPagerTopRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataPager")
            {
                this.ViewState[this._currPageKey] = Convert.ToInt32(e.CommandArgument);

                this.ShowDetail(Convert.ToInt32(e.CommandArgument));
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

            this.ShowDetail(_reqPage);
        }
        protected void ExportXMLButton_Click(object sender, ImageClickEventArgs e)
        {
            this._purchaseRequestBL = new PurchaseRequestBL();
            PRCRequestHd _prcRequestHd = this._purchaseRequestBL.GetSinglePRCRequestHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            try
            {
                if (_prcRequestHd.Status == 2) return;
                TextWriter tw = new StreamWriter(ApplicationConfig.UploadXMLPath + "PurchaseRequest/" + _purchaseRequestBL.GetCustomerCode() + _prcRequestHd.TransNmbr.Replace('/', '_') + ".xml");

                tw.WriteLine("<PurchaseRequest>");
                tw.WriteLine("<TransNmbr>" + _prcRequestHd.TransNmbr + "</TransNmbr>");
                tw.WriteLine("<FileNmbr>" + _prcRequestHd.FileNmbr + "</FileNmbr>");
                tw.WriteLine("<TransDate>" + _prcRequestHd.TransDate + "</TransDate>");
                tw.WriteLine("<OrgUnit>" + _prcRequestHd.OrgUnit + "</OrgUnit>");
                tw.WriteLine("<Remark>" + _prcRequestHd.Remark + "</Remark>");
                PurchaseRequestBL _prcRequestBL = new PurchaseRequestBL();
                List<PRCRequestDt> _PRCRequestDetailData = _prcRequestBL.GetPRCRequestDtListOfHeader(_prcRequestHd.TransNmbr);
                foreach (PRCRequestDt _rs in _PRCRequestDetailData)
                {
                    tw.WriteLine("<PurchaseRequestDetail>");
                    tw.WriteLine("<ProductCode>" + _rs.ProductCode + "</ProductCode>");
                    tw.WriteLine("<Specification>" + _rs.Specification + "</Specification>");
                    tw.WriteLine("<Qty>" + _rs.Qty + "</Qty>");
                    tw.WriteLine("<Unit>" + _rs.Unit + "</Unit>");
                    tw.WriteLine("</PurchaseRequestDetail>");
                }

                tw.WriteLine("</PurchaseRequest>");

                tw.Close();
                _prcRequestBL.InsertPRCPRxmlList(_purchaseRequestBL.GetCustomerCode(), _purchaseRequestBL.GetCustomerCode() + _prcRequestHd.TransNmbr.Replace('/', '_') + ".xml");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._purchaseRequestBL.UnPosting(this.TransNoTextBox.Text, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.PurchaseRequest), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
                if (_result1 == true)
                    this.WarningLabel2.Text = _result;
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
            }
            else
            {
                this.ReasonTextBox.Text = "";
                this.ReasonPanel.Visible = false;
                this.WarningLabel2.Text = _result;
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

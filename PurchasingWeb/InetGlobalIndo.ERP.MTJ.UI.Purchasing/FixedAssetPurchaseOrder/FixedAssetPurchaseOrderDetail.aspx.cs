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
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.CustomControl;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.FixedAssetPurchaseOrder
{
    public partial class FixedAssetPurchaseOrderDetail : FixedAssetPurchaseOrderBase
    {
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private ShipmentBL _shipmentBL = new ShipmentBL();
        private TermBL _termBL = new TermBL();
        private FixedAssetPurchaseOrderBL _faPOBL = new FixedAssetPurchaseOrderBL();
        private DeliveryBL _deliveryBL = new DeliveryBL();
        private ReportPurchaseBL _reportPurchaseBL = new ReportPurchaseBL();
        private PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CompanyConfig _companyConfigBL = new CompanyConfig();
        private ReportListBL _reportListBL = new ReportListBL();
        private UserBL _userBL = new UserBL();
        private CompanyBL _companyBL = new CompanyBL();
        private UnpostingActivitiesBL _unpostingActivitiesBL = new UnpostingActivitiesBL();

        private int _prmRevisi = 0;

        private int _no = 0;
        //private int _nomor = 0;

        private int _no2 = 0;
        //private int _nomor2 = 0;

        private string _awal = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater_ctl";
        private string _akhir = "_ListCheckBox";
        private string _cbox = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox";
        private string _tempHidden = "ctl00$DefaultBodyContentPlaceHolder$TempHidden";

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

        private string _imgGetApproval = "get_approval.jpg";
        private string _imgPosting = "posting.jpg";
        private string _imgUnposting = "unposting.jpg";
        private string _imgApprove = "approve.jpg";
        private string _imgPreview = "preview.jpg";

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

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";

                this.AddButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                //this.ShowRevisiDDL();

                this.ShowData();
                this.SetAttribute();
                this.ShowDataDetail1();
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

        private void SetAttribute()
        {
            //this.DeleteHeaderButton.Attributes.Add("OnClick", "return AskYouFirst();");
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
                if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
            }
        }

        public void ClearLabel()
        {
            this.Label.Text = "";
            this.WarningLabel.Text = "";
        }

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
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
            this._faPOBL = new FixedAssetPurchaseOrderBL();
            PRCFAPOHd _prcFAPOHd = this._faPOBL.GetSinglePRCFAPOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_prcFAPOHd.CurrCode);
            byte _decimalPlace2 = this._currencyBL.GetDecimalPlace(_prcFAPOHd.ShippingCurr);

            this.TransNoTextBox.Text = _prcFAPOHd.TransNmbr;
            this.FileNmbrTextBox.Text = _prcFAPOHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_prcFAPOHd.TransDate);
            this.SupplierTextBox.Text = _prcFAPOHd.SuppCode + " - " + _supplierBL.GetSuppNameByCode(_prcFAPOHd.SuppCode);
            this.AttnTextBox.Text = _prcFAPOHd.Attn;
            this.SupplierPONoTextBox.Text = _prcFAPOHd.SuppPONo;
            this.TermTextBox.Text = _termBL.GetTermNameByCode(_prcFAPOHd.Term);
            this.ShipmentTextBox.Text = _shipmentBL.GetShipmentNameByCode(_prcFAPOHd.ShipmentType);
            this.ShipmentNameTextBox.Text = _prcFAPOHd.ShipmentName;
            this.DeliveryTextBox.Text = _deliveryBL.GetDeliveryNameByCode(_prcFAPOHd.Delivery);
            this.SubjectTextBox.Text = _prcFAPOHd.Subject;

            if (_prcFAPOHd.DeliveryDate != null)
            {
                this.DeliveryDateTextBox.Text = DateFormMapper.GetValue(_prcFAPOHd.DeliveryDate);
            }
            else
            {
                this.DeliveryDateTextBox.Text = "";
            }

            this.CurrCodeTextBox.Text = _prcFAPOHd.CurrCode;
            this.ForexRateTextBox.Text = (_prcFAPOHd.ForexRate == 0) ? "0" : _prcFAPOHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DPPercentTextBox.Text = (_prcFAPOHd.DP == 0) ? "0" : _prcFAPOHd.DP.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DPForexTextBox.Text = (_prcFAPOHd.DPForex == 0) ? "0" : _prcFAPOHd.DPForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CurrTextBox.Text = _prcFAPOHd.CurrCode;
            this.BaseForexTextBox.Text = (_prcFAPOHd.BaseForex == 0) ? "0" : _prcFAPOHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscTextBox.Text = (_prcFAPOHd.Disc == 0) ? "0" : _prcFAPOHd.Disc.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscForexTextBox.Text = (_prcFAPOHd.DiscForex == 0) ? "0" : _prcFAPOHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.PPHPercentTextBox.Text = (_prcFAPOHd.PPH == 0) ? "0" : _prcFAPOHd.PPH.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.PPHForexTextBox.Text = (_prcFAPOHd.PPHForex == 0) ? "0" : _prcFAPOHd.PPHForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNPercentTextBox.Text = (_prcFAPOHd.PPN == 0) ? "0" : _prcFAPOHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_prcFAPOHd.PPNForex == 0) ? "0" : _prcFAPOHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_prcFAPOHd.TotalForex == 0) ? "0" : _prcFAPOHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_prcFAPOHd.ShippingCurr != null)
            {
                this.ShippingCurrCodeTextBox.Text = _prcFAPOHd.ShippingCurr;
            }
            else
            {
                this.ShippingCurrCodeTextBox.Text = "";
            }

            decimal _tempShippingRate = (_prcFAPOHd.ShippingRate == null) ? 0 : Convert.ToDecimal(_prcFAPOHd.ShippingRate);
            this.ShippingCurrRateTextBox.Text = (_tempShippingRate == 0) ? "0" : _tempShippingRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));

            decimal _tempShippingForex = (_prcFAPOHd.ShippingForex == null) ? 0 : Convert.ToDecimal(_prcFAPOHd.ShippingForex);
            this.ShippingForexTextBox.Text = (_tempShippingForex == 0) ? "0" : _tempShippingForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));

            this.RemarkTextBox.Text = _prcFAPOHd.Remark;

            this.StatusLabel.Text = FixedAssetPurchaseOrderDataMapper.GetStatusText(_prcFAPOHd.Status);
            this.StatusHiddenField.Value = _prcFAPOHd.Status.ToString();

            this.ShowActionButton();
            this.ShowPreviewButton();

            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.EditButton.Visible = false;
            }
            else
            {
                this.DeleteButton.Visible = true;
                this.AddButton.Visible = true;
                this.EditButton.Visible = true;
            }

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.Panel1.Visible = true;
            this.Panel4.Visible = true;
            this.Panel2.Visible = false;
        }

        public void ShowDataDetail1()
        {
            this.TempHidden.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            //this._page = Convert.ToInt32((_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey) == "") ? "0" : Rijndael.Decrypt(_nvcExtractor.GetValue(ApplicationConfig.RequestPageKey), ApplicationConfig.EncryptionKey));

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._faPOBL.GetListPRCFAPODt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();

            //this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PRCFAPODt _temp = (PRCFAPODt)e.Item.DataItem;

                string _transNmbr = _temp.TransNmbr;
                string _faName = _temp.FANAme;

                if (this.TempHidden.Value == "")
                {
                    this.TempHidden.Value = _faName;
                }
                else
                {
                    this.TempHidden.Value += "," + _faName;
                }

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                CheckBox _listCheckbox;
                _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox");
                _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden.ClientID + ", " + _listCheckbox.ClientID + ", '" + _faName + "', '" + _awal + "', '" + _akhir + "', '" + _cbox + "')");

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._faKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_faName, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_transNmbr, ApplicationConfig.EncryptionKey));
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
                    if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                    {
                        _editButton.Visible = false;
                    }
                    else
                    {
                        _editButton.PostBackUrl = this._editDetailPage + "?" + this._faKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_faName, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_transNmbr, ApplicationConfig.EncryptionKey));
                        _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                    }
                }

                ImageButton _closeButton = (ImageButton)e.Item.FindControl("CloseButton");
                this._permClose = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Close);

                if (this._permClose == PermissionLevel.NoAccess)
                {
                    _closeButton.Visible = false;
                }
                else
                {
                    if ((this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower()) && (_temp.DoneClosing.ToString().Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatusDetail(FixedAssetPurchaseOrderStatusDt.Open).ToString().Trim().ToLower()) && _temp.Qty < ((_temp.QtyRR == null) ? 0 : _temp.QtyRR))
                    {
                        _closeButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/close.jpg";
                        _closeButton.CommandArgument = _faName;
                        _closeButton.CommandName = "CloseButton";
                        _closeButton.Attributes.Add("OnClick", "return ConfirmFillDescription('" + this.DescriptionHiddenField.ClientID + "', '" + _confirmTitle + "');");
                    }
                    else
                    {
                        _closeButton.Visible = false;
                    }
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

                Literal _faNameLiteral = (Literal)e.Item.FindControl("FANameLiteral");
                _faNameLiteral.Text = HttpUtility.HtmlEncode(_temp.FANAme);

                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode((_temp.Qty == 0) ? "0" : _temp.Qty.ToString("#,###.##"));

                Literal _unitLiteral = (Literal)e.Item.FindControl("UnitLiteral");
                _unitLiteral.Text = HttpUtility.HtmlEncode(_temp.UnitName);

                Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                _priceLiteral.Text = HttpUtility.HtmlEncode((_temp.PriceForex == 0) ? "0" : _temp.PriceForex.ToString("#,###.##"));

                Literal _amountLiteral = (Literal)e.Item.FindControl("AmountLiteral");
                _amountLiteral.Text = HttpUtility.HtmlEncode((_temp.AmountForex == 0) ? "0" : _temp.AmountForex.ToString("#,###.##"));

                Literal _doneClosingLiteral = (Literal)e.Item.FindControl("DoneClosingLiteral");
                _doneClosingLiteral.Text = FixedAssetPurchaseOrderDataMapper.GetStatusTextDetail(Convert.ToChar(_temp.DoneClosing));
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CloseButton")
            {
                string _result = this._faPOBL.Closing(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), e.CommandArgument.ToString(), this.DescriptionHiddenField.Value, HttpContext.Current.User.Identity.Name);

                if (_result == "")
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "Closing Success";
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = _result;
                }

                this._prmRevisi = 0;
                this.ShowData();
                this.ShowDataDetail1();
                this.DescriptionHiddenField.Value = "";
            }
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel4.Visible = false;
            this.Panel2.Visible = true;

            String _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportPurchaseBL.FixedAssetPurchaseOrderPrintPreview(_transNo);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            string _path = _reportListBL.GetPathPrintPreview(AppModule.GetValue(TransactionType.FixedAssetPurchaseOrder), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));

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
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._faPOBL.GetApproval(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._faPOBL.Approve(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.ApproveRow.Visible = false;

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._faPOBL.Posting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._faPOBL.Unposting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result;
            }

            this.ShowData();
            this.ShowDataDetail1();
        }

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._faPOBL.DeleteMultiPRCFAPODt(_tempSplit, Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

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

            this.ShowData();
            this.ShowDataDetail1();
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ApproveForceButton_Click(object sender, EventArgs e)
        {
            //string[] _date = this.DateTextBox.Text.Split('-');
            //int _year = Convert.ToInt32(_date[0]);
            //int _period = Convert.ToInt32(_date[1]);
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            this.ClearLabel();

            string _result = this._faPOBL.Approve(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);
            this.ApproveRow.Visible = false;
            if (_result == "")
            {
                this.ClearLabel();
                this.Label.Text = "Approve Success";
            }
            else
            {
                this.ClearLabel();
                this.Label.Text = _result;
            }

            this.ShowData();
            this.ShowDataDetail1();
        }

        protected void NotApproveForceButton_Click(object sender, EventArgs e)
        {
            this.ApproveRow.Visible = false;
            this.ClearLabel();
            this._prmRevisi = 0;
            this.ShowData();
            this.ShowDataDetail1();
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._faPOBL.Unposting(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.FixedAssetPurchaseOrder), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
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

            this.ShowData();
            this.ShowDataDetail1();
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}
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

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder
{
    public partial class PurchaseOrderDetail : PurchaseOrderBase
    {
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private SupplierBL _supplierBL = new SupplierBL();
        private ShipmentBL _shipmentBL = new ShipmentBL();
        private TermBL _termBL = new TermBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral2;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                //this.DeleteHeaderButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.GenerateDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/generate.jpg";

                this.AddButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.DeleteButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
                //this.ShowRevisiDDL();

                this._prmRevisi = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisi), ApplicationConfig.EncryptionKey));
                this.ShowData(_prmRevisi);
                this.SetAttribute();
                this.ShowDataDetail1(_prmRevisi);
                this.ShowDataDetail2(_prmRevisi);
            }
        }

        private void SetButtonPermission()
        {
            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                this.AddButton2.Visible = false;
                this.GenerateDetailButton.Visible = false;
            }

            this._permDelete = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Delete);

            if (this._permDelete == PermissionLevel.NoAccess)
            {
                //this.DeleteHeaderButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.DeleteButton2.Visible = false;
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
                if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
                {
                    this.PreviewButton.Visible = false;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
                else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                {
                    this.PreviewButton.Visible = true;
                }
            }
        }

        public void ClearLabel()
        {
            this.Label.Text = "";
            this.WarningLabel.Text = "";
            this.WarningLabel2.Text = "";
        }

        //public void ShowRevisiDDL()
        //{
        //    this.RevisiDropDownList.Items.Clear();
        //    this.RevisiDropDownList.DataTextField = "Revisi";
        //    this.RevisiDropDownList.DataValueField = "Revisi";
        //    this.RevisiDropDownList.DataSource = this._purchaseOrderBL.GetListDDLRevisi(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
        //    this.RevisiDropDownList.DataBind();
        //}

        public void ShowActionButton()
        {
            if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgGetApproval;

                this._permGetApproval = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.GetApproval);

                if (this._permGetApproval == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgApprove;

                this._permApprove = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Approve);

                if (this._permApprove == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgPosting;

                this._permPosting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Posting);

                if (this._permPosting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
                }
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ActionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + _imgUnposting;

                this._permUnposting = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Unposting);

                if (this._permUnposting == PermissionLevel.NoAccess)
                {
                    this.ActionButton.Visible = false;
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

        //        if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(PurchaseOrderStatus.Posted).ToString().ToLower())
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

        public void ShowData(int _prmRevisiNo)
        {
            this._purchaseOrderBL = new PurchaseOrderBL();
            PRCPOHd _prcPOHd = this._purchaseOrderBL.GetSinglePRCPOHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _prmRevisiNo);

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_prcPOHd.CurrCode);
            byte _decimalPlace2 = this._currencyBL.GetDecimalPlace(_prcPOHd.ShippingCurr);

            this.TransNoTextBox.Text = _prcPOHd.TransNmbr;
            this.FileNmbrTextBox.Text = _prcPOHd.FileNmbr;
            //this.RevisiDropDownList.SelectedValue = _prcPOHd.Revisi.ToString();
            this.DateTextBox.Text = DateFormMapper.GetValue(_prcPOHd.TransDate);
            this.SupplierTextBox.Text = _prcPOHd.SuppCode + " - " + _supplierBL.GetSuppNameByCode(_prcPOHd.SuppCode);
            this.AttnTextBox.Text = _prcPOHd.Attn;
            this.SupplierPONoTextBox.Text = _prcPOHd.SuppPONo;
            this.TermTextBox.Text = _termBL.GetTermNameByCode(_prcPOHd.Term);
            this.ShipmentTextBox.Text = _shipmentBL.GetShipmentNameByCode(_prcPOHd.ShipmentType);
            this.ShipmentNameTextBox.Text = _prcPOHd.ShipmentName;
            this.DeliveryTextBox.Text = _deliveryBL.GetDeliveryNameByCode(_prcPOHd.Delivery);
            this.SubjectTextBox.Text = _prcPOHd.Subject;

            if (_prcPOHd.DeliveryDate != null)
            {
                this.DeliveryDateTextBox.Text = DateFormMapper.GetValue(_prcPOHd.DeliveryDate);
            }
            else
            {
                this.DeliveryDateTextBox.Text = "";
            }

            this.CurrCodeTextBox.Text = _prcPOHd.CurrCode;
            this.ForexRateTextBox.Text = (_prcPOHd.ForexRate == 0) ? "0" : _prcPOHd.ForexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DPPercentTextBox.Text = (_prcPOHd.DP == 0) ? "0" : _prcPOHd.DP.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DPForexTextBox.Text = (_prcPOHd.DPForex == 0) ? "0" : _prcPOHd.DPForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.CurrTextBox.Text = _prcPOHd.CurrCode;
            this.BaseForexTextBox.Text = (_prcPOHd.BaseForex == 0) ? "0" : _prcPOHd.BaseForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscTextBox.Text = (_prcPOHd.Disc == 0) ? "0" : _prcPOHd.Disc.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.DiscForexTextBox.Text = (_prcPOHd.DiscForex == 0) ? "0" : _prcPOHd.DiscForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPHPercentTextBox.Text = (_prcPOHd.PPH == 0) ? "0" : _prcPOHd.PPH.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPHForexTextBox.Text = (_prcPOHd.PPHForex == 0) ? "0" : _prcPOHd.PPHForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNPercentTextBox.Text = (_prcPOHd.PPN == 0) ? "0" : _prcPOHd.PPN.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.PPNForexTextBox.Text = (_prcPOHd.PPNForex == 0) ? "0" : _prcPOHd.PPNForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.TotalForexTextBox.Text = (_prcPOHd.TotalForex == 0) ? "0" : _prcPOHd.TotalForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            if (_prcPOHd.ShippingCurr != null)
            {
                this.ShippingCurrCodeTextBox.Text = _prcPOHd.ShippingCurr;
            }
            else
            {
                this.ShippingCurrCodeTextBox.Text = "";
            }

            decimal _tempShippingRate = (_prcPOHd.ShippingRate == null) ? 0 : Convert.ToDecimal(_prcPOHd.ShippingRate);
            this.ShippingCurrRateTextBox.Text = (_tempShippingRate == 0) ? "0" : _tempShippingRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));

            decimal _tempShippingForex = (_prcPOHd.ShippingForex == null) ? 0 : Convert.ToDecimal(_prcPOHd.ShippingForex);
            this.ShippingForexTextBox.Text = (_tempShippingForex == 0) ? "0" : _tempShippingForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace2));

            this.RemarkTextBox.Text = _prcPOHd.Remark;

            this.StatusLabel.Text = PurchaseOrderDataMapper.GetStatusText(_prcPOHd.Status);
            this.StatusHiddenField.Value = _prcPOHd.Status.ToString();

            this.ShowActionButton();
            this.ShowPreviewButton();
            //this.ShowReviseButton();

            if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                //this.AddButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.AddButton2.Visible = false;
                this.DeleteButton2.Visible = false;
                this.EditButton.Visible = false;
                this.GenerateDetailButton.Visible = false;
            }
            else
            {
                //this.AddButton.Visible = true;
                this.DeleteButton.Visible = true;
                this.AddButton2.Visible = true;
                this.DeleteButton2.Visible = true;
                this.EditButton.Visible = true;
                this.GenerateDetailButton.Visible = true;
            }

            if (_nvcExtractor.GetValue(ApplicationConfig.ActionResult) != "")
            {
                this.WarningLabel.Text = _nvcExtractor.GetValue(ApplicationConfig.ActionResult);
            }

            this.Panel1.Visible = true;
            this.Panel3.Visible = true;
            this.Panel4.Visible = true;
            this.Panel2.Visible = false;
        }

        public void ShowDataDetail1(int _prmRevisiNo)
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
                this.ListRepeater.DataSource = this._purchaseOrderBL.GetListPRCPODt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _prmRevisiNo);
            }
            this.ListRepeater.DataBind();

            //this.AllCheckBox.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox.ClientID + ", " + this.CheckHidden.ClientID + ", '" + _awal + "', '" + _akhir + "', '" + _tempHidden + "' );");

            this.DeleteButton.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PRCPODt _temp = (PRCPODt)e.Item.DataItem;

                string _transNmbr = _temp.TransNmbr;
                string _revisi = _temp.Revisi.ToString();
                string _productCode = _temp.ProductCode;

                string _all = _transNmbr + "-" + _revisi + "-" + _productCode;

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

                ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton");
                _viewButton.PostBackUrl = this._viewDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_productCode, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey));
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
                    if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
                    {
                        _editButton.Visible = false;
                    }
                    else
                    {
                        _editButton.PostBackUrl = this._editDetailPage + "?" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_productCode, ApplicationConfig.EncryptionKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.TransNoTextBox.Text, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey));
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
                    if ((this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower()) && (_temp.DoneClosing.ToString().Trim().ToLower() == PurchaseOrderDataMapper.GetStatusDetail(PurchaseOrderStatusDt.Open).ToString().Trim().ToLower()) && _temp.Qty < ((_temp.QtyRR == null) ? 0 : _temp.QtyRR))
                    {
                        _closeButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/close.jpg";
                        _closeButton.CommandArgument = _productCode;
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

                Literal _productCodeLiteral = (Literal)e.Item.FindControl("ProductCodeLiteral");
                _productCodeLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductCode);

                Literal _productNameLiteral = (Literal)e.Item.FindControl("ProductNameLiteral");
                _productNameLiteral.Text = HttpUtility.HtmlEncode(_temp.ProductName);

                Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyLiteral");
                _qtyLiteral.Text = HttpUtility.HtmlEncode((_temp.Qty == 0) ? "0" : _temp.Qty.ToString("#,###.##"));

                Literal _unitLiteral = (Literal)e.Item.FindControl("UnitLiteral");
                _unitLiteral.Text = HttpUtility.HtmlEncode(_temp.UnitName);

                Literal _priceLiteral = (Literal)e.Item.FindControl("PriceLiteral");
                _priceLiteral.Text = HttpUtility.HtmlEncode((_temp.PriceForex == 0) ? "0" : _temp.PriceForex.ToString("#,###.##"));

                Literal _amountLiteral = (Literal)e.Item.FindControl("AmountLiteral");
                _amountLiteral.Text = HttpUtility.HtmlEncode((_temp.AmountForex == 0) ? "0" : _temp.AmountForex.ToString("#,###.##"));

                Literal _discLiteral = (Literal)e.Item.FindControl("DiscLiteral");
                _discLiteral.Text = HttpUtility.HtmlEncode((_temp.DiscForex == 0) ? "0" : _temp.DiscForex.ToString("#,###.##"));

                Literal _nettoLiteral = (Literal)e.Item.FindControl("NettoLiteral");
                _nettoLiteral.Text = HttpUtility.HtmlEncode((_temp.NettoForex == 0) ? "0" : _temp.NettoForex.ToString("#,###.##"));

                Literal _doneClosingLiteral = (Literal)e.Item.FindControl("DoneClosingLiteral");
                _doneClosingLiteral.Text = PurchaseOrderDataMapper.GetStatusTextDetail(Convert.ToChar(_temp.DoneClosing));
            }
        }

        protected void ListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CloseButton")
            {
                string _result = this._purchaseOrderBL.Closing(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), 0, e.CommandArgument.ToString(), this.DescriptionHiddenField.Value, HttpContext.Current.User.Identity.Name);

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
                this.ShowData(_prmRevisi);
                this.ShowDataDetail1(_prmRevisi);
                this.DescriptionHiddenField.Value = "";
            }
        }

        public void ShowDataDetail2(int _prmRevisiNo)
        {
            this.TempHidden2.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater2.DataSource = this._purchaseOrderBL.GetListDDLPRNoVPRRequestForPO(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _prmRevisiNo);
            }
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");

            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            V_PRRequestForPO _temp = (V_PRRequestForPO)e.Item.DataItem;

            string _transNmbr = _temp.TransNmbr;
            string _revisi = _temp.Revisi.ToString();
            string _requestNo = _temp.PR_No;
            string _all = _transNmbr + "-" + _revisi + "-" + _requestNo;

            if (this.TempHidden2.Value == "")
            {
                this.TempHidden2.Value = _all;
            }
            else
            {
                this.TempHidden2.Value += "," + _all;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral2");
            //_no = 0;
            _no2 += 1;
            //_no = _nomor + _no;
            _noLiteral.Text = _no2.ToString();
            //_nomor += 1;

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox2");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");

            //ImageButton _viewButton = (ImageButton)e.Item.FindControl("ViewButton2");
            ////_viewButton.PostBackUrl = this._viewDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_transNmbr, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_productCode, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey)) + "&" + this._requestNoKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_requestNo, ApplicationConfig.EncryptionKey));
            //_viewButton.PostBackUrl = this._viewDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_transNmbr, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey)) + "&" + this._requestNoKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_requestNo, ApplicationConfig.EncryptionKey));
            //_viewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";

            //ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton2");
            //if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(PurchaseOrderStatus.Posted).ToString().ToLower())
            //{
            //    _editButton.Visible = false;
            //}
            //else
            //{
            //    //_editButton.PostBackUrl = this._editDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_transNmbr, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_productCode, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey)) + "&" + this._requestNoKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_requestNo, ApplicationConfig.EncryptionKey));
            //    _editButton.PostBackUrl = this._editDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_transNmbr, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_revisi, ApplicationConfig.EncryptionKey)) + "&" + this._requestNoKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_requestNo, ApplicationConfig.EncryptionKey));
            //    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
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

            Literal _requestNoLiteral = (Literal)e.Item.FindControl("RequestNoLiteral");
            _requestNoLiteral.Text = _temp.FileNmbr;

            Literal _requestByLiteral = (Literal)e.Item.FindControl("RequestByLiteral");
            _requestByLiteral.Text = HttpUtility.HtmlEncode(_temp.RequestBy);
        }

        protected void RevisiDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabel();
            this._prmRevisi = 0;
            this.ShowData(_prmRevisi);
            this.ShowDataDetail1(_prmRevisi);
            this.ShowDataDetail2(_prmRevisi);
        }

        protected void PreviewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            this.Panel2.Visible = true;

            String _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            this.ReportViewer1.LocalReport.AddTrustedCodeModuleInCurrentAppDomain(ApplicationConfig.CommonDLL);

            ReportDataSource _reportDataSource1 = this._reportPurchaseBL.PurchaseOrderPrintPreview(_transNo);

            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);

            string _path = _reportListBL.GetPathPrintPreview(AppModule.GetValue(TransactionType.PurchaseOrder), _userBL.GetCompanyId(HttpContext.Current.User.Identity.Name));

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
            Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisi)));
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ActionButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.OnHold).ToString().ToLower())
            {
                string _result = this._purchaseOrderBL.GetApproval(this.TransNoTextBox.Text, 0, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.WaitingForApproval).ToString().ToLower())
            {
                string _result = this._purchaseOrderBL.Approve(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), 0, HttpContext.Current.User.Identity.Name);
                
                this.ApproveRow.Visible = false;

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Approved).ToString().ToLower())
            {
                string _result = this._purchaseOrderBL.Posting(this.TransNoTextBox.Text, 0, HttpContext.Current.User.Identity.Name);

                this.Label.Text = _result;
            }
            else if (this.StatusHiddenField.Value.Trim().ToLower() == PurchaseOrderDataMapper.GetStatus(TransStatus.Posted).ToString().ToLower())
            {
                this.ReasonPanel.Visible = true;
                //string _result = this._purchaseOrderBL.Unposting(this.TransNoTextBox.Text, 0, HttpContext.Current.User.Identity.Name);

                //this.Label.Text = _result;
            }

            this._prmRevisi = 0;
            this.ShowData(_prmRevisi);
            this.ShowDataDetail1(_prmRevisi);
            this.ShowDataDetail2(_prmRevisi);
        }

        protected void ReviseButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            string _result = this._purchaseOrderBL.Revisi(_transNo, HttpContext.Current.User.Identity.Name);

            if (_result == "")
            {
                this.Label.Text = "Revisi Successfully created";

                this._prmRevisi = _purchaseOrderBL.GetNewRevisiByCode(_transNo);
                //this._prmRevisi = _purchaseOrderBL.GetNewRevisiByCode(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                //this.ShowRevisiDDL();
                this.ShowData(_prmRevisi);
                this.ShowDataDetail1(this._prmRevisi);
                this.ShowDataDetail2(this._prmRevisi);
            }
            else
            {
                this.ClearLabel();
                this.Label.Text = _result;
            }
        }

        //protected void AddButton_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect(this._addDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(this.RevisiDropDownList.SelectedValue, ApplicationConfig.EncryptionKey)));
        //}

        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._purchaseOrderBL.DeleteMultiPRCPODt(_tempSplit);

            if (_result == true)
            {
                this.WarningLabel2.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel2.Text = "Delete Failed";
            }

            this.CheckHidden.Value = "";
            this.AllCheckBox.Checked = false;

            this._prmRevisi = 0;
            this.ShowData(_prmRevisi);
            this.ShowDataDetail1(_prmRevisi);
            this.ShowDataDetail2(_prmRevisi);
        }

        protected void AddButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._addDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(0.ToString(), ApplicationConfig.EncryptionKey)));
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._purchaseOrderBL.DeleteMultiPRCPODt2(_tempSplit);

            if (_result == true)
            {
                this.WarningLabel2.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel2.Text = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;

            this._prmRevisi = 0;
            this.ShowData(_prmRevisi);
            this.ShowDataDetail1(_prmRevisi);
            this.ShowDataDetail2(_prmRevisi);
        }

        protected void DeleteHeaderButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string[] _tempSplit = new string[1];
            string _error = "";
            string _page = "0";

            this.ClearLabel();

            _tempSplit[0] = _transNo + "- 0";

            bool _result = this._purchaseOrderBL.DeleteMultiPRCPOHd(_tempSplit);

            if (_result == true)
            {
                _error = "Delete Success";

                Response.Redirect(this._homePage + "?" + ApplicationConfig.RequestPageKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_page, ApplicationConfig.EncryptionKey)) + "&" + ApplicationConfig.ActionResult + "=" + _error);
            }
            else
            {
                this.Label.Text = "Delete Failed";
                this._prmRevisi = _purchaseOrderBL.GetNewRevisiByCode(_transNo);
                //this.ShowRevisiDDL();
                this.ShowData(_prmRevisi);
            }
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

            string _result = this._purchaseOrderBL.Approve(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), 0, HttpContext.Current.User.Identity.Name);
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

            this._prmRevisi = 0;
            this.ShowData(_prmRevisi);
            this.ShowDataDetail1(_prmRevisi);
            this.ShowDataDetail2(_prmRevisi);
        }

        protected void NotApproveForceButton_Click(object sender, EventArgs e)
        {
            this.ApproveRow.Visible = false;
            this.ClearLabel();
            this._prmRevisi = 0;
            this.ShowData(_prmRevisi);
            this.ShowDataDetail1(_prmRevisi);
            this.ShowDataDetail2(_prmRevisi);
        }

        protected void GenerateDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();

            bool _result = this._purchaseOrderBL.GeneratePODt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), this._prmRevisi);

            if (_result == true)
            {
                this.WarningLabel.Text = "Generate Success";
                this.ShowData(_prmRevisi);
                this.SetAttribute();
                this.ShowDataDetail1(_prmRevisi);
                this.ShowDataDetail2(_prmRevisi);
            }
            else
            {
                this.WarningLabel.Text = "Generate Failed";
            }

            this._prmRevisi = 0;
        }

        protected void YesButton_OnClick(object sender, EventArgs e)
        {
            DateTime _transdate = DateFormMapper.GetValue(this.DateTextBox.Text);
            int _year = _transdate.Year;
            int _period = _transdate.Month;

            string _result = this._purchaseOrderBL.Unposting(this.TransNoTextBox.Text, 0, HttpContext.Current.User.Identity.Name);

            //this.Label.Text = _result;

            if (_result == "Unposting Success")
            {
                bool _result1 = _unpostingActivitiesBL.InsertUnpostingActivities(AppModule.GetValue(TransactionType.PurchaseOrder), this.TransNoTextBox.Text, this.FileNmbrTextBox.Text, HttpContext.Current.User.Identity.Name, this.ReasonTextBox.Text, Convert.ToByte(ActivityTypeDataMapper.GetActivityType(ActivityType.UnPosting)));
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

            this._prmRevisi = 0;
            this.ShowData(_prmRevisi);
            this.ShowDataDetail1(_prmRevisi);
            this.ShowDataDetail2(_prmRevisi);
        }

        protected void NoButton_OnClick(object sender, EventArgs e)
        {
            this.ReasonPanel.Visible = false;
        }
    }
}
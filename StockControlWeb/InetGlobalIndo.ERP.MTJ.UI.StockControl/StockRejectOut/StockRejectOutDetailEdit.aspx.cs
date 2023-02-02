using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using System.Web.UI.WebControls;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectOut
{
    public partial class StockRejectOutDetailEdit : StockRejectOutBase
    {
        private StockRejectOutBL _stockRejectOutBL = new StockRejectOutBL();
        private PurchaseReturBL _purchaseReturBL = new PurchaseReturBL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private UnitBL _unitBL = new UnitBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowLocation();
                this.ClearLabel();

                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowLocation()
        {
            STCRejectOutHd _stcRejectOutHd = this._stockRejectOutBL.GetSingleSTCRejectOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.LocationDropDownList.Items.Clear();
            this.LocationDropDownList.DataTextField = "WLocationName";
            this.LocationDropDownList.DataValueField = "WLocationCode";
            this.LocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(_stcRejectOutHd.WrhsCode);
            this.LocationDropDownList.DataBind();
            this.LocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            //string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCRejectOutHd _stcRejectOutHd = this._stockRejectOutBL.GetSingleSTCRejectOutHd(_transNo);
            //STCRejectOutDt _stcRejectOutDt = this._stockRejectOutBL.GetSingleSTCRejectOutDt(_transNo, _productCode, _locationCode);
            STCRejectOutDt _stcRejectOutDt = this._stockRejectOutBL.GetSingleSTCRejectOutDt(_transNo, _productCode);
            MsProduct _msProduct = this._productBL.GetSingleProduct(_stcRejectOutDt.ProductCode);

            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_productCode);
            this.LocationDropDownList.SelectedValue = _stcRejectOutDt.LocationCode;
            this.QtyTextBox.Text = (_stcRejectOutDt.Qty == 0) ? "0" : _stcRejectOutDt.Qty.ToString("#,##0.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
            this.PriceTextBox.Text = Convert.ToDecimal(_stcRejectOutDt.PriceCost).ToString("#,##0.##");
            decimal _total = _stcRejectOutDt.PriceCost == null ? 0 : Convert.ToDecimal(_stcRejectOutDt.PriceCost) * _stcRejectOutDt.Qty;
            this.TotalPriceTextBox.Text = _total.ToString("#,##0.##");
            this.RemarkTextBox.Text = _stcRejectOutDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _refCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._referenceKey), ApplicationConfig.EncryptionKey);
            //string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            //STCRejectOutDt _stcRejectOutDt = this._stockRejectOutBL.GetSingleSTCRejectOutDt(_transNo, _productCode, _locationCode);
            STCRejectOutDt _stcRejectOutDt = this._stockRejectOutBL.GetSingleSTCRejectOutDt(_transNo, _productCode);

            _stcRejectOutDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcRejectOutDt.Remark = this.RemarkTextBox.Text;
            _stcRejectOutDt.LocationCode = this.LocationDropDownList.SelectedValue;

            PRCReturDt _prcReturDt = this._purchaseReturBL.GetSinglePRCReturDtForStcRejOut(_refCode, _productCode);

            if (_prcReturDt != null)
            {
                decimal _qty = _prcReturDt.Qty;
                decimal _qtySJ = _prcReturDt.QtySJ == null ? 0 : Convert.ToDecimal(_prcReturDt.QtySJ);
                decimal _qtyClose = _prcReturDt.QtyClose == null ? 0 : Convert.ToDecimal(_prcReturDt.QtyClose);
                decimal _totalQty = _qty - _qtyClose - _qtySJ;

                if (_totalQty >= Convert.ToDecimal(this.QtyTextBox.Text))
                {
                    bool _result = this._stockRejectOutBL.EditSTCRejectOutDt(_stcRejectOutDt);

                    if (_result == true)
                    {
                        Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                    }
                    else
                    {
                        this.ClearLabel();
                        this.WarningLabel.Text = "Your Failed Add Data";
                    }
                }
                else
                {
                    //this.WarningLabel.Text = "Qty must be <= " + _totalQty.ToString("#,#0");
                    this.WarningLabel.Text = "Qty more than in " + _refCode;
                }

            }
            else
            {
                this.WarningLabel.Text = "Product is not Exist In " + _refCode + " or product is already retur";
            }



            //PRCReturDt _prcReturDt = this._purchaseReturBL.GetSinglePRCReturDt(_refCode, _productCode);
            //Decimal _totalQty = _prcReturDt.Qty - (_prcReturDt.QtySJ == null ? 0 : Convert.ToDecimal(_prcReturDt.QtySJ)) - (_prcReturDt.QtyClose == null ? 0 : Convert.ToDecimal(_prcReturDt.QtyClose));

            //if (_totalQty >= Convert.ToDecimal(this.QtyTextBox.Text))
            //{
            //    bool _result = this._stockRejectOutBL.EditSTCRejectOutDt(_stcRejectOutDt);

            //    if (_result == true)
            //    {
            //        Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            //    }
            //    else
            //    {
            //        this.ClearLabel();
            //        this.WarningLabel.Text = "Your Failed Edit Data";
            //    }
            //}
            //else
            //{
            //    this.ClearLabel();
            //    this.WarningLabel.Text = "Qty more than in Purchase Retur";
            //}

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}
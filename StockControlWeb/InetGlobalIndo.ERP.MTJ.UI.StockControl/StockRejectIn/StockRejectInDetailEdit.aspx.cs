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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockRejectIn
{
    public partial class StockRejectInDetailEdit : StockRejectInBase
    {
        private StockRejectInBL _stockRejectInBL = new StockRejectInBL();
        private StockRejectOutBL _stockRejectOutBL = new StockRejectOutBL();
        private ProductBL _product = new ProductBL();
        private WarehouseBL _warehouse = new WarehouseBL();
        private UnitBL _unit = new UnitBL();
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

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCRejectInHd _stcRejectInHd = this._stockRejectInBL.GetSingleSTCRejectInHd(_transNo);
            STCRejectInDt _stcRejectInDt = this._stockRejectInBL.GetSingleSTCRejectInDt(_transNo, _productCode, _locationCode);
            MsProduct _msProduct = this._product.GetSingleProduct(_stcRejectInDt.ProductCode);

            this.ProductTextBox.Text = _product.GetProductNameByCode(_productCode);
            this.LocationTextBox.Text = _warehouse.GetWarehouseLocationNameByCode(_locationCode);
            this.QtyTextBox.Text = (_stcRejectInDt.Qty == 0) ? "0" : _stcRejectInDt.Qty.ToString("#,##0.##");
            this.UnitTextBox.Text = _unit.GetUnitNameByCode(_msProduct.Unit);
            this.RemarkTextBox.Text = _stcRejectInDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCRejectInDt _stcRejectInDt = this._stockRejectInBL.GetSingleSTCRejectInDt(_transNo, _productCode, _locationCode);

            _stcRejectInDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcRejectInDt.Remark = this.RemarkTextBox.Text;

            String _reffRejOut = this._stockRejectInBL.GetSingleSTCRejectInHd(_transNo).TransReff;
            STCRejectInDt _stcRejectInDt2 = this._stockRejectInBL.GetSingleSTCRejectInDtExist(_transNo, _productCode);
            STCRejectOutDt _stcRejectOutDt = this._stockRejectOutBL.GetSingleSTCRejectOutDtForStcRejIn(_reffRejOut, _productCode);


            if (_stcRejectOutDt != null)
            {
                decimal _qty = _stcRejectOutDt.Qty;
                decimal _qtyIn = _stcRejectOutDt.QtyIn == null ? 0 : Convert.ToDecimal(_stcRejectOutDt.QtyIn);
                decimal _totalQty = _qty - _qtyIn;

                if (_totalQty >= Convert.ToDecimal(this.QtyTextBox.Text))
                {
                    bool _result = this._stockRejectInBL.EditSTCRejectInDt(_stcRejectInDt);

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
                    this.WarningLabel.Text = "Qty more than in " + _reffRejOut;
                }
            }
            else
            {
                this.WarningLabel.Text = "Product is not Exist In " + _reffRejOut + " Or Product is Already Retur";
            }


            //bool _result = this._stockRejectInBL.EditSTCRejectInDt(_stcRejectInDt);

            //if (_result == true)
            //{
            //    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            //}
            //else
            //{
            //    this.ClearLabel();
            //    this.WarningLabel.Text = "Your Failed Edit Data";
            //}
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
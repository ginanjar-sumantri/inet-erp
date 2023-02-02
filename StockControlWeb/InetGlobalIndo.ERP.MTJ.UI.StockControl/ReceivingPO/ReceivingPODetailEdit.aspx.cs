using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO
{
    public partial class ReceivingPODetailEdit : ReceivingPOBase
    {
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private ProductBL _productBL = new ProductBL();
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
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCReceiveDt _stcReceiveDt = this._receivingPOBL.GetSingleSTCReceiveDt(_transNo, _productCode, _locationCode);

            this.QtyTextBox.Text = (_stcReceiveDt.Qty == 0) ? "0" : _stcReceiveDt.Qty.ToString("#,##0.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_stcReceiveDt.Unit);
            this.RemarkTextBox.Text = _stcReceiveDt.Remark;
            this.ProductTextBox.Text = _stcReceiveDt.ProductCode + " - " + _productBL.GetProductNameByCode(_stcReceiveDt.ProductCode);
            this.LocationTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_stcReceiveDt.LocationCode);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            string _transNoPO = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey2), ApplicationConfig.EncryptionKey);

            _receivingPOBL = new ReceivingPOBL();

            STCReceiveDt _stcReceiveDt = this._receivingPOBL.GetSingleDt(_transNo, _productCode, _locationCode);

            _stcReceiveDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcReceiveDt.Remark = this.RemarkTextBox.Text;

            PRCPODt _prcPODt = _purchaseOrderBL.GetSinglePRCPODt(_transNoPO, 0, _productCode);

            decimal _qty = _prcPODt.Qty;
            decimal _qtyRR = _prcPODt.QtyRR == null ? 0 : Convert.ToDecimal(_prcPODt.QtyRR);
            decimal _qtyClose = _prcPODt.QtyClose == null ? 0 : Convert.ToDecimal(_prcPODt.QtyClose);
            decimal _totalQty = _qty - _qtyClose - _qtyRR;

            if (_totalQty >= Convert.ToDecimal(this.QtyTextBox.Text))
            {
                String _result = this._receivingPOBL.EditSTCReceiveDt(_stcReceiveDt);

                if (_result != "")
                {
                    if (_result.Substring(0, 5) != ApplicationConfig.Error)
                    {
                        this.WarningLabel.Text = _result.Substring(6, _result.Length - 6);
                    }
                }
                else
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
            }
            else
            {
                this.WarningLabel.Text = "Qty more than in " + _transNoPO;
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._locationKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            _receivingPOBL = new ReceivingPOBL();

            STCReceiveDt _stcReceiveDt = this._receivingPOBL.GetSingleDt(_transNo, _productCode, _locationCode);

            _stcReceiveDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcReceiveDt.Remark = this.RemarkTextBox.Text;

            String _result = this._receivingPOBL.EditSTCReceiveDt(_stcReceiveDt);

            if (_result != "")
            {
                if (_result.Substring(0, 5) != ApplicationConfig.Error)
                {
                    this.WarningLabel.Text = _result.Substring(6, _result.Length - 6);
                }
            }
            else
            {
                Response.Redirect(this._viewDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._locationKey)));
            }
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRetur
{
    public partial class PurchaseReturDetailEdit : PurchaseReturBase
    {
        private PurchaseReturBL _purchaseReturBL = new PurchaseReturBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private ReceivingPOBL _rcvPOBL = new ReceivingPOBL();
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

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void SetAttribute()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            //string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            //decimal _qtyRemain = (_rcvPOBL.GetQtyFromReceiveDtForRR(_transNo, _productCode, _locationCode) == 0) ? 0 : _rcvPOBL.GetQtyFromReceiveDtForRR(_transNo, _productCode, _locationCode);
            //this.QtyTextBox.Attributes.Add("OnKeyDown", "return ValidateQtyRemain(" + this.QtyTextBox.ClientID + "," + _qtyRemain + ");");
            this.UnitTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountForexTextBox.Attributes.Add("ReadOnly", "True");
            this.AmountForexTextBox.Attributes.Add("Backcolor", "#CCCCCC");
            //this.QtyTextBox.Attributes.Add("OnBlur", "return ValidateQtyRemain(" + this.QtyTextBox.ClientID + "," + this.QtyRemainHidden.ClientID + ");");
            this.QtyTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.QTYSJTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
            //this.QTYCloseTextBox.Attributes.Add("OnKeyDown", "return NumericWithDot();");
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

            PRCReturDt _prcReturDt = this._purchaseReturBL.GetSinglePRCReturDt(_transNo, _productCode);

            this.ProductTextBox.Text = _productCode + " - " + _productBL.GetProductNameByCode(_prcReturDt.ProductCode);
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_prcReturDt.Unit);
            this.QtyTextBox.Text = (_prcReturDt.Qty == 0) ? "0" : _prcReturDt.Qty.ToString("#,###.##");
            //this.QTYSJTextBox.Text = (_prcReturDt.QtySJ == 0) ? "0" : Convert.ToDecimal(_prcReturDt.QtySJ).ToString("#,###.##");
            //this.QTYCloseTextBox.Text = (_prcReturDt.QtyClose == 0) ? "0" : Convert.ToDecimal(_prcReturDt.QtyClose).ToString("#,###.##");
            this.PriceTextBox.Text = (_prcReturDt.Price == 0) ? "0" : Convert.ToDecimal(_prcReturDt.Price).ToString("#,###.##");
            this.AmountForexTextBox.Text = (_prcReturDt.AmountForex == 0) ? "0" : Convert.ToDecimal(_prcReturDt.AmountForex).ToString("#,###.##");
            this.RemarkTextBox.Text = _prcReturDt.Remark;
            //this.LocationTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_prcReturDt.LocationCode);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            PRCReturDt _prcReturDt = this._purchaseReturBL.GetSinglePRCReturDt(_transNo, _productCode);
            PRCReturHd _prcReturHd = this._purchaseReturBL.GetSinglePRCReturHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            //if (Convert.ToDecimal(this.QtyTextBox.Text) <= _rcvPOBL.GetQtyFromReceiveDtForRR(_prcReturHd.RRNo, _productCode, _locationCode))
            //{
            _prcReturDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            //_prcReturDt.QtySJ = Convert.ToDecimal(this.QTYSJTextBox.Text);
            //_prcReturDt.QtyClose = Convert.ToDecimal(this.QTYCloseTextBox.Text);
            _prcReturDt.Price = Convert.ToDecimal(this.PriceTextBox.Text);
            _prcReturDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _prcReturDt.Remark = this.RemarkTextBox.Text;
            //_prcReturDt.EditBy = HttpContext.Current.User.Identity.Name;
            //_prcReturDt.EditDate = DateTime.Now;

            bool _result = this._purchaseReturBL.EditPRCReturDt(_prcReturDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
            //}
            //else
            //{
            //    this.WarningLabel.Text = "Qty more than Qty in Receiving PO";
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

        protected void QtyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.QtyTextBox.Text == "")
                this.QtyTextBox.Text = "0";
            //if (this.QTYSJTextBox.Text == "")
            //    this.QTYSJTextBox.Text = "0";
            //if (this.QTYCloseTextBox.Text == "")
            //    this.QTYCloseTextBox.Text = "0";

            this.QtyTextBox.Text = Convert.ToDecimal(this.QtyTextBox.Text).ToString("#,#0.##");
        }

        protected void PriceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.PriceTextBox.Text == "")
                this.PriceTextBox.Text = "0";

            this.AmountForexTextBox.Text = (Convert.ToDecimal(this.QtyTextBox.Text) * Convert.ToDecimal(this.PriceTextBox.Text)).ToString("#,#0.##");
        }
    }
}
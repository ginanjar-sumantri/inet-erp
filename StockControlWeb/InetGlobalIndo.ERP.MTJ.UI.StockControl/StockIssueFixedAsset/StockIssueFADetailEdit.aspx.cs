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

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.StockIssueFixedAsset
{
    public partial class StockIssueToFADetailEdit : StockIssueFixedAssetBase
    {
        private StockIssueFixedAssetBL _stockIssueFABL = new StockIssueFixedAssetBL();
        private StockIssueRequestBL _stockIssueReqBL = new StockIssueRequestBL();
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

                this.ClearLabel();
                this.SetAttribute();
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
            this.QtyTextBox.Attributes.Add("OnBlur", "return ChangeFormat(" + this.QtyTextBox.ClientID + ");");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCIssueToFAHd _stcIssueToFAHd = this._stockIssueFABL.GetSingleSTCIssueToFAHd(_transNo);
            STCIssueToFADt _stcIssueToFADt = this._stockIssueFABL.GetSingleSTCIssueToFADt(_transNo, _productCode, _locationCode);
            MsProduct _msProduct = this._productBL.GetSingleProduct(_stcIssueToFADt.ProductCode);

            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_productCode);
            this.LocationTextBox.Text = _warehouseBL.GetWarehouseLocationNameByCode(_locationCode);
            this.QtyTextBox.Text = (_stcIssueToFADt.Qty == 0) ? "0" : _stcIssueToFADt.Qty.ToString("#,##0.##");
            this.UnitTextBox.Text = _unitBL.GetUnitNameByCode(_msProduct.Unit);
            this.RemarkTextBox.Text = _stcIssueToFADt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);

            STCIssueToFADt _stcIssueToFADt = this._stockIssueFABL.GetSingleSTCIssueToFADt(_transNo, _productCode, _locationCode);

            _stcIssueToFADt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcIssueToFADt.Remark = this.RemarkTextBox.Text;

            bool _result = this._stockIssueFABL.EditSTCIssueToFADt(_stcIssueToFADt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
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
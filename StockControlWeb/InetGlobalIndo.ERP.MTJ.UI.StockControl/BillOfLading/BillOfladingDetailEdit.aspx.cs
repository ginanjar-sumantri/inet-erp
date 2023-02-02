using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public partial class BillOfladingDetailEdit : BillOfLadingBase
    {
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private SalesOrderBL _salesOrderBL = new SalesOrderBL();
        private DeliveryOrderBL _deliveryOrderBL = new DeliveryOrderBL();
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
            this.QtyTextBox.Attributes.Add("OnBlur", "ChangeFormat(" + QtyTextBox.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowData()
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            string _DONoCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._DOKey), ApplicationConfig.EncryptionKey);
            //string _wrhsCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsCode), ApplicationConfig.EncryptionKey);

            STCSJDt _stcSJDt = this._billOfLadingBL.GetSingleSTCSJDt(_transNmbr, _DONoCode, _productCode, _locationCode);

            this.DONoTextBox.Text = this._deliveryOrderBL.GetFileNmbrMKTDOHd(_stcSJDt.DONo);
            this.ProductCodeTextBox.Text = _stcSJDt.ProductCode;
            this.ProductNameTextBox.Text = this._productBL.GetProductNameByCode(_stcSJDt.ProductCode);
            this.LocationNameTextBox.Text = this._warehouseBL.GetWarehouseLocationNameByCode(_stcSJDt.LocationCode);
            this.QtyTextBox.Text = (_stcSJDt.Qty == 0) ? "0" : _stcSJDt.Qty.ToString("#,##0.##");
            this.UnitNameTextBox.Text = this._unitBL.GetUnitNameByCode(_stcSJDt.Unit);
            this.RemarkTextBox.Text = _stcSJDt.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _productNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);
            string _locationNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._locationKey), ApplicationConfig.EncryptionKey);
            string _doNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._DOKey), ApplicationConfig.EncryptionKey);
            //string _wrhsCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._wrhsCode), ApplicationConfig.EncryptionKey);

            STCSJDt _stcSJDt = this._billOfLadingBL.GetSingleSTCSJDt(_transNmbr, _doNo, _productNo, _locationNo);

            _stcSJDt.Qty = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcSJDt.QtyReceive = Convert.ToDecimal(this.QtyTextBox.Text);
            _stcSJDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._billOfLadingBL.EditSTCSJDt(_stcSJDt);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.BillOfLading
{
    public partial class BillOfLadingDetailView : BillOfLadingBase
    {
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private BillOfLadingBL _billOfLadingBL = new BillOfLadingBL();
        private ProductBL _productBL = new ProductBL();
        private UnitBL _unitBL = new UnitBL();
        private DeliveryOrderBL _deliveryOrderBL = new DeliveryOrderBL();
        private PermissionBL _permBL = new PermissionBL();

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

                STCSJHd _stcSJHd = this._billOfLadingBL.GetSingleSTCSJHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_stcSJHd.Status != BillOfLadingDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
                this.SetButtonPermission();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
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

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._DOKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._DOKey)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)) + "&" + this._locationKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._locationKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}
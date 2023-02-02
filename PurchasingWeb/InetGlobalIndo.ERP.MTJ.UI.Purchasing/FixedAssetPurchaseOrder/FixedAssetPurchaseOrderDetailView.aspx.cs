using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.FixedAssetPurchaseOrder
{
    public partial class FixedAssetPurchaseOrderDetailView : FixedAssetPurchaseOrderBase
    {
        private FixedAssetPurchaseOrderBL _faPOBL = new FixedAssetPurchaseOrderBL();
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

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

                PRCFAPOHd _prcFAPOHd = this._faPOBL.GetSinglePRCFAPOHd(_transNo);
                if (_prcFAPOHd.Status == FixedAssetPurchaseOrderDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.Visible = false;
                }
                else
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowData();
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

        public void ShowData()
        {
            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _faName = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._faKey), ApplicationConfig.EncryptionKey);

            PRCFAPODt _prcFAPODt = this._faPOBL.GetSinglePRCFAPODt(_transNo, _faName);

            this.ProductTextBox.Text = _faName + " - " + this._productBL.GetProductNameByCode(_faName);

            this.SpecificationTextBox.Text = _prcFAPODt.Specification;
            this.QtyConvertionTextBox.Text = (_prcFAPODt.Qty == 0) ? "0" : _prcFAPODt.Qty.ToString("#,###.##");
            this.UnitConvertionTextBox.Text = this._unitBL.GetUnitNameByCode(_prcFAPODt.Unit);

            this.PriceTextBox.Text = (_prcFAPODt.PriceForex == 0) ? "0" : _prcFAPODt.PriceForex.ToString("#,###.##");
            this.AmountTextBox.Text = (_prcFAPODt.AmountForex == 0) ? "0" : _prcFAPODt.AmountForex.ToString("#,###.##");
            this.RemarkTextBox.Text = _prcFAPODt.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._faKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._faKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}
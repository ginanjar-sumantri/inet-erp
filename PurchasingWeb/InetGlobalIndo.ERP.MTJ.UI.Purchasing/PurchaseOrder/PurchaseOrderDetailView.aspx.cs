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

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder
{
    public partial class PurchaseOrderDetailView : PurchaseOrderBase
    {
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
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
                string _revisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisi), ApplicationConfig.EncryptionKey);

                PRCPOHd _prcPOHd = this._purchaseOrderBL.GetSinglePRCPOHd(_transNo, Convert.ToInt32(_revisi));
                if (_prcPOHd.Status == PurchaseOrderDataMapper.GetStatus(TransStatus.Posted))
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
            string _revisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisi), ApplicationConfig.EncryptionKey);
            string _productCode = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._productKey), ApplicationConfig.EncryptionKey);

            PRCPODt _prcPODt = this._purchaseOrderBL.GetSinglePRCPODt(_transNo, Convert.ToInt32(_revisi), _productCode);

            this.ProductTextBox.Text = _productCode + " - " + this._productBL.GetProductNameByCode(_productCode);

            this.SpecificationTextBox.Text = _prcPODt.Specification;

            if (_prcPODt.ETD != null)
            {
                this.ETDTextBox.Text = DateFormMapper.GetValue(_prcPODt.ETD);
            }
            else
            {
                this.ETDTextBox.Text = "";
            }

            if (_prcPODt.ETA != null)
            {
                this.ETATextBox.Text = DateFormMapper.GetValue(_prcPODt.ETA);
            }
            else
            {
                this.ETATextBox.Text = "";
            }

            this.QtyTextBox.Text = (_prcPODt.QtyWrhsPO == 0) ? "0" : _prcPODt.QtyWrhsPO.ToString("#,###.##");
            this.QtyFreeTextBox.Text = (_prcPODt.QtyWrhsFree == 0) ? "0" : _prcPODt.QtyWrhsFree.ToString("#,###.##");
            this.QtyTotalTextBox.Text = (_prcPODt.QtyWrhsTotal == 0) ? "0" : _prcPODt.QtyWrhsTotal.ToString("#,###.##");
            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_prcPODt.UnitWrhs);
            this.QtyConvertionTextBox.Text = (_prcPODt.Qty == 0) ? "0" : _prcPODt.Qty.ToString("#,###.##");

            this.UnitConvertionTextBox.Text = this._unitBL.GetUnitNameByCode(_prcPODt.Unit);

            this.PriceTextBox.Text = (_prcPODt.PriceForex == 0) ? "0" : _prcPODt.PriceForex.ToString("#,###.##");
            this.AmountTextBox.Text = (_prcPODt.AmountForex == 0) ? "0" : _prcPODt.AmountForex.ToString("#,###.##");
            this.DiscPercentTextBox.Text = (_prcPODt.Disc == 0) ? "0" : _prcPODt.Disc.ToString("#,###.##");
            this.DiscTextBox.Text = (_prcPODt.DiscForex == 0) ? "0" : _prcPODt.DiscForex.ToString("#,###.##");
            this.NettoTextBox.Text = (_prcPODt.NettoForex == 0) ? "0" : _prcPODt.NettoForex.ToString("#,###.##");
            this.RemarkTextBox.Text = _prcPODt.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisi)) + "&" + this._productKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._productKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisi)));
        }
    }
}
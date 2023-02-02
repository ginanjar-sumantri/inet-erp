using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAAddStock
{
    public partial class FAAddStockDetailEdit : FAAddStockBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private StockChangeToFABL _faAddStockBL = new StockChangeToFABL();
        private ProductBL _productBL = new ProductBL();
        private WarehouseBL _wrhsBL = new WarehouseBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ClearLabel();
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
            else
            {
                GLFAAddStockHd _glFAAddStockHd = this._faAddStockBL.GetSingleFAAddStockHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_glFAAddStockHd.Status != FAAddStockDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            GLFAAddStockDt _glFAAddStockDt = this._faAddStockBL.GetSingleFAAddStockDt(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey)));
            GLFAAddStockDt2 _glFAAddStockDt2 = this._faAddStockBL.GetSingleFAAddStockDt2(new Guid(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._code), ApplicationConfig.EncryptionKey)));

            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_glFAAddStockDt.ProductCode);
            this.LocationTextBox.Text = _wrhsBL.GetWarehouseLocationNameByCode(_glFAAddStockDt.LocationCode);
            if (_glFAAddStockDt2 != null)
            {
                byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAAddStockDt2.CurrCode);
                this.FACodeTextBox.Text = _glFAAddStockDt2.FACode;
                this.FANameTextBox.Text = _glFAAddStockDt2.FAName;
                this.FAStatusTextBox.Text = _fixedAssetBL.GetFAStatusSubNameByCode(_glFAAddStockDt2.FAStatus);
                this.FAOwnerCheckBox.Checked = FAAddStockDataMapper.IsChecked(_glFAAddStockDt2.FAOwner);
                this.FASubGroupTextBox.Text = _fixedAssetBL.GetFAGroupSubNameByCode(_glFAAddStockDt2.FASubGroup);
                this.FALocationTypeTextBox.Text = _glFAAddStockDt2.FALocationType;
                if (_glFAAddStockDt2.FALocationCode != null && _glFAAddStockDt2.FALocationCode != "")
                {
                    this.FALocationTextBox.Text = _fixedAssetBL.GetFALocNameByLocTypeAndCode(FixedAssetsDataMapper.GetValueFALocation(_glFAAddStockDt2.FALocationType), _glFAAddStockDt2.FALocationCode).Name;
                }
                this.LifeMonthTextBox.Text = (_glFAAddStockDt2.LifeMonth == 0) ? "0" : _glFAAddStockDt2.LifeMonth.ToString();
                this.AmountForexTextBox.Text = (_glFAAddStockDt2.AmountForex == 0) ? "0" : _glFAAddStockDt2.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
                this.StatusProcessCheckBox.Checked = FAAddStockDataMapper.IsChecked(_glFAAddStockDt2.FgProcess);
                this.CurrTextBox.Text = _glFAAddStockDt2.CurrCode;
                this.SpecificationTextBox.Text = _glFAAddStockDt2.Spesification;
            }
            else
            {
                this.FACodeTextBox.Text = "";
                this.FANameTextBox.Text = "";
                this.FAStatusTextBox.Text = "";
                this.FAOwnerCheckBox.Checked = false;
                this.FASubGroupTextBox.Text = "";
                this.FALocationTypeTextBox.Text = "";
                this.CurrTextBox.Text = "";
                this.FALocationTextBox.Text = "";
                this.LifeMonthTextBox.Text = "0";
                this.AmountForexTextBox.Text = "0";
                this.StatusProcessCheckBox.Checked = false;
                this.SpecificationTextBox.Text = "";
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._code + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._code)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}
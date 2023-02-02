using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.PriceGroup
{
    public partial class PriceGroupView : PriceGroupBase
    {
        private PriceGroupBL _priceGroupBL = new PriceGroupBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRate = new CurrencyRateBL();

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

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
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

        public void ShowData()
        {
            Master_PriceGroup _msPriceGroup = this._priceGroupBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._yearKey), ApplicationConfig.EncryptionKey)));

            this.PriceGroupCodeTextBox.Text = _msPriceGroup.PriceGroupCode;
            this.YearTextBox.Text = _msPriceGroup.Year.ToString();
            this.CurrTextBox.Text = _msPriceGroup.CurrCode;

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(this.CurrTextBox.Text);

            this.AmountForexTextBox.Text = (_msPriceGroup.AmountForex == 0 ? "0" : _msPriceGroup.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace)));
            this.RemarkTextBox.Text = _msPriceGroup.PGDesc;
            this.IsActiveCheckBox.Checked = _msPriceGroup.FgActive;
            this.StartDateTextBox.Text = DateFormMapper.GetValue(_msPriceGroup.StartDate);
            this.EndDateTextBox.Text = DateFormMapper.GetValue(_msPriceGroup.EndDate);
            this.SellingCurrOWTextBox.Text = _msPriceGroup.SellingCurrOW;
            this.SellingPriceOWTextBox.Text = Convert.ToDecimal(_msPriceGroup.SelingPriceOW).ToString("#,##0.00");
            this.SellingCurrBMTextBox.Text = _msPriceGroup.SellingCurrBM;
            this.SellingPriceBMTextBox.Text = Convert.ToDecimal(_msPriceGroup.SelingPriceBM).ToString("#,##0.00");
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + _yearKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._yearKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAPurchase
{
    public partial class FAPurchaseDetailView : FAPurchaseBase
    {
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private FAPurchaseBL _faPurchaseBL = new FAPurchaseBL();
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
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
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
            else
            {
                char _status = this._faPurchaseBL.GetStatusFAPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                if (_status == FAPurchaseDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.Visible = false;
                }
                else
                {
                    this.EditButton.Visible = true;
                }
            }
        }

        public void ShowData()
        {
            GLFAPurchaseDt _glFAPurchaseDt = this._faPurchaseBL.GetSingleFAPurchaseDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItem), ApplicationConfig.EncryptionKey));
            GLFAPurchaseHd _glFAPurchaseHd = _faPurchaseBL.GetSingleFAPurchaseHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAPurchaseHd.CurrCode);

            this.FACodeTextBox.Text = _glFAPurchaseDt.FACode;
            this.FANameTextBox.Text = _glFAPurchaseDt.FAName;
            this.FAStatusTextBox.Text = this._fixedAssetBL.GetFAStatusSubNameByCode(_glFAPurchaseDt.FAStatus);
            this.FAOwnerCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_glFAPurchaseDt.FAOwner);
            this.FASubGroupTextBox.Text = this._fixedAssetBL.GetFAGroupSubNameByCode(_glFAPurchaseDt.FASubGroup);
            this.FALocationTypeTextBox.Text = _glFAPurchaseDt.FALocationType;
            this.FALocationTextBox.Text = this._fixedAssetBL.GetFALocationNameByCode(_glFAPurchaseDt.FALocationCode);
            this.LifeMonthTextBox.Text = (_glFAPurchaseDt.LifeMonth == 0) ? "0" : _glFAPurchaseDt.LifeMonth.ToString();
            this.QtyTextBox.Text = (_glFAPurchaseDt.Qty == null) ? "0" : Convert.ToDecimal(_glFAPurchaseDt.Qty).ToString("#,##0.##");
            this.PriceForexTextBox.Text = (_glFAPurchaseDt.PriceForex == null) ? "0" : Convert.ToDecimal(_glFAPurchaseDt.PriceForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_glFAPurchaseDt.AmountForex == null) ? "0" : Convert.ToDecimal(_glFAPurchaseDt.AmountForex).ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.StatusProcessCheckBox.Checked = FixedAssetsDataMapper.IsFGAddValue(_glFAPurchaseDt.FgProcess);
            this.SpecificationTextBox.Text = _glFAPurchaseDt.Spesification;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeItem + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItem)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}
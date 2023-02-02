using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAService
{
    public partial class FAServiceDetailView : FAServiceBase
    {
        private FAServiceBL _faServiceBL = new FAServiceBL();
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private UnitBL _unitBL = new UnitBL();
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
        }

        protected void ShowData()
        {
            string _transNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _itemNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey);

            GLFAServiceDt _glFAServiceDt = this._faServiceBL.GetSingleFAServiceDt(_transNmbr, _itemNo);
            GLFAServiceHd _glFAServiceHd = _faServiceBL.GetSingleFAServiceHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currencyBL.GetDecimalPlace(_glFAServiceHd.CurrCode);

            this.FixedAssetMaintenanceTextBox.Text = this._fixedAssetBL.GetFixedAssetMaintenanceNameByCode(_glFAServiceDt.FAMaintenance);
            this.AddValueCheckBox.Checked = FAServiceDataMapper.GetAddValue(_glFAServiceDt.FgAddValue);

            decimal _tempQty = (_glFAServiceDt.Qty == null) ? 0 : Convert.ToDecimal(_glFAServiceDt.Qty);
            this.QtyTextBox.Text = (_tempQty == 0) ? "0" : _tempQty.ToString("#,###.##");

            this.UnitTextBox.Text = this._unitBL.GetUnitNameByCode(_glFAServiceDt.Unit);

            decimal _priceForex = (_glFAServiceDt.PriceForex == null) ? 0 : Convert.ToDecimal(_glFAServiceDt.PriceForex);
            this.PriceForexTextBox.Text = (_priceForex == 0) ? "0" : _priceForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            decimal _amountForex = (_glFAServiceDt.AmountForex == null) ? 0 : Convert.ToDecimal(_glFAServiceDt.AmountForex);
            this.AmountForexTextBox.Text = (_amountForex == 0) ? "0" : _amountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));

            this.RemarkTextBox.Text = _glFAServiceDt.Remark;

            char _status = this._faServiceBL.GetStatusFAServiceHd(_transNmbr);

            if (_status == FAServiceDataMapper.GetStatus(TransStatus.Posted))
            {
                this.EditButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
            }
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + this._codeItemKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeItemKey)) + "&" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}

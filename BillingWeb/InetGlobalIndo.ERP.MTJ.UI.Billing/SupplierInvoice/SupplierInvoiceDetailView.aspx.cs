using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.SupplierInvoice
{
    public partial class SupplierInvoiceDetailView : SupplierInvoiceBase
    {
        private SupplierInvoiceBL _supplierInvoiceBL = new SupplierInvoiceBL();
        private AccountBL _accountBL = new AccountBL();
        private SupplierBL _supplierBL = new SupplierBL();
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

        protected void ShowData()
        {
            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _transNo = this._supplierInvoiceBL.GetTransactionNoFromSupplierInvoiceHd(new Guid(_invServHd));

            string _currCode = this._supplierInvoiceBL.GetCurrCodeFromSupplierInvoiceHd(new Guid(_invServHd));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            string _invServDt = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey);
            Billing_SupplierInvoiceDt _supplierInvoiceDt = this._supplierInvoiceBL.GetSingleSupplierInvoiceDt(new Guid(_invServDt));

            this.AccountCodeTextBox.Text = _supplierInvoiceDt.Account;
            this.AccountNameTextBox.Text = this._accountBL.GetAccountNameByCode(_supplierInvoiceDt.Account);

            this.ItemDescriptionTextBox.Text = _supplierInvoiceDt.ItemDescription;
            this.AmountForexTextBox.Text = (_supplierInvoiceDt.AmountForex == 0) ? "0" : _supplierInvoiceDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _supplierInvoiceDt.Remark;

            char _status = this._supplierInvoiceBL.GetStatusSupplierInvoiceHd(new Guid(_invServHd));

            if (_status == SupplierInvoiceDataMapper.GetStatus(TransStatus.Posted))
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
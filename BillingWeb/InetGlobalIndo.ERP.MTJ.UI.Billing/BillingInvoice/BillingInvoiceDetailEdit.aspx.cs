using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingInvoice
{
    public partial class BillingInvoiceDetailEdit : BillingInvoiceBase
    {
        private BillingInvoiceBL _billingInvoiceBL = new BillingInvoiceBL();
        private CustBillAccountBL _custBillAccountBL = new CustBillAccountBL();
        private AccountBL _accountBL = new AccountBL();
        private ProductBL _productBL = new ProductBL();
        private SubledBL _subledBL = new SubledBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currencyBL = new CurrencyBL();

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

            _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.ClearLabel();
                this.SetAttribute();
                //this.ShowAccount();
                this.ShowCustomerBillAcount();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttributeRate()
        {
            this.AmountForexTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.AmountForexTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        protected void SetAttribute()
        {
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
            this.SetAttributeRate();
        }

        //protected void ShowAccount()
        //{
        //    this.AccountDropDownList.Items.Clear();
        //    this.AccountDropDownList.DataTextField = "AccountName";
        //    this.AccountDropDownList.DataValueField = "Account";
        //    this.AccountDropDownList.DataSource = this._accountBL.GetListForDDL();
        //    this.AccountDropDownList.DataBind();
        //    this.AccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //public void GetSubled()
        //{
        //    char _tempSubled = this._accountBL.GetAccountSubLed(this.AccountDropDownList.SelectedValue);
        //    this.FgSubledHiddenField.Value = _tempSubled.ToString();

        //    this.SubledDropDownList.Items.Clear();
        //    this.SubledDropDownList.DataTextField = "SubLed_Name";
        //    this.SubledDropDownList.DataValueField = "SubLed_No";
        //    this.SubledDropDownList.DataSource = this._subledBL.GetListSubled(Convert.ToChar(this.FgSubledHiddenField.Value));
        //    this.SubledDropDownList.DataBind();
        //    this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        //protected void AccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.AccountDropDownList.SelectedValue != "null")
        //    {
        //        this.AccountTextBox.Text = this.AccountDropDownList.SelectedValue;
        //    }
        //    else
        //    {
        //        this.AccountTextBox.Text = "";
        //    }

        //    this.GetSubled();
        //}

        //protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.AccountDropDownList.SelectedValue = this.AccountTextBox.Text;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.AccountDropDownList.SelectedValue = "null";
        //    }

        //    this.GetSubled();
        //}

        protected void ShowCustomerBillAcount()
        {
            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            string _custCode = this._billingInvoiceBL.GetCustCodeFromBillingInvoiceHd(new Guid(_invServHd));
            string _currCode = this._billingInvoiceBL.GetCurrCodeFromBillingInvoiceHd(new Guid(_invServHd));

            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);
            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);

            this.CustomerBillAccountDropDownList.Items.Clear();
            this.CustomerBillAccountDropDownList.DataTextField = "CustBillAccount";
            this.CustomerBillAccountDropDownList.DataValueField = "CustBillCode";
            this.CustomerBillAccountDropDownList.DataSource = this._custBillAccountBL.GetListDDLCustBillAccount(_custCode, _currCode);
            this.CustomerBillAccountDropDownList.DataBind();
            this.CustomerBillAccountDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.SetAttributeRate();
        }

        protected void ShowData()
        {
            this.ClearLabel();


            string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            string _currCode = this._billingInvoiceBL.GetCurrCodeFromBillingInvoiceHd(new Guid(_invServHd));
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

            string _transNo = this._billingInvoiceBL.GetTransactionNoFromBillingInvoiceHd(new Guid(_invServHd));

            string _invServDt = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey);
            Billing_InvoiceDt _invoiceDt = this._billingInvoiceBL.GetSingleBillingInvoiceDt(new Guid(_invServDt));

            //this.AccountTextBox.Text = _invServiceDt.Account;
            //this.AccountDropDownList.SelectedValue = _invServiceDt.Account;
            //this.GetSubled();
            //this.FgSubledHiddenField.Value = _invServiceDt.FgSubLed.ToString();

            //if (_invServiceDt.SubLed != null)
            //{
            //    this.SubledDropDownList.SelectedValue = _invServiceDt.SubLed;
            //}
            //else
            //{
            //    this.SubledDropDownList.SelectedValue = "null";
            //}

            this.CustomerBillAccountDropDownList.SelectedValue = _invoiceDt.CustBillCode.ToString();
            this.CustomerBillDescriptionTextBox.Text = _invoiceDt.CustBillDescription;
            this.ProductTextBox.Text = _productBL.GetProductNameByCode(_invoiceDt.ProductCode);
            this.AmountForexTextBox.Text = (_invoiceDt.AmountForex == 0) ? "0" : _invoiceDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.RemarkTextBox.Text = _invoiceDt.Remark;
        }

        protected void CustomerBillAccountDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CustomerBillAccountDropDownList.SelectedValue != "null")
            {
                Master_CustBillAccount _custBillAccount = this._custBillAccountBL.GetSingleCustBillAccount(new Guid(this.CustomerBillAccountDropDownList.SelectedValue));

                string _invServHd = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                string _currCode = this._billingInvoiceBL.GetCurrCodeFromBillingInvoiceHd(new Guid(_invServHd));

                byte _decimalPlace = this._currencyBL.GetDecimalPlace(_currCode);

                this.CustomerBillDescriptionTextBox.Text = _custBillAccount.CustBillDescription;
                this.ProductTextBox.Text = _productBL.GetProductNameByCode(_custBillAccount.ProductCode);
                var _amountForex = Convert.ToDecimal((_custBillAccount.AmountForex == null) ? 0 : _custBillAccount.AmountForex);
                this.AmountForexTextBox.Text = (_amountForex == 0) ? "0" : _amountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            }
            else
            {
                this.CustomerBillDescriptionTextBox.Text = "";
                this.ProductTextBox.Text = "";
                this.AmountForexTextBox.Text = "0";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string _invServDt = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeItemKey), ApplicationConfig.EncryptionKey);
            Billing_InvoiceDt _invoiceDt = this._billingInvoiceBL.GetSingleBillingInvoiceDt(new Guid(_invServDt));
            Master_CustBillAccount _custBillAccount = this._custBillAccountBL.GetSingleCustBillAccount(new Guid(this.CustomerBillAccountDropDownList.SelectedValue));

            //_invServiceDt.FgSubLed = Convert.ToChar(this.FgSubledHiddenField.Value);
            //if (this.SubledDropDownList.SelectedValue != "null")
            //{
            //    _invServiceDt.SubLed = this.SubledDropDownList.SelectedValue;
            //}
            //else
            //{
            //    _invServiceDt.SubLed = null;
            //}
            //_invServiceDt.Account = this.AccountTextBox.Text;
            _invoiceDt.CustBillCode = new Guid(this.CustomerBillAccountDropDownList.SelectedValue);
            _invoiceDt.CustBillDescription = this.CustomerBillDescriptionTextBox.Text;
            _invoiceDt.ProductCode = _custBillAccount.ProductCode;
            _invoiceDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);
            _invoiceDt.Remark = this.RemarkTextBox.Text;

            bool _result = this._billingInvoiceBL.EditBillingInvoiceDt(_invoiceDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}
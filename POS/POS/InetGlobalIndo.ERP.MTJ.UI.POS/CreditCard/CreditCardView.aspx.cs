using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.CreditCard
{
    public partial class CreditCardView : CreditCardBase
    {
        private CreditCardBL _creditCardBL = new CreditCardBL();
        private PermissionBL _permBL = new PermissionBL();
        private AccountBL _accountBL = new AccountBL();

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

                this.CreditCardCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.CreditCardNameTextBox.Attributes.Add("ReadOnly", "True");
                this.CreditCardTypeCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.AccountCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.AccountTextBox.Attributes.Add("ReadOnly", "True");

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
            POSMsCreditCard _posMsCreditCard = this._creditCardBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CreditCardCodeTextBox.Text = _posMsCreditCard.CreditCardCode;
            this.CreditCardNameTextBox.Text = _posMsCreditCard.CreditCardName;
            this.CreditCardTypeCodeTextBox.Text = _posMsCreditCard.CreditCardTypeCode;
            this.AccountCodeTextBox.Text = _posMsCreditCard.Account;
            this.AccountTextBox.Text = _accountBL.GetAccountNameByCode(_posMsCreditCard.Account);
            this.BankChargeTextBox.Text = ((Decimal)_posMsCreditCard.BankCharge).ToString("#,##0.0");
            this.CustomerChargeTextBox.Text = ((Decimal)_posMsCreditCard.CustomerCharge).ToString("#,##0.0");
            this.AccountBankChargeCodeTextBox.Text = _posMsCreditCard.AccountBankCharge;
            this.AccountBankChargeTextBox.Text = _accountBL.GetAccountNameByCode(_posMsCreditCard.AccountBankCharge);

        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}
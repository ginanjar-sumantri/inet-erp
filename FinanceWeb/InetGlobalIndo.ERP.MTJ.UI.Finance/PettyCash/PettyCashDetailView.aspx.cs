using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCash
{
    public partial class PettyCashDetailView : PettyCashBase
    {
        private PettyBL _pettyBL = new PettyBL();
        private CurrencyBL _currencyBL = new CurrencyBL();
        private CurrencyRateBL _currencyRateBL = new CurrencyRateBL();
        private AccountBL _accountBL = new AccountBL();
        private SubledBL _subledBL = new SubledBL();
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

                FINPettyHd _finPettyHd = this._pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                if (_finPettyHd.Status != PettyCashDataMapper.GetStatus(TransStatus.Posted))
                {
                    this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                }
                else
                {
                    this.EditButton.Visible = false;
                }
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowDataEdit();

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

        public void ShowDataEdit()
        {
            FINPettyDt _finPettyDt = this._pettyBL.GetSingleDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._numberKey), ApplicationConfig.EncryptionKey));
            FINPettyHd _finPettyHd = this._pettyBL.GetSingleHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            string _curren = this._accountBL.GetSingleAccount(_finPettyDt.Account).CurrCode;
            byte _decimalPlace = this._currencyBL.GetDecimalPlace(_curren);

            this.AccountTextBox.Text = _finPettyDt.Account;
            this.AccountNameTextBox.Text = _accountBL.GetAccountNameByCode(_finPettyDt.Account);
            this.FgSubledHiddenField.Value = _finPettyDt.FgSubLed.ToString();
            this.SubledgerTextBox.Text = _subledBL.GetSubledNameByCode(_finPettyDt.SubLed);
            this.RemarkTextBox.Text = _finPettyDt.Remark;
            this.AmountTextbox.Text = (_finPettyDt.AmountForex == 0) ? "0" : _finPettyDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            //this.CurrTextBox.Text = _finPettyHd.CurrCode;

            char _status = this._pettyBL.GetStatusFINPettyCashHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_status == PettyCashDataMapper.GetStatus(TransStatus.Posted))
            {
                this.EditButton.Visible = false;
            }
            else
            {
                this.EditButton.Visible = true;
            }

        }




        protected void AccountTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.AccountNameTextBox.Text = _accountBL.GetAccountNameByCode(this.AccountTextBox.Text);

            }
            catch (Exception ex)
            {
                this.AccountTextBox.Text = "";

            }

        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editDetailPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._numberKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._numberKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}
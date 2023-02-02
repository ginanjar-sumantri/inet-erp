using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FARevaluation
{
    public partial class FARevaluationDetailEdit : FARevaluationBase
    {
        private FARevaluationBL _faRevalutionBL = new FARevaluationBL();
        private PermissionBL _permBL = new PermissionBL();
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
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

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void SetAttribute()
        {
            this.DevaluationAmountTextBox.Attributes.Add("OnBlur", "ChangeFormat2(" + this.DevaluationAmountTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + ");");
        }

        public void ShowData()
        {
            GLFADevaluationDt _glFADevaluationDt = this._faRevalutionBL.GetSingleGLFADevaluationDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeFA)));

            MsFixedAsset _msFixedAsset = _fixedAssetBL.GetSingleFixedAsset(_glFADevaluationDt.FACode);
            byte _decimalPlace = _currencyBL.GetDecimalPlace(_msFixedAsset.CurrCode);

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.FixedAssetTextBox.Text = _fixedAssetBL.GetFixedAssetNameByCode(_glFADevaluationDt.FACode);
            this.DevaluationLifeTextBox.Text = Convert.ToString(_glFADevaluationDt.NewLife);
            this.DevaluationAmountTextBox.Text = _glFADevaluationDt.NewAmount.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFADevaluationDt _glFADevaluationDt = this._faRevalutionBL.GetSingleGLFADevaluationDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeFA)));

            _glFADevaluationDt.NewLife = Convert.ToInt32(this.DevaluationLifeTextBox.Text);
            _glFADevaluationDt.NewAmount = Convert.ToDecimal(this.DevaluationAmountTextBox.Text);
            _glFADevaluationDt.AdjustLife = _glFADevaluationDt.NewLife - _glFADevaluationDt.BalanceLife;
            _glFADevaluationDt.AdjustAmount = _glFADevaluationDt.NewAmount - _glFADevaluationDt.BalanceAmount;

            bool _result = this._faRevalutionBL.EditGLFADevaluationDt(_glFADevaluationDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}

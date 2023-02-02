using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FASales
{
    public partial class FASalesDetailEdit : FASalesBase
    {
        private FASalesBL _faSalesBL = new FASalesBL();
        private FixedAssetsBL _fixedAssetBL = new FixedAssetsBL();
        private PermissionBL _permBL = new PermissionBL();
        private CurrencyBL _currBL = new CurrencyBL();

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
                this.PageTitleLiteral.Text = _pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.FADropdownlist();

                this.ClearLabel();
                this.ShowData();
                this.SetAttribute();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.AmountHomeTextBox.Attributes.Add("ReadOnly", "True");

            this.AmountForexTextBox.Attributes.Add("OnBlur", "return Amount(" + this.AmountForexTextBox.ClientID + ", " + this.AmountHomeTextBox.ClientID + "," + this.ForexRateTextBox.ClientID + "," + this.DecimalPlaceHiddenField.ClientID + "," + this.DecimalPlaceHomeHiddenField.ClientID + ");");
        }

        protected void FADropdownlist()
        {
            this.FADropDownList.DataSource = this._fixedAssetBL.GetListFixedAsset();
            this.FADropDownList.DataValueField = "FACode";
            this.FADropDownList.DataTextField = "FAName";
            this.FADropDownList.DataBind();
        }

        public void ShowData()
        {
            GLFASalesDt _glFASalesDt = this._faSalesBL.GetSingleFASalesDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeFA), ApplicationConfig.EncryptionKey));
            GLFASalesHd _glFASalesHd = this._faSalesBL.GetSingleFASalesHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            byte _decimalPlace = _currBL.GetDecimalPlace(_glFASalesHd.CurrCode);
            byte _decimalPlaceHome = _currBL.GetDecimalPlace(_currBL.GetCurrDefault());
            decimal _forexRate = this._faSalesBL.GetForexRate(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey)));

            this.DecimalPlaceHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlace);
            this.DecimalPlaceHomeHiddenField.Value = CurrencyDataMapper.GetDecimal(_decimalPlaceHome);

            this.FADropDownList.SelectedValue = _glFASalesDt.FACode;
            this.ForexRateTextBox.Text = (_forexRate == 0) ? "0" : _forexRate.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountCurrTextBox.Text = (_glFASalesDt.AmountCurrent == 0) ? "0" : _glFASalesDt.AmountCurrent.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountForexTextBox.Text = (_glFASalesDt.AmountForex == 0) ? "0" : _glFASalesDt.AmountForex.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlace));
            this.AmountHomeTextBox.Text = (_glFASalesDt.AmountHome == 0) ? "0" : _glFASalesDt.AmountHome.ToString(CurrencyDataMapper.GetFormatDecimal(_decimalPlaceHome));
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFASalesDt _glFASalesDt = this._faSalesBL.GetSingleFASalesDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeFA), ApplicationConfig.EncryptionKey));

            _glFASalesDt.AmountHome = Convert.ToDecimal(this.AmountHomeTextBox.Text);
            _glFASalesDt.AmountForex = Convert.ToDecimal(this.AmountForexTextBox.Text);

            bool _result = this._faSalesBL.EditFASalesDt(_glFASalesDt);

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
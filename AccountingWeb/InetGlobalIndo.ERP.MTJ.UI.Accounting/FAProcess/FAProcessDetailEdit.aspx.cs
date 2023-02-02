using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAProcess
{
    public partial class FAProcessDetailEdit : FAProcessBase
    {
        private FixedAssetsBL _faProcessBL = new FixedAssetsBL();
        private GLPeriodBL _glPeriodBL = new GLPeriodBL();
        private PermissionBL _permBL = new PermissionBL();

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

                this.ClearLabel();
                this.SetAttribute();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.AmountDeprTextBox.Attributes.Add("ReadOnly", "True");
            this.TotalDeprTextBox.Attributes.Add("ReadOnly", "True");

            this.AdjustDeprTextBox.Attributes.Add("OnBlur", "Total(" + this.AmountDeprTextBox.ClientID + ", " + this.AdjustDeprTextBox.ClientID + "," + this.TotalDeprTextBox.ClientID + ");");
        }

        public void ShowData()
        {
            GLFAProcessDt _glFAProcessDt = this._faProcessBL.GetSingleFAProcessDt(Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeFA), ApplicationConfig.EncryptionKey));

            this.FATextBox.Text = _faProcessBL.GetFixedAssetNameByCode(_glFAProcessDt.FACode);
            this.AmountDeprTextBox.Text = (_glFAProcessDt.AmountDepr == 0) ? "0" : _glFAProcessDt.AmountDepr.ToString("#,###.##");
            this.AdjustDeprTextBox.Text = (_glFAProcessDt.AdjustDepr == 0) ? "0" : _glFAProcessDt.AdjustDepr.ToString("#,###.##");
            this.TotalDeprTextBox.Text = (_glFAProcessDt.TotalDepr == 0) ? "0" : _glFAProcessDt.TotalDepr.ToString("#,###.##");

            decimal _tempBalanceAmount = (_glFAProcessDt.BalanceAmount == null) ? 0 : Convert.ToDecimal(_glFAProcessDt.BalanceAmount);
            this.BalanceAmountTextBox.Text = (_tempBalanceAmount == 0) ? "0" : _tempBalanceAmount.ToString("#,###.##");

            decimal _tempBalanceLife = (_glFAProcessDt.BalanceLife == null) ? 0 : Convert.ToDecimal(_glFAProcessDt.BalanceLife);
            this.BalanceLifeTextBox.Text = (_tempBalanceLife == 0) ? "0" : _tempBalanceLife.ToString("#,###.##");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAProcessDt _glFAProcessDt = this._faProcessBL.GetSingleFAProcessDt(Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeFA), ApplicationConfig.EncryptionKey));

            _glFAProcessDt.AdjustDepr = Convert.ToDecimal(this.AdjustDeprTextBox.Text);
            _glFAProcessDt.TotalDepr = Convert.ToDecimal(this.TotalDeprTextBox.Text);

            bool _result = this._faProcessBL.EditFAProcessDt(_glFAProcessDt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codePeriod)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codePeriod)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
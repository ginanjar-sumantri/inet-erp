using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Home.Term
{
    public partial class TermDtEdit : TermBase
    {
        private TermBL _termDtBL = new TermBL();
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.PercentBaseTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.RangeTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PercentPPnTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
            this.PeriodTextBox.Attributes.Add("OnKeyDown", "return NumericWithTab();");
        }

        protected void ShowData()
        {
            string a = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            int b = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._termCodeKey), ApplicationConfig.EncryptionKey));

            MsTermDt _msTermDt = this._termDtBL.GetSingleTermDt(a, b);

            this.PeriodTextBox.Text = _msTermDt.Period.ToString();
            this.RangeTextBox.Text = _msTermDt.XRange.ToString();
            this.TypeRangeDropDownList.SelectedValue = _msTermDt.TypeRange;
            this.PercentBaseTextBox.Text = _msTermDt.PercentBase.ToString("#,##0.##");
            this.PercentPPnTextBox.Text = _msTermDt.PercentPPn.ToString("#,##0.##");
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsTermDt _msTermDt = this._termDtBL.GetSingleTermDt(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._termCodeKey), ApplicationConfig.EncryptionKey)));

            _msTermDt.XRange = Convert.ToInt32(this.RangeTextBox.Text);
            _msTermDt.TypeRange = this.TypeRangeDropDownList.SelectedValue;
            _msTermDt.PercentBase = Convert.ToDecimal(this.PercentBaseTextBox.Text);
            _msTermDt.PercentPPn = Convert.ToDecimal(this.PercentPPnTextBox.Text);

            bool _result = this._termDtBL.EditTermDt(_msTermDt);

            if (_result == true)
            {
                Response.Redirect(_viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_viewPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
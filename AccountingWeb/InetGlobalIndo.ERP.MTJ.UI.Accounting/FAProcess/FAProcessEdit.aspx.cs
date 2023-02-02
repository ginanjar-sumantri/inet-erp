using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAProcess
{
    public partial class FAProcessEdit : FAProcessBase
    {
        private FixedAssetsBL _faProcessBL = new FixedAssetsBL();
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
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.YearDropdownlist();
                this.PeriodDropdownlist();

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
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void YearDropdownlist()
        {
            int _year = DateTime.Now.Year;
            for (int i = 9; i >= 0; i--)
            {
                this.YearDDL.Items.Add((_year - i).ToString());
            }
        }

        protected void PeriodDropdownlist()
        {
            this.PeriodDDL.DataSource = this._faProcessBL.GetPeriod();
            this.PeriodDDL.DataValueField = "MonthCode";
            this.PeriodDDL.DataTextField = "MonthName";
            this.PeriodDDL.DataBind();
        }

        public void ShowData()
        {
            GLFAProcessHd _glFAProcessHd = this._faProcessBL.GetSingleFAProcessHd(Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)));

            this.YearDDL.SelectedValue = Convert.ToString(_glFAProcessHd.Year);
            this.PeriodDDL.SelectedValue = Convert.ToString(_glFAProcessHd.Period);
            this.RemarkTextBox.Text = _glFAProcessHd.Remark;
            //this.StatusLabel.Text = FixedAssetStatus.StatusText(_glFAProcessHd.Status);
            //this.StatusHiddenField.Value = _glFAProcessHd.Status.ToString();
            this.PeriodHiddenField.Value = _glFAProcessHd.Period.ToString();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAProcessHd _glFAProcessHd = this._faProcessBL.GetSingleFAProcessHd(Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)));

            _glFAProcessHd.Remark = this.RemarkTextBox.Text;
            _glFAProcessHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAProcessHd.DatePrep = DateTime.Now;

            bool _result = this._faProcessBL.EditFAProcessHd(_glFAProcessHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codePeriod)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            GLFAProcessHd _glFAProcessHd = this._faProcessBL.GetSingleFAProcessHd(Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)), Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codePeriod), ApplicationConfig.EncryptionKey)));

            _glFAProcessHd.Remark = this.RemarkTextBox.Text;
            _glFAProcessHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _glFAProcessHd.DatePrep = DateTime.Now;

            bool _result = this._faProcessBL.EditFAProcessHd(_glFAProcessHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codePeriod + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codePeriod)));
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }
    }
}
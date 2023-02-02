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

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AdjustDiffRate
{
    public partial class AdjustDiffRateEdit : AdjustDiffRateBase
    {
        private GLAdjustDiffRateBL _adjDiffRateBL = new GLAdjustDiffRateBL();
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
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute();
                this.ClearLabel();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            GLAdjustDiffRateHd _adjDiffRateHd = this._adjDiffRateBL.GetSingleGLAdjustDiffRateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            
            this.TransactionNumberTextBox.Text = _adjDiffRateHd.TransNmbr;
            this.FileNmbrTextBox.Text = _adjDiffRateHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_adjDiffRateHd.TransDate);
            this.RemarkTextBox.Text = _adjDiffRateHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            GLAdjustDiffRateHd _finReceipt = this._adjDiffRateBL.GetSingleGLAdjustDiffRateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finReceipt.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finReceipt.Remark = this.RemarkTextBox.Text;

            _finReceipt.UserPrep = HttpContext.Current.User.Identity.Name;
            _finReceipt.DatePrep = DateTime.Now;

            bool _result = this._adjDiffRateBL.EditGLAdjustDiffRateHd(_finReceipt);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            GLAdjustDiffRateHd _finReceipt = this._adjDiffRateBL.GetSingleGLAdjustDiffRateHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finReceipt.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _finReceipt.Remark = this.RemarkTextBox.Text;

            _finReceipt.UserPrep = HttpContext.Current.User.Identity.Name;
            _finReceipt.DatePrep = DateTime.Now;

            bool _result = this._adjDiffRateBL.EditGLAdjustDiffRateHd(_finReceipt);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}
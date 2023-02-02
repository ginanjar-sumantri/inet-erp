using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TransactionClose
{
    public partial class TransactionCloseEdit : TransactionCloseBase
    {
        private TransactionCloseBL _transCloseBL = new TransactionCloseBL();
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
            this.DescTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.DescTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ShowData()
        {
            Master_TransactionClose _msTransClose = this._transCloseBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.YearTextBox.Text = _msTransClose.StartDate.Year.ToString();
            this.PeriodTextBox.Text = _msTransClose.StartDate.Month.ToString();
            this.YearEndTextBox.Text = _msTransClose.EndDate.Year.ToString();
            this.PeriodEndTextBox.Text = _msTransClose.EndDate.Month.ToString();
            this.DescTextBox.Text = _msTransClose.Description;
            this.StatusCheckBox.Checked = TransactionCloseDataMapper.GetTransCloseStatusBool(_msTransClose.Status);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Master_TransactionClose _msTransClose = this._transCloseBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msTransClose.Description = this.DescTextBox.Text;
            _msTransClose.Status = TransactionCloseDataMapper.GetTransCloseStatus(this.StatusCheckBox.Checked);
            _msTransClose.EditBy = HttpContext.Current.User.Identity.Name;
            _msTransClose.EditDate = DateTime.Now;

            bool _result = this._transCloseBL.Edit(_msTransClose);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void CancelButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}

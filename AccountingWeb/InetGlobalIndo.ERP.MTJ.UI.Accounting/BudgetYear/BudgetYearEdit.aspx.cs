using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Accounting;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BudgetYear
{
    public partial class BudgetYearEdit : GLBudgetYearBase
    {
        private GLBudgetBL _glBudgetBL = new GLBudgetBL();
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
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";
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
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowData()
        {
            string[] _yearRevisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey).Split('|');
            GLBudgetYearHd _glBudgetYearHd = this._glBudgetBL.GetSingleGLBudgetYearHd(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]));

            this.YearTextBox.Text = _glBudgetYearHd.Year.ToString();
            this.RemarkTextBox.Text = _glBudgetYearHd.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            string[] _yearRevisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey).Split('|');
            GLBudgetYearHd _glBudgetYearHd = this._glBudgetBL.GetSingleGLBudgetYearHd(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]));

            _glBudgetYearHd.Year = Convert.ToInt32(this.YearTextBox.Text);
            _glBudgetYearHd.Remark = this.RemarkTextBox.Text;
            _glBudgetYearHd.EditBy = HttpContext.Current.User.Identity.Name;
            _glBudgetYearHd.EditDate = DateTime.Now;

            bool _result = this._glBudgetBL.EditGLBudgetYearHd(_glBudgetYearHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            string[] _yearRevisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey).Split('|');
            GLBudgetYearHd _glBudgetYearHd = this._glBudgetBL.GetSingleGLBudgetYearHd(Convert.ToInt32(_yearRevisi[0]), Convert.ToInt32(_yearRevisi[1]));

            _glBudgetYearHd.Year = Convert.ToInt32(this.YearTextBox.Text);
            _glBudgetYearHd.Remark = this.RemarkTextBox.Text;
            _glBudgetYearHd.EditBy = HttpContext.Current.User.Identity.Name;
            _glBudgetYearHd.EditDate = DateTime.Now;

            bool _result = this._glBudgetBL.EditGLBudgetYearHd(_glBudgetYearHd);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
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
    }
}
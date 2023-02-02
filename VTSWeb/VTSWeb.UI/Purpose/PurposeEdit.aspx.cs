using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTSWeb.Database;
using VTSWeb.SystemConfig;
using VTSWeb.BusinessRule;
using VTSWeb.Common;

namespace VTSWeb.UI
{
    public partial class PurposeEdit : PurposeBase

    {
        private MsPurposeBL _PurposeBL = new MsPurposeBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.PurposeCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.ShowData();

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }

        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }
        public void ShowData()
        {
            MsPurpose _msMsPurpose = this._PurposeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.PurposeCodeTextBox.Text = _msMsPurpose.PurposeCode;
            this.PurposeNameTextBox.Text = _msMsPurpose.PurposeName;
            this.RemarkTextBox.Text = _msMsPurpose.Remark;

        }

        public void ClearData()
        {
            this.PurposeNameTextBox.Text = "";
            this.PurposeCodeTextBox.Text = "";
            this.RemarkTextBox.Text = "";

        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs  e)
        {
            Response.Redirect(this._homePage);
        }
        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();

        }
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsPurpose _msMsPurpose = this._PurposeBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

           _msMsPurpose.PurposeName = this.PurposeNameTextBox.Text;
           _msMsPurpose.Remark = this.RemarkTextBox.Text;

            bool _result = this._PurposeBL.Edit(_msMsPurpose);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Edit Data";
            }

        }
    }


}
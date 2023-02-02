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
    public partial class RegionalEdit : RegionalBase

    {
        private MsRegionalBL _RegionalBL = new MsRegionalBL();

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
                this.RegionalCodeTextBox.Attributes.Add("ReadOnly", "True");
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
            MsRegional _msMsRegional = this._RegionalBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.RegionalCodeTextBox.Text = _msMsRegional.RegionalCode;
            this.RegionalNameTextBox.Text = _msMsRegional.RegionalName;

        }

        public void ClearData()
        {
            this.RegionalNameTextBox.Text = "";
            this.RegionalCodeTextBox.Text = "";

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
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsRegional _msMsRegional = this._RegionalBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msMsRegional.RegionalName = this.RegionalNameTextBox.Text;

            bool _result = this._RegionalBL.Edit(_msMsRegional);

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
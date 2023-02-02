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
    public partial class MsAreaEdit : AreaBase
        
    {
        private MsAreaBL _areaBL = new MsAreaBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.AreaCodeTextBox.Attributes.Add("ReadOnly", "True");
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
            MsArea _msMsArea = this._areaBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.AreaCodeTextBox.Text = _msMsArea.AreaCode;
            this.AreaNamaTextBox.Text = _msMsArea.AreaName;
            this.RemarkTextBox.Text = _msMsArea.Remark;

        }

        public void ClearData()
        {
            this.AreaNamaTextBox.Text = "";
            this.AreaCodeTextBox.Text = "";
            this.RemarkTextBox.Text = "";

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
            MsArea _msMsArea = this._areaBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msMsArea.AreaName= this.AreaNamaTextBox.Text;
            _msMsArea.Remark = this.RemarkTextBox.Text;

            bool _result = this._areaBL.Edit(_msMsArea);

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
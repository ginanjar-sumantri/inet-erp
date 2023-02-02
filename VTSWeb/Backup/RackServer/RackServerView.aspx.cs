using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VTSWeb.BusinessRule;
using VTSWeb.Common;
using VTSWeb.SystemConfig;
using VTSWeb.Database;

namespace VTSWeb.UI
{
    public partial class RackServerView : RackServerBase
    {
        private MsRackServerBL _RackServerBL = new MsRackServerBL();

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
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = "../images/edit2.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
                //this.AddButton.ImageUrl = "../images/add.jpg";
                //this.DeleteButton.ImageUrl = "../images/delete.jpg";
                //this.SaveButton.ImageUrl = "../images/Save.jpg";
                //this.CancelButton2.ImageUrl = "../images/cancel.jpg";
                //this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.ClearLabel();
                this.ShowData();
                //this.Panel.Visible = false;
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            //this.RackBoxNameTextBox.Text = "";
        }

        public void ShowData()
        {
            MsRackServer _msRackServer = this._RackServerBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.RackCodeTextBox.Text = _msRackServer.RackCode;
            this.RackNameTextBox.Text = _msRackServer.RackName;
            this.RemarkTextBox.Text = _msRackServer.Remark;
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void AddButton_Click(object sender, ImageClickEventArgs e)
        {
            //this.Panel.Visible = true;
        }
        protected void DeleteButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsRackBox _msRackBox = new MsRackBox();
            _msRackBox.RackCode = this.RackCodeTextBox.Text;
            _msRackBox.RackBoxName = this.RackNameTextBox.Text;
           
            bool _result = this._RackServerBL.Add(_msRackBox);

            if (_result == true)
            {
                this.WarningLabel.Text = "You Success Add Data";
            }
            else
            {
                this.WarningLabel.Text = "You Failed Add Data";
            }

        }
        protected void CancelButton2_Click(object sender, ImageClickEventArgs e)
        {
            //this.Panel.Visible = false;
        }
        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        public void ShowDataDt()
        {

        }

        protected void RackBoxListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
   
        }
}


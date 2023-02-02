using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public partial class RackServerAdd : RackServerBase
    {
        private MsRackServerBL _RackServerBL = new MsRackServerBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                Response.Redirect("..\\Login.aspx");
            }
            if (!this.Page.IsPostBack == true)
            {
                this.WarningLabel.Text = "";
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }

        }
        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ClearData()
        {
            this.RackCodeTextBox.Text = "";
            this.RackNameTextBox.Text = "";
            this.RemarkTextBox.Text = "";

        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsRackServer _msRackServer = new MsRackServer();

            _msRackServer.RackCode = this.RackCodeTextBox.Text;
            _msRackServer.RackName = this.RackNameTextBox.Text;
            _msRackServer.Remark = this.RemarkTextBox.Text;

            bool _result = this._RackServerBL.Add(_msRackServer);
            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
            this.ClearLabel();
        }
    }
}

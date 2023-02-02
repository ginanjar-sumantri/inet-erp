﻿using System;
using System.Web;
using System.Web.UI;
using VTSWeb.Database;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;

namespace VTSWeb.UI
{
    public partial class MsAreaAdd : AreaBase
    {
        private MsAreaBL _areaBL = new MsAreaBL();

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
            this.AreaNamaTextBox.Text = "";
            this.AreaCodeTextBox.Text = "";
            this.RemarkTextBox.Text = "";
       
        }
  
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsArea _msMsArea = new MsArea();

            _msMsArea.AreaCode = this.AreaCodeTextBox.Text;
            _msMsArea.AreaName = this.AreaNamaTextBox.Text;
            _msMsArea.Remark = this.RemarkTextBox.Text;

            bool _result = this._areaBL.Add(_msMsArea);
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
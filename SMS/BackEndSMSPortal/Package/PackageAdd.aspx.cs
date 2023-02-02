using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal.Package
{
    public partial class PackageAdd : PackageBase
    {
        PackageBL _packageBL = new PackageBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userAdmin"] != null)
            {
                if (HttpContext.Current.Session["userAdmin"].ToString() == "" || HttpContext.Current.Session["userAdmin"].ToString() == null)
                {
                    Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
                }
            }
            else {
                Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                Clear();
            }

            SaveImageButton.ImageUrl = "../images/save.jpg";
            CancelImageButton.ImageUrl = "../images/cancel.jpg";
            this.SMSPerDayTextBox.Attributes.Add ( "OnKeyUp", "HarusAngka(this);") ;
            this.SMSPerDayTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
        }

        protected void Clear()
        {
            this.PackageNameTextBox.Text = "";
            this.SMSPerDayTextBox.Text = "";
            this.DescriptionTextBox.Text = "";
        }

        protected void CancelImageButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._homePage);
        }
        protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
        {
            MSPackage _newData = new MSPackage();
            if (!_packageBL.isPackageExist(this.PackageNameTextBox.Text))
            {
                _newData.PackageName = this.PackageNameTextBox.Text;
                _newData.SMSPerDay = Convert.ToInt32(this.SMSPerDayTextBox.Text);
                _newData.Description = this.DescriptionTextBox.Text;

                if (_packageBL.AddPackage(_newData))
                {
                    Clear();
                    this.WarningLabel.Text = "Save Success";
                    Response.Redirect(_homePage);
                }
            }
        }
}
}

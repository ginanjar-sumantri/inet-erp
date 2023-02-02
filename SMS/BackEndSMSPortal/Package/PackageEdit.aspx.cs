using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal.Package
{
    public partial class PackageEdit : PackageBase
    {
        PackageBL _packageBL = new PackageBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (HttpContext.Current.Session["userAdmin"].ToString() == "" || HttpContext.Current.Session["userAdmin"].ToString() == null)
            {
                Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
            }
                
            if (!this.Page.IsPostBack == true)
            {
                Clear();
                ShowData();
            }

            this.PackageNameTextBox.ReadOnly = true;
            SaveImageButton.ImageUrl = "../images/save.jpg";
            CancelImageButton.ImageUrl = "../images/cancel.jpg";
            this.SMSPerDayTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
            this.SMSPerDayTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
        }

        protected void ShowData()
        {
            MSPackage _editData = this._packageBL.getSinglePackage(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.PackageNameTextBox.Text = _editData.PackageName;
            this.SMSPerDayTextBox.Text = _editData.SMSPerDay.ToString();
            this.DescriptionTextBox.Text = _editData.Description;
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
            MSPackage _editData = _packageBL.getSinglePackage ( this.PackageNameTextBox.Text ) ;
            
            _editData.SMSPerDay = Convert.ToInt32(this.SMSPerDayTextBox.Text);
            _editData.Description = this.DescriptionTextBox.Text;

            if (_packageBL.EditSubmit())
            {
                Clear();
                this.WarningLabel.Text = "Save Success";
                Response.Redirect(_homePage);
            }
        }
    }
}

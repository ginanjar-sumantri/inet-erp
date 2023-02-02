using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal.OrganizationSetting
{
    public partial class OrganizationSettingEdit : OrganizationSettingBase
    {
        OrganizationSettingBL _organizationSettingBL = new OrganizationSettingBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

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
                ShowData();
            }

            this.UserLimitTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
            this.UserLimitTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
            this.MaskingSDTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
            this.MaskingSDTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
        
        }

        protected void ShowData()
        {
            MsOrganization _msOrganization = this._organizationSettingBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.OrganizationNameTextBox.Text = _msOrganization.OrganizationName;
            this.UserLimitTextBox.Text = _msOrganization.UserLimit.ToString();
            this.MaskingSDTextBox.Text = _msOrganization.MaskingSD.ToString();
            this.HostedCheckBox.Checked = Convert.ToBoolean ( _msOrganization.FgHosted );
        }

        protected void Clear()
        {
            this.OrganizationNameTextBox.Text = "";
            this.UserLimitTextBox.Text = "";
            this.MaskingSDTextBox.Text = "";
            this.HostedCheckBox.Checked = false;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            MsOrganization _msOrganization = this._organizationSettingBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            _msOrganization.UserLimit = Convert.ToInt16 ( this.UserLimitTextBox.Text );
            _msOrganization.MaskingSD = Convert.ToInt16(this.MaskingSDTextBox.Text);
            _msOrganization.FgHosted = this.HostedCheckBox.Checked;
            _organizationSettingBL.SubmitEdit();
            Response.Redirect(this._homePage);
        }

}
}

using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.EmailNotificationSetup
{
    public partial class EmailNotificationSetupEdit : EmailNotificationSetupBase
    {
        private EmailNotificationSetupBL _emailSetupBL = new EmailNotificationSetupBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.TypeTextBox.Attributes.Add("ReadOnly","true");
            this.SubTypeTextBox.Attributes.Add("ReadOnly", "true");
        }

        protected void ShowData()
        {
            BILMsEmailNotificationSetup _emailNotificationSetup = this._emailSetupBL.GetSingle(Convert.ToByte(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.TypeTextBox.Text = EmailNotificationDataMapper.GetTypeText(_emailNotificationSetup.NotificationType);
            this.SubTypeTextBox.Text = EmailNotificationDataMapper.GetIDText(_emailNotificationSetup.ID);
            this.EmailFromTextBox.Text = _emailNotificationSetup.EmailFrom;
            this.EmailToTextBox.Text = _emailNotificationSetup.EmailTo;
            this.SubjectTextBox.Text = _emailNotificationSetup.Subject;
            this.BodyMessageTextBox.Text = _emailNotificationSetup.BodyMessage;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            BILMsEmailNotificationSetup _masterEmailNotificationSetup = _emailSetupBL.GetSingle(Convert.ToByte(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            _masterEmailNotificationSetup.EmailFrom = this.EmailFromTextBox.Text;
            _masterEmailNotificationSetup.EmailTo = this.EmailToTextBox.Text;
            _masterEmailNotificationSetup.Subject = this.SubjectTextBox.Text;
            _masterEmailNotificationSetup.BodyMessage = this.BodyMessageTextBox.Text;
            
            bool _result = this._emailSetupBL.Edit(_masterEmailNotificationSetup);

            if (_result == true)
            {
                Response.Redirect(_homePage);
            }
            else
            {
                this.WarningLabel.Text = "Edit data gagal";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }
    }
}

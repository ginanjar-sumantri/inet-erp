using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Billing;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.EmailNotificationSetup
{
    public partial class EmailNotificationSetupView : EmailNotificationSetupBase
    {
        private EmailNotificationSetupBL _emailSetupBL = new EmailNotificationSetupBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.ShowData();
            }
        }

        protected void ShowData()
        {
            BILMsEmailNotificationSetup _emailNotificationSetup = this._emailSetupBL.GetSingle(Convert.ToByte(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey)));

            this.TypeTextBox.Text = EmailNotificationDataMapper.GetTypeText( _emailNotificationSetup.NotificationType);
            this.SubTypeTextBox.Text = EmailNotificationDataMapper.GetIDText(_emailNotificationSetup.ID);
            this.EmailFromTextBox.Text = _emailNotificationSetup.EmailFrom;
            this.EmailToTextBox.Text = _emailNotificationSetup.EmailTo;
            this.SubjectTextBox.Text = _emailNotificationSetup.Subject;
            this.BodyMessageLabel.Text = _emailNotificationSetup.BodyMessage;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}

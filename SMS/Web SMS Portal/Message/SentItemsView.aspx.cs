using System;
using System.Web;
using System.Web.UI;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class SentItemsView : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.CekSession();

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Sent Items";

                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.ShowData();
            }
        }

        public void ShowData()
        {
            SMSGatewaySend _smsGatewaySend = this._smsMessageBL.GetSingleSMSGatewaySend(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CategoryTextBox.Text = _smsGatewaySend.Category;
            this.DestinationTextBox.Text = _smsGatewaySend.DestinationPhoneNo;
            this.MessageTextBox.Text = _smsGatewaySend.Message;
            this.TimeSentTextBox.Text = _smsGatewaySend.DateSent.ToString();
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._sentItemsPage);
        }
    }
}
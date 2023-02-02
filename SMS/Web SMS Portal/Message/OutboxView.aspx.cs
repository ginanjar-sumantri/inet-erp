using System;
using System.Web;
using System.Web.UI;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class OutboxView : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Outbox";

                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.SetButtonPermission();

                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            //this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            //if (this._permEdit == PermissionLevel.NoAccess)
            //{
            //    this.EditButton.Visible = false;
            //}
        }

        public void ShowData()
        {
            SMSGatewaySend _smsGatewaySend = this._smsMessageBL.GetSingleSMSGatewaySend(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CategoryTextBox.Text = _smsGatewaySend.Category;
            this.DestinationTextBox.Text = _smsGatewaySend.DestinationPhoneNo;
            this.MessageTextBox.Text = _smsGatewaySend.Message;
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._outboxPage);
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class InboxView : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();
        private Boolean fgReplied = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.CekSession();

            if (Session["FgWebAdmin"] != null)
                if (Session["FgWebAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Inbox";
                this.ReplyButton.ImageUrl = "../images/reply.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
            }
            this.ShowData();
        }

        public void ShowData()
        {
            SMSGatewayReceive _smsGatewayRcv = this._smsMessageBL.GetSingleSMSGatewayReceive(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CategoryTextBox.Text = _smsGatewayRcv.Category;
            this.SenderTextBox.Text = _smsGatewayRcv.SenderPhoneNo;
            this.MessageTextBox.Text = _smsGatewayRcv.Message;

            if (_smsGatewayRcv.ReplyId != null && _smsGatewayRcv.ReplyId != 0)
            {
                this.fgReplied = true;
                this.ReplyButton.ImageUrl = "../images/view_reply.jpg";
            }

            if ((byte)_smsGatewayRcv.flagRead == 0)
            {
                this._smsMessageBL.SetSMSGatewayReceiveToRead(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.Session["UserID"].ToString());
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._inboxPage);
        }

        protected void ReplyButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.fgReplied)
                Response.Redirect(this._replyViewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            else 
                Response.Redirect(this._composePage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class ReplyView : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();

            if (Session["FgWebAdmin"] != null)
                if (Session["FgWebAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Outbox";

                //string _imageUrl = "../images/home_small.jpg";
                //string _titleTree = "&nbsp;SMS";
                //TreeViewBL _treeViewBL = new TreeViewBL();
                //String[] _roles = new RoleBL().GetRolesIDByUserName(HttpContext.Current.User.Identity.Name);
                //this.TreeView1.ShowLines = true;
                //this.TreeView1.Nodes.Add(_treeViewBL.Render(Request.ApplicationPath));

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
            String _idInbox = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            String _replyID = this._smsMessageBL.GetSingleSMSGatewayReceive(_idInbox).ReplyId.ToString();
            SMSGatewaySend _smsGatewaySend = this._smsMessageBL.GetSingleSMSGatewaySend(_replyID);

            this.CategoryTextBox.Text = "";//SMSCategoryDataMapper.SMSCategoryText(SMSCategoryDataMapper.SMSCategory(_smsGatewaySend.Category));
            this.DestTextBox.Text = _smsGatewaySend.DestinationPhoneNo;
            this.MessageTextBox.Text = _smsGatewaySend.Message;
            this.UsernameTextBox.Text = _smsGatewaySend.userID;
            this.DateSentTextBox.Text = DateFormMapper.GetValue(_smsGatewaySend.DateSent);
            String _statusLabel = "";
            if ((_smsGatewaySend.flagSend == null || _smsGatewaySend.flagSend == 0))
            {
                _statusLabel = "Queued";
            }
            else if (_smsGatewaySend.flagSend == 1)
            {
                _statusLabel = "Sent";
            }
            else
            {
                _statusLabel = "Failed";
            }
            this.ReplyStatusLabel.Text = _statusLabel;
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._inboxPage);
        }
    }
}
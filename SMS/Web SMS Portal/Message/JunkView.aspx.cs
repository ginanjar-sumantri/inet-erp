using System;
using System.Web;
using System.Web.UI;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class JunkView : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.CekSession();

            if (Session["FgWebAdmin"] != null)
                if (Session["FgWebAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Inbox";

                //string _imageUrl = "../images/home_small.jpg";
                //string _titleTree = "&nbsp;SMS";
                //TreeViewBL _treeViewBL = new TreeViewBL();
                //String[] _roles = new RoleBL().GetRolesIDByUserName(HttpContext.Current.User.Identity.Name);
                //this.TreeView1.ShowLines = true;
                //this.TreeView1.Nodes.Add(_treeViewBL.Render(Request.ApplicationPath));

                this.ReplyButton.ImageUrl = "../images/reply.jpg";
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
            SMSGatewayReceive _smsGatewayRcv = this._smsMessageBL.GetSingleSMSGatewayReceive(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.CategoryTextBox.Text = "";// SMSCategoryDataMapper.SMSCategoryText(SMSCategoryDataMapper.SMSCategory(_smsGatewayRcv.PrefixMessage));
            this.SenderTextBox.Text = _smsGatewayRcv.SenderPhoneNo;
            this.MessageTextBox.Text = _smsGatewayRcv.Message;

            //MsPhoneBook _msPhoneBook = this._phoneBookBL.GetSingleMsPhoneBook(_smsGatewayRcv.SenderPhoneNo);
            //if (_msPhoneBook != null)
            //{
            //    this.CustIDTextBox.Text = _msPhoneBook.CustID;
            //    this.CustNameTextBox.Text = _msPhoneBook.CustName;
            //}
            //else
            //{
            //    this.CustIDTextBox.Text = "";
            //    this.CustNameTextBox.Text = "";
            //}

            if ((byte)_smsGatewayRcv.flagRead == 0)
            {
                this._smsMessageBL.SetSMSGatewayReceiveToRead(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), HttpContext.Current.Session["UserID"].ToString());
                //
                //SMSGatewayReceive _smsGatewayRcvNew = this._smsMessageBL.GetSingleSMSGatewayReceive(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                //_smsGatewayRcvNew.flagRead = 1;
                //_smsGatewayRcvNew.userName = HttpContext.Current.User.Identity.Name;
                //this._smsMessageBL.EditSMSGatewayReceive(_smsGatewayRcvNew);
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._junkPage);
        }

        protected void ReplyButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._composePage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }
    }
}
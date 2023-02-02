using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.AutoReply
{
    public partial class AutoReplyEdit : AutoReplyBase
    {
        protected AutoReplyBL _autoReply = new AutoReplyBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (HttpContext.Current.Session["userID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
            {
                if ((HttpContext.Current.Session["userID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
                    Response.Redirect("../Login/Login.aspx");
            }
            else Response.Redirect("../Login/Login.aspx");

            if (!Page.IsPostBack)
            {
                ShowData();
            }
        }

        protected void ShowData()
        {
            TrAutoReply _editData = _autoReply.GetSingleAutoReply(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.SenderPhoneTextBox.Text = _editData.PhoneNumber;
            this.KeyWordTextBox.Text = _editData.KeyWord;
            this.ReplyMessageTextBox.Text = _editData.ReplyMessage;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.SenderPhoneTextBox.Text != "")
            {
                if (this.KeyWordTextBox.Text != "")
                {
                    if (this.ReplyMessageTextBox.Text != "")
                    {
                        TrAutoReply _editData = _autoReply.GetSingleAutoReply(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                        _editData.PhoneNumber = this.SenderPhoneTextBox.Text;
                        _editData.KeyWord = this.KeyWordTextBox.Text;
                        _editData.ReplyMessage = this.ReplyMessageTextBox.Text;
                        if (_autoReply.EditSubmit())
                            Response.Redirect(this._homePage + "?result=Success Update Data.");
                        else
                            this.WarningLabel.Text = "Failed to update data.";
                    }
                    else this.WarningLabel.Text = "Reply message must be filled.";
                }
                else this.WarningLabel.Text = "Key Word must be filled.";
            }
            else this.WarningLabel.Text = "Sender Phone Number must be filled.";
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}
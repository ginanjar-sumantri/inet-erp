using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.ManageUser
{
    public partial class ManageUserEdit : ManageUserBase//System.Web.UI.Page
    {
        ManageUserBL _manageUserBL = new ManageUserBL();

        protected NameValueCollectionExtractor _nvcExtractor;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageLabel.Text = "MANAGE USER EDIT";
                this.SaveImageButton.ImageUrl = "../images/save.jpg";
                this.CancelImageButton.ImageUrl = "../images/cancel.jpg";
            }

            ShowPage();
        }

        protected void ShowPage()
        {

            bool _user = _manageUserBL.CekhaveUser(Convert.ToInt16(HttpContext.Current.Session["Organization"].ToString()), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            if (_user == true)
            {
                this.UserIDTextBox.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            }
        }
        protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.PasswordTextBox.Text != "" && this.ConfirmPasswordTextBox.Text != "")
            {
                MsUser _msuser = _manageUserBL.GetSingle(Convert.ToInt32(HttpContext.Current.Session["Organization"].ToString()), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

                _msuser.password = Rijndael.Encrypt(this.PasswordTextBox.Text, ApplicationConfig.EncryptionKey);

                bool _submit = _manageUserBL.Edit(_msuser);

                if (_submit == true)
                {
                    Response.Redirect(_ManageUserHome);
                }
                else
                {
                    this.WarningLabel.Text = "Your Failed Edit Data";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please, insert Text Box";
            }
        }
        protected void CancelImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._ManageUserHome);
        }
}
}
using System;
using System.Web;
using System.Web.UI;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class ContactsView : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {            
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (HttpContext.Current.Session["userID"] != null && HttpContext.Current.Session["Organization"] != null && HttpContext.Current.Session["FgWebAdmin"] != null)
            {
                if ((HttpContext.Current.Session["userID"].ToString() == "") || (HttpContext.Current.Session["Organization"].ToString() == "") || (HttpContext.Current.Session["FgWebAdmin"].ToString() == ""))
                    Response.Redirect("../Login/Login.aspx");
            }
            else Response.Redirect("../Login/Login.aspx");

            if (Session["FgAdmin"] != null)
                if (Session["FgAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Contact View";

                this.EditButton.ImageUrl = "../images/edit2.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                //this.SetButtonPermission();

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
            MsPhoneBook _msPhoneBook = this._smsMessageBL.GetSinglePhoneBook(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.NameTextBox.Text = _msPhoneBook.Name;
            this.PhoneNumberTextBox.Text = _msPhoneBook.PhoneNumber;
            this.CompanyTextBox.Text = _msPhoneBook.Company;
            this.DateOfBirthTextBox.Text = DateFormMapper.GetValue(_msPhoneBook.DateOfBirth);
            this.ReligionTextBox.Text = _msPhoneBook.Religion;
            this.EmailTextBox.Text = _msPhoneBook.Email;
            this.CityTextBox.Text = _msPhoneBook.City;
            this.PhoneBookGroupTextBox.Text = _msPhoneBook.PhoneBookGroup;
            this.fgBirthDayCheckBox.Checked = (_msPhoneBook.fgBirthDay == null) ? false : (bool)_msPhoneBook.fgBirthDay;
            this.BirthdayWishesTexBox.Text = _msPhoneBook.BirthdayWishes;
            this.RemarkTextBox.Text = _msPhoneBook.Remark;
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._contactsEditPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_contactsPage);
        }
    }
}
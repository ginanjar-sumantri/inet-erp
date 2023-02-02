using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class ContactsEdit : MessageBase
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
                this.SubPageTitleLiteral.Text = "Contact Edit";

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";

                this.PhoneNumberTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PhoneNumberTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PrefixPhoneNumber.DataSource = _smsMessageBL.GetListCountryCode();
                this.PrefixPhoneNumber.DataValueField = this.PrefixPhoneNumber.DataTextField = "CountryCode";
                this.PrefixPhoneNumber.DataBind();

                this.ClearLabel();
                this.ShowData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            MsPhoneBook _msPhoneBook = this._smsMessageBL.GetSinglePhoneBook(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.NameTextBox.Text = _msPhoneBook.Name;

            String _preFix = _msPhoneBook.PhoneNumber.Substring(0, 2);
            if (_preFix == "+1" || _preFix == "+7")
            {
                this.PrefixPhoneNumber.SelectedValue = _msPhoneBook.PhoneNumber.Substring(0, 2);
                this.PhoneNumberTextBox.Text = _msPhoneBook.PhoneNumber.Substring(2);
            }
            else
            {
                this.PrefixPhoneNumber.SelectedValue = _msPhoneBook.PhoneNumber.Substring(0, 3);
                this.PhoneNumberTextBox.Text = _msPhoneBook.PhoneNumber.Substring(3);
            }

            this.CompanyTextBox.Text = _msPhoneBook.Company;
            this.DateOfBirthTextBox.Text = DateFormMapper.GetValue(_msPhoneBook.DateOfBirth);
            this.ReligionDropDownList.SelectedValue = _msPhoneBook.Religion;
            this.EmailTextBox.Text = _msPhoneBook.Email;
            this.CityTextBox.Text = _msPhoneBook.City;
            this.PhoneBookGroupTextBox.Text = _msPhoneBook.PhoneBookGroup;
            this.fgBirthDayCheckBox.Checked = (_msPhoneBook.fgBirthDay == null) ? false : (bool)_msPhoneBook.fgBirthDay;
            this.BirthdayWishesTexBox.Text = _msPhoneBook.BirthdayWishes;
            this.RemarkTextBox.Text = _msPhoneBook.Remark;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsPhoneBook _msPhoneBook = this._smsMessageBL.GetSinglePhoneBook(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _msPhoneBook.OrganizationID = Convert.ToInt32(HttpContext.Current.Session["Organization"]);
            _msPhoneBook.UserID = HttpContext.Current.Session["userID"].ToString();
            _msPhoneBook.Name = this.NameTextBox.Text;
            _msPhoneBook.PhoneNumber = this.PrefixPhoneNumber.SelectedValue + this.PhoneNumberTextBox.Text;
            _msPhoneBook.Company = this.CompanyTextBox.Text;
            
            if (this.DateOfBirthTextBox.Text != "")
                _msPhoneBook.DateOfBirth = DateFormMapper.GetValue(this.DateOfBirthTextBox.Text);
            else
                _msPhoneBook.DateOfBirth = null;

            _msPhoneBook.Religion = this.ReligionDropDownList.SelectedValue ;
            _msPhoneBook.Email = this.EmailTextBox.Text;
            _msPhoneBook.City = this.CityTextBox.Text;
            _msPhoneBook.PhoneBookGroup = this.PhoneBookGroupTextBox.Text;
            _msPhoneBook.Remark = this.RemarkTextBox.Text;
            _msPhoneBook.fgBirthDay = this.fgBirthDayCheckBox.Checked;
            _msPhoneBook.BirthdayWishes = this.BirthdayWishesTexBox.Text;

            bool _result = this._smsMessageBL.EditPhoneBook(_msPhoneBook);

            if (_result == true)
            {
                Response.Redirect(this._contactsPage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._contactsPage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}
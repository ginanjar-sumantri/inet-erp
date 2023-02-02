using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Message
{
    public partial class ContactsAdd : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
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
                this.SubPageTitleLiteral.Text = "Contacts Add";

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.ResetButton.ImageUrl = "../images/reset.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
                
                this.DateOfBirthTextBox.Attributes.Add("ReadOnly", "True");

                this.PhoneNumberTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                this.PhoneNumberTextBox.Attributes.Add("OnChange", "HarusAngka(this);");

                this.PrefixPhoneNumber.DataSource = _smsMessageBL.GetListCountryCode();
                this.PrefixPhoneNumber.DataValueField = this.PrefixPhoneNumber.DataTextField = "CountryCode";
                this.PrefixPhoneNumber.DataBind();

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ClearData()
        {
            this.ClearLabel();

            this.NameTextBox.Text = "";
            this.PhoneNumberTextBox.Text = "";
            this.CompanyTextBox.Text = "";
            this.DateOfBirthTextBox.Text = "";            
            this.EmailTextBox.Text = "";
            this.CityTextBox.Text = "";
            this.PhoneBookGroupTextBox.Text = "";
            this.fgBirthDayCheckBox.Checked = false;
            this.BirthdayWishesTexBox.Text = "";
            this.RemarkTextBox.Text = "";            
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            MsPhoneBook _msPhoneBook = new MsPhoneBook();

            _msPhoneBook.OrganizationID = Convert.ToInt32(HttpContext.Current.Session["Organization"]);
            _msPhoneBook.UserID = HttpContext.Current.Session["userID"].ToString();
            _msPhoneBook.Name = this.NameTextBox.Text;
            _msPhoneBook.PhoneNumber = this.PrefixPhoneNumber.SelectedValue + this.PhoneNumberTextBox.Text;
            _msPhoneBook.Company = this.CompanyTextBox.Text;
            if (this.DateOfBirthTextBox.Text != "")
                _msPhoneBook.DateOfBirth = DateFormMapper.GetValue(this.DateOfBirthTextBox.Text);
            else
                _msPhoneBook.DateOfBirth = null;
            _msPhoneBook.Religion = this.ReligionDropDownList.SelectedValue;
            _msPhoneBook.Email = this.EmailTextBox.Text;
            _msPhoneBook.City = this.CityTextBox.Text;
            _msPhoneBook.PhoneBookGroup = this.PhoneBookGroupTextBox.Text;
            _msPhoneBook.Remark = this.RemarkTextBox.Text;
            _msPhoneBook.fgBirthDay = this.fgBirthDayCheckBox.Checked;
            _msPhoneBook.BirthdayWishes = this.BirthdayWishesTexBox.Text;

            bool _result = this._smsMessageBL.AddPhoneBook(_msPhoneBook);

            if (_result == true)
            {
                Response.Redirect(this._contactsPage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_contactsPage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}
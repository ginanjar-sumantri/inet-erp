using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal.Balance
{
    public partial class BalanceEdit : BalanceBase
    {
        BackEndBL _backEndBL = new BackEndBL();
        RegistrationBL _registrationBL = new RegistrationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (Session["userAdmin"] != null)
            {
                if (HttpContext.Current.Session["userAdmin"].ToString() == "" || HttpContext.Current.Session["userAdmin"].ToString() == null)
                {
                    Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
                }
            }
            else {
                Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
            }

            if (!this.Page.IsPostBack == true)
            {
                Clear();
                ShowData();
                this.AccountBalanceChangeTextBox.Text = "0";
            }

        this.AccountBalanceChangeTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
        this.AccountBalanceChangeTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
        
        }

        protected void ShowData()
        {
            MsOrganization _msOrganization = this._backEndBL.getSingleMsorganizationByID(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.OrganizationNameTextBox.Text = _msOrganization.OrganizationName;
            this.AccountBalanceTextBox.Text = Convert.ToDecimal(_msOrganization.MaskingBalanceAccount).ToString("0.00");
        }

        protected void Clear()
        {
            this.OrganizationNameTextBox.Text = "";
            this.AccountBalanceChangeTextBox.Text = "";
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void IncreaseButton_Click(object sender, EventArgs e)
        {
            if ( Convert.ToDecimal ( this.AccountBalanceChangeTextBox.Text ) > 0) {
                _backEndBL.EditBalance(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToDecimal(this.AccountBalanceChangeTextBox.Text), true, this.DescriptionTextBox.Text);
                Response.Redirect(this._homePage);
            } else {
                this.WarningLabel.Text = "Balance change amount must be more than 0.";
            }
        }
        protected void DecreaseButton_Click(object sender, EventArgs e)
        {
            if ( Convert.ToDecimal ( this.AccountBalanceChangeTextBox.Text ) > 0) {
                _backEndBL.EditBalance(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), Convert.ToDecimal(this.AccountBalanceChangeTextBox.Text), false, this.DescriptionTextBox.Text);
                Response.Redirect(this._homePage);
            }
            else
            {
                this.WarningLabel.Text = "Balance change amount must be more than 0.";
            }
        }
}
}

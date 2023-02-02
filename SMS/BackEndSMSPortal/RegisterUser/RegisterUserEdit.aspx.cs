using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal.RegisterUser
{
    public partial class RegisterUserEdit : RegisterUserBase
    {
        BackEndBL _backEndBL = new BackEndBL();
        RegistrationBL _registrationBL = new RegistrationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            //this.PasswordTextBox.Attributes.Add("ReadOnly", "True");

            //this.PageLabel.Text = "Register User";
            if (HttpContext.Current.Session["userAdmin"].ToString() == "" || HttpContext.Current.Session["userAdmin"].ToString() == null)
            {
                Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
            }
            else
            {
                if (!this.Page.IsPostBack == true)
                {
                    DDLPackageName();
                    Clear();
                    ShowData();
                }
            }
        }

        protected void ShowData()
        {
            MsOrganization _msOrganization = this._backEndBL.getSingleMsorganization(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            MsUser _msUser = this._backEndBL.getSingleMsUser(_msOrganization.OrganizationID.ToString(), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._dataKey), ApplicationConfig.EncryptionKey));

            this.OrganizationNameTextBox.Text = _msOrganization.OrganizationName;
            this.UserLimitTextBox.Text = _msOrganization.UserLimit.ToString();
            this.BalanceCheckCodeTextBox.Text = _msOrganization.BalanceCheckCode;
            this.UserIDTextBox.Text = _msUser.UserID;
            //this.PasswordTextBox.Text = Rijndael.Decrypt(_msUser.password, ApplicationConfig.EncryptionKey);

            foreach (ListItem _item in this.PackageNameDDL.Items)
            {
                if (_item.Text == _msUser.PackageName) _item.Selected = true;
                else _item.Selected = false;
            }
            //this.PackageNameDDL.SelectedItem.Text = _msUser.PackageName;

            this.EmailTextBox.Text = _msUser.Email;
        }

        protected void Clear()
        {
            this.OrganizationNameTextBox.Text = "";
            this.UserLimitTextBox.Text = "";
            this.BalanceCheckCodeTextBox.Text = "";
            this.UserIDTextBox.Text = "";
            //this.PasswordTextBox.Text = "";
            this.PackageNameDDL.SelectedIndex = 0;
            this.EmailTextBox.Text = "";
        }

        protected void DDLPackageName()
        {
            this.PackageNameDDL.Items.Clear();
            this.PackageNameDDL.DataTextField = "PackageName";
            this.PackageNameDDL.DataValueField = "SMSPerDay";
            this.PackageNameDDL.DataSource = this._backEndBL.GetListPackageForDDL();
            this.PackageNameDDL.DataBind();
            this.PackageNameDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.PackageNameDDL.SelectedIndex != 0)
            {
                MsOrganization _msOrganization = _backEndBL.getSingleMsorganization(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                MsUser _msUser = this._backEndBL.getSingleMsUser(_msOrganization.OrganizationID.ToString(), Rijndael.Decrypt(this._nvcExtractor.GetValue(this._dataKey), ApplicationConfig.EncryptionKey));

                _msOrganization.OrganizationName = this.OrganizationNameTextBox.Text;
                _msOrganization.UserLimit = Convert.ToInt16(this.UserLimitTextBox.Text);
                _msOrganization.BalanceCheckCode = this.BalanceCheckCodeTextBox.Text;
                
                bool _submit = _backEndBL.EditOrganization(_msOrganization);

                if (_submit == true)
                {
                    int _count = _registrationBL.GetOrganizationID(this.OrganizationNameTextBox.Text);
                    _msUser.OrganizationID = _count;
                    _msUser.UserID = this.UserIDTextBox.Text;
                    _msUser.PackageName = this.PackageNameDDL.SelectedItem.Text;
                    _msUser.Email = this.EmailTextBox.Text;
                    
                    bool _submit1 = _backEndBL.EditUser(_msUser);

                    if (_submit1 == true)
                    {
                        Response.Redirect(this._registerUserPage);
                    }
                }
            }
            else
            {
                this.WarningLabel.Text = "Please, Select Package Name";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._registerUserPage);
        }
    }
}

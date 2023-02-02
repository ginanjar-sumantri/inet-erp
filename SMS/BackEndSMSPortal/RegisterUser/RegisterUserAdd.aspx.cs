using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.BackEndSMSPortal.RegisterUser
{
    public partial class RegisterUserAdd : RegisterUserBase
    {
        BackEndBL _backEndBL = new BackEndBL();
        RegistrationBL _registrationBL = new RegistrationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.PageLabel.Text = "Register User";
            if (Session["userAdmin"] != null)
            {
                if (HttpContext.Current.Session["userAdmin"].ToString() == "" || HttpContext.Current.Session["userAdmin"].ToString() == null)
                {
                    Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
                }
                else
                {
                    if (!this.Page.IsPostBack == true)
                    {
                        this.DDLPackageName();
                        this.ClearData();
                    }
                }
            }
            else {
                Response.Redirect("../BackEndLogin/BackEndLogin.aspx");
            }
        }

        protected void ClearData()
        {
            this.OrganizationNameTextBox.Text = "";
            this.UserLimitTextBox.Text = "";
            this.BalanceCheckCodeTextBox.Text = "";
            this.UserIDTextBox.Text = "";
            this.PasswordTextBox.Text = "";
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
                if (_backEndBL.CekOrganization(this.OrganizationNameTextBox.Text))
                {
                    MsOrganization _msOrganization = new MsOrganization();
                    
                    _msOrganization.OrganizationName = this.OrganizationNameTextBox.Text;
                    _msOrganization.UserLimit = Convert.ToInt16(this.UserLimitTextBox.Text);
                    _msOrganization.BalanceCheckCode = this.BalanceCheckCodeTextBox.Text;
                    _msOrganization.BalanceCheckRequest = false;
                    _msOrganization.LastBalance = "";
                    _msOrganization.GatewayStatus = false;
                    _msOrganization.Email = this.EmailTextBox.Text;
                    _msOrganization.FooterAdditionalMessage = "";
                    _msOrganization.GatewayStatusNoticeLastSent = DateTime.Now.Date ;
                    _msOrganization.GlobalReplyMessage = "";
                    _msOrganization.MaskingBalanceAccount = 0;
                    _msOrganization.MaskingSD = 1;
                    _msOrganization.FgHosted = this.HostedCheckBox.Checked;
                    
                    bool _submit = _registrationBL.AddOrganization(_msOrganization);

                    if (_submit == true)
                    {
                        int _orgID = _registrationBL.GetOrganizationID(this.OrganizationNameTextBox.Text);
                        
                        MsUser _msUserWebAmin = new MsUser();
                        _msUserWebAmin.OrganizationID = _orgID;
                        _msUserWebAmin.UserID = this.UserIDTextBox.Text;
                        _msUserWebAmin.password = Rijndael.Encrypt(this.PasswordTextBox.Text, ApplicationConfig.EncryptionKey);
                        _msUserWebAmin.fgAdmin = false;
                        _msUserWebAmin.fgWebAdmin = true;
                        _msUserWebAmin.RegistrationDate = DateTime.Now;
                        _msUserWebAmin.ExpiredDate = DateTime.Now.AddYears(1);
                        _msUserWebAmin.PackageName = this.PackageNameDDL.SelectedItem.Text;
                        _msUserWebAmin.SMSLimit = Convert.ToInt32(this.PackageNameDDL.SelectedValue);
                        _msUserWebAmin.LastLimitReset = DateTime.Now;
                        _msUserWebAmin.Email = this.EmailTextBox.Text;
                        _msUserWebAmin.fgActive = true;

                        MsUser _msUserAdminApps = new MsUser();
                        _msUserAdminApps.OrganizationID = _orgID;
                        _msUserAdminApps.UserID = "admin";
                        _msUserAdminApps.password = Rijndael.Encrypt("admin", ApplicationConfig.EncryptionKey);
                        _msUserAdminApps.fgAdmin = true;
                        _msUserAdminApps.fgWebAdmin = false;
                        _msUserAdminApps.RegistrationDate = DateTime.Now;
                        _msUserAdminApps.ExpiredDate = DateTime.Now.AddYears(1);
                        _msUserAdminApps.PackageName = this.PackageNameDDL.SelectedItem.Text;
                        _msUserAdminApps.SMSLimit = Convert.ToInt32(this.PackageNameDDL.SelectedValue);
                        _msUserAdminApps.LastLimitReset = DateTime.Now;
                        _msUserAdminApps.Email = this.EmailTextBox.Text;
                        _msUserAdminApps.fgActive = true;

                        bool _submit1 = _registrationBL.RegisterNewAdminUsers(_msUserWebAmin, _msUserAdminApps);

                        if (_submit1 == true)
                        {
                            this.ClearData();
                            this.WarningLabel.Text = "Save Success";
                        }
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Organization Name alrady exist";
                }
            }
            else
            {
                this.WarningLabel.Text = "Please, Select Organization Name";
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(this._registerUserPage);
        }
    }
}

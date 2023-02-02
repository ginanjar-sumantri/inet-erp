using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.ManageUser
{
    public partial class ManageUserAdd : System.Web.UI.Page
    {
        ManageUserBL _manageUserBL = new ManageUserBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack == true)
            {
                this.PageLabel.Text = "MANAGE USER ADD";
                this.SaveImageButton.ImageUrl = "../images/save.jpg";
                this.CancelImageButton.ImageUrl = "../images/cancel.jpg";
            }
        }

        protected void Clear()
        {
            this.UserIDTextBox.Text = "";
            this.PasswordTextBox.Text = "";
            this.ConfirmPasswordTextBox.Text = "";
            this.WarningLabel.Text = "";
            this.EmailTextBox.Text = "";
        }

        protected void SaveImageButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _count = _manageUserBL.CekCountUserInOrganization(Convert.ToInt32(HttpContext.Current.Session["Organization"].ToString()));

            if (_count)
            {
                if (this.UserIDTextBox.Text != "" && this.PasswordTextBox.Text != "" && this.ConfirmPasswordTextBox.Text != "")
                {
                    if (HttpContext.Current.Session["FgWebAdmin"].ToString() == "True")
                    {
                        bool _Cek = _manageUserBL.CeknothaveUser(Convert.ToInt16(HttpContext.Current.Session["Organization"].ToString()), this.UserIDTextBox.Text);

                        if (_Cek == true)
                        {
                            //Double _id = _manageUserBL.GetOrganizationID(HttpContext.Current.Session["Organization"].ToString());

                            MsUser _user = _manageUserBL.GetSingle(Convert.ToInt16(HttpContext.Current.Session["Organization"].ToString()), HttpContext.Current.Session["UserID"].ToString());
                            MSPackage _msPackage = _manageUserBL.GetSingle(_user.PackageName);

                            MsUser _msUser = new MsUser();
                            _msUser.OrganizationID = Convert.ToInt32(HttpContext.Current.Session["Organization"].ToString());
                            _msUser.UserID = this.UserIDTextBox.Text;
                            _msUser.password = Rijndael.Encrypt(this.PasswordTextBox.Text, ApplicationConfig.EncryptionKey);
                            _msUser.fgAdmin = false;
                            _msUser.fgWebAdmin = false;
                            _msUser.RegistrationDate = DateTime.Now;
                            _msUser.ExpiredDate = DateTime.Now.AddYears(1);
                            _msUser.PackageName = _user.PackageName;
                            _msUser.SMSLimit = _msPackage.SMSPerDay;
                            _msUser.LastLimitReset = DateTime.Now;
                            _msUser.Email = this.EmailTextBox.Text;
                            _msUser.fgActive = true;

                            bool _submit = _manageUserBL.Add(_msUser);

                            if (_submit == true)
                            {
                                Clear();
                                this.WarningLabel.Text = "SUKSESS";
                            }
                        }
                        else
                        {
                            this.WarningLabel.Text = "Sorry, User Name Already Exists";
                        }
                    }
                    else
                    {
                        this.WarningLabel.Text = "Must be Admin can be create manage User";
                    }
                }
                else
                {
                    this.WarningLabel.Text = "Please, insert Text Box";
                }
            }
            else
                this.WarningLabel.Text = "Cann't add user, because user is Limit";
        }

        protected void CancelImageButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManageUser.aspx");
        }
    }
}
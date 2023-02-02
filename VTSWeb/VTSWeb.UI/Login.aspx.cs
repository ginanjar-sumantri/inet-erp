using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VTSWeb.BusinessRule;
using VTSWeb.SystemConfig;
using VTSWeb.Common;

public partial class Login : System.Web.UI.Page
{
    UserBL _userBL = new UserBL();

    protected void Page_Load(object sender, EventArgs e)
    {

        Literal _compName = (Literal)this.FindControl("CompanyNameLiteral");
        _compName.Text = ApplicationConfig.CompanyName;

        this.AppNameLiteral.Text = ApplicationConfig.CompanyName + " :: Login";

        Literal _failureText = (Literal)this.FindControl("FailureText2");
        _failureText.Text = Request.QueryString["FailureText"];

        LinkButton _forgotPassword = (LinkButton)this.FindControl("ForgotPasswordLinkButton");
        _forgotPassword.PostBackUrl = ApplicationConfig.ForgotPassword;

        this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish + "StyleSheet.css\" rel=\"Stylesheet\" />";

        if (this.Page.IsPostBack == false)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {

            }
            else
            {
                TextBox _userName = (TextBox)this.FindControl("UserName");
                //DropDownList _connMode = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");

                _userName.Text = cookie["Name"];
                //_compList.SelectedValue = cookie["Company"];
                //this.BindDataToInstanceDDL(new Guid(_compList.SelectedValue));
                //_connMode.SelectedValue = cookie["Instance"];

            }
            //this.ShowInstanceDDL();
        }
    }

    protected void ShowInstanceDDL()
    {
        //var _collection = _userBL.GetInstanceFromRegistry();

        //int i = 0;
        //foreach (var _item in _collection)
        //{
        //    this.ConnModeDropDownList.Items.Insert(i, new ListItem(_item, i.ToString()));
        //    i++;
        //}
        //this.ConnModeDropDownList.Items.Insert(0, new ListItem("Production", "0"));
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        String _password = Rijndael.Encrypt(this.Password.Text, ApplicationConfig.EncryptionKey);

        Boolean _user = _userBL.ValidateUser(this.UserName.Text, _password, "Production");

        if (_user == true)
        {
            HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
            if (cookie == null)
            {
                cookie = new HttpCookie(ApplicationConfig.CookiesPreferences);
            }

            cookie[ApplicationConfig.CookieName] = this.UserName.Text;
            cookie[ApplicationConfig.CookiePassword] = this.Password.Text;
            //GetConString.ConnString = this.ConnModeDropDownList.SelectedItem.Text;
            GetConString.ConnString = "Production";
            cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(ApplicationConfig.LoginLifeTimeExpired));
            Response.Cookies.Add(cookie);

            Response.Redirect("Default.aspx");
        }
        else
        {
            this.FailureText.Text = "UserName And Password Wrong";
        }
    }
}

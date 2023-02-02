using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTSWeb.SystemConfig;
using VTSWeb.Common;
using VTSWeb.CustomControl;
using VTSWeb.BusinessRule;

public partial class _default : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
        if (cookie == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!this.Page.IsPostBack == true)
        {
            this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish + "StyleSheet.css\" rel=\"Stylesheet\" />";
            this.JScriptLiteral.Text = "<script src=\"" + "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish + "JScript.js" + "\" type=\"text/javascript\"></script>";

            this.Menu1.StaticPopOutImageUrl = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish + "images/transparent.gif";

            this.ShowData();
        }
    }
    protected void ShowData()
    {
        HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];

        this.LogoutButton.Attributes.Add("OnClick", "return AskYouFirstLogOut();");

        if (cookie == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            this.LoginNameLabel.Text = "Welcome, " + (((cookie[ApplicationConfig.CookieName]).ToString() == "") ? "" : (cookie[ApplicationConfig.CookieName]).ToString());
            cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(ApplicationConfig.LoginLifeTimeExpired));
            Response.Cookies.Add(cookie);
        //this.InstanceHidenField.Value = cookie[ApplicationConfig.CookieInstance] == "" ? "" : cookie[ApplicationConfig.CookieInstance];

            String _homeURL = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish;
            NavigationMenu _navigationMenu = new NavigationMenu();
            this.Menu1.MaximumDynamicDisplayLevels = 5;
            //_navigationMenu.RenderMenu(0, this.Menu1, _homeURL);
            _navigationMenu.RenderMenu(0, new PermissionBL().GetEmployeeLevelByUserName((cookie[ApplicationConfig.CookieName]).ToString()), this.Menu1, _homeURL, (cookie[ApplicationConfig.CookieName]).ToString());
            this.Menu1.ItemWrap = true;

        //    _navigationMenu.Dispose();
        }
        
    }
    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        //WorkstationBL _workstationBL = new WorkstationBL();
        HttpCookie cookie = Request.Cookies[ApplicationConfig.CookiesPreferences];
        //_workstationBL.logingOut(cookie[ApplicationConfig.CookieName].ToString());
        GetConString.ConnString = "";
        cookie[ApplicationConfig.CookieName] = "";
        cookie[ApplicationConfig.CookiePassword] = "";
        cookie.Expires = DateTime.Now;
        Response.Cookies.Add(cookie);
        Response.Redirect("http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + ApplicationConfig.StringSeparatorPublish + "Login.aspx");
    }
  
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {

    }

}

using System;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using System.Web;
using System.Web.UI.HtmlControls;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.bodyLogin.Attributes.Add("onload", "if (self.parent.frames.length != 0)self.parent.location=document.location;");

        UserBL _user = new UserBL();
        String _theme = ApplicationConfig.LoginTheme ;
        this.bodyLogin.Style.Add("background-image", "url('" + ApplicationConfig.HomeWebAppURL + _user.GetThemeComponent(ThemeComponent.BackgroundimageLogin, _theme) + "');");
        this.bodyLogin.Style.Add("background-color", _user.GetThemeComponent(ThemeComponent.BackgroundColorLogin, _theme));
        HtmlTable _panelLogin = (HtmlTable)this.Login1.FindControl("panelLogin");
        _panelLogin.Style.Add("background-image", "url('" + ApplicationConfig.HomeWebAppURL + _user.GetThemeComponent(ThemeComponent.BackgroundImagePanelLogin, _theme) + "');");
        _panelLogin.Width = _user.GetThemeComponent(ThemeComponent.PanelLoginWidth, _theme);
        _panelLogin.Height = _user.GetThemeComponent(ThemeComponent.PanelLoginHeight, _theme);

        Literal _compName = (Literal)this.Login1.FindControl("CompanyNameLiteral");
        //Literal _appName = (Literal)this.Login1.FindControl("AppNameLiteral");

        _compName.Text = ApplicationConfig.CompanyName;
        //_appName.Text = ApplicationConfig.MembershipAppName;

        this.AppNameLiteral.Text = ApplicationConfig.MembershipAppName + " :: Login";

        Literal _failureText = (Literal)this.Login1.FindControl("FailureText2");
        _failureText.Text = Request.QueryString["FailureText"];

        LinkButton _forgotPassword = (LinkButton)this.Login1.FindControl("ForgotPasswordLinkButton");
        _forgotPassword.PostBackUrl = ApplicationConfig.ForgotPassword;

        this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + ApplicationConfig.HomeWebAppURL + "StyleSheet.css\" rel=\"Stylesheet\" />";

        if (this.Page.IsPostBack == false)
        {
            DropDownList _compList = (DropDownList)this.Login1.FindControl("CompanyDropDownList");

            _compList.DataSource = new CompanyBL().GetListForDDL();
            _compList.DataTextField = "Name";
            _compList.DataValueField = "CompanyID";
            _compList.DataBind();

            this.BindDataToInstanceDDL(new Guid(_compList.SelectedValue));

            HttpCookie cookie = Request.Cookies["Preferences"];
            if (cookie == null)
            {

            }
            else
            {
                TextBox _userName = (TextBox)this.Login1.FindControl("UserName");
                DropDownList _connMode = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");

                _userName.Text = cookie["Name"];
                _compList.SelectedValue = cookie["Company"];
                this.BindDataToInstanceDDL(new Guid(_compList.SelectedValue));
                _connMode.SelectedValue = cookie["Instance"];

            }
        }
    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        UserBL _user = new UserBL();

        TextBox _userName = (TextBox)this.Login1.FindControl("UserName");
        TextBox _password = (TextBox)this.Login1.FindControl("Password");
        DropDownList _connMode = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");
        DropDownList _compList = (DropDownList)this.Login1.FindControl("CompanyDropDownList");

        _user.SaveLastConnectionMode(_userName.Text, _compList.SelectedValue, _connMode.SelectedValue);

        master_Company_aspnet_User _compAndUser = new CompanyBL().GetSingleCompanyUser(_compList.SelectedValue, _user.GetUserIDByName(_userName.Text));
        if (_compAndUser == null)
        {
            Response.Redirect(ApplicationConfig.LoginPage + "?FailureText=" + System.Web.HttpUtility.UrlEncode("User not associated with the company(" + _compList.SelectedItem.Text + ")."));
        }
        else
        {
            Boolean _IsUserAssosiatedWithTheDB = new CompanyBL().GetSingleDatabaseUser(new Guid(_connMode.SelectedValue), _compAndUser.UserID);

            if (_IsUserAssosiatedWithTheDB == true)
            {
                this.Login1.DestinationPageUrl = ApplicationConfig.HomeWebAppURL + "index.aspx";
                Response.Redirect(this.Login1.DestinationPageUrl);
            }
            else
            {
                Response.Redirect(ApplicationConfig.LoginPage + "?FailureText=" + System.Web.HttpUtility.UrlEncode("User not associated with the database(" + _connMode.SelectedItem.Text + ") of " + _compList.SelectedItem.Text + "."));
            }
        }
    }

    protected void CompanyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList _compList = (DropDownList)this.Login1.FindControl("CompanyDropDownList");

        this.BindDataToInstanceDDL(new Guid(_compList.SelectedValue));
    }

    private Boolean BindDataToInstanceDDL(Guid _prmCompID)
    {
        Boolean _result = false;

        try
        {
            DropDownList _instanceList = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");
            _instanceList.Items.Clear();

            List<master_Database> _database = new List<master_Database>();
            _database = new CompanyBL().GetListDatabaseByCompany(_prmCompID);
            foreach (var _item in _database)
            {
                _instanceList.Items.Add(new ListItem(ConnectionModeMapper.GetLabel(ConnectionModeMapper.MapThis(_item.Status)), _item.DatabaseID.ToString()));
            }
        }
        catch (Exception)
        {

        }

        return _result;
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        TextBox _userName = (TextBox)this.Login1.FindControl("UserName");
        DropDownList _connMode = (DropDownList)this.Login1.FindControl("ConnModeDropDownList");
        DropDownList _compList = (DropDownList)this.Login1.FindControl("CompanyDropDownList");

        HttpCookie cookie = Request.Cookies["Preferences"];
        if (cookie == null)
        {
            cookie = new HttpCookie("Preferences");
        }

        cookie["Name"] = _userName.Text;
        cookie["Instance"] = _connMode.SelectedValue;
        cookie["Company"] = _compList.SelectedValue;
        cookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(ApplicationConfig.LoginLifeTimeExpired));
        Response.Cookies.Add(cookie);
    }
}
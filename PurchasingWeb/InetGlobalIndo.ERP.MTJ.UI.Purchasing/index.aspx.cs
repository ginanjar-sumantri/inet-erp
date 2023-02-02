using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
        ////////////////////////////////////////TAMBAHAN THEMING///////////////////////////////////////
        CompanyConfig _companyConfig = new CompanyConfig();
        String _theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
        String _bgImage = _companyConfig.GetThemeComponent(ThemeComponent.BackgroundImage, _theme);
        this.bodyIndex.Style.Add("background-image", "url('" + ApplicationConfig.HomeWebAppURL + "images/" + _bgImage + "')");
        this.bodyIndex.Style.Add("background-repeat", "repeat-x");
        this.bodyIndex.Style.Add("background-color", _companyConfig.GetThemeComponent(ThemeComponent.BackgroundColorBody, _theme));
        ////////////////////////////////////////TAMBAHAN THEMING///////////////////////////////////////

        this.ModuleTitleLiteral.Text = "Purchasing";
        this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + ApplicationConfig.HomeWebAppURL + "StyleSheet.css\" rel=\"Stylesheet\" />";
        this.LoginStatus1.LogoutImageUrl = ApplicationConfig.HomeWebAppURL + "images/logout.jpg";
        this.ConnModeLiteral.Text = ConnectionModeMapper.GetLabel(new UserBL().ConnectionMode(HttpContext.Current.User.Identity.Name));

        if (!this.Page.IsPostBack)
        {
            if (Request.QueryString["showPage"] != null)
                this.mainFrame.Attributes.Add("src", Request.QueryString["showPage"] + ".aspx" );
            else
                this.mainFrame.Attributes.Add("src", "Default.aspx");

            this.Menu2.StaticPopOutImageUrl = ApplicationConfig.HomeWebAppURL + "images/transparent.gif";

            ////////////////////////////////////////TAMBAHAN POSISI LOGO///////////////////////////////////////
            this.welcometable.Style.Add("color", _companyConfig.GetThemeComponent(ThemeComponent.WelcomeTextColor, _theme));
            if (_companyConfig.GetSingle(CompanyConfigure.PosisiLogo).SetValue == "0")
            {
                this.AppImage.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/logo.gif";
                this.CompanyNameLiteral.Text = new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name);
                this.CompanyLogoImage.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name);
            }
            else if (_companyConfig.GetSingle(CompanyConfigure.PosisiLogo).SetValue == "1")
            {
                this.CompanyLogoImage.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/logo.gif";
                this.CompanyNameLiteral.Text = new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name);
                this.AppImage.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name);
            }
            ////////////////////////////////////////TAMBAHAN POSISI LOGO///////////////////////////////////////

            String[] _roles = new RoleBL().GetRolesIDByUserName(HttpContext.Current.User.Identity.Name);
            TopNavigationMenu _topNavigationMenu = new TopNavigationMenu();
            this.Menu2.MaximumDynamicDisplayLevels = 5;
            _topNavigationMenu.RenderItem(0, _roles, ERPModule.Purchasing, this.Menu2, new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name));
            _topNavigationMenu.Dispose();
        }
    }
}

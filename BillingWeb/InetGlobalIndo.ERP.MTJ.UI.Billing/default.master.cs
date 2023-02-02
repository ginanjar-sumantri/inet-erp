using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.CustomControl;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.BusinessRule;

public partial class _default : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            ////////////////////////////////////////TAMBAHAN THEMING///////////////////////////////////////
            CompanyConfig _companyConfig = new CompanyConfig();
            String _theme = _companyConfig.GetSingle(CompanyConfigure.Theme).SetValue;
            String _bgImage = _companyConfig.GetThemeComponent(ThemeComponent.BackgroundImageBawah, _theme);
            this.bodyDefaultMaster.Style.Add("background-image", "url('" + ApplicationConfig.HomeWebAppURL + "images/" + _bgImage + "')");
            this.bodyDefaultMaster.Style.Add("background-repeat", "repeat-x");
            this.bodyDefaultMaster.Style.Add("background-color", _companyConfig.GetThemeComponent(ThemeComponent.BackgroundColorBody, _theme));
            ////////////////////////////////////////TAMBAHAN THEMING///////////////////////////////////////


            this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + ApplicationConfig.HomeWebAppURL + "StyleSheet.css\" rel=\"Stylesheet\" />";


            //this.AppImage.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/logo.jpg";
            //this.imgPrev.Src = ApplicationConfig.HomeWebAppURL + "images/prev1.jpg";
            //this.imgNext.Src = ApplicationConfig.HomeWebAppURL + "images/next1.jpg";
            //this.CompanyNameLiteral.Text = ApplicationConfig.CompanyName;
            //this.CompanyNameLiteral.Text = new UserBL().ConnectionCompany(HttpContext.Current.User.Identity.Name);
            //this.CompanyLogoImage.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + ApplicationConfig.CompanyLogo;
            //this.CompanyLogoImage.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/" + new UserBL().CompanyLogo(HttpContext.Current.User.Identity.Name);
            //this.ConnModeLiteral.Text = ConnectionModeMapper.GetLabel(new UserBL().ConnectionMode(HttpContext.Current.User.Identity.Name));
            this.JScriptLiteral.Text = "<script src=\"" + ApplicationConfig.HomeWebAppURL + "JScript.js" + "\" type=\"text/javascript\"></script>";
            //this.LoginStatus1.LogoutImageUrl = ApplicationConfig.HomeWebAppURL + "images/logout.jpg";

            //QuickLaunchNavigationMenu _quickLaunchNavMenu = new QuickLaunchNavigationMenu();
            //this.Menu1.StaticDisplayLevels = Convert.ToInt32(ApplicationConfig.QuickLaunchNavStaticDisplayLevel);
            //this.Menu1.Visible = false;
            //this.Menu1.Items.Add(_quickLaunchNavMenu.RenderItem(24, new AppModule().GetValue(ERPModule.ShipPorting), "administrators", null));
            //_quickLaunchNavMenu.Dispose();

            //string _imageUrl = ApplicationConfig.HomeWebAppURL + "images/billing_small.jpg";
            //string _titleTree = "&nbsp;Billing";
            //QuickLaunchNavigationMenu _quickLaunchNavNode = new QuickLaunchNavigationMenu();
            //this.TreeView1.ShowLines = true;
            //this.TreeView1.Nodes.Add(_quickLaunchNavNode.RenderNode(956, AppModule.GetValue(ERPModule.Billing), "administrators", null, _imageUrl, _titleTree));
            //_quickLaunchNavNode.Dispose();

            //string _dir = Request.ServerVariables["APPL_PHYSICAL_PATH"];
            //string _title = "Billing";
            //QuickLaunchNavigationMenu _quickLaunchSiteMapPath = new QuickLaunchNavigationMenu();
            //_quickLaunchSiteMapPath.GenerateSiteMapPath(956, AppModule.GetValue(ERPModule.Billing), "administrators", null, _dir, _title, 956);

            //String[] _roles = new RoleBL().GetRolesIDByUserName(HttpContext.Current.User.Identity.Name);

            //TopNavigationMenu _topNavigationMenu = new TopNavigationMenu();
            //this.Menu2.MaximumDynamicDisplayLevels = 5;
            //_topNavigationMenu.RenderItem(0, _roles, ERPModule.Billing, this.Menu2, new UserBL().GetCompanyId(HttpContext.Current.User.Identity.Name));
            //_topNavigationMenu.Dispose();


        }
    }
}

using System;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

public partial class PopUp : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack == true)
        {
            this.StyleSheetLiteral.Text = "<link type=\"text/css\" href=\"" + ApplicationConfig.HomeWebAppURL + "StyleSheet.css\" rel=\"Stylesheet\" />";
            this.JScriptLiteral.Text = "<script src=\"" + ApplicationConfig.HomeWebAppURL + "JScript.js" + "\" type=\"text/javascript\"></script>";
        }
    }
}

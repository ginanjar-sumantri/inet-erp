using System;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AppNameLiteral.Text = ApplicationConfig.MembershipAppName + " :: Forgot Password";
    }
}
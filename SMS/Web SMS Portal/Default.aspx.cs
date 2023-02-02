using System;
using SMSLibrary;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect(Request.ApplicationPath + "Login/Login.aspx");
    }
}

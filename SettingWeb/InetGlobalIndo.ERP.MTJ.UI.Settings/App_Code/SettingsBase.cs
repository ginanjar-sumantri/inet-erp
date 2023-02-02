using InetGlobalIndo.ERP.MTJ.SystemConfig;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings
{
    public abstract class SettingsBase : System.Web.UI.Page
    {
        private string _pageTitle = ApplicationConfig.MembershipAppName + " :: Settings";

        protected short _moduleID = 23;
        protected bool _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.SettingsWebAppURL + "ErrorPermission.aspx";

        public SettingsBase()
        {
            this.Title = this._pageTitle;
        }
    }
}
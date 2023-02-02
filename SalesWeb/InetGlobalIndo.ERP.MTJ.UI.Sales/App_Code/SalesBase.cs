using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Sales
{
    public abstract class SalesBase : System.Web.UI.Page
    {
        private string _pageTitle = ApplicationConfig.MembershipAppName + " :: Sales";

        protected short _moduleID = 22;
        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";

        public SalesBase()
        {
            this.Title = this._pageTitle;
        }
    }
}
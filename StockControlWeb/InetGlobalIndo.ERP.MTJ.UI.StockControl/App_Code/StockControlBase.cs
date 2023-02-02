using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl
{
    public abstract class StockControlBase : System.Web.UI.Page
    {
        private string _pageTitle = ApplicationConfig.MembershipAppName + " :: Stock Control";

        protected short _moduleID = 25;
        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.StockControlWebAppURL + "ErrorPermission.aspx";

        public StockControlBase()
        {
            this.Title = this._pageTitle;
        }
    }
}
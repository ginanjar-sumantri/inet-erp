
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
namespace InetGlobalIndo.ERP.MTJ.UI.Home.ChangePassword
{
    public abstract class ChangePasswordBase : HomeBase
    {
        protected short _menuID = 471;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";

        protected string _pageTitleLiteral = "Change Password";

        public ChangePasswordBase()
        {
        }

        ~ChangePasswordBase()
        {
        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAMaintanance
{
    public abstract class FAMaintananceBase : AccountingBase
    {
        protected short _menuID = 45;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FAMaintenance.aspx";
        protected string _addPage = "FAMaintenanceAdd.aspx";
        protected string _editPage = "FAMaintenanceEdit.aspx";
        protected string _viewPage = "FAMaintenanceView.aspx";
        protected string _addDetailPage = "FAMaintenanceAcc.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Fixed Asset Maintenance";

        protected NameValueCollectionExtractor _nvcExtractor;

        public FAMaintananceBase()
        {

        }

        ~FAMaintananceBase()
        {

        }
    }
}

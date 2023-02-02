using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Subled
{
    public abstract class SubledBase : AccountingBase
    {
        protected short _menuID = 35;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "Subled.aspx";
        protected string _addPage = "SubledAdd.aspx";
        protected string _editPage = "SubledEdit.aspx";
        protected string _viewPage = "SubledView.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;
        
        protected string _pageTitleLiteral = "Sub Ledger";

        public SubledBase()
        {

        }

        ~SubledBase()
        {

        }
    }
}

using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierBeginning
{
    public abstract class SupplierBeginningBase : FinanceBase
    {
        protected short _menuID = 482;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "SuppBeginning.aspx";
        protected string _addPage = "SuppBeginningAdd.aspx";
        protected string _editPage = "SuppBeginningEdit.aspx";
        protected string _viewPage = "SuppBeginningView.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Supplier Beginning";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SupplierBeginningBase()
        {

        }

        ~SupplierBeginningBase()
        {

        }
    }
}
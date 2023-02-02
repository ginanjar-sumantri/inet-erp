using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerBeginning
{
    public abstract class CustomerBeginningBase : FinanceBase
    {
        protected short _menuID = 478;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting;

        protected string _homePage = "CustBeginning.aspx";
        protected string _addPage = "CustBeginningAdd.aspx";
        protected string _editPage = "CustBeginningEdit.aspx";
        protected string _viewPage = "CustBeginningView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Customer Beginning";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerBeginningBase()
        {

        }

        ~CustomerBeginningBase()
        {

        }
    }
}
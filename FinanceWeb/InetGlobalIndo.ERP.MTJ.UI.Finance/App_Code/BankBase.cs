using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.Bank
{
    public abstract class BankBase : FinanceBase
    {
        protected short _menuID = 13;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;

        protected string _codeKey = "code";

        protected string _homePage = "Bank.aspx";
        protected string _addPage = "BankAdd.aspx";
        protected string _editPage = "BankEdit.aspx";
        protected string _viewPage = "BankView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Bank";

        public BankBase()
        {

        }

        ~BankBase()
        {

        }
    }
}
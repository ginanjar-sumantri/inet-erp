using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TransactionClose
{
    public abstract class TransactionCloseBase : AccountingBase
    {
        protected short _menuID = 1501;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "TransactionClose.aspx";
        protected string _addPage = "TransactionCloseAdd.aspx";
        protected string _editPage = "TransactionCloseEdit.aspx";
        protected string _viewPage = "TransactionCloseView.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Transaction Close";


        public TransactionCloseBase()
        {

        }

        ~TransactionCloseBase()
        {

        }
    }
}
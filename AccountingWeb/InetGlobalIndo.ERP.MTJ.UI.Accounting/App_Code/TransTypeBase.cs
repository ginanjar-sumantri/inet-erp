using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.TransType
{
    public abstract class TransTypeBase : AccountingBase
    {
        protected short _menuID = 2;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "AccountAuthor.aspx";
        protected string _addPage = "AccountAuthorAdd.aspx";
        protected string _transHomePage = "TransType.aspx";
        protected string _transAddPage = "TransTypeAdd.aspx";
        protected string _transEditPage = "TransTypeEdit.aspx";
        protected string _transViewPage = "AccountAuthor.aspx";

        protected string _codeKey = "code";

        protected string _authorPageTitleLiteral = "Transaction Type Setup Account";
        protected string _transPageTitleLiteral = "Transaction Type File";

        protected NameValueCollectionExtractor _nvcExtractor;

        public TransTypeBase()
        {

        }

        ~TransTypeBase()
        {

        }
    }
}
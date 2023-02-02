using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.LimitAuthorization
{
    public abstract class LimitAuthorizationBase : FinanceBase
    {
        protected short _menuID = 995;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete;

        protected string _homePage = "LimitAuthorization.aspx";
        protected string _addPage = "LimitAuthorizationAdd.aspx";
        protected string _editPage = "LimitAuthorizationEdit.aspx";
        protected string _viewPage = "LimitAuthorizationView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _transTypeKey = "TransTypeCode";
        
        protected string _pageTitleLiteral = "Limit Authorization";

        protected NameValueCollectionExtractor _nvcExtractor;

        public LimitAuthorizationBase()
        {

        }

        ~LimitAuthorizationBase()
        {

        }
    }
}
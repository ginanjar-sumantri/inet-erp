using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FACondition
{
    public abstract class FAConditionBase : AccountingBase
    {
        protected short _menuID = 46;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FACondition.aspx";
        protected string _addPage = "FAConditionAdd.aspx";
        protected string _editPage = "FAConditionEdit.aspx";
        protected string _viewPage = "FAConditionView.aspx";

        protected string _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Fixed Asset Condition";

        public FAConditionBase()
        {

        }

        ~FAConditionBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAGroup
{
    public abstract class FAGroupBase : AccountingBase
    {
        protected short _menuID = 52;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FAGroup.aspx";
        protected string _addPage = "FAGroupAdd.aspx";
        protected string _editPage = "FAGroupEdit.aspx";
        protected string _viewPage = "FAGroupView.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Fixed Asset Group";

        protected NameValueCollectionExtractor _nvcExtractor;

        public FAGroupBase()
        {

        }

        ~FAGroupBase()
        {

        }
    }
}
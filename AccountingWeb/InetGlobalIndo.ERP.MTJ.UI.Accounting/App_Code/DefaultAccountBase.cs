using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.DefaultAccount
{
    public abstract class DefaultAccountBase : AccountingBase
    {
        protected short _menuID = 858;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "DefaultAccount.aspx";
        protected String _addPage = "DefaultAccountAdd.aspx";
        protected String _editPage = "DefaultAccountEdit.aspx";
        protected String _detailPage = "DefaultAccountDetail.aspx";
        protected String _addDetailPage = "DefaultAccountDtAdd.aspx";
        protected String _editDetailPage = "DefaultAccountDtEdit.aspx";

        protected String _codeKey = "SetupCode";
        protected String _codeValue = "SetValue";
      
        protected NameValueCollectionExtractor _nvcExtractor;

        protected String _pageTitleLiteral = "Default Account";

        public DefaultAccountBase()
        {

        }

        ~DefaultAccountBase()
        {

        }
    }
}
using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Account
{
    public abstract class AccountBase : AccountingBase
    {
        protected short _menuID = 12;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _codeKey = "code";

        protected String _homePage = "Account.aspx";
        protected String _addPage = "AccountAdd.aspx";
        protected String _editPage = "AccountEdit.aspx";
        protected String _viewPage = "AccountView.aspx";
        protected String _importPage = "AccountImport.aspx";

        protected String _pageTitleLiteral = "Account File";

        protected NameValueCollectionExtractor _nvcExtractor;

        public AccountBase()
        {
        }

        ~AccountBase()
        {
        }
    }
}
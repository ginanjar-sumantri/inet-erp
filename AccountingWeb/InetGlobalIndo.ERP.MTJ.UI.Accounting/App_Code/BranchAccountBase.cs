using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.BranchAccount
{
    public abstract class BranchAccountBase : AccountingBase
    {
        protected short _menuID = 1716;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _codeKey = "code";

        protected String _homePage = "BranchAccount.aspx";
        protected String _addPage = "BranchAccountAdd.aspx";
        protected String _editPage = "BranchAccountEdit.aspx";
        protected String _viewPage = "BranchAccountView.aspx";

        protected String _pageTitleLiteral = "Branch Account";

        protected NameValueCollectionExtractor _nvcExtractor;

        public BranchAccountBase()
        {

        }

        ~BranchAccountBase()
        {

        }
    }
}
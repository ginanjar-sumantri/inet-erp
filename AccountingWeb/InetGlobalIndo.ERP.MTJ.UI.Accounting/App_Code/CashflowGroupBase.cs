using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CashflowGroup
{
    public abstract class CashflowGroupBase : AccountingBase
    {
        protected short _menuID = 2434;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _codeKey = "code";
        protected String _codeKey2 = "code2";

        protected String _homePage = "CashflowGroup.aspx";
        protected String _addPage = "CashflowGroupAdd.aspx";
        protected String _editPage = "CashflowGroupEdit.aspx";

        protected String _pageTitleLiteral = "Account Type File";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CashflowGroupBase()
        {

        }

        ~CashflowGroupBase()
        {

        }
    }
}
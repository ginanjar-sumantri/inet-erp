using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AccSubGroup
{
    public abstract class AccSubGroupBase : AccountingBase
    {
        protected short _menuID = 10;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _codeKey = "code";

        protected String _homePage = "AccSubGroup.aspx";
        protected String _addPage = "AccSubGroupAdd.aspx";
        protected String _editPage = "AccSubGroupEdit.aspx";
        protected String _viewPage = "AccSubGroupView.aspx";
        protected String _importPage = "AccSubGroupImport.aspx";

        protected String _pageTitleLiteral = "Account Sub Group File";

        protected NameValueCollectionExtractor _nvcExtractor;

        public AccSubGroupBase()
        {

        }

        ~AccSubGroupBase()
        {

        }
    }
}
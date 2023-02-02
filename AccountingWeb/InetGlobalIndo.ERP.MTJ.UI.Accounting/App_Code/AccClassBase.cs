using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AccClass
{
    public abstract class AccClassBase : AccountingBase
    {
        protected short _menuID = 11;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "AccClass.aspx";
        protected String _addPage = "AccClassAdd.aspx";
        protected String _editPage = "AccClassEdit.aspx";
        protected String _viewPage = "AccClassView.aspx";
        protected String _importPage = "AccClassImport.aspx";

        protected String _codeKey = "code";
        protected String _pageTitleLiteral = "Class Account File";

        protected NameValueCollectionExtractor _nvcExtractor;

        public AccClassBase()
        {
        }

        ~AccClassBase()
        {
        }
    }
}
using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AccGroup
{
    public abstract class AccGroupBase : AccountingBase 
    {
        protected short _menuID = 9;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "AccGroup.aspx";
        protected String _addPage = "AccGroupAdd.aspx";
        protected String _editPage = "AccGroupEdit.aspx";
        protected String _viewPage = "AccGroupView.aspx";

        protected String _codeKey = "code";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected String _pageTitleLiteral = "Account Group File";

        public AccGroupBase()
        {

        }

        ~AccGroupBase()
        {

        }
    }
}
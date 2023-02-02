using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.AccType
{
    public abstract class AccTypeBase : AccountingBase
    {
        protected short _menuID = 8;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _codeKey = "code";

        protected String _homePage = "AccType.aspx";
        protected String _addPage = "AccTypeAdd.aspx";
        protected String _editPage = "AccTypeEdit.aspx";
        protected String _viewPage = "AccTypeView.aspx";

        protected String _pageTitleLiteral = "Account Type File";

        protected NameValueCollectionExtractor _nvcExtractor;

        public AccTypeBase()
        {

        }

        ~AccTypeBase()
        {

        }
    }
}
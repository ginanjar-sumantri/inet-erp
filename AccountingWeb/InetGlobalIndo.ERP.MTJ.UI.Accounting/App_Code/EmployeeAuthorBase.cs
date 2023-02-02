using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.EmployeeAuthor
{
    public abstract class EmployeeAuthorBase : AccountingBase
    {
        protected short _menuID = 991;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "EmployeeAuthor.aspx";
        protected String _addPage = "EmployeeAuthorAdd.aspx";
        protected String _editPage = "EmployeeAuthorEdit.aspx";
        protected String _viewPage = "EmployeeAuthorView.aspx";

        protected String _codeKey = "code";
        protected String _transTypeKey = "TransTypeCode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected String _pageTitleLiteral = "Employee Authorization";

        public EmployeeAuthorBase()
        {

        }

        ~EmployeeAuthorBase()
        {

        }
    }
}
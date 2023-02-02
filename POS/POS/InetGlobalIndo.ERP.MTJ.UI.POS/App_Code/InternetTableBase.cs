using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for MemberTypeBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.Member
{
    public abstract class InternetTableBase : POSBase
    {
        protected short _menuID = 2375;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "InternetTable.aspx";
        protected string _addPage = "InternetTableAdd.aspx";
        protected string _editPage = "InternetTableEdit.aspx";
        protected string _viewPage = "InternetTableView.aspx";

        protected string _pageTitleLiteral = "Internet Table";

        protected NameValueCollectionExtractor _nvcExtractor;

        public InternetTableBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

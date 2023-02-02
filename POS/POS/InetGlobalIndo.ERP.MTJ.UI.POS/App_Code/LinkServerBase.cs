using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for LinkServerBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.LinkServer
{
    public abstract class LinkServerBase : POSBase
    {
        protected short _menuID = 2565;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "LinkServer.aspx";
        protected string _addPage = "LinkServerAdd.aspx";
        protected string _editPage = "LinkServerEdit.aspx";
        protected string _viewPage = "LinkServerView.aspx";

        protected string _pageTitleLiteral = "Link Server";

        protected NameValueCollectionExtractor _nvcExtractor;

        public LinkServerBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

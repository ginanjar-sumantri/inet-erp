using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for DiscoverFSBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.DiscoverFS
{
    public abstract class DiscoverFSBase : POSBase
    {
        protected short _menuID = 2534;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "DiscoverFS.aspx";
        protected string _addPage = "DiscoverFSAdd.aspx";
        protected string _editPage = "DiscoverFSEdit.aspx";
        protected string _viewPage = "DiscoverFSView.aspx";

        protected string _pageTitleLiteral = "Discover Financial Services";

        protected NameValueCollectionExtractor _nvcExtractor;

        public DiscoverFSBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

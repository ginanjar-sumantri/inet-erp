using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for ShippingZoneBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.Synchronize
{
    public abstract class SynchronizeBase : POSBase
    {
        protected short _menuID = 2564;
        protected PermissionLevel _permAccess;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "Synchronize.aspx";
        protected string _pageTitleLiteral = "Synchronize";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SynchronizeBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

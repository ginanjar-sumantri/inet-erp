using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for TransportationTRSBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.TransportationRS
{
    public abstract class TransportationRSBase : POSBase
    {
        protected short _menuID = 2512;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "TransportationRS.aspx";
        protected string _addPage = "TransportationRSAdd.aspx";
        protected string _editPage = "TransportationRSEdit.aspx";
        protected string _viewPage = "TransportationRSView.aspx";

        protected string _pageTitleLiteral = "Transportation Related Surcharge";

        protected NameValueCollectionExtractor _nvcExtractor;

        public TransportationRSBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

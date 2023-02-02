using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for OtherSurchargeBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.OtherSurcharge
{
    public abstract class OtherSurchargeBase : POSBase
    {
        protected short _menuID = 2516;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "OtherSurcharge.aspx";
        protected string _addPage = "OtherSurchargeAdd.aspx";
        protected string _editPage = "OtherSurchargeEdit.aspx";
        protected string _viewPage = "OtherSurchargeView.aspx";

        protected string _pageTitleLiteral = "Other Surcharge";

        protected NameValueCollectionExtractor _nvcExtractor;

        public OtherSurchargeBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

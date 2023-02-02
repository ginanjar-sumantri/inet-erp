using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;

/// <summary>
/// Summary description for CashierAccountBase
/// </summary>
namespace InetGlobalIndo.ERP.MTJ.UI.POS.CustomerDO
{
    public abstract class CustomerDOBase : POSBase
    {
        protected short _menuID = 2480;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "CustomerDO.aspx";
        protected string _addPage = "CustomerDOAdd.aspx";
        protected string _editPage = "CustomerDOEdit.aspx";
        protected string _viewPage = "CustomerDOView.aspx";

        protected string _pageTitleLiteral = "Customer Delivery Order";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerDOBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

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
namespace InetGlobalIndo.ERP.MTJ.UI.POS.CashierAccount
{
    public abstract class CashierAccountBase : POSBase
    {
        protected short _menuID = 2437;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.HomeWebAppURL + "ErrorPermission.aspx";
        protected string _theme, _rowColor, _rowColorAlternate, _rowColorHover;

        protected string _codeKey = "code";

        protected string _homePage = "CashierAccount.aspx";
        protected string _addPage = "CashierAccountAdd.aspx";
        protected string _editPage = "CashierAccountEdit.aspx";
        protected string _viewPage = "CashierAccountView.aspx";

        protected string _pageTitleLiteral = "Cashier Account";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CashierAccountBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

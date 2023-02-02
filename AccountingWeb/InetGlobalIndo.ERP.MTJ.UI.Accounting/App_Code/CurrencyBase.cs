using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Currency
{
    public abstract class CurrencyBase : AccountingBase
    {
        protected short _menuID = 4;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "Currency.aspx";
        protected String _addPage = "CurrencyAdd.aspx";
        protected String _editPage = "CurrencyEdit.aspx";
        protected String _viewPage = "CurrencyView.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected String _codeKey = "CurrCode";
        protected String _pageTitleLiteral = "Currency";

        public CurrencyBase()
        {

        }

        ~CurrencyBase()
        {

        }
    }
}
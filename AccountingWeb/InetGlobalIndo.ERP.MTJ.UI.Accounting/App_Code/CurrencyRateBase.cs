using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.CurrencyRate
{
    public abstract class CurrencyRateBase : AccountingBase
    {
        protected short _menuID = 5;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "CurrencyRate.aspx";
        protected String _addPage = "CurrencyRateAdd.aspx";
        protected String _editPage = "CurrencyRateEdit.aspx";
        protected String _viewPage = "CurrencyRateView.aspx";

        protected String _codeKey = "code";
        protected String _codeDate = "date";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected String _pageTitleLiteral = "Currency Rate";

        public CurrencyRateBase()
        {

        }

        ~CurrencyRateBase()
        {

        }
    }
}
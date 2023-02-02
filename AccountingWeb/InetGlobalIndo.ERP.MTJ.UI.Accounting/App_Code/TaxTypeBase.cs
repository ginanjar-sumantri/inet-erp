using System;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.Currency
{
    public abstract class TaxTypeBase : AccountingBase
    {
        protected short _menuID = 2431;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected String _homePage = "TaxType.aspx";
        protected String _addPage = "TaxTypeAdd.aspx";
        protected String _editPage = "TaxTypeEdit.aspx";
        protected String _viewPage = "TaxTypeView.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected String _codeKey = "TaxCode";
        protected String _pageTitleLiteral = "Tax Type";

        public TaxTypeBase()
        {

        }

        ~TaxTypeBase()
        {

        }
    }
}
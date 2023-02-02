using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier
{
    public class SupplierBase : PurchasingBase
    {
        protected short _menuID = 29;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;

        protected string _homePage = "Supplier.aspx";
        protected string _addPage = "SupplierAdd.aspx";
        protected string _editPage = "SupplierEdit.aspx";
        protected string _viewPage = "SupplierView.aspx";
        protected string _addDetailPage = "SupplierContactAdd.aspx";
        protected string _viewDetailPage = "SupplierContactView.aspx";
        protected string _editDetailPage = "SupplierContactEdit.aspx";
        protected string _importPage = "SupplierImport.aspx";
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _itemCodeKey = "itemCode";

        protected string _pageTitleLiteral = "Supplier";
        protected string _pageTitleLiteral2 = "Supplier Contact";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SupplierBase()
        {

        }

        ~SupplierBase()
        {

        }
    }
}
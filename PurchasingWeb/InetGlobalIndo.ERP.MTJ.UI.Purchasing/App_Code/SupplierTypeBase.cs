using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;


namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.SupplierType
{
    public abstract class SupplierTypeBase : PurchasingBase
    {
        protected short _menuID = 30;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "SupplierType.aspx";
        protected string _addPage = "SupplierTypeAdd.aspx";
        protected string _editPage = "SupplierTypeEdit.aspx";
        protected string _viewPage = "SupplierTypeView.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Supplier Type";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SupplierTypeBase()
        {

        }

        ~SupplierTypeBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.SupplierGroup
{
    public abstract class SupplierGroupBase : PurchasingBase
    {
        protected short _menuID = 31;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;

        protected string _homePage = "SupplierGroup.aspx";
        protected string _addPage = "SupplierGroupAdd.aspx";
        protected string _editPage = "SupplierGroupEdit.aspx";
        protected string _viewPage = "SupplierGroupView.aspx";
        protected string _addDetailPage = "SupplierGroupAccountAdd.aspx";
        protected string _viewDetailPage = "SupplierGroupAccountView.aspx";
        protected string _editDetailPage = "SupplierGroupAccountEdit.aspx";
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _currCode = "CurrCode";

        protected string _pageTitleLiteral = "Supplier Group";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SupplierGroupBase()
        {

        }

        ~SupplierGroupBase()
        {

        }
    }
}
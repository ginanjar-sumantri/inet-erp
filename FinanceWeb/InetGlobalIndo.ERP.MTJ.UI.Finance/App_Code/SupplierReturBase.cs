using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierRetur
{
    public class SupplierReturBase : FinanceBase
    {
        protected short _menuID = 2557;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "SupplierRetur.aspx";
        protected string _addPage = "SupplierReturAdd.aspx";
        protected string _editPage = "SupplierReturEdit.aspx";
        protected string _detailPage = "SupplierReturDetail.aspx";
        protected string _addDetailPage = "SupplierReturDetailAdd.aspx";
        protected string _editDetailPage = "SupplierReturDetailEdit.aspx";

        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";
        protected string _productKey = "ProductKey";
        protected string _allCode = "AllCode";
        protected string _suppCode = "SuppCode";

        protected string _pageTitleLiteral = "Supplier Retur";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SupplierReturBase()
        {

        }

        ~SupplierReturBase()
        {

        }
    }
}
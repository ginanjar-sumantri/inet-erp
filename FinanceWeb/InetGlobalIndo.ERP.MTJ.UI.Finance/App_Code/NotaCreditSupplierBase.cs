using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditSupplier
{
    public abstract class NotaCreditSupplierBase:FinanceBase 
    {
        protected short _menuID = 326;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "NotaCreditSupplier.aspx";
        protected string _addPage = "NotaCreditSupplierAdd.aspx";
        protected string _editPage = "NotaCreditSupplierEdit.aspx";
        protected string _detailPage = "NotaCreditSupplierDetail.aspx";
        protected string _addDetailPage = "NotaCreditSupplierDetailAdd.aspx";
        protected string _editDetailPage = "NotaCreditSupplierDetailEdit.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _pageTitleLiteral = "Supplier Credit Note";

        protected NameValueCollectionExtractor _nvcExtractor;

        public NotaCreditSupplierBase()
        {

        }

        ~NotaCreditSupplierBase()
        { 
        
        }
    }
}
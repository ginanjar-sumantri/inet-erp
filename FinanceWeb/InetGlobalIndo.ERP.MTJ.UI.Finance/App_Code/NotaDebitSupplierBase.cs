using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaDebitSupplier
{
    public class NotaDebitSupplierBase : FinanceBase
    {
        protected short _menuID = 316;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "NotaDebitSupplier.aspx";
        protected string _addPage = "NotaDebitSupplierAdd.aspx";
        protected string _editPage = "NotaDebitSupplierEdit.aspx";
        protected string _detailPage = "NotaDebitSupplierDetail.aspx";
        protected string _addDetailPage = "NotaDebitSupplierDetailAdd.aspx";
        protected string _editDetailPage = "NotaDebitSupplierDetailEdit.aspx";
        protected string _viewDetailPage = "NotaDebitSupplierDetailView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _pageTitleLiteral = "Supplier Debit Note";

        protected NameValueCollectionExtractor _nvcExtractor;

        public NotaDebitSupplierBase()
        {

        }

        ~NotaDebitSupplierBase()
        {

        }
    }
}
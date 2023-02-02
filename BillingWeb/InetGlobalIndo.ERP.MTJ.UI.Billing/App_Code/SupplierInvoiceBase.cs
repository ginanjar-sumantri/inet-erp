using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.SupplierInvoice
{
    public class SupplierInvoiceBase : BillingBase
    {
        protected short _menuID = 984;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "SupplierInvoice.aspx";
        protected string _detailPage = "SupplierInvoiceDetail.aspx";
        protected string _editPage = "SupplierInvoiceEdit.aspx";
        protected string _addPage = "SupplierInvoiceAdd.aspx";
        protected string _addDetailPage = "SupplierInvoiceDetailAdd.aspx";
        protected string _editDetailPage = "SupplierInvoiceDetailEdit.aspx";
        protected string _viewDetailPage = "SupplierInvoiceDetailView.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _pageTitleLiteral = "Supplier Invoice";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SupplierInvoiceBase()
        {
        }

        ~SupplierInvoiceBase()
        {

        }
    }
}
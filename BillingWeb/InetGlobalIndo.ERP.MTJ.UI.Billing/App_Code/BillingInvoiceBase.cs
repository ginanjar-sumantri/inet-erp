using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingInvoice
{
    public abstract class BillingInvoiceBase : BillingBase
    {
        protected short _menuID = 957;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Billing Invoice";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _homePage = "BillingInvoice.aspx";
        protected string _addPage = "BillingInvoiceAdd.aspx";
        protected string _editPage = "BillingInvoiceEdit.aspx";
        protected string _detailPage = "BillingInvoiceDetail.aspx";
        protected string _addDetailPage = "BillingInvoiceDetailAdd.aspx";
        protected string _editDetailPage = "BillingInvoiceDetailEdit.aspx";
        protected string _viewDetailPage = "BillingInvoiceDetailView.aspx";

        public BillingInvoiceBase()
        {

        }

        ~BillingInvoiceBase()
        {

        }
    }
}
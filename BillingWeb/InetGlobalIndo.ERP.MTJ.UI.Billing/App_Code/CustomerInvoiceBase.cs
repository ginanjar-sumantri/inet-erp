using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustomerInvoice
{
    public class CustomerInvoiceBase : BillingBase
    {
        protected short _menuID = 976;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        
        protected string _homePage = "CustomerInvoice.aspx";
        protected string _detailPage = "CustomerInvoiceDetail.aspx";
        protected string _editPage = "CustomerInvoiceEdit.aspx";
        protected string _addPage = "CustomerInvoiceAdd.aspx";
        protected string _addDetailPage = "CustomerInvoiceDetailAdd.aspx";
        protected string _editDetailPage = "CustomerInvoiceDetailEdit.aspx";
        protected string _viewDetailPage = "CustomerInvoiceDetailView.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _pageTitleLiteral = "Customer Invoice";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerInvoiceBase()
        {
        }

        ~CustomerInvoiceBase()
        {
        }
    }
}
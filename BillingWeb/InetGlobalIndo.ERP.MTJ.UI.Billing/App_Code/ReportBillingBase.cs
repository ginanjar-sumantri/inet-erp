using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Report
{
    public class ReportBillingBase : BillingBase
    {
        protected short _menuIDCustomerBillingAccount = 1500;
        protected short _menuIDCustomerInvoiceCategory = 1542;
        protected short _menuIDCustomerBandwidthUsage = 1707;
        protected short _menuIDRadiusReport = 2151;
        protected short _menuIDBillingInvoiceSummary = 2313;
        protected short _menuIDCustomerInvoiceSummary = 2314;
        protected short _menuIDSalesConfirmasi = 2326;
        protected short _menuIDBeritaAcara = 2327;

        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected string _pageTitleLiteral = "Report";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ReportBillingBase()
        {
        }

        ~ReportBillingBase()
        {
        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.PreviewTaxBillingInvoice
{
    public class PreviewTaxBillingInvoiceBase : BillingBase
    {
        protected short _menuID = 999;

        protected PermissionLevel _permAccess, _prmPrintPreview;

        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected string _pageTitleLiteral = "Preview Tax Billing Invoice";

        public PreviewTaxBillingInvoiceBase()
        {
        }

        ~PreviewTaxBillingInvoiceBase()
        {
        }
    }
}
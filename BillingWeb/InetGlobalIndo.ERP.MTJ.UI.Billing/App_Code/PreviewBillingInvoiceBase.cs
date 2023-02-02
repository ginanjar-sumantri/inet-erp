using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.PreviewBillingInvoice
{
    public class PreviewBillingInvoiceBase : BillingBase
    {
        protected short _menuID = 983;
        protected PermissionLevel _permAccess, _prmPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        protected string _pageTitleLiteral = "Preview Billing Invoice";

        public PreviewBillingInvoiceBase()
        {
        }

        ~PreviewBillingInvoiceBase()
        {
        }
    }
}
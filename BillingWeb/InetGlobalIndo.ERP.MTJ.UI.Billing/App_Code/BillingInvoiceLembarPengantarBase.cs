using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingInvoiceLembarPengantar
{
    public class BillingInvoiceLembarPengantarBase : BillingBase
    {
        protected short _menuID = 1014;

        protected PermissionLevel _permAccess, _prmPrintPreview;

        protected string _pageTitleLiteral = "Lembar Pengantar";

        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        public BillingInvoiceLembarPengantarBase()
        {
        }

        ~BillingInvoiceLembarPengantarBase()
        {
        }
    }
}

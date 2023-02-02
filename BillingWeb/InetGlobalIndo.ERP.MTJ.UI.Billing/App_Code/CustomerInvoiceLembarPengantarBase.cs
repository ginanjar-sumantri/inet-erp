using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustomerInvoiceLembarPengantar
{
    public class CustomerInvoiceLembarPengantarBase : BillingBase
    {
        protected short _menuID = 1015;

        protected PermissionLevel _permAccess, _prmPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";
        protected string _pageTitleLiteral = "Lembar Pengantar";

        public CustomerInvoiceLembarPengantarBase()
        {
        }

        ~CustomerInvoiceLembarPengantarBase()
        {
        }
    }
}

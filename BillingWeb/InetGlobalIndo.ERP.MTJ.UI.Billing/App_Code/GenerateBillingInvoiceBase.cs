
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
namespace InetGlobalIndo.ERP.MTJ.UI.Billing.GenerateBillingInvoice
{
    public class GenerateBillingInvoiceBase : BillingBase
    {
        protected string _pageTitleLiteral = "Generate Billing Invoice";

        protected short _menuID = 973;
        protected PermissionLevel _permAccess, _permGenerate;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        public GenerateBillingInvoiceBase()
        {
        }

        ~GenerateBillingInvoiceBase()
        {
        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BAListUpgradeDowngrade
{
    public class BAListUpgradeDowngradeBase : BillingBase
    {
        protected short _menuID = 1816;

        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _homePage = "BAListUpgradeDowngrade.aspx";

        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "BA List Upgrade / Downgrade";

        protected NameValueCollectionExtractor _nvcExtractor;

        public BAListUpgradeDowngradeBase()
        {
        }

        ~BAListUpgradeDowngradeBase()
        {
        }
    }
}
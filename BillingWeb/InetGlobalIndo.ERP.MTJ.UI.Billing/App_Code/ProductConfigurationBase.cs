using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.BillingInvoice
{
    public abstract class ProductConfigurationBase : BillingBase
    {
        protected short _menuID = 1538;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Product Configuration";

        protected string _codeKey = "code";

        protected string _homePage = "ProductConfiguration.aspx";
        protected string _addPage = "ProductConfigurationAdd.aspx";
        protected string _editPage = "ProductConfigurationEdit.aspx";
        protected string _viewPage = "ProductConfigurationView.aspx";

        public ProductConfigurationBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        ~ProductConfigurationBase()
        {

        }
    }
}

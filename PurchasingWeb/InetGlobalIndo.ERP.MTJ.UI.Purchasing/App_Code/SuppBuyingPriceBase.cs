using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.UI.Purchasing.Supplier;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.SupplierBuyingPrice
{
    public abstract class SuppBuyingPriceBase : PurchasingBase
    {
        protected short _menuID = 57;
        protected PermissionLevel _permAccess, _permView;

  
        protected string _errorPermissionPage = ApplicationConfig.SalesWebAppURL + "ErrorPermission.aspx";



        protected string _pageTitleLiteral = "Supplier Buying Price";
 

        protected NameValueCollectionExtractor _nvcExtractor;

        public SuppBuyingPriceBase()
        {

        }

        ~SuppBuyingPriceBase()
        {

        }
    }
}
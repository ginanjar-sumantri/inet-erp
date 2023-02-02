using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRetur
{
    public abstract class PurchaseReturBase : PurchasingBase
    {
        protected short _menuID = 1708;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PurchaseRetur.aspx";
        protected string _addPage = "PurchaseReturAdd.aspx";
        protected string _editPage = "PurchaseReturEdit.aspx";
        protected string _detailPage = "PurchaseReturDetail.aspx";
        protected string _addDetailPage = "PurchaseReturDetailAdd.aspx";
        protected string _editDetailPage = "PurchaseReturDetailEdit.aspx";
        protected string _viewDetailPage = "PurchaseReturDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Purchase Retur";

        public PurchaseReturBase()
        {
        }

        ~PurchaseReturBase()
        {
        }
    }
}
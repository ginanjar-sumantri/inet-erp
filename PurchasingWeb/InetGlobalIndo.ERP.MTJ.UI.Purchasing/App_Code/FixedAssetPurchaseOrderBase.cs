using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.FixedAssetPurchaseOrder
{
    public abstract class FixedAssetPurchaseOrderBase : PurchasingBase
    {
        protected short _menuID = 1862;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FixedAssetPurchaseOrder.aspx";
        protected string _addPage = "FixedAssetPurchaseOrderAdd.aspx";
        protected string _editPage = "FixedAssetPurchaseOrderEdit.aspx";
        protected string _detailPage = "FixedAssetPurchaseOrderDetail.aspx";
        protected string _addDetailPage = "FixedAssetPurchaseOrderDetailAdd.aspx";
        protected string _editDetailPage = "FixedAssetPurchaseOrderDetailEdit.aspx";
        protected string _viewDetailPage = "FixedAssetPurchaseOrderDetailView.aspx";

        protected string _codeKey = "code";
        protected string _faKey = "name";

        protected string _pageTitleLiteral = "Fixed Asset Purchase Order";

        protected NameValueCollectionExtractor _nvcExtractor;

        public FixedAssetPurchaseOrderBase()
        {

        }

        ~FixedAssetPurchaseOrderBase()
        {

        }
    }
}
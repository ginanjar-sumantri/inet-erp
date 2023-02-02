using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.DirectPurchase
{
    public abstract class DirectPurchaseBase : PurchasingBase
    {
        protected short _menuID = 1850;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "DirectPurchase.aspx";
        protected string _addPage = "DirectPurchaseAdd.aspx";
        protected string _editPage = "DirectPurchaseEdit.aspx";
        protected string _detailPage = "DirectPurchaseDetail.aspx";
        protected string _addDetailPage = "DirectPurchaseDetailAdd.aspx";
        protected string _editDetailPage = "DirectPurchaseDetailEdit.aspx";
        protected string _viewDetailPage = "DirectPurchaseDetailView.aspx";

        protected string _codeKey = "code";
        protected string _productKey = "ProductCode";
        protected string _locationKey = "LocationCode";
        protected string _wrhsKey = "WrhsCode";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Direct Purchase";

        public DirectPurchaseBase()
        {
        }

        ~DirectPurchaseBase()
        {
        }
    }
}
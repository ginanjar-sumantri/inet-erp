using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder
{
    public abstract class PurchaseOrderBase : PurchasingBase
    {
        protected short _menuID = 814;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClose, _permRevisi;
        protected string _errorPermissionPage = ApplicationConfig.PurchasingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PurchaseOrder.aspx";
        protected string _addPage = "PurchaseOrderAdd.aspx";
        protected string _editPage = "PurchaseOrderEdit.aspx";
        protected string _detailPage = "PurchaseOrderDetail.aspx";
        protected string _editDetailPage = "PurchaseOrderDetailEdit.aspx";
        protected string _viewDetailPage = "PurchaseOrderDetailView.aspx";
        protected string _addDetailPage2 = "PurchaseOrderDetail2Add.aspx";
        protected string _viewDetailPage2 = "PurchaseOrderDetail2View.aspx";
        protected string _editDetailPage2 = "PurchaseOrderDetail2Edit.aspx";

        protected string _codeKey = "code";
        protected string _codeRevisi = "revisi";
        protected string _productKey = "ProductCode";
        protected string _requestNoKey = "RequestNo";

        protected string _pageTitleLiteral = "Purchase Order";
        protected string _pageTitleLiteral2 = "Purchase Order - Detail";

        protected NameValueCollectionExtractor _nvcExtractor;

        public PurchaseOrderBase()
        {

        }

        ~PurchaseOrderBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierPayment
{
    public abstract class DPSupplierPaymentBase : FinanceBase
    {
        protected short _menuID = 461;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "DPSuppPayment.aspx";
        protected string _addPage = "DPSuppPaymentAdd.aspx";
        protected string _editPage = "DPSuppPaymentEdit.aspx";
        protected string _detailPage = "DPSuppPaymentDetail.aspx";
        protected string _viewDetailPage = "DPSuppPaymentDetailView.aspx";
        protected string _editDetailPage = "DPSuppPaymentDetailEdit.aspx";
        protected string _addDetailPage = "DPSuppPaymentDetailAdd.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _itemKey = "ItemNo";

        protected string _pageTitleLiteral = "DP Supplier Payment";

        protected NameValueCollectionExtractor _nvcExtractor;

        public DPSupplierPaymentBase()
        {

        }

        ~DPSupplierPaymentBase()
        {

        }
    }
}
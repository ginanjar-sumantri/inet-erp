using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPSupplierRetur
{
    public abstract class DPSupplierReturBase : FinanceBase
    {
        protected short _menuID = 477;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "DPSuppRetur.aspx";
        protected string _addPage = "DPSuppReturAdd.aspx";
        protected string _editPage = "DPSuppReturEdit.aspx";
        protected string _viewPage = "DPSuppReturDetail.aspx";
        protected string _addDetailPage = "DPSuppReturDetailAdd.aspx";
        protected string _viewDetailPage = "DPSuppReturDetailView.aspx";
        protected string _editDetailPage = "DPSuppReturDetailEdit.aspx";
        protected string _addDetailPage2 = "DPSuppReturPaymentAdd.aspx";
        protected string _viewDetailPage2 = "DPSuppReturPaymentView.aspx";
        protected string _editDetailPage2 = "DPSuppReturPaymentEdit.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _pageTitleLiteral = "DP Supplier Retur";

        protected NameValueCollectionExtractor _nvcExtractor;


        public DPSupplierReturBase()
        {

        }

        ~DPSupplierReturBase()
        {

        }
    }
}
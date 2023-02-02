using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierDifferenceRate
{
    public abstract class SupplierDifferenceRateBase : FinanceBase
    {
        protected short _menuID = 496;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "SuppDiffRate.aspx";
        protected string _addPage = "SuppDiffRateAdd.aspx";
        protected string _editPage = "SuppDiffRateEdit.aspx";
        protected string _detailPage = "SuppDiffRateDetail.aspx";
        protected string _viewDetailPage = "SuppDiffRateDetailView.aspx";
        protected string _editDetailPage = "SuppDiffRateDetailEdit.aspx";
        protected string _addDetailPage = "SuppDiffRateDetailAdd.aspx";

        protected string _codeKey = "code";
        protected string _itemKey = "codeItem";

        protected string _pageTitleLiteral = "Supplier Difference Rate";
               
        protected NameValueCollectionExtractor _nvcExtractor;

        public SupplierDifferenceRateBase()
        {

        }

        ~SupplierDifferenceRateBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerDifferenceRate
{
    public class CustomerDifferenceRateBase : FinanceBase
    {
        protected short _menuID = 490;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "CustDiffRate.aspx";
        protected string _addPage = "CustDiffRateAdd.aspx";
        protected string _editPage = "CustDiffRateEdit.aspx";
        protected string _detailPage = "CustDiffRateDetail.aspx";
        protected string _addDetailPage = "CustDiffRateDetailAdd.aspx";
        protected string _editDetailPage = "CustDiffRateDetailEdit.aspx";
        protected string _viewDetailPage = "CustDiffRateDetailView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _invoiceKey = "InvoiceNo";

        protected string _pageTitleLiteral = "Customer Difference Rate";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerDifferenceRateBase()
        {

        }

        ~CustomerDifferenceRateBase()
        {

        }
    }
}
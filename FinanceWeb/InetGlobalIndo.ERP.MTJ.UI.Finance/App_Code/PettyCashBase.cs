using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCash
{
    public abstract class PettyCashBase : FinanceBase
    {
        protected short _menuID = 255;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PettyCash.aspx";
        protected string _addPage = "PettyCashAdd.aspx";
        protected string _editPage = "PettyCashEdit.aspx";
        protected string _detailPage = "PettyCashDetail.aspx";
        protected string _addDetailPage = "PettyCashDetailAdd.aspx";
        protected string _viewDetailPage = "PettyCashDetailView.aspx";
        protected string _editDetailPage = "PettyCashDetailEdit.aspx";
        protected string _printPreviewPage = "PettyCashPrintPreview.aspx";
        protected string _printPreviewPageDetail = "PettyCashPrintPreviewDetail.aspx";
        protected string _advanceSearchPage = "PettyCashAdvancedSearch.aspx";

        protected string _codeKey = "code";
        protected string _numberKey = "number";
        protected string _codeTransaction = "";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Petty Cash";

        public PettyCashBase()
        {

        }

        ~PettyCashBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PettyCashReceive
{
    public class PettyCashReceiveBase : FinanceBase
    {
        protected short _menuID = 807;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PettyCashReceive.aspx";
        protected string _addPage = "PettyCashReceiveAdd.aspx";
        protected string _editPage = "PettyCashReceiveEdit.aspx";
        protected string _printPreviewPage = "PettyCashReceivePrintPreview.aspx";
        protected string _detailPage = "PettyCashReceiveDetail.aspx";
        protected string _addDetailPage = "PettyCashReceiveDetailAdd.aspx";
        protected string _viewDetailPage = "PettyCashReceiveDetailView.aspx";
        protected string _editDetailPage = "PettyCashReceiveDetailEdit.aspx";
        protected string _printPreviewPageDetail = "PettyCashReceivePrintPreviewDetail.aspx";
        protected string _advanceSearchPage = "PettyCashReceiveAdvancedSearch.aspx";

        protected string _codeKey = "code";
        protected string _number = "number";
        protected string _codeTransaction = "";

        protected string _pageTitleLiteral = "Cash and Bank Transfer Intern";

        protected NameValueCollectionExtractor _nvcExtractor;

        public PettyCashReceiveBase()
        {

        }

        ~PettyCashReceiveBase()
        {

        }
    }
}
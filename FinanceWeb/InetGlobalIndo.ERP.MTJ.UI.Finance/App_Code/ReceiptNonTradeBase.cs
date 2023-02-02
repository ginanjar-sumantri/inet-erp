using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptNonTrade
{
    public class ReceiptNonTradeBase : FinanceBase
    {
        protected short _menuID = 308;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "ReceiptNonTrade.aspx";
        protected string _addPage = "ReceiptNonTradeAdd.aspx";
        protected string _editPage = "ReceiptNonTradeEdit.aspx";
        protected string _detailPage = "ReceiptNonTradeDetail.aspx";
        protected string _addDetailPage = "ReceiptNonTradeCrAdd.aspx";
        protected string _viewDetailPage = "ReceiptNonTradeCrView.aspx";
        protected string _editDetailPage = "ReceiptNonTradeCrEdit.aspx";
        protected string _addDetailPage2 = "ReceiptNonTradeDbAdd.aspx";
        protected string _viewDetailPage2 = "ReceiptNonTradeDbView.aspx";
        protected string _editDetailPage2 = "ReceiptNonTradeDbEdit.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _pageTitleLiteral = "Receipt Non Trade";

        protected NameValueCollectionExtractor _nvcExtractor;

        public ReceiptNonTradeBase()
        {

        }

        ~ReceiptNonTradeBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.ReceiptTrade
{
    public class ReceiptTradeBase : FinanceBase
    {
        protected short _menuID = 405;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "ReceiptTrade.aspx";
        protected string _addPage = "ReceiptTradeAdd.aspx";
        protected string _editPage = "ReceiptTradeEdit.aspx";
        protected string _detailPage = "ReceiptTradeDetail.aspx";
        protected string _addDetailPage2 = "ReceiptTradeCrAdd.aspx";
        protected string _viewDetailPage2 = "ReceiptTradeCrView.aspx";
        protected string _editDetailPage2 = "ReceiptTradeCrEdit.aspx";
        protected string _addDetailPage = "ReceiptTradeDbAdd.aspx";
        protected string _viewDetailPage = "ReceiptTradeDbView.aspx";
        protected string _editDetailPage = "ReceiptTradeDbEdit.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _pageTitleLiteral = "Receipt Trade";

        protected NameValueCollectionExtractor _nvcExtractor;
        
        public ReceiptTradeBase()
        {

        }

        ~ReceiptTradeBase()
        {

        }
    }
}
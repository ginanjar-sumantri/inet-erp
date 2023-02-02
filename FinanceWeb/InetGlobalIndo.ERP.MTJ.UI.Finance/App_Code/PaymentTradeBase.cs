using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentTrade
{
    public abstract class PaymentTradeBase : FinanceBase
    {
        protected short _menuID = 391;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PaymentTrade.aspx";
        protected string _addPage = "PaymentTradeAdd.aspx";
        protected string _editPage = "PaymentTradeEdit.aspx";
        protected string _detailPage = "PaymentTradeDetail.aspx";
        protected string _addDetailPage = "PaymentTradeDbAdd.aspx";
        protected string _viewDetailPage = "PaymentTradeDbView.aspx";
        protected string _editDetailPage = "PaymentTradeDbEdit.aspx";
        protected string _addDetailPage2 = "PaymentTradeCrAdd.aspx";
        protected string _viewDetailPage2 = "PaymentTradeCrView.aspx";
        protected string _editDetailPage2 = "PaymentTradeCrEdit.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Payment Trade";


        public PaymentTradeBase()
        {

        }

        ~PaymentTradeBase()
        {

        }
    }
}
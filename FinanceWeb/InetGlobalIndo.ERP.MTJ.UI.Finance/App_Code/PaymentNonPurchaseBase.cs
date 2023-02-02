using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.PaymentNonPurchase
{
    public abstract class PaymentNonPurchaseBase : FinanceBase
    {
        protected short _menuID = 273;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "PaymentNonPurchase.aspx";
        protected string _addPage = "PaymentNonPurchaseAdd.aspx";
        protected string _editPage = "PaymentNonPurchaseEdit.aspx";
        protected string _detailPage = "PaymentNonPurchaseDetail.aspx";
        protected string _addDetailPage = "PaymentNonPurchaseDbAdd.aspx";
        protected string _viewDetailPage = "PaymentNonPurchaseDbView.aspx";
        protected string _editDetailPage = "PaymentNonPurchaseDbEdit.aspx";

        protected string _addDetailPage2 = "PaymentNonPurchaseCrAdd.aspx";
        protected string _viewDetailPage2 = "PaymentNonPurchaseCrView.aspx";
        protected string _editDetailPage2 = "PaymentNonPurchaseCrEdit.aspx";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _pageTitleLiteral = "Payment Non Trade";

        protected NameValueCollectionExtractor _nvcExtractor;


       
       

        public PaymentNonPurchaseBase()
        {

        }

        ~PaymentNonPurchaseBase()
        {

        }
    }
}
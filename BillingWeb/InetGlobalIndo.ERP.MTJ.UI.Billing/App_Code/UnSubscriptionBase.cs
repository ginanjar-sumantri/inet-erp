using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.UnSubscription
{
    public abstract class UnSubscriptionBase : BillingBase
    {
        protected short _menuID = 1834;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview, _permClosem, _permTaxPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "UnSubscription";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";
        protected string _productKey = "ProductCode";

        protected string _homePage = "UnSubscription.aspx";
        protected string _addPage = "UnSubscriptionAdd.aspx";
        protected string _editPage = "UnSubscriptionEdit.aspx";
        protected string _detailPage = "UnSubscriptionDetail.aspx";
        protected string _addDetailPage = "UnSubscriptionDetailAdd.aspx";
        protected string _editDetailPage = "UnSubscriptionDetailEdit.aspx";
        protected string _viewDetailPage = "UnSubscriptionDetailView.aspx";
 

        public UnSubscriptionBase()
        {

        }

        ~UnSubscriptionBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.SalesConfirmation
{
    public abstract class SalesConfirmationBase : BillingBase
    {
        protected short _menuID = 1810;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Sales Confirmation";

        protected string _codeKey = "code";
        protected string _detailKey = "codeDetail";

        protected string _homePage = "SalesConfirmation.aspx";
        protected string _addPage = "SalesConfirmationAdd.aspx";
        protected string _editPage = "SalesConfirmationEdit.aspx";
        protected string _detailPage = "SalesConfirmationDetail.aspx";
        protected string _addDetailPage = "SalesConfirmationDetailAdd.aspx";
        protected string _editDetailPage = "SalesConfirmationDetailEdit.aspx";

        public SalesConfirmationBase()
        {

        }

        ~SalesConfirmationBase()
        {

        }
    }
}
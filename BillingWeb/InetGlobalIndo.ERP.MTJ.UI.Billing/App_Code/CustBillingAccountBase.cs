using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.CustBillingAccountBase
{
    public class CustBillingAccountBase : BillingBase
    {
        protected short _menuID = 964;

        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _prmPrintPreview, _prmUpdate, _permGetApproval, _permApprove, _permPosting, _permUnposting;
        

        protected string _homePage = "CustBillingAccount.aspx";
        protected string _addPage = "CustBillingAccountAdd.aspx";
        protected string _editPage = "CustBillingAccountEdit.aspx";
        protected string _viewPage = "CustBillingAccountView.aspx";

        protected string _homePageTrRadiusTemporary = "BILTrRadiusActivateTemporary.aspx";
        protected string _addPageTrRadiusTemporary = "BILTrRadiusActivateTemporaryAdd.aspx";
        protected string _viewPageTrRadiusTemporary = "BILTrRadiusActivateTemporaryView.aspx";
        protected string _editPageTrRadiusTemporary = "BILTrRadiusActivateTemporaryEdit.aspx";

        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";

        protected string _pageTitleLiteral = "Customer Billing Account";
        protected string _pageTitleRadiusLiteral = "Billing Radius Active";
        protected string _pageTitleRadiusTemporaryLiteral = "Billing Radius Active Temporary";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustBillingAccountBase()
        {
        }

        ~CustBillingAccountBase()
        {
        }
    }
}
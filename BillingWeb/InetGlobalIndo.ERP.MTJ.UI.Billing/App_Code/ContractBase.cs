using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Billing.Contract
{
    public abstract class ContractBase : BillingBase
    {
        protected short _menuID = 1827;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnPosting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.BillingWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Contract";

        protected string _codeKey = "code";
        protected string _codeItemKey = "CodeItem";

        protected string _homePage = "Contract.aspx";
        protected string _addPage = "ContractAdd.aspx";
        protected string _editPage = "ContractEdit.aspx";
        protected string _detailPage = "ContractDetail.aspx";

        public ContractBase()
        {

        }

        ~ContractBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer
{
    public abstract class SupplierInvConsignmentBase : FinanceBase
    {
        protected short _menuID = 2432;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "SupplierInvConsignment.aspx";
        protected string _addPage = "SupplierInvConsignmentAdd.aspx";
        protected string _editPage = "SupplierInvConsignmentEdit.aspx";
        protected string _detailPage = "SupplierInvConsignmentDetail.aspx";
        protected string _addDetailPage = "SupplierInvConsignmentDetailAdd.aspx";
        protected string _editDetailPage = "SupplierInvConsignmentDetailEdit.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";
        protected string _allCode = "AllCode";
        protected string _suppCode = "SuppCode";

        protected string _pageTitleLiteral = "Supplier Invoice Consignment";

        protected NameValueCollectionExtractor _nvcExtractor;

        public SupplierInvConsignmentBase()
        {

        }

        ~SupplierInvConsignmentBase()
        {

        }
    }
}
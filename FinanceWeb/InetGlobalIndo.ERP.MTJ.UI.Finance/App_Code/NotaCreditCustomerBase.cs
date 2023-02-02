using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaCreditCustomer
{
    public abstract class NotaCreditCustomerBase : FinanceBase
    {
        protected short _menuID = 344;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "NotaCreditCustomer.aspx";
        protected string _addPage = "NotaCreditCustomerAdd.aspx";
        protected string _editPage = "NotaCreditCustomerEdit.aspx";
        protected string _detailPage = "NotaCreditCustomerDetail.aspx";
        protected string _addDetailPage = "NotaCreditCustomerDetailAdd.aspx";
        protected string _editDetailPage = "NotaCreditCustomerDetailEdit.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";

        protected string _pageTitleLiteral = "Customer Credit Note";

        protected NameValueCollectionExtractor _nvcExtractor;

        public NotaCreditCustomerBase()
        {

        }

        ~NotaCreditCustomerBase()
        {

        }
    }
}
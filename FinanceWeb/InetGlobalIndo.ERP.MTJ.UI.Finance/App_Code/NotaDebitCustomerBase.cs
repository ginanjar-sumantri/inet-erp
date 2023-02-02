using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.NotaDebitCustomer
{
    public class NotaDebitCustomerBase : FinanceBase
    {
        protected short _menuID = 338;
        protected PermissionLevel _permAccess, _permAdd, _permView, _permEdit, _permDelete, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "NotaDebitCustomer.aspx";
        protected string _addPage = "NotaDebitCustomerAdd.aspx";
        protected string _editPage = "NotaDebitCustomerEdit.aspx";
        protected string _detailPage = "NotaDebitCustomerDetail.aspx";
        protected string _addDetailPage = "NotaDebitCustomerDetailAdd.aspx";
        protected string _editDetailPage = "NotaDebitCustomerDetailEdit.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";

        protected string _pageTitleLiteral = "Customer Debit Note";

        protected NameValueCollectionExtractor _nvcExtractor;

        public NotaDebitCustomerBase()
        {

        }

        ~NotaDebitCustomerBase()
        {

        }
    }
}
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CustomerRetur
{
    public class CustomerReturBase : FinanceBase
    {
        protected short _menuID = 2557;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "CustomerRetur.aspx";
        protected string _addPage = "CustomerReturAdd.aspx";
        protected string _editPage = "CustomerReturEdit.aspx";
        protected string _detailPage = "CustomerReturDetail.aspx";
        protected string _addDetailPage = "CustomerReturDetailAdd.aspx";
        protected string _editDetailPage = "CustomerReturDetailEdit.aspx";

        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";
        protected string _productKey = "ProductKey";
        protected string _allCode = "AllCode";
        protected string _custCode = "CustCode";

        protected string _pageTitleLiteral = "Customer Retur";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CustomerReturBase()
        {

        }

        ~CustomerReturBase()
        {

        }
    }
}
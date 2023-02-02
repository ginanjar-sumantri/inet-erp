using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerList
{
    public class DPCustomerListBase : FinanceBase
    {
        protected short _menuID = 446;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting;

        protected string _codeKey = "code";
        protected string _homePage = "DPCustomerList.aspx";
        protected string _addPage = "DPCustomerListAdd.aspx";
        protected string _editPage = "DPCustomerListEdit.aspx";
        protected string _viewPage = "DPCustomerListView.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "DP Customer List";

        public DPCustomerListBase()
        {

        }

        ~DPCustomerListBase()
        {

        }
    }
}
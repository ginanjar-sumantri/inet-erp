using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.DPCustomerRetur
{
    public abstract class DPCustomerReturBase : FinanceBase
    {
        protected short _menuID = 457;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;

        protected string _homePage = "DPCustomerRetur.aspx";
        protected string _addPage = "DPCustomerReturAdd.aspx";
        protected string _editPage = "DPCustomerReturEdit.aspx";
        protected string _viewPage = "DPCustomerReturDetail.aspx";
        protected string _addDetailPage = "DPCustomerReturDtAdd.aspx";
        protected string _viewDetailPage = "DPCustomerReturDtView.aspx";
        protected string _editDetailPage = "DPCustomerReturDtEdit.aspx";
        protected string _addDetailPage2 = "DPCustomerReturPayAdd.aspx";
        protected string _viewDetailPage2 = "DPCustomerReturPayView.aspx";
        protected string _editDetailPage2 = "DPCustomerReturPayEdit.aspx";
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _codeKey = "code";
        protected string _codeItem = "CodeItem";
        protected string _codeDP = "CodeDP";

        protected string _pageTitleLiteral = "DP Customer Retur";

        protected NameValueCollectionExtractor _nvcExtractor;

        public DPCustomerReturBase()
        {

        }

        ~DPCustomerReturBase()
        {

        }
    }
}
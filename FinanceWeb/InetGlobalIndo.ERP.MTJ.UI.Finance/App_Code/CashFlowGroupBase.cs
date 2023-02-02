using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.CashFlowGroup
{
    public abstract class CashFlowGroupBase : FinanceBase
    {
        protected short _menuID = 1785;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit;
        protected string _errorPermissionPage = ApplicationConfig.FinanceWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "CashFlowGroup.aspx";
        protected string _addPage = "CashFlowGroupAdd.aspx";
        protected string _editPage = "CashFlowGroupEdit.aspx";
        protected string _detailPage = "CashFlowGroupDetail.aspx";
        protected string _addDetailPage = "CashFlowGroupDetailAdd.aspx";
        protected string _editDetailPage = "CashFlowGroupDetailEdit.aspx";
        protected string _viewDetailPage = "CashFlowGroupDetailView.aspx";

        protected string _codeKey = "code";
        protected string _codeType = "type";

        protected string _pageTitleLiteral = "Cash Flow Group";

        protected NameValueCollectionExtractor _nvcExtractor;

        public CashFlowGroupBase()
        {

        }

        ~CashFlowGroupBase()
        {

        }
    }
}
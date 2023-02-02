using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Accounting.FAAddStock
{
    public abstract class FAAddStockBase : AccountingBase
    {
        protected short _menuID = 1016;
        protected PermissionLevel _permAccess, _permAdd, _permDelete, _permView, _permEdit, _permGetApproval, _permApprove, _permPosting, _permUnposting, _permPrintPreview;
        protected string _errorPermissionPage = ApplicationConfig.AccountingWebAppURL + "ErrorPermission.aspx";

        protected string _homePage = "FAAddStock.aspx";
        protected string _addPage = "FAAddStockAdd.aspx";
        protected string _editPage = "FAAddStockEdit.aspx";
        protected string _detailPage = "FAAddStockDetail.aspx";
        protected string _editDetailPage = "FAAddStockDetailEdit.aspx";
        protected string _viewDetailPage = "FAAddStockDetailView.aspx";

        protected string _codeKey = "code";
        protected string _code = "CodeDt";

        protected NameValueCollectionExtractor _nvcExtractor;

        protected string _pageTitleLiteral = "Fixed Asset - Add from Stock";

        public FAAddStockBase()
        {

        }

        ~FAAddStockBase()
        {

        }
    }
}